using UnityEngine;

namespace Remnants
{
    // 해피엔딩 문 연출
    public class HappyEndingDoor : Interactive
    {
        #region Variables
        // 애니메이션
        public Animator animator;

        // 애니메이션 파라미터 스트링
        private string paramHappy = "Happy";
        #endregion

        #region Custom Method
        protected override void DoAction()
        {
            // 문 열기, 충돌체 제거
            animator.SetTrigger(paramHappy);
            this.GetComponent<BoxCollider>().enabled = false;
        }
        #endregion
    }
}

