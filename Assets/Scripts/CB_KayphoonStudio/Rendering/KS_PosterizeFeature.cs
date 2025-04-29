using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace KayphoonStudio.Rendering
{

    public class PosterizeFeature : ScriptableRendererFeature
    {
    [System.Serializable]
    public class Settings {
        public RenderPassEvent renderPassEvent = RenderPassEvent.AfterRendering;
        public Material posterizeMaterial = null;
        [Range(2,64)] public int steps = 4;
    }
    public Settings settings = new Settings();

    class PosterizePass : ScriptableRenderPass
    {
        public Material mat;
        public int steps;
        private RenderTargetHandle tempHandle;
        private RenderTargetIdentifier source;

        public PosterizePass() {
        tempHandle.Init("_TempPosterizeTexture");
        }

        public void Setup(RenderTargetIdentifier src) {
        source = src;
        }

        public override void Execute(ScriptableRenderContext ctx, ref RenderingData data)
        {
        if (mat == null) return;

        mat.SetFloat("_Steps", steps);

        var cmd = CommandBufferPool.Get("PosterizePass");
        var desc = data.cameraData.cameraTargetDescriptor;
        desc.depthBufferBits = 0;

        cmd.GetTemporaryRT(tempHandle.id, desc, FilterMode.Bilinear);
        cmd.Blit(source, tempHandle.Identifier(), mat);
        cmd.Blit(tempHandle.Identifier(), source);
        ctx.ExecuteCommandBuffer(cmd);
        CommandBufferPool.Release(cmd);
        }
    }

    PosterizePass pass;

    public override void Create()
    {
        pass = new PosterizePass {
        mat   = settings.posterizeMaterial,
        steps = settings.steps
        };
        pass.renderPassEvent = settings.renderPassEvent;
    }

    public override void SetupRenderPasses(ScriptableRenderer renderer, in RenderingData renderingData)
    {
        if (settings.posterizeMaterial == null) return;
        pass.Setup(renderer.cameraColorTargetHandle);
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData data)
    {
        if (settings.posterizeMaterial == null) return;
        renderer.EnqueuePass(pass);
    }
    }
}