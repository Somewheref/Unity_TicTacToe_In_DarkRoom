Shader "Kayphoon Studio/Posterize"
{
  Properties {
    _MainTex ("Texture", 2D) = "white" {}
    _Steps   ("Posterize Steps", Range(2,64)) = 4
  }
  SubShader {
    Tags { "RenderPipeline"="UniversalPipeline" "RenderType"="Opaque" }
    Pass {
      Name "Posterize"
      ZTest Always Cull Off ZWrite Off

      HLSLPROGRAM
      #pragma vertex VertDefault
      #pragma fragment Frag
      #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

      TEXTURE2D(_MainTex);
      SAMPLER(sampler_MainTex);
      float _Steps;

      struct Attributes { float4 positionOS:POSITION; float2 uv:TEXCOORD0; };
      struct Varyings  { float4 positionCS:SV_POSITION; float2 uv:TEXCOORD0; };

      Varyings VertDefault(Attributes IN) {
        Varyings OUT;
        OUT.positionCS = TransformObjectToHClip(IN.positionOS);
        OUT.uv         = IN.uv;
        return OUT;
      }

      half4 Frag(Varyings IN) : SV_Target {
        float4 col = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, IN.uv);
        col.rgb = floor(col.rgb * _Steps) / _Steps;
        return col;
      }
      ENDHLSL
    }
  }
}
