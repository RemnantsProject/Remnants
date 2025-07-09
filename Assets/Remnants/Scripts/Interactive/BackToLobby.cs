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
        private Animator animator;
        #endregion
        private void Start()
        {
            animator = GetComponent<Animator>();
        }
        protected override void DoAction()
        {
            Debug.Log("[BackToLobby] HasVisitedRoom = " + TeleportState.Instance.HasVisitedRoom);
                if (!TeleportState.Instance.HasVisitedRoom)
                return;
            StartCoroutine(OpenDoor());
        }

        #region Custom Method
        IEnumerator OpenDoor()
        {
            animator.SetTrigger("Open");
            yield return new WaitForSeconds(1f);
            fader.FadeTo(loadToScene);
        }
        #endregion
    }
}