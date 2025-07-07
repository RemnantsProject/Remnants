using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Remnants
{
    // 특정 구역에 진입했을 때 이벤트 발생을 담당하는 트리거 클래스
    public class FirstTrigger : Trigger // Trigger를 상속받음
    {
        #region Variables
        public GameObject pet;

        // 펫을 활성화할지 여부 (아직 코드상 사용되지 않음)
        [SerializeField]
        private bool enabledPet = false;

        #endregion

        #region Unity Event Method
        // Unity의 이벤트 함수는 없음 (예: Start, Update는 사용되지 않음)
        #endregion

        #region Custom Method

        // 트리거가 시작될 때 실행되는 코루틴 함수
        protected override IEnumerator StartTrigger()
        {
            // player가 존재할 경우
            if (player != null)
            {
                if (enabledPet)
                {
                    // player 오브젝트 비활성화
                    player.SetActive(false);

                    // 트리거 연출 텍스트 출력
                    sequenceText.text = sequence;

                    // 2초 대기
                    yield return new WaitForSeconds(2f);

                    // player 오브젝트 다시 활성화
                    player.SetActive(true);

                    pet.SetActive(true);

                    // 텍스트 제거
                    sequenceText.text = "";

                    // 이 트리거 오브젝트에 붙은 모든 BoxCollider 컴포넌트를 가져옴
                    BoxCollider[] colliders = this.gameObject.GetComponents<BoxCollider>();

                    // 각 BoxCollider를 비활성화하여 트리거 재실행 방지
                    foreach (BoxCollider collider in colliders)
                    {
                        collider.enabled = false;
                    }
                }
                else
                {
                    // player 오브젝트 비활성화
                    player.SetActive(false);

                    // 트리거 연출 텍스트 출력
                    sequenceText.text = sequence;

                    // 2초 대기
                    yield return new WaitForSeconds(2f);

                    // player 오브젝트 다시 활성화
                    player.SetActive(true);

                    // 텍스트 제거
                    sequenceText.text = "";

                    // 이 트리거 오브젝트에 붙은 모든 BoxCollider 컴포넌트를 가져옴
                    BoxCollider[] colliders = this.gameObject.GetComponents<BoxCollider>();

                    // 각 BoxCollider를 비활성화하여 트리거 재실행 방지
                    foreach (BoxCollider collider in colliders)
                    {
                        collider.enabled = false;
                    }
                }
            }
            else // player가 존재하지 않을 경우에도 동일한 동작 (단, player 관련 처리는 없음)
            {
                // 트리거 텍스트 출력
                sequenceText.text = sequence;

                // 2초 대기
                yield return new WaitForSeconds(2f);

                // 텍스트 제거
                sequenceText.text = "";

                // 트리거 재실행 방지
                BoxCollider[] colliders = this.gameObject.GetComponents<BoxCollider>();
                foreach (BoxCollider collider in colliders)
                {
                    collider.enabled = false;
                }
            }
        }

        #endregion
    }
}
