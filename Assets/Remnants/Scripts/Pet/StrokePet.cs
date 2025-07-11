using UnityEngine;

namespace Remnants
{
    public class StrokePet : Interactive
{
        #region Variables
        public Renderer targetRenderer;

        public GameObject angelRing;
        public GameObject blade;

        public Material smileMaterial;
        public Material scaryMaterial;

        private bool isStroke = false;
        #endregion

        #region Unity Event Method

        #endregion

        #region Custom Method
        protected override void DoAction()
        {
            isStroke = !isStroke;

            if (targetRenderer != null)
            {
                targetRenderer.material = smileMaterial;
                angelRing.SetActive(true);
                blade.SetActive(false);
            }
            else
            {
                targetRenderer.material = scaryMaterial;
                angelRing.SetActive(false);
                blade.SetActive(true);
            }
        }
        #endregion
    }

}
