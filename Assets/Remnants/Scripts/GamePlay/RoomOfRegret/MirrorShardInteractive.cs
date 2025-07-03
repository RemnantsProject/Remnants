using UnityEngine;

namespace Remnants
{
    public class MirrorShardInteractive : Interactive
    {
        public PuzzleMirror puzzleMirror; // 퍼즐 보드 매니저
        public int shardIndex;            // 이 조각의 번호 (0 ~ 4)

        protected override void DoAction()
        {
            base.DoAction();

            if (puzzleMirror != null)
            {
                puzzleMirror.AddPiece(shardIndex);
            }

            // 오브젝트 제거 
            Destroy(this.gameObject);
        }
    }
}