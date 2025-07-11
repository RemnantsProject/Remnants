using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ScreenCrackPass : ScriptableRenderPass
{
    private readonly Material _material;
    private RenderTargetIdentifier _source;
    private int _temporaryColorTextureID;

    public ScreenCrackPass(Material material)
    {
        _material = material;
        _temporaryColorTextureID = Shader.PropertyToID("_TemporaryColorTexture");
        renderPassEvent = RenderPassEvent.AfterRenderingTransparents;
    }

    public void Setup(RenderTargetIdentifier source)
    {
        _source = source;
    }


    public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
    {
        Debug.Log("[ScreenCrackPass] OnExecute!");

        CommandBuffer cmd = CommandBufferPool.Get("ScreenCrackPass");

        RenderTextureDescriptor descriptor = renderingData.cameraData.cameraTargetDescriptor;
        descriptor.depthBufferBits = 0;

        cmd.GetTemporaryRT(_temporaryColorTextureID, descriptor, FilterMode.Bilinear);
        RenderTargetIdentifier tempRT = new RenderTargetIdentifier(_temporaryColorTextureID);

        // struct는 null 체크 불가. 기본값과 비교.
        RenderTargetIdentifier src = !_source.Equals(default(RenderTargetIdentifier))
            ? _source
            : new RenderTargetIdentifier(BuiltinRenderTextureType.CameraTarget);

        cmd.Blit(src, tempRT, _material);
        cmd.Blit(tempRT, src);

        context.ExecuteCommandBuffer(cmd);
        CommandBufferPool.Release(cmd);
    }

    public override void FrameCleanup(CommandBuffer cmd)
    {
        if (cmd != null)
            cmd.ReleaseTemporaryRT(_temporaryColorTextureID);
    }
}
