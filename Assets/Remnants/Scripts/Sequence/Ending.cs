using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

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
        // 누가 말하고 있는지 보여 주는 텍스트
        public TextMeshProUGUI whoIsSayingText;
        // 펫 오브젝트
        public GameObject thePet;
        // 시퀀스 UI
        public GameObject sequenceUI;
        // 선택지 UI
        public GameObject selectUI;
        // 문 열림 애니메이션
        public Animator happyEndingAnimator;
        public Animator badEndingAnimator;

        // 누가 말하고 있는지 (0 : 없음, 1 : 플레이어, 2 : 펫)
        private int whoIsSaying;

        // 대사 여부
        private bool isSequencePlaying = true;

        // 분기 결정
        private bool isHappy = false;
        private bool isBad = false;

        // 대사 정보를 담는 구조체
        private struct Dialogue
        {
            public int speaker;
            public string line;
            public float waitTime;

            public Dialogue(int speaker, string line, float waitTime)
            {
                this.speaker = speaker;
                this.line = line;
                this.waitTime = waitTime;
            }
        }

        // 실제 재생할 대사 목록
        private List<Dialogue> sequence = new List<Dialogue>();
        private List<Dialogue> happySequence = new List<Dialogue>();
        private List<Dialogue> badSequence = new List<Dialogue>();

        private bool skipTriggered = false;

        #endregion

        #region Unity Event Method
        // 연출 시작
        private void Start()
        {
            // 초기화
            isHappy = false;
            isBad = false;

            //오프닝 연출 시작
            StartCoroutine(SequencePlay());
        }

        // 대사 순서 등록
        private void Awake()
        {
            sequence = new List<Dialogue>
            {
                new Dialogue(1, "이제 모든 방을 지나왔어.", 4f),
                new Dialogue(1, "그 안엔, 내가 숨기고 외면했던 감정들이 있었지.", 4f),
                new Dialogue(1, "하지만...", 4f),
                new Dialogue(1, "이제 더 이상 피할 곳이 없어.", 4f),

                new Dialogue(0, "", 4f),

                new Dialogue(1, "뭐... 뭐야...", 4f),
                new Dialogue(1, "네가 왜 거기에...", 4f),

                new Dialogue(2, "그때.. 기억나?", 4f),
                new Dialogue(2, "네가 나를 비웃던 그 순간.", 4f),
                new Dialogue(2, "넌 가만히 있었지.", 4f),
                new Dialogue(2, "마치... 그게 아무 일 아니라는 듯, 웃으면서.", 4f),
                new Dialogue(2, "그때에 나는,", 4f),
                new Dialogue(2, "모든 것이 무너지고 말았어.", 4f),

                new Dialogue(1, "너... 설마...", 4f),

                new Dialogue(2, "그래, 맞아. 난...", 4f),
                new Dialogue(2, "네가 외면했던...", 4f),
                new Dialogue(2, "네 소꿉친구야.", 4f),
                new Dialogue(2, "정확히 말하면,", 4f),
                new Dialogue(2, "그 아이의 슬픔, 외로움, 분노.", 4f),
                new Dialogue(2, "그 감정들이 모여 만든 존재이지.", 4f),
                new Dialogue(2, "넌 날 해친 적 없다고 믿었겠지.", 4f),
                new Dialogue(2, "하지만 그 침묵이...", 4f),
                new Dialogue(2, "나를 여기까지 오게 만들었어.", 4f),

                new Dialogue(1, "미안해... 난 그저 장난일 뿐이었는데...", 4f),
                new Dialogue(1, "하지만 넌, 혼자서 얼마나 무서웠겠어...", 4f),
                new Dialogue(1, "다 내 잘못이야. 미안해...", 4f),

                new Dialogue(2, "알아, 원망은 안 해.", 4f),
                new Dialogue(2, "왜냐하면...", 4f),
                new Dialogue(2, "그저 네가, 진짜로 나를 바라봐 주길 바랐거든.", 4f),
                new Dialogue(2, "하지만 이제 넌, 선택해야 해.", 4f),
                new Dialogue(2, "다시 돌아가서 죄책감을 짊어지고 현실을 살아갈 건지,", 4f),
                new Dialogue(2, "아니면...", 4f),
                new Dialogue(2, "여기에 남아 모든 걸 잊고 평생 죄책감을 짊어지고 살아갈 건지.", 4f),
                new Dialogue(2, "판단은 너의 몫이야.", 4f),
            };

            happySequence = new List<Dialogue>
            {
                new Dialogue(1, "결심했어, 난...", 4f),
                new Dialogue(1, "현실로 돌아갈 거야.", 4f),

                new Dialogue(2, "결국... 기억났구나.", 4f),
                new Dialogue(2, "그날, 내가 얼마나 무서웠는지.", 4f),

                new Dialogue(1, "그래... 처음엔 그냥 장난인 줄로만 생각했어,", 4f),
                new Dialogue(1, "하지만, 그때 너의 표정을 살짝 보면서...", 4f),
                new Dialogue(1, "진짜... 많이 힘들고 아팠겠다라는 생각도 들었어...", 4f),

                new Dialogue(2, "그럼, 이젠 외면하고 도망치지 않을 수 있겠어?", 4f),

                new Dialogue(1, "응. 이제야 제대로 마주할 수 있을 것 같아...", 4f),
                new Dialogue(1, "고맙고, 미안해...", 4f),
            };

            badSequence = new List<Dialogue>
            {
                new Dialogue(1, "난 그냥 여기에 남아 있을래...", 4f),

                new Dialogue(2, "여기에... 남는다고?", 4f),
                new Dialogue(2, "정말이야?", 4f),
                new Dialogue(2, "마지막으로 날 바라봐 줄 수도 없는 거야?", 4f),

                new Dialogue(1, "너한텐 미안하지만...", 4f),
                new Dialogue(1, "난 돌아갈 용기가 없어...", 4f),
                new Dialogue(1, "널 보면... 내가 얼마나 비겁했는지 다시 떠올라...", 4f),

                new Dialogue(2, "괜찮아, 난 널 미워하지 않아, 정말로!", 4f),

                new Dialogue(1, "그래서 더 힘든 것 같아...", 4f),
                new Dialogue(1, "난... 그냥 여기 남을게.", 4f),
                new Dialogue(1, "그래야 너한테 덜 미안할 것 같아...", 4f),

                new Dialogue(2, "...", 4f),
                new Dialogue(2, "그렇게라도 네 마음이 편해진다면, 말리지 않을게.", 4f),

                new Dialogue(1, "정말 미안해...", 4f),
            };
        }

        private void Update()
        {
            if (!isSequencePlaying)
                return;

            // 누가 말하고 있는지 체크
            switch (whoIsSaying)
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
        // 돌아가기 선택했을 때
        public void OnHappyButtonClick()
        {
            // 선택지 숨기고 대사 처리
            whoIsSaying = 0;
            selectUI.SetActive(false);
            StartCoroutine(PlaySelectedSequence(happySequence, 1.5f));
            isHappy = true;
        }

        // 남기 선택했을 때
        public void OnBadButtonClick()
        {
            // 선택지 숨기고 대사 처리
            whoIsSaying = 0;
            selectUI.SetActive(false);
            StartCoroutine(PlaySelectedSequence(badSequence, 1.5f));
            isBad = true;
        }

        // 오프닝 연출 코루틴 함수
        IEnumerator SequencePlay()
        {
            // 플레이 캐릭터 비활성화
            PlayerInput input = thePlayer.GetComponent<PlayerInput>();
            input.enabled = false;

            // 페이드 인 연출
            fader.FadeStart(17f);

            // 대사 순서대로 출력
            foreach (var line in sequence)
            {
                whoIsSaying = line.speaker;
                sequenceText.text = line.line;
                yield return new WaitForSeconds(line.waitTime);
            }

            isSequencePlaying = false;

            // 대사 텍스트 및 누가 말하는지 숨기기
            sequenceText.text = "";
            whoIsSayingText.text = "";

            // 선택지 보이기
            selectUI.SetActive(true);
        }

        // 선택지 연출
        IEnumerator PlaySelectedSequence(List<Dialogue> selectedSequence, float delay)
        {
            isSequencePlaying = true;

            // 선택 직후 잠시 기다림
            yield return new WaitForSeconds(delay);

            foreach (var line in selectedSequence)
            {
                whoIsSaying = line.speaker;
                sequenceText.text = line.line;
                yield return new WaitForSeconds(line.waitTime);
            }

            // 대사 숨김
            isSequencePlaying = false;
            sequenceText.text = "";

            whoIsSayingText.gameObject.SetActive(false);

            if (happyEndingAnimator != null && badEndingAnimator != null)
            {
                if(isHappy)
                {
                    happyEndingAnimator.SetTrigger("Happy");
                }
                else if(isBad)
                {
                    badEndingAnimator.SetTrigger("Bad");
                }
            }
        }
        #endregion
    }
}

