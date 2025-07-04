using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Remnants
{
    public class TeleportToOtherMap : Interactive
    {
        [SerializeField] private Transform otherMap;
        [SerializeField] private Volume urpVolume;
        private ColorAdjustments colorAdjustments;
        private Coroutine fadeCoroutine;

        private bool hasUsed = false; // 이미 사용했는지 여부

        [SerializeField] private bool goToOtherMap = true; // true면 흑백, false면 컬러 복귀

        [SerializeField] private TextMeshProUGUI sequenceText;



        protected override void DoAction()
        {
            if (hasUsed) // 이미 사용했으면 무시
            {
                SequenceText("(아무 일도 일어나지 않는다)");
                return;
            }
            hasUsed = true;

            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player == null) return;

            CharacterController controller = player.GetComponent<CharacterController>();
            if (controller != null) controller.enabled = false;

            if (otherMap != null)
                player.transform.position = otherMap.position + Vector3.up * 0.5f;

            if (controller != null) controller.enabled = true;

            // 색상 처리
            if (urpVolume != null && urpVolume.profile.TryGet(out colorAdjustments))
            {
                if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);

                if (goToOtherMap)
                {
                    Debug.Log(" ColorAdjustments 가져옴");
                    colorAdjustments.saturation.value = -100f;
                }
                else
                {
                    Debug.LogWarning(" ColorAdjustments 못 가져옴");
                    fadeCoroutine = StartCoroutine(FadeToColor());
                }
            }
            TeleportState.Instance.HasVisitedRoom = true;
        }

        private IEnumerator FadeToColor()
        {
            float start = colorAdjustments.saturation.value;
            float t = 0f;
            float duration = 2f;

            while (t < duration)
            {
                t += Time.deltaTime;
                colorAdjustments.saturation.value = Mathf.Lerp(start, 0f, t / duration);
                yield return null;
            }

            colorAdjustments.saturation.value = 0f;
        }
        private Coroutine feedbackCoroutine;

        private void SequenceText(string message)
        {
            if (feedbackCoroutine != null)
                StopCoroutine(feedbackCoroutine);

            feedbackCoroutine = StartCoroutine(SequenceRoutine(message));
        }

        private IEnumerator SequenceRoutine(string message)
        {
            sequenceText.text = message;
            sequenceText.gameObject.SetActive(true);
            yield return new WaitForSeconds(2f);
            sequenceText.gameObject.SetActive(false);
        }
    }
}