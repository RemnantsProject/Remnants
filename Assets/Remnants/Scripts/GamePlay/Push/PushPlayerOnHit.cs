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
        private Transform targetPlayer;
        private Vector3 pushDir;
        #endregion

        #region Unity Event Method
        private void OnCollisionEnter(Collision collision)
        {
            //collision 된 오브젝트가 7(Player)라면
            if(collision.gameObject.layer == 7)
            {
                targetPlayer = collision.transform;
                pushDir = (collision.transform.position - this.transform.position).normalized;
                pushDir.y = 0f;
                isPushing = true;
            }
        }
        private void Update()
        {
            if(isPushing && targetPlayer != null)
            {
                //Debug.Log("ㅇㅇ아아앙ㅇ");
                targetPlayer.position += pushDir * pushSpeed * Time.deltaTime;  //플레이어 position을 밀기
                pushDistance -= pushSpeed * Time.deltaTime;     //밀려야 하는 남은 거리 계산

                //다 밀렸다면 종료
                if(pushDistance <= 0f)
                {
                    isPushing = false;  
                    pushDistance = 1.5f;    //리셋
                }
            }
        }
        #endregion

        #region Custom Method

        #endregion
    }

}
