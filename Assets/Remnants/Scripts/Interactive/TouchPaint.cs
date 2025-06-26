using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace Remnants
{
    //액자 상호작용
    public class TouchPaint : Interactive
    {
        #region Variables
        //액자
        public GameObject fakePicture;  //빈 액자
        public GameObject realPicture;  //눈 그림이 있는 액자
        public GameObject nextPicture;  //다음 액자

        public SceneFader fader;
        [SerializeField]
        private string loadToScene = "Room1";
        #endregion

        #region Unity Event Method
        void Start()
        {
            SceneStateSaver.Instance.SaveCurrentSceneState();
        }
        protected override void DoAction()
        {
            StartCoroutine(TouchingPaint());
        }
        #endregion

        #region Custom Method
        IEnumerator TouchingPaint()
        {
           
            //액자
            fakePicture.SetActive(false);
            realPicture.SetActive(true);
            nextPicture.SetActive(true);
            string sceneName = SceneManager.GetActiveScene().name;
            // 오브젝트 상태 저장
            GameStateManager.Instance.MarkObjectActivated(sceneName, realPicture.GetComponent<RestorableObject>().objectID);
            GameStateManager.Instance.MarkObjectActivated(sceneName, nextPicture.GetComponent<RestorableObject>().objectID);

            // 플레이어 상태 저장
            SceneStateSaver.Instance.SaveCurrentSceneState();
            fader.FadeTo(loadToScene);
            yield return new WaitForSeconds(2f);
      
        }
        #endregion
    }
}