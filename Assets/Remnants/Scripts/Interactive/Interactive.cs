using TMPro;
using UnityEngine;

namespace Remnants
{
    public class Interactive : MonoBehaviour
    {
        #region Variables
        //theDistance를 protected 로 중복 가능하게
        protected float theDistance;

        //액션 UI
        public GameObject actionUI;
        public TextMeshProUGUI actionText;

        public GameObject extraCross;       //커서 올렸을 때 그 오브젝트에 콜라이더가 붙어있다면

        //인터렉티브 기능 사용 여부
        [SerializeField]
        protected bool unInteractive = false;

        [SerializeField]
        protected string action = "Do Interactive Action";
        #endregion

        #region Unity Event Method
        private void Update()       //Update로 구하는 이유는 실시간으로 구해야 하기 때문
        {
            //오브젝트와 플레이어 사이간 거리 구하기
            theDistance = PlayerCasting.distanceFromTarget;
        }
        private void OnMouseOver()
        {
            extraCross.SetActive(true);

            if (theDistance <= 2f)
            {
                ShowActionUI();

            }
            //키입력 체크
            if (Input.GetKeyDown(KeyCode.E))
            {
                //
                extraCross.SetActive(false);

                //UI 숨기기
                HideActionUI();

                //액션
                DoAction();
            }
        }
        private void OnMouseExit()
        {
            extraCross.SetActive(false);
        }
        #endregion

        #region Custom Method
        //Action UI 보여주기
        protected void ShowActionUI()
        {
            actionUI.SetActive(true);
            actionText.text = action;
        }

        //Action UI 숨기기
        protected void HideActionUI()
        {
            actionUI.SetActive(false);
            actionText.text = "";
        }
        protected virtual void DoAction()
        {

        }
        #endregion
    }

}
