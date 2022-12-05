Shader "Unlit/DepthShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _ColorOne ("Color One", color) = (1,1,1,1)
        _ColorTwo ("Color Two", color) = (1,1,1,1)
        _Ramp("Ramp ", float) = 1
        _Multiplier ("Multiplier ", float) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
                float4 screenSpace : TEXCOORD1;
                float3 normal : TEXCOORD2;
                float3 viewDir : TEXCOORD3;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            sampler2D _CameraDepthTexture;
            float4 _ColorOne, _ColorTwo;
            float _Ramp, _Multiplier;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.uv.x += _Time.y;
                UNITY_TRANSFER_FOG(o, o.vertex);
                o.screenSpace = ComputeScreenPos(o.vertex);
                o.normal = UnityObjectToWorldNormal(v.normal);
                o.viewDir = normalize(WorldSpaceViewDir(v.vertex));
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                float2 screenSpaceUV = i.screenSpace.xy / i.screenSpace.w;
                float depth = Linear01Depth(SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, screenSpaceUV));
                float3 mixedColor = lerp(_ColorOne, _ColorTwo, depth);
                float frensel = pow(1 - dot(i.viewDir, i.normal),_Ramp) * _Multiplier;
                float3 finalColor = lerp(mixedColor, col, frensel);

                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
}
