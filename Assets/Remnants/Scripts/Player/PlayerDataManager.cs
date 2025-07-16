using UnityEngine;

namespace Remnants
{
    //장착 무기 타입 enum
    public enum WeaponType
    {
        None,
        Pistol,
    }

    //퍼즐 아이템 enum값 설정
    public enum PuzzleKey
    {
        ROOM01_KEY,
        LEFTEYE_KEY,
        RIGHTEYE_KEY,
        MAX_KEY              //퍼즐 아이템 갯수
    }

    //플레이어 데이터 관리 클래스 - 싱글톤(다음 씬에서 데이터 보존)
    public class PlayerDataManager : PersistanceSingleton<PlayerDataManager>
    {
        #region Variables
        private int sceneNumber;        //플레이중인 씬 빌드 번호

        private bool[] hasKeys;         //퍼즐 아이템 소지 여부 체크

        #endregion

        #region Property

        //플레이중인 씬 빌드 번호
        public int SceneNumber
        {
            get
            {
                return sceneNumber;
            }
            set
            {
                sceneNumber = value;
            }
        }

        #endregion

        #region Unity Event Method
        protected override void Awake()
        {
            base.Awake();

            //플레이 데이터 초기화 
            InitPlayerData();
        }
        #endregion

        #region Custom Method
        //플레이 데이터 초기화 
        public void InitPlayerData(PlayData pData = null)
        {
            if(pData != null)
            {
                sceneNumber = pData.sceneNumber;


                //....
            }
            else
            {
                sceneNumber = -1;
            }   
            //퍼즐 아이템 설정: 퍼즐 아이템 갯수만큼 불형 변수 요소수 생성
            hasKeys = new bool[(int)PuzzleKey.MAX_KEY];
        }
        //퍼즐 아이템 획득 - 매개변수로 퍼즐키 타입 받는다
        public void GainPuzzleKey(PuzzleKey key)
        {
            hasKeys[(int)key] = true;
        }

        //퍼즐 아이템 소지 여부 체크 - 매개변수로 퍼즐키 타입 받는다
        public bool HasPuzzleKey(PuzzleKey key)
        {
            return hasKeys[(int)key];
        }
        #endregion

    }
}
