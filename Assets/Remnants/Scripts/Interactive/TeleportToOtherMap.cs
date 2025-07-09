using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

namespace Remnants
{
    public class TeleportToOtherMap : Interactive
    {
        #region Variables
        [SerializeField] private Transform otherMap;
        [SerializeField] private Volume urpVolume;
        private ColorAdjustments colorAdjustments;

        [SerializeField] private Image[] crackImages;
        [SerializeField] private float stageInterval = 0.5f;
        [SerializeField] private float finalHold = 0.5f;

        [SerializeField] private CanvasGroup fadePanel;   // Fade 용 CanvasGroup
        [SerializeField] private float fadeDuration = 1f; // 페이드 인/아웃 시간

        [SerializeField] private TextMeshProUGUI sequenceText;

        [SerializeField] private FullscreenCrackEffect crackEffect;

        private static bool goToOtherMap = true;
        private bool hasUsed = false;
        #endregion

        private void Awake()
        {
            if (urpVolume != null && urpVolume.profile.TryGet(out colorAdjustments))
                colorAdjustments.saturation.overrideState = true;
        }

        #region Custom Method
        protected override void DoAction()
        {
            if (!hasUsed || hasUsed && !goToOtherMap)
            {
                hasUsed = true;
                TeleportState.Instance.HasVisitedRoom = true;
                StartCoroutine(TeleportRoutine());
                var col = GetComponent<Collider>();
                if (col != null) col.enabled = false;
               
            }
        
        }

        private IEnumerator TeleportRoutine()
        {
            // 크랙 연출
            yield return PlayCrackSequence();

            if (crackEffect != null)
                crackEffect.PlayFullScreenCrack();


            // 화면 블랙아웃
            yield return Fade(0, 1);

            // 순간이동
            TeleportPlayer();

            // 흑백 or 컬러 복귀
            if (goToOtherMap)
            {
                // 첫 호출: 흑백
                colorAdjustments.saturation.value = -100f;
            }
            else
            {
                // 두 번째 호출: 컬러로 서서히 복귀
                yield return FadeToColor();
            }

            //  플래그 토글
            goToOtherMap = !goToOtherMap;

            // 화면 원복
            yield return Fade(1, 0);
            fadePanel.gameObject.SetActive(false);

            //  두 번째 호출(돌아올 때)이 끝난 뒤에만 비활성화
            if (goToOtherMap)
                gameObject.SetActive(false);
        }

        private IEnumerator PlayCrackSequence()
        {
            if (crackImages == null || crackImages.Length == 0)
            {
                Debug.LogWarning("[PlayCrackSequence] crackImages가 할당되지 않았거나 길이가 0입니다.");
                yield break;
            }
            // 1) 모든 크랙 이미지 비활성화
            foreach (var img in crackImages)
                img.gameObject.SetActive(false);

            // 2) 단계별로 한 장씩 켜기
            for (int i = 0; i < crackImages.Length; i++)
            {
                // 이전 이미지 끄기
                if (i > 0)
                    crackImages[i - 1].gameObject.SetActive(false);

                // 현재 이미지 켜기
                crackImages[i].transform.SetAsLastSibling();
                crackImages[i].gameObject.SetActive(true);

                // 일정 시간 대기
                yield return new WaitForSeconds(stageInterval);
            }

            // 3) 마지막 스테이지 잠깐 유지
            yield return new WaitForSeconds(finalHold);

            // 4) 마지막 이미지도 끄기
            crackImages[crackImages.Length - 1].gameObject.SetActive(false);
        }

        private IEnumerator Fade(float from, float to)
        {
            // 페이드 패널 활성화 & 초기 알파
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

        private void TeleportPlayer()
        {
            var p = GameObject.FindWithTag("Player");
            var cc = p.GetComponent<CharacterController>();
            if (cc) cc.enabled = false;
            p.transform.position = otherMap.position + Vector3.up * .5f;
            if (cc) cc.enabled = true;
        }
        private IEnumerator FadeToColor()
        {
            float start = colorAdjustments.saturation.value;
            float t = 0, dur = 2f;
            while (t < dur)
            {
                t += Time.deltaTime;
                colorAdjustments.saturation.value = Mathf.Lerp(start, 0, t / dur);
                yield return null;
            }
            colorAdjustments.saturation.value = 0;
        }
        #endregion
    }
}
