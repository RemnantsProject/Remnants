using UnityEngine;
using System.Collections.Generic;

namespace Remnants
{
    [System.Serializable]
    public class SceneData
    {
        public Vector3 playerPosition;           // 플레이어 위치 추가
        public Quaternion playerRotation;
        public List<string> activatedObjectNames = new List<string>();     // 생성된 오브젝트들의 목록
    }
}