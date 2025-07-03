using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections;
using TMPro;

// 문 열린 후 페이드 아웃 효과 부모 클래스
namespace Remnants
{
    public abstract class FadeTriggerBase : MonoBehaviour
    {
        #region Variables
        // 플레이어 오브젝트
        public GameObject thePlayer;
        // 페이드 할 이미지
        public Image fadeImage;

        // 페이드 아웃 지속 시간
        [SerializeField]
        protected float fadeDuration = 2f;

        // 엔딩 대사 (배열 처리)
        public TextMeshProUGUI[] endingLines;

        // 대사 한 줄 페이드 효과 지속 시간
        [SerializeField]
        private float textFadeDuration = 1f;

        // 대사 한 줄 유지 시간
        [SerializeField]
        private float textDisplayDuration = 2f;
        #endregion

        #region Property
        // 자식 클래스에서 사용할 페이드 색상
        protected abstract Color FadeColor { get; }
        #endregion

        #region Unity Event Method
        private void OnTriggerEnter(Collider other)
        {
            // 플레이어 체크
            if (other.tag == "Player")
            {
                // 트리거 해제
                this.GetComponent<BoxCollider>().enabled = false;
                StartCoroutine(SequencePlayer());
            }
        }
        #endregion

        #region Custom Method
        IEnumerator SequencePlayer()
        {
            // 플레이 캐릭터 비활성화(플레이 멈춤)
            PlayerInput input = thePlayer.GetComponent<PlayerInput>();
            input.enabled = false;

            // 페이드 아웃 효과 연출
            yield return StartCoroutine(FadeOutImage(FadeColor, fadeDuration));
        }

        IEnumerator FadeOutImage(Color color, float duration)
        {
            float elapsed = 0f;

            // 시작 색상 - 투명
            Color startColor = color;
            startColor.a = 0f;

            // 종료 색상 - 완전 불투명
            Color endColor = color;
            endColor.a = 1f;

            // 페이드 이미지 활성화
            fadeImage.gameObject.SetActive(true);
            fadeImage.color = startColor;

            // 경과 시간에 따라 점점 불투명해짐
            while (elapsed < duration)
            {
                fadeImage.color = Color.Lerp(startColor, endColor, (elapsed / duration));
                elapsed += Time.deltaTime;
                yield return null;
            }

            // 최종 색상
            fadeImage.color = endColor;
        }
        #endregion
    }
}