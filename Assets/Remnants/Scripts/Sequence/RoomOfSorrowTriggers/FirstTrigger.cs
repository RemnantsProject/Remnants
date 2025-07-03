using System.Collections;
using UnityEngine;

namespace Remnants
{
    public class FirstTrigger : Trigger
    {
        #region Variables

        #endregion

        #region Unity Event Method

        #endregion

        #region Custom Method
        protected override IEnumerator StartTrigger()
        {
            if(player != null)
            {
                player.SetActive(false);
                sequenceText.text = sequence;

                yield return new WaitForSeconds(2f);

                player.SetActive(true);
                sequenceText.text = "";

                BoxCollider[] colliders = this.gameObject.GetComponents<BoxCollider>();

                foreach (BoxCollider collider in colliders)
                {
                    collider.enabled = false;
                }
            }
            else
            {
                sequenceText.text = sequence;

                yield return new WaitForSeconds(2f);

                sequenceText.text = "";

                BoxCollider[] colliders = this.gameObject.GetComponents<BoxCollider>();

                foreach (BoxCollider collider in colliders)
                {
                    collider.enabled = false;
                }
            }            
        }
        #endregion
    }

}
