using System.Collections;
using TMPro;
using UnityEngine;

namespace Remnants
{
    public class RoomOfSorrowFlower : Interactive
    {
        #region Variables
        public SceneFader whiteFader;
        [SerializeField]
        private string loadToScene = "RoomOfSorrow_2";

        public TextMeshProUGUI sequenceText;
        [SerializeField]
        private string sequence = "Sequence";
        #endregion

        #region Unity Event Method

        #endregion

        #region Custom Method
        protected override void DoAction()
        {
            StartCoroutine(LoadScene());
        }

        IEnumerator LoadScene()
        {
            sequenceText.text = sequence;

            yield return new WaitForSeconds(2f);

            sequenceText.text = "";

            whiteFader.FadeTo(loadToScene);
        }
        #endregion
    }

}
