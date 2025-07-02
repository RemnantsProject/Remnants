using System.Collections;
using UnityEngine;

namespace Remnants
{
    public class RoomOfFearBadEnding : MonoBehaviour
    {
        #region Variables
        public SceneFader fader;
        [SerializeField]
        private string loadToScene = "MainScene02";

        //public AudioSource doorBang;    //문여는 소리
        //public AudioSource bgm01;       //배경음
        #endregion

        #region Custom Method
        private void OnTriggerEnter(Collider other)
        {
            
            SequencePlayer();
        }

        IEnumerator SequencePlayer()
        {

            //bgm01.Stop();



            yield return new WaitForSeconds(0.3f);

            fader.FadeTo(loadToScene);

            //충돌체 제거
            this.GetComponent<BoxCollider>().enabled = false;
        }
        #endregion

    }
}