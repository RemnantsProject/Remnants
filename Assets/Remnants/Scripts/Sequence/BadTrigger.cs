using UnityEngine;

namespace Remnants
{
    public class BadTrigger :FadeTriggerBase
{
        // 검은색 페이드
        protected override Color FadeColor => Color.gray;

        // BGM
        protected override string EndingBgmName => "BadEndingBgm";
    }
}

