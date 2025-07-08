using System.Collections;
using TMPro;
using UnityEngine;

namespace Remnants
{
    //단서를 찾는 Interactive의 상속받는 클래스
    public class FindingClues : Interactive
    {
        #region Variables
        public TextMeshProUGUI sequenceText;
        [SerializeField]
        private string sequence = "Find Clue";
        private string notClueText = "이게 아니야..";

        [SerializeField]
        private bool isClue = false;
        #endregion

        #region Property
        public bool IsClue
        {
            get
            {
                return isClue;
            }
        }
        #endregion

        #region Unity Event Method

        #endregion

        #region Custom Method
        protected override void DoAction()
        {
            StartCoroutine(FindingClue());
        }

        IEnumerator FindingClue()
        {
            if (!IsClue)
            {
                sequenceText.text = notClueText;
                yield return new WaitForSeconds(3f);
                sequenceText.text = "";
            }
            else
            {
                sequenceText.text = sequence;
                yield return new WaitForSeconds(3f);
                sequenceText.text = "";

                //임시 삭제 코드
                Destroy(this.gameObject);
            }
            
        }
        #endregion
    }

}
