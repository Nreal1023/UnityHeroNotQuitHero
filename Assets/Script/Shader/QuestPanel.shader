Shader "Unlit/QuestPanel"
{
   Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _LeftColor ("Left Color", Color) = (1,1,1,1)
        _RightColor ("Right Color", Color) = (1,1,1,1)
        _GradientRange ("Gradient Range", Range(0, 1)) = 0.5
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
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
            fixed4 _LeftColor;
            fixed4 _RightColor;
            float _GradientRange;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 leftColor = _LeftColor;
                fixed4 rightColor = _RightColor;
                float gradient = i.uv.x <= 0.5 ? i.uv.x * 2 : (1 - i.uv.x) * 2; // Calculate gradient

                // Apply gradient to the colors
                fixed4 finalColor = lerp(leftColor, rightColor, gradient);

                // Apply gradient range
                finalColor.a *= smoothstep(_GradientRange, 1, gradient);

                return finalColor;
            }
            ENDCG
        }
    }
}
