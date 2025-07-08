using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace Remnants
{
    public class ClueManager : MonoBehaviour
    {
        #region Variables
        public GameObject exitFlower;

        private FindingClues[] allClues;
        #endregion

        #region Unity Event Method
        private void Start()
        {
            allClues = FindObjectsByType<FindingClues>(FindObjectsSortMode.None);
            StartCoroutine(ActiveFlower());
        }
        #endregion

        #region Custom Method
        IEnumerator ActiveFlower()
        {
            while (true)
            {
                bool hasAnyRealClue = false;

                foreach (var clue in allClues)
                {
                    if (clue != null && clue.IsClue)
                    {
                        hasAnyRealClue = true;
                        break;
                    }
                }

                if (!hasAnyRealClue && exitFlower != null)
                {
                    exitFlower.SetActive(true);
                    yield break; // 조건 만족했으면 코루틴 종료
                }

                yield return new WaitForSeconds(1f); // 매초마다 검사
            }
            #endregion
        }
    }
}
