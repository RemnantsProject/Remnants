using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace Remnants
{
    public class BackToLobby : Interactive
    {
        #region Variables
        public SceneFader fader;
        [SerializeField]
        private string loadToScene = "Lobby";
        #endregion
        protected override void DoAction()
        {
            if (!TeleportState.Instance.HasVisitedRoom)
            {
                return;
            }

            StartCoroutine(OpenDoor());
        }

        #region Custom Method
        IEnumerator OpenDoor()
        {          
           
            yield return new WaitForSeconds(1f);
            fader.FadeTo(loadToScene);
        }
        #endregion
    }
}