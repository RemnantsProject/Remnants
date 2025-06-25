using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

namespace Remnants
{
    // 엔딩 신 연출
    public class Ending : MonoBehaviour
    {
        #region Variables
        // 플레이어 오브젝트
        public GameObject thePlayer;
        // 페이더 객체
        public SceneFader fader;
        // 시나리오 대사 처리
        public TextMeshProUGUI sequenceText;

        // 누가 말하고 있는지
        private int whoIsSaying;
        // 누가 말하고 있는지 보여 주는 텍스트
        public TextMeshProUGUI whoIsSayingText;

        // 플레이어 대사
        [SerializeField]
        private string pl_Sequence01 = "이제 모든 방을 지나왔어.";
        [SerializeField]
        private string pl_Sequence02 = "그 안엔, 내가 숨기고 외면했던 감정들이 있었지.";
        [SerializeField]
        private string pl_Sequence03 = "하지만...";
        [SerializeField]
        private string pl_Sequence04 = "이제 더 이상 피할 곳이 없어.";
        [SerializeField]
        private string pl_Sequence05 = "뭐... 뭐야...";
        [SerializeField]
        private string pl_Sequence06 = "네가 왜 거기에...";
        [SerializeField]
        private string pl_Sequence07 = "너... 설마...";
        [SerializeField]
        private string pl_Sequence08 = "하, 하지만 나는 직접 때린 적이 없다고!";
        [SerializeField]
        private string pl_Sequence09 = "계속 그렇게 생각했어.";
        [SerializeField]
        private string pl_Sequence10 = "하지만 그건, 회피였어.";
        [SerializeField]
        private string pl_Sequence11 = "나도 몰랐던 건 아니야.";
        [SerializeField]
        private string pl_Sequence12 = "그 웃음이, 그 외면이,";
        [SerializeField]
        private string pl_Sequence13 = "누군가에겐 칼이 될 수 있다는 걸...";

        // 펫 대사
        [SerializeField]
        private string pe_Sequence01 = "그때.. 기억나?";
        [SerializeField]
        private string pe_Sequence02 = "네가 나를 비웃던 그 순간.";
        [SerializeField]
        private string pe_Sequence03 = "넌 가만히 있었지.";
        [SerializeField]
        private string pe_Sequence04 = "마치... 그게 아무 일 아니라는 듯, 웃으면서.";
        [SerializeField]
        private string pe_Sequence05 = "그때에 나는,";
        [SerializeField]
        private string pe_Sequence06 = "모든 것이 무너지고 있었어.";
        [SerializeField]
        private string pe_Sequence07 = "그래, 맞아. 난...";
        [SerializeField]
        private string pe_Sequence08 = "네가 외면했던...";
        [SerializeField]
        private string pe_Sequence09 = "네 소꿉친구야.";
        [SerializeField]
        private string pe_Sequence10 = "정확히 말하면,";
        [SerializeField]
        private string pe_Sequence11 = "그 아이의 감정이지.";
        [SerializeField]
        private string pe_Sequence12 = "알아, 원망은 안 해.";
        [SerializeField]
        private string pe_Sequence13 = "왜냐하면...";
        [SerializeField]
        private string pe_Sequence14 = "그저 네가, 진짜로 나를 바라봐 주길 바랐거든.";
        [SerializeField]
        private string pe_Sequence15 = "하지만 이제 넌, 선택해야 해.";
        [SerializeField]
        private string pe_Sequence16 = "다시 돌아가서 죄책감을 짊어지고 현실을 살아갈 건지,";
        [SerializeField]
        private string pe_Sequence17 = "아니면...";
        [SerializeField]
        private string pe_Sequence18 = "여기에 남아 모든 걸 잊고 평생 죄책감을 짊어지고 살아갈 건지.";
        [SerializeField]
        private string pe_Sequence19 = "판단은 너의 몫이야.";
        #endregion

        #region Unity Event Method
        private void Start()
        {
            //커서 제어
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            //오프닝 연출 시작
            StartCoroutine(SequencePlay());
        }
        private void Update()
        {
            // 누가 말하고 있는지 체크
            switch(whoIsSaying)
            {
                case 0:
                    whoIsSayingText.text = "";
                    break;
                case 1:
                    whoIsSayingText.text = "플레이어";
                    sequenceText.color = Color.white;
                    break;
                case 2:
                    whoIsSayingText.text = "펫";
                    sequenceText.color = Color.cyan;
                    break;
            }
        }
        #endregion

        #region Custom Method
        // 오프닝 연출 코루틴 함수
        IEnumerator SequencePlay()
        {
            // 0. 플레이 캐릭터 비활성화
            PlayerInput input = thePlayer.GetComponent<PlayerInput>();
            input.enabled = false;

            //1. 페이드 인 연출
            fader.FadeStart(17f);

            // 2. 플레이어 시나리오 텍스트 화면 출력 - 플레이어와 펫의 대화 연출
            whoIsSaying = 1;

            sequenceText.text = pl_Sequence01;
            yield return new WaitForSeconds(4f);

            sequenceText.text = pl_Sequence02;
            yield return new WaitForSeconds(4f);

            sequenceText.text = pl_Sequence03;
            yield return new WaitForSeconds(4f);

            sequenceText.text = pl_Sequence04;
            yield return new WaitForSeconds(4f);



            whoIsSaying = 0;

            sequenceText.text = "";
            yield return new WaitForSeconds(2f);



            whoIsSaying = 1;

            sequenceText.text = pl_Sequence05;
            yield return new WaitForSeconds(4f);

            sequenceText.text = pl_Sequence06;
            yield return new WaitForSeconds(4f);



            whoIsSaying = 2;

            sequenceText.text = pe_Sequence01;
            yield return new WaitForSeconds(4f);

            sequenceText.text = pe_Sequence02;
            yield return new WaitForSeconds(4f);

            sequenceText.text = pe_Sequence03;
            yield return new WaitForSeconds(4f);

            sequenceText.text = pe_Sequence04;
            yield return new WaitForSeconds(4f);

            sequenceText.text = pe_Sequence05;
            yield return new WaitForSeconds(4f);

            sequenceText.text = pe_Sequence06;
            yield return new WaitForSeconds(4f);



            whoIsSaying = 1;

            sequenceText.text = pl_Sequence07;
            yield return new WaitForSeconds(4f);


            whoIsSaying = 2;

            sequenceText.text = pe_Sequence07;
            yield return new WaitForSeconds(4f);

            sequenceText.text = pe_Sequence08;
            yield return new WaitForSeconds(4f);

            sequenceText.text = pe_Sequence09;
            yield return new WaitForSeconds(4f);

            sequenceText.text = pe_Sequence10;
            yield return new WaitForSeconds(4f);

            sequenceText.text = pe_Sequence11;
            yield return new WaitForSeconds(4f);


            whoIsSaying = 1;

            sequenceText.text = pl_Sequence08;
            yield return new WaitForSeconds(4f);

            sequenceText.text = pl_Sequence09;
            yield return new WaitForSeconds(4f);

            sequenceText.text = pl_Sequence10;
            yield return new WaitForSeconds(4f);

            sequenceText.text = pl_Sequence11;
            yield return new WaitForSeconds(4f);

            sequenceText.text = pl_Sequence12;
            yield return new WaitForSeconds(4f);

            sequenceText.text = pl_Sequence13;
            yield return new WaitForSeconds(4f);



            whoIsSaying = 2;

            sequenceText.text = pe_Sequence12;
            yield return new WaitForSeconds(4f);

            sequenceText.text = pe_Sequence13;
            yield return new WaitForSeconds(4f);

            sequenceText.text = pe_Sequence14;
            yield return new WaitForSeconds(4f);

            sequenceText.text = pe_Sequence15;
            yield return new WaitForSeconds(4f);

            sequenceText.text = pe_Sequence16;
            yield return new WaitForSeconds(4f);

            sequenceText.text = pe_Sequence17;
            yield return new WaitForSeconds(4f);

            sequenceText.text = pe_Sequence18;
            yield return new WaitForSeconds(4f);

            sequenceText.text = pe_Sequence19;
            yield return new WaitForSeconds(4f);
        }
        #endregion
    }
}

