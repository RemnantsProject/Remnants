using UnityEngine;

namespace Remnants 
{
    public class RoomOfVoidExit : Interactive
    {
        #region Variables
        public SceneFader fader;
        [SerializeField]
        private string loadToScene = "Lobby";
        
        public AudioSource bgm01;       //배경음
        #endregion

        #region Custom Method
        private void Start()
        {
            bgm01.Play();
        }
        protected override void DoAction()
        {

            //배경음 종료
            bgm01.Stop();
            
            //씬 종료시 처리할 내용 구현
            //....

            //다음씬으로 이동
            fader.FadeTo(loadToScene);

            //충돌체 제거
            this.GetComponent<BoxCollider>().enabled = false;
        }
        #endregion
    }
}