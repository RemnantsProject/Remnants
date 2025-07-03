using System.Collections;
using TMPro;
using UnityEngine;

namespace Remnants
{
    public class LockDoor : Interactive
    {
        #region Variables
        public TextMeshProUGUI sequenceText;
        //public AudioManager audioManager;

        [SerializeField]
        protected string sequence = "";
        #endregion

        #region Unity Event Method

        #endregion

        #region Custom Method
        protected override void DoAction()
        {
            StartCoroutine(StartTrigger());
        }

        IEnumerator StartTrigger()
        {
            sequenceText.text = sequence;

            yield return new WaitForSeconds(2f);

            sequenceText.text = "";
        }
        #endregion
    }

}
