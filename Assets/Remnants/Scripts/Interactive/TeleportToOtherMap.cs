using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Rendering.Universal;


namespace Remnants
{
    public class TeleportToOtherMap : Interactive
    {
        #region Variables
        [SerializeField]
        private Transform teleportTarget; // 이동할 위치
        [SerializeField] private Volume urpVolume; // URP Volume
        private ColorAdjustments colorAdjustments;
        #endregion

        #region Unity Event Method
        protected override void DoAction()
        {
            if (teleportTarget == null)
            {
                Debug.LogWarning("Teleport target not set.");
                return;
            }

            // 플레이어를 찾아서 위치 이동
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                player.transform.position = teleportTarget.position;
                player.transform.rotation = teleportTarget.rotation; // 필요하면 회전도
            }

            //충돌 제거
            CharacterController controller = player.GetComponent<CharacterController>();
            if (controller != null)
            {
                controller.enabled = false;
                player.transform.position = teleportTarget.position + Vector3.up * 0.5f;
                controller.enabled = true;
            }
            //흑백전환
            if (urpVolume != null && urpVolume.profile.TryGet(out colorAdjustments))
            {
                colorAdjustments.saturation.value = -100f;
                Debug.Log("URP ColorAdjustments applied: grayscale");
            }
            else
            {
                Debug.LogWarning("ColorAdjustments override not found.");
            }
        }
        #endregion

    }
}