using TMPro;
using UnityEngine;
using System.Collections;

namespace Remnants
{
    public class ClearHint : MonoBehaviour
    {
        #region Variables  
        //시나리오 대사 처리
        public TextMeshProUGUI sequenceText;

        [SerializeField]
        private string sequence01 = "...Where am I?";

        [SerializeField]
        private string sequence02 = "I need get out of here";

        #endregion

        #region Unity Event Method
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                StartCoroutine(GameClearHint());
            }

        }

        #endregion

        #region Custom Method
        IEnumerator GameClearHint()
        {
            //화면 하단에 시나리오 텍스트 화면 출력(1초)
            sequenceText.text = sequence01;
            yield return new WaitForSeconds(1.5f);

            sequenceText.text = sequence02;
            yield return new WaitForSeconds(1.5f);

            //1.5초후에 시나리오 텍스트 없어진다
            sequenceText.text = "";
        }

        #endregion
    }
}