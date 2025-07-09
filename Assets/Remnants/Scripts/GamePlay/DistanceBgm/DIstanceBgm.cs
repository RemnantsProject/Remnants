using UnityEngine;
using UnityEngine.Audio;

namespace Remnants
{
    public class DIstanceBgm : MonoBehaviour
    {
        #region Variables
        public Transform player;
        public AudioMixer audioMixer;
        [SerializeField]
        private string FamilySfx_1 = "FamilySfx_1";
        [SerializeField]
        private float maxDistance =10f;
        #endregion

        #region Unity Event Method
        private void Update()
        {
            if (player == false)
                return;

            float distance = Vector3.Distance(player.position, this.transform.position);
            float t = Mathf.Clamp01(1 - distance / maxDistance);
            float voluem = Mathf.Lerp(-80f, 0f, t);

            audioMixer.SetFloat(FamilySfx_1, voluem);
        }
        #endregion

        #region Custom Method

        #endregion
    }

}
