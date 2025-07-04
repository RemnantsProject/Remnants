using UnityEngine;

namespace Remnants
{
    //단서를 찾는 Interactive의 상속받는 클래스
    public class FindingClues : Interactive
    {
        #region Variables
        [SerializeField]
        private bool IsClue = false;
        #endregion

        #region Unity Event Method

        #endregion

        #region Custom Method
        protected override void DoAction()
        {
            
        }
        #endregion
    }

}
