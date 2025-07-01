using Unity.Cinemachine;
using UnityEngine;

namespace Remnants
{
    public class PushPlayerOnHit : MonoBehaviour
    {
        #region Variables
        [SerializeField]
        private float pushDistance = 1.5f;      //밀리는 거리
        [SerializeField]
        private float pushSpeed = 5f;           //밀리는 속도

        [SerializeField]
        private bool isPushing = false;
        private CharacterController playerController;
        private Vector3 pushDir;
        private float pushTimer = 0f;
        #endregion

        #region Unity Event Method
        private void OnCollisionEnter(Collision collision)
        {
            //collision 된 오브젝트가 7(Player)라면
            if(collision.gameObject.layer == 7)
            {
                // CharacterController 컴포넌트 얻기
                playerController = collision.gameObject.GetComponent<CharacterController>();
                if (playerController == null) return;

                // 밀어낼 방향 계산
                pushDir = (collision.transform.position - transform.position).normalized;
                pushDir.y = 0f;

                isPushing = true;
                pushTimer = pushSpeed;
            }
        }
        private void Update()
        {
            if (isPushing && playerController != null)
            {
                // 일정 시간 동안만 밀어내기
                playerController.Move(pushDir * pushDistance * Time.deltaTime);
                pushTimer -= Time.deltaTime;

                if (pushTimer <= 0f)
                {
                    isPushing = false;
                }
            }
        }
        #endregion

        #region Custom Method

        #endregion
    }

}
