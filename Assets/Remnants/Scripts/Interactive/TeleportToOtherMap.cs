using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

namespace Remnants
{
    public class TeleportToOtherMap : Interactive
    {
        [Header("=== References ===")]
        [SerializeField] private Transform otherMap;
        [SerializeField] private Volume urpVolume;
        [SerializeField] private Image[] crackImages;
        [SerializeField] private CanvasGroup fadePanel;
        [SerializeField] private TextMeshProUGUI sequenceText;

        [Header("=== Timing ===")]
        [SerializeField] private float overlayInterval = 0.02f;
        //[SerializeField] private float overlayHold = 0.5f;    //사용하지 않음
        [SerializeField] private float fallDelay = 0.05f;
        [SerializeField] private float fallDistance = 300f;
        [SerializeField] private float fallTime = 1f;
        [SerializeField] private float fadeDuration = 1f;

        private ColorAdjustments _colorAdjustments;
        private static bool _goToOtherMap = true;
        private bool _hasUsed;

        private void Awake()
        {
         
            if (urpVolume != null && urpVolume.profile.TryGet(out _colorAdjustments))
                _colorAdjustments.saturation.overrideState = true;

         
            foreach (var img in crackImages)
            {
                var sprite = img.sprite;
                img.SetNativeSize();
                var rt = img.rectTransform;
                rt.anchorMin = rt.anchorMax = Vector2.zero;
                rt.pivot = Vector2.zero;
                rt.anchoredPosition = new Vector2(sprite.rect.x, sprite.rect.y);
                img.gameObject.SetActive(false);
            }
        }

        protected override void DoAction()
        {
            if (!_hasUsed || (_hasUsed && !_goToOtherMap))
            {
                _hasUsed = true;
                TeleportState.Instance.HasVisitedRoom = true;
                StartCoroutine(MirrorBreakThenTeleport());

                if (TryGetComponent<Collider>(out var col)) col.enabled = false;
            }
        }
        private IEnumerator MirrorBreakThenTeleport()
        {
            //  크랙 오버레이
            yield return CrackOverlayRoutine();    // 이 코루틴이 끝날 때까지 대기

            //  잠시 홀드
            yield return new WaitForSeconds(0.3f);

            StartCoroutine(CrackFallRoutine());
            yield return new WaitForSeconds(fallDelay * crackImages.Length + fallTime);

            //  원래 TeleportRoutine 실행
            yield return TeleportRoutine();
        }
        private IEnumerator TeleportRoutine()
        {
            //  화면 블랙아웃
            yield return Fade(0, 1);

            //  순간이동
            TeleportPlayer();

            // 컬러 ↔ 흑백
            if (_goToOtherMap)
            {
                _colorAdjustments.saturation.value = -100f;
            }
            else
            {
                yield return FadeToColor();
            }

            //  화면 원복
            yield return Fade(1, 0);
            fadePanel.gameObject.SetActive(false);

            _goToOtherMap = !_goToOtherMap;
            if (_goToOtherMap)
                gameObject.SetActive(false);
        }

        private IEnumerator Fade(float from, float to)
        {
            fadePanel.gameObject.SetActive(true);
            fadePanel.alpha = from;
            float t = 0;
            while (t < fadeDuration)
            {
                t += Time.deltaTime;
                fadePanel.alpha = Mathf.Lerp(from, to, t / fadeDuration);
                yield return null;
            }
            fadePanel.alpha = to;
        }

        private IEnumerator FadeToColor()
        {
            float start = _colorAdjustments.saturation.value;
            float t = 0, dur = 2f;
            while (t < dur)
            {
                t += Time.deltaTime;
                _colorAdjustments.saturation.value = Mathf.Lerp(start, 0, t / dur);
                yield return null;
            }
            _colorAdjustments.saturation.value = 0;
        }

        private void TeleportPlayer()
        {
            var player = GameObject.FindWithTag("Player");
            var cc = player.GetComponent<CharacterController>();
            if (cc) cc.enabled = false;
            player.transform.position = otherMap.position + Vector3.up * .5f;
            if (cc) cc.enabled = true;
        }

        private IEnumerator CrackOverlayRoutine()
        {
            // 순차적으로 각 조각 켜기
            for (int i = 0; i < crackImages.Length; i++)
            {
                crackImages[i].gameObject.SetActive(true);
                yield return new WaitForSeconds(overlayInterval);
            }
        }

        private IEnumerator CrackFallRoutine()
        {
            for (int i = 0; i < crackImages.Length; i++)
            {
                var img = crackImages[i];
                var rt = img.rectTransform;

                // 짧게 지연을 두고
                yield return new WaitForSeconds(fallDelay);

                // 아래로 떨어뜨리며 알파 페이드아웃, 살짝 회전
                rt.DOAnchorPosY(rt.anchoredPosition.y - fallDistance, fallTime).SetEase(Ease.InQuad);
                img.DOFade(0, fallTime);
                float angle = Random.Range(-30f, 30f);
                rt.DOLocalRotate(new Vector3(0, 0, angle), fallTime);
            }
        }
    }
}
