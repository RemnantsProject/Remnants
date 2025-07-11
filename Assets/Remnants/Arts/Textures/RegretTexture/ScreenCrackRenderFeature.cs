using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ScreenCrackRenderFeature : ScriptableRendererFeature
{
    public Material crackMaterial;
    private ScreenCrackPass _pass;

    public override void Create()
    {
        if (crackMaterial == null)
        {
            Debug.LogWarning("[ScreenCrackRenderFeature] crackMaterial이 비어있습니다!");
            return;
        }

        _pass = new ScreenCrackPass(crackMaterial);
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        if (_pass == null)
        {
            Debug.LogError("[ScreenCrackRenderFeature] ScreenCrackPass 생성 안됨!");
            return;
        }

        // 항상 CameraTarget 사용
        _pass.Setup(new RenderTargetIdentifier(BuiltinRenderTextureType.CameraTarget));
        renderer.EnqueuePass(_pass);
    }
}
