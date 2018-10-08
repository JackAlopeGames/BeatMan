﻿Shader "Custom/Hologram"
{
	Properties
	{
		_MainTex("Albedo Texture", 2D) = "white" {}
		_Lines("Hologram Lines Texture", 2D) = "white" {}
		_Tilling("Tilling", Range(0,1)) = 1
		_TintColor("Tint Color", Color) = (1,1,1,1)
		_TintLines("Tint Lines", Color) = (1,1,1,1)
		_Transparency("Transparency", Range(0.0,1)) = 0.25
		_CutoutThresh("Cutout Threshold", Range(0.0,1.0)) = 0.2
		_Distance("Distance", Float) = 1
		_Amplitude("Amplitude", Float) = 1
		_Speed("Speed", Float) = 1
		_Amount("Amount", Range(0.0,1.0)) = 1
		_SpeedH("Speed Hologram", Range(0.0,1.0)) = 1
	}

		SubShader
	{
		Tags{ "Queue" = "Transparent" "RenderType" = "Transparent" }
		LOD 100

		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha

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

			sampler2D _MainTex,_Lines;
			float4 _MainTex_ST;
			float4 _TintColor,_TintLines;
			float _Transparency, _Tilling;
			float _CutoutThresh;
			float _Distance;
			float _Amplitude;
			float _Speed, _SpeedH;
			float _Amount;

			v2f vert(appdata v)
			{
				v2f o;
				v.vertex.x += sin(_Time.y * _Speed + v.vertex.y * _Amplitude) * _Distance * _Amount;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				// sample the texture
				fixed4 lines = tex2D(_Lines,i.uv * float2(1,_Tilling) + float2(0, _Time.x * _SpeedH));
				fixed4 col = (tex2D(_MainTex, i.uv) *_TintColor) + lines * _TintLines;
				col.a = _Transparency;
				clip(col.r - _CutoutThresh);
				return col;
			}
			ENDCG
		}
	}
}