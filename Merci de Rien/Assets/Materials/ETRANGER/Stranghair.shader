Shader "Unlit/Stranghair"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_Mask ("Noise", 2D) = "white" {}
		_Stars ("Stars", 2D) = "black" {}
		_ScreenScaleTex("ScreenScaleTex", Float) = 1.0
		_ScreenScaleStars("ScreenScaleStars", Float) = 1.0
		_NoiseClip("NoiseClip", Float) = 0.5
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
		ZWrite Off
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
				float4 color : COLOR;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
				float4 color : COLOR;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
			sampler2D _Mask;
			sampler2D _Stars;
            float4 _MainTex_ST;
			float _ScreenScaleTex;
			float _ScreenScaleStars;
			float _NoiseClip;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.color = v.color;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
				float2 screenPos = ComputeScreenPos(i.vertex).xy / _ScreenParams.xy;
				float mask = tex2D(_Mask, i.uv);
				
				fixed4 col = tex2D(_MainTex, screenPos * _ScreenScaleTex + _Time[0] * 2.0);
				for(int j = 0; j < 3; j++)
				{
					col += tex2D(_Stars, screenPos * _ScreenScaleStars * j + ((_Time[0] * 2.0) *j * j) + float2(0.45, 0.2) * j);
				}

				
                return fixed4(col.rgb, mask * _NoiseClip) * i.color;
            }
            ENDCG
        }
    }
}
