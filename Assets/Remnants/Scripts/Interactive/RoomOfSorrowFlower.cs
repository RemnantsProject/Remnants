using System.Collections;
using UnityEngine;

namespace Remnants
{
    public class RoomOfSorrowFlower : Interactive
    {
        #region Variables
        public SceneFader whiteFader;
        [SerializeField]
        private string loadToScene = "RoomOfSorrow_2";
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


            yield return new WaitForSeconds(1f);

            whiteFader.FadeTo(loadToScene, true);
        }
        #endregion
    }

}
