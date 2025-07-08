using UnityEngine;
using System.Collections.Generic;

namespace Remnants
{
    public class SceneObjectRegistry : MonoBehaviour
    {
        #region Variables
        // 싱글톤 인스턴스
        public static SceneObjectRegistry Instance;
        // 오브젝트 ID
        private Dictionary<string, GameObject> objects = new Dictionary<string, GameObject>();
        #endregion
        #region Unity Event Method
        private void Awake()
        {
            //초기화
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
        #endregion

        #region Custom Method
        // 오브젝트를 등록하는 함수
        public void Register(RestorableObject obj)
        {
            if (!string.IsNullOrEmpty(obj.objectID) && !objects.ContainsKey(obj.objectID))
            {
                objects.Add(obj.objectID, obj.gameObject);
                //Debug.Log($"[SceneObjectRegistry] Registered object: {obj.objectID}");
            }
            else
            {
                //Debug.LogWarning($"[SceneObjectRegistry] Duplicate or invalid objectID: {obj.objectID}");
            }
        }
        // 등록된 ID로 오브젝트
        public GameObject GetObjectByID(string id)
        {
           if(objects.TryGetValue(id, out var obj))
            {
                return obj;
            }
            
            return null;
        }
        public List<string> GetAllObjectIDs()
        {
            return new List<string>(objects.Keys);
        }

        public void RegisterAllInScene()
        {
            var restorables = FindObjectsOfType<RestorableObject>(true); // 비활성 포함
            foreach (var r in restorables)
            {
                Register(r);
            }
        }
        #endregion
    }
}