using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace Remnants
{
    //액자 상호작용
    public class TouchPaint : Interactive
    {
        #region Variables
        //액자 오브젝트
        public GameObject fakePicture;  //빈 액자
        public GameObject realPicture;  //눈 그림이 있는 액자
        public GameObject nextPicture;  //다음 액자

        public SceneFader fader;
        [SerializeField] private string loadToScene = "Room1";

        public Transform[] cameraTargets;
        public int currentPictureIndex; // 몇 번째 액자인지 판단해서 할당

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
            // 액자 상태 변경
            fakePicture.SetActive(false);
            realPicture.SetActive(true);
            nextPicture.SetActive(true);

            string sceneName = SceneManager.GetActiveScene().name;
            GameStateManager.Instance.MarkObjectActivated(sceneName, realPicture.GetComponent<RestorableObject>().objectID);
            GameStateManager.Instance.MarkObjectActivated(sceneName, nextPicture.GetComponent<RestorableObject>().objectID);

            SceneStateSaver.Instance.SaveCurrentSceneState();

            // 빨려들어가는 연출
            CameraSuckEffect camSuck = Camera.main.GetComponent<CameraSuckEffect>();
            if (camSuck != null && cameraTargets.Length > currentPictureIndex)
            {
                camSuck.target = cameraTargets[currentPictureIndex];
                camSuck.SuckIn();
                yield return new WaitForSeconds(camSuck.duration);
            }

            // 씬 전환
            fader.FadeTo(loadToScene);
            yield return new WaitForSeconds(2f);
        }
        #endregion
    }
}
