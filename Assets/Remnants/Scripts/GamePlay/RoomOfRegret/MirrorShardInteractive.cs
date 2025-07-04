using System;
using UnityEngine;

namespace Remnants
{
    public class MirrorShardInteractive : Interactive
    {
        [SerializeField] private int index; // 조각 인덱스

        protected override void DoAction()
        {
            // 퍼즐 매니저에 조각 수집 요청
            PuzzleManager.Instance.CollectPiece(index);

            // 오브젝트 제거 
            Destroy(this.gameObject);
        }
    }
}