Shader "Unlit/ColorMapShaderLegacy"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Inverse ("Inverse", float) = 0.0
        _BaseColor ("Base Color", Color) = (1, 1, 1, 1)
        _HighlightColor ("Highlight Color", Color) = (0, 0, 0, 0)
        _DarkestThreshold ("Darkest threshold", float) = 0.25
        _DarkThreshold ("Dark threshold", float) = 0.5
        _LightThreshold ("Light threshold", float) = 0.75
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

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _Inverse;
            float4 _BaseColor;
            float4 _HighlightColor;
            float _DarkestThreshold;
            float _DarkThreshold;
            float _LightThreshold;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);

                if (_Inverse > 0.5) {
                    o.uv.y = 1.0 - o.uv.y;
                }
                

                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);

                //col = col * _BaseColor;

                if (col.x <= _LightThreshold && col.x > _DarkThreshold) {
                    col = col * _LightThreshold;
                } else if (col.x <= _DarkThreshold && col.x > _DarkestThreshold) {
                    col = col * _DarkThreshold;
                } else if (col.x <= _DarkestThreshold) {
                    col = col * _DarkestThreshold;
                }

                col = _HighlightColor * col.r;

                if (_Inverse) {
                    col.rgb = 1 - col;
                    //col = col.r * _BaseColor;
                }

                col = _BaseColor * col.r;

                col.a = 1;

                return col;
            }
            ENDCG
        }
    }
}
