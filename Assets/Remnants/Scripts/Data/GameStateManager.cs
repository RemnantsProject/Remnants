using System.Collections.Generic;
using UnityEngine;

namespace Remnants
{
    //씬별로 오브젝트 활성 상태를 저장
    public class GameStateManager : MonoBehaviour
    {
        #region Variables
        public static GameStateManager Instance;

        // 씬 이름 -> 그 씬의 상태 정보 저장
        public Dictionary<string, SceneData> savedScenes = new Dictionary<string, SceneData>();
        #endregion

        #region Unity Event Method

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this.gameObject); // 씬 바뀌어도 살아있음
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
        #endregion

        #region Custom Method
        public void MarkObjectActivated(string sceneName, string objectName)
        {
            if (!savedScenes.ContainsKey(sceneName))
                savedScenes[sceneName] = new SceneData();

            if (!savedScenes[sceneName].activatedObjectNames.Contains(objectName))
                savedScenes[sceneName].activatedObjectNames.Add(objectName);
        }

        public void SavePlayerState(string sceneName, Vector3 position, Quaternion rotation)
        {
            if (!savedScenes.ContainsKey(sceneName))
                savedScenes[sceneName] = new SceneData();

            savedScenes[sceneName].playerPosition = position;
            savedScenes[sceneName].playerRotation = rotation;
        }

        public SceneData GetSceneData(string sceneName)
        {
            if (savedScenes.ContainsKey(sceneName))
                return savedScenes[sceneName];
            return null;
        }
        #endregion
    }
}