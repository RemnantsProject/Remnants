using System.Collections;
using TMPro;
using UnityEngine;

namespace Remnants
{
    public class RoomOfAngerExit : Interactive
    {
        #region Variablse
        public SceneFader fader;
        [SerializeField]
        private string loadToScene = "Lobby";

        public Animator animator;
        public TextMeshProUGUI sequnceText;

        [SerializeField]
        private string sequnce = "나는 그 죄를 모두 기억하고, 내 안에서 껴안기로 했다";
        #endregion

        #region Unity Event Method

        #endregion

        #region Custom Method
        protected override void DoAction()
        {
            StartCoroutine(Clear());            
        }

        IEnumerator Clear()
        {
            sequnceText.text = sequnce;

            this.GetComponent<CapsuleCollider>().enabled = false;

            yield return new WaitForSeconds(2f);

            sequnceText.text = "";

            animator.SetBool("IsClear", true);

            yield return new WaitForSeconds(1f);

            fader.FadeTo(loadToScene);

            
        }
        #endregion
    }

}
