using UnityEngine;
using System.Collections;
using TMPro;

namespace Remnants
{
    //공허의방 출구에 가까워지면 발생하는 Trigger
    public class SoundTrigger : MonoBehaviour
    {
        #region Variables
        //시나리오 대사 처리
        public TextMeshProUGUI sequenceText;

        public GameObject lastTrigger;
        //public GameObject Exit;

        [SerializeField]
        private string sequence01 = "";
        [SerializeField]
        private string sequence02 = "";
        #endregion

        #region Unity Event Method
        private void Start()
        {
            lastTrigger.SetActive(false);
            //Exit.SetActive(false);
        }
        private void OnTriggerEnter(Collider other)
        {
            //플레이어 체크
            if (other.tag == "Player")
            {
                //트리거 해제
                this.GetComponent<SphereCollider>().enabled = false;
                StartCoroutine(SequencePlayer());
            }
        }
        #endregion

        #region Custom Method
        IEnumerator SequencePlayer()
        {
            sequenceText.text = sequence01;
            yield return new WaitForSeconds(2f);
            sequenceText.text = sequence02;
            yield return new WaitForSeconds(2f);
            lastTrigger.SetActive(true);
            //Exit.SetActive(true);
        }
        #endregion

    }
}