Shader "Custom/ScreenCrack_URP"
{
    Properties
    {
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _CrackTex ("Crack Mask", 2D) = "white" {}
        _DistortAmount ("Distortion Amount", Range(0, 0.1)) = 0.02
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "RenderPipeline"="UniversalPipeline" }
        Pass
        {
            Name "ScreenCrackPass"
            Tags { "LightMode"="UniversalForward" }

            ZTest Always Cull Off ZWrite Off

            HLSLPROGRAM
            #pragma vertex VertDefault
            #pragma fragment Frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);

            TEXTURE2D(_CrackTex);
            SAMPLER(sampler_CrackTex);

            float _DistortAmount;

            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionCS : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            Varyings VertDefault(Attributes input)
            {
                Varyings output;
                output.positionCS = TransformObjectToHClip(input.positionOS.xyz);
                output.uv = input.uv;
                return output;
            }

            half4 Frag(Varyings input) : SV_Target
            {
                float2 uv = input.uv;

                float crack = SAMPLE_TEXTURE2D(_CrackTex, sampler_CrackTex, uv).r;

                float2 offset = float2(sin(uv.y * 100.0) * 0.005, cos(uv.x * 100.0) * 0.005);
                uv += offset * crack * _DistortAmount;

                half4 col = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, uv);

                col.rgb *= lerp(1.0, 0.6, crack);

                return col;
            }
            ENDHLSL
        }
    }
}
