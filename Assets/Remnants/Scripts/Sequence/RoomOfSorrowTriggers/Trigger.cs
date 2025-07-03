using System.Collections;
using TMPro;
using UnityEngine;

namespace Remnants
{
    public class Trigger : MonoBehaviour
    {
        #region Variables
        public GameObject player;
        public TextMeshProUGUI sequenceText;

        [SerializeField]
        protected string sequence = "";
        #endregion

        #region Unity Event Method
        private void OnTriggerEnter(Collider other)
        {
            if(other.tag == "Player")
            {
                StartCoroutine(StartTrigger());
            }
        }
        #endregion

        #region Custom Method
        protected virtual IEnumerator StartTrigger()
        {
            yield return null;
        }
        #endregion
    }

}
