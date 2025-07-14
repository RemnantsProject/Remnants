using UnityEngine;

namespace Remnants
{
    public class StoryObjectScript : Interactive
    {
        #region Variables
        public GameObject storyUI;
        #endregion

        #region Property
        #endregion

        #region Unity Event Method
        private void Update()
        {
            if (IsUIOpened && Input.GetKeyDown(KeyCode.Escape))
            {
                //Debug.Log($"{gameObject.name} ESC 눌림");
                StoryClose();
            }
        }
        #endregion

        #region Custom Method
        protected override void DoAction()
        {
            if (!IsUIOpened)
            {
                StoryOpen();
            }
            
        }

        private void StoryOpen()
        {
            storyUI.SetActive(true);
            IsUIOpened = true;
        }
        private void StoryClose()
        {
            storyUI.SetActive(false);
            IsUIOpened = false;
        }
        #endregion
    }

}
