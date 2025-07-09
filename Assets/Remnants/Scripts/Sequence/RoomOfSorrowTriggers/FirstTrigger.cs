using System.Collections;
using UnityEngine;

namespace Remnants
{
    /// <summary>
    /// 특정 트리거 구역에 플레이어가 진입했을 때 이벤트(텍스트 출력, 펫 활성화 등)를 발생시키는 클래스
    /// </summary>
    public class FirstTrigger : Trigger // 부모 클래스 Trigger를 상속
    {
        #region Variables

        // 등장시킬 펫 오브젝트
        public GameObject pet;

        // 펫을 활성화할지 여부를 설정하는 변수 (Inspector에서 설정 가능)
        [SerializeField]
        private bool enabledPet = false;

        #endregion

        #region Custom Method

        /// <summary>
        /// 트리거가 시작될 때 호출되는 코루틴 함수
        /// </summary>
        protected override IEnumerator StartTrigger()
        {
            // 플레이어가 존재할 경우
            if (player != null)
            {
                // 플레이어 비활성화 (트리거 연출 중 조작 방지)
                player.SetActive(false);

                // 연출용 텍스트 출력
                sequenceText.text = sequence;

                // 연출 시간 대기
                yield return new WaitForSeconds(2f);

                // 플레이어 다시 활성화
                player.SetActive(true);

                // 펫 활성화 설정이 true이고, pet 오브젝트가 존재하면 활성화
                if (enabledPet && pet != null)
                {
                    pet.SetActive(true);

                    // 펫 활성화를 위해 잠깐 시간 추가
                    yield return new WaitForSeconds(2f);
                }

                // 연출 텍스트 제거
                sequenceText.text = "";

                // 트리거 재실행 방지를 위해 콜라이더 비활성화
                DisableAllColliders();
            }
            else
            {
                // 플레이어가 존재하지 않을 경우에도 연출은 진행됨

                // 연출용 텍스트 출력
                sequenceText.text = sequence;

                // 연출 시간 대기
                yield return new WaitForSeconds(2f);

                // 연출 텍스트 제거
                sequenceText.text = "";

                // 트리거 재실행 방지
                DisableAllColliders();
            }
        }

        /// <summary>
        /// 이 트리거에 붙은 모든 BoxCollider를 비활성화하는 함수 (재진입 방지용)
        /// </summary>
        private void DisableAllColliders()
        {
            // 해당 게임 오브젝트에 있는 모든 BoxCollider 가져오기
            BoxCollider[] colliders = GetComponents<BoxCollider>();

            // 각각 비활성화
            foreach (BoxCollider collider in colliders)
            {
                collider.enabled = false;
            }
        }

        #endregion
    }
}
