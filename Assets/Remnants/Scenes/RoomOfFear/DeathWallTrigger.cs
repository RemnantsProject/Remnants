using UnityEngine;

namespace Remnants
{
    //벽 충돌박스
    public class DeathWallTrigger : MonoBehaviour
    {
        #region Property
        public bool IsCatch { get; private set; }
        #endregion

        #region Unity Event Method
        private void Start()
        {
            IsCatch = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            // Layer 이름으로 비교 (또는 태그도 가능)
            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                Debug.Log("Game Over: 벽에 닿음");
                IsCatch = true;

                // 게임 오버 처리
                // Time.timeScale = 0f;
                // GameManager.Instance.GameOver(); 가능
            }
        }
        #endregion
    }
}