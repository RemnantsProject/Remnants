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
            Debug.Log("[Teleport] Crack Trigger!");

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
