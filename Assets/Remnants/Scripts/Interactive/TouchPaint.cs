using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.ParticleSystem;

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

        public ParticleSystem theParticle;
        public Transform CameraRoot;
        public SceneFader fader;
        [SerializeField] private string loadToScene = "Room1";

        private bool hasInteracted = false;  // 상호작용을 했는지 확인
        #endregion

        #region Unity Event Method
        void Start()
        {
            SceneStateSaver.Instance.SaveCurrentSceneState();
        }
        protected override void DoAction()
        {
            // 상호작용을 이미 한 경우 다시 실행되지 않도록
            if (hasInteracted) return;

            hasInteracted = true;  // 상호작용했음을 표시
            StartCoroutine(TouchingPaint());
        }
        #endregion

        #region Custom Method
        IEnumerator TouchingPaint()
        {
            // 1. 거울 파티클의 방향을 CameraRoot 쪽으로! (실시간 LookAt)
            if (theParticle != null && CameraRoot != null)
            {
                theParticle.transform.LookAt(CameraRoot.position);
                theParticle.gameObject.SetActive(true); // 혹시 비활성화였다면
                theParticle.Play();
            }

            //플레이어 빨려들기 타겟 지정 
            var playerSuck = CameraRoot.GetComponent<PlayerBlackholeSuck>();         
            playerSuck.targetObject = this.transform; 
            playerSuck.StartSuck();

            // 액자 상태 변경
            fakePicture.SetActive(false);
            realPicture.SetActive(true);
            nextPicture.SetActive(true);

            string sceneName = SceneManager.GetActiveScene().name;
            GameStateManager.Instance.MarkObjectActivated(sceneName, realPicture.GetComponent<RestorableObject>().objectID);
            GameStateManager.Instance.MarkObjectActivated(sceneName, nextPicture.GetComponent<RestorableObject>().objectID);

            SceneStateSaver.Instance.SaveCurrentSceneState();

            // 씬 전환
            fader.FadeTo(loadToScene);
            yield return new WaitForSeconds(2f);
        }
        #endregion
    }
}
