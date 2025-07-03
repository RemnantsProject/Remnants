using UnityEngine;
using TMPro;
using System.Collections;

namespace Remnants
{
    public class PuzzleMirror : MonoBehaviour
    {
        #region Variables
        public TextMeshProUGUI regretText;
        public string[] regretLines = new string[5];

        private int collected = 0;
        private Coroutine hideTextCoroutine;

        public GameObject[] worldShards = new GameObject[5]; // 거울에 붙을 조각 오브젝트들
        public GameObject completeMirror;                    // 완성된 거울
        public GameObject brokenMirrorRoot;                  // 깨진 조각 묶음

        #endregion
        #region Custom Method
        public void AddPiece(int index)
        {
            if (index < worldShards.Length)
            {
                worldShards[index].SetActive(true);

                // 자책 대사 출력
                regretText.text = regretLines[index];

                // 이전 코루틴 멈추고 새로 시작 (겹침 방지)
                if (hideTextCoroutine != null)
                    StopCoroutine(hideTextCoroutine);
                hideTextCoroutine = StartCoroutine(HideTextAfterDelay(3f));

                collected++;

                if (collected == worldShards.Length)
                {
                    OnPuzzleComplete();
                }
            }
        }
        

        private IEnumerator HideTextAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            regretText.text = "";
        }

        private void OnPuzzleComplete()
        {
            Debug.Log("거울 퍼즐 완성!");

            // 깨진 조각들은 숨기고, 완성된 거울 보여주기
            if (brokenMirrorRoot != null)
                brokenMirrorRoot.SetActive(false);

            if (completeMirror != null)
                completeMirror.SetActive(true);

            // 추가 연출, 사운드, 빛 효과 등 여기서!
        }
        #endregion
    }
}