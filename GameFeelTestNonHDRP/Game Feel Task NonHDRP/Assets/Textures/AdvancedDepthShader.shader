Shader "Unlit/AdvancedDepthShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _ColorOne ("Color One", color) = (1,1,1,1)
        _ColorTwo ("Color Two", color) = (1,1,1,1)
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
                float4 screenPosition : TEXCOORD1;
                float3 normal : TEXCOORD2;
                float3 viewDir : TEXCOORD3;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            sampler2D _CameraDepthTexture;
            float4 _ColorOne, _ColorTwo;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.uv.x += _Time.y;
                UNITY_TRANSFER_FOG(o, o.vertex);
                o.screenPosition = ComputeScreenPos(o.vertex);
                o.normal = UnityObjectToWorldNormal( v.normal);
                o.viewDir = normalize(WorldSpaceViewDir(v.vertex));
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                
                float2 textureCoordinate = i.screenPosition.xy / i.screenPosition.w;

                float r = dot(i.viewDir, normalize(i.normal)) * 2 + _Time.y;
                float g = (atan2(i.normal.y, i.normal.x) / 2 * UNITY_PI) + 0.5 + _Time.y;

                fixed4 col = tex2D(_MainTex, float2(r, g));
                float depth = Linear01Depth(SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, textureCoordinate));
                float3 color = lerp(_ColorOne, _ColorTwo, depth);
                float fresnel = pow(dot(i.viewDir, normalize(i.normal)), 0.3);

                return fixed4(pow(color * fresnel, 2) * 2 + col * (1 - fresnel) * 2, 1);
            }
            ENDCG
        }
    }
}
