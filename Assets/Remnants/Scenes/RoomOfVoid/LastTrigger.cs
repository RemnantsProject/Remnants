using TMPro;
using UnityEngine;
using System.Collections;

namespace Remnants
{
    //출구 를 발견할때 생기는 다사 트리거
    public class LastTrigger : MonoBehaviour
    {
        #region Variables
        //시나리오 대사 처리
        public TextMeshProUGUI sequenceText;

        [SerializeField]
        private string sequence01 = "";
        [SerializeField]
        private string sequence02 = "";
        #endregion

        #region Unity Event Method
        private void OnTriggerEnter(Collider other)
        {
            //플레이어 체크
            if (other.tag == "Player")
            {
                //트리거 해제
                this.GetComponent<BoxCollider>().enabled = false;
                StartCoroutine(SequencePlayer());
            }
        }
        #endregion

        #region Custom Method
        IEnumerator SequencePlayer()
        {
            sequenceText.text = sequence01;
            yield return new WaitForSeconds(1f);
            sequenceText.text = sequence02;
            yield return new WaitForSeconds(1.8f);
        }
        #endregion
    }
}
