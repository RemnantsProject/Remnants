using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Remnants
{
    public class DistanceBgm : MonoBehaviour
    {
        #region Variables
        public Transform player;
        public AudioMixer audioMixer;
        public AudioManager audioManager;
        public List<string> sounds;

        [SerializeField]
        private string mixerVolumeParam = "Sfx"; // AudioMixer 파라미터 이름
        
        [SerializeField]
        private float maxDistance = 10f;

        private bool isPlaying = false;
        #endregion

        #region Unity Event Method
        private void Update()
        {
            if (player == null) return;

            float distance = Vector3.Distance(player.position, transform.position);
            float t = Mathf.Clamp01(1 - distance / maxDistance);
            float volume = Mathf.Lerp(-80f, 0f, t);

            audioMixer.SetFloat(mixerVolumeParam, volume);

            if (!isPlaying)
            {
                foreach (var soundName in sounds)
                {
                    audioManager.Play(soundName);
                } 
                isPlaying = true;
            }
        }
        #endregion

        #region Custom Method
        public void FalsePlaying()
        {
            isPlaying = false;
        }
        #endregion
    }
}
