Shader "Custom/GymSpace" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Tillingx("Tilling X",Range(0,20)) = 1
		_Tillingy("Tilling Y",Range(0,20)) = 1
        _Emissive("Emissive", 2D) = "white" {}
		_Speed("Speed", Range(0,1)) = .5
			_EmissivePower("Speed", Range(0,5)) = 1
	}
		SubShader{
			Tags { "RenderType" = "Opaque" }
			LOD 200

			CGPROGRAM

			#pragma surface surf Standard fullforwardshadows

			#pragma target 3.0

	    sampler2D _MainTex, _Emissive;
		float _Tillingy, _Tillingx, _Speed, _EmissivePower;

		struct Input {
			float2 uv_MainTex;
		};

		fixed4 _Color;


		void surf (Input IN, inout SurfaceOutputStandard o) {

			fixed emission = tex2D(_Emissive, IN.uv_MainTex).r;

			fixed4 c = (emission)*  tex2D(_MainTex, IN.uv_MainTex * float2(_Tillingx, _Tillingy) + float2(0, _Time.y) *_Speed) *_Color;

			o.Albedo = c.rgb;

			o.Emission = ((1 -emission)*  tex2D(_MainTex, IN.uv_MainTex * float2(_Tillingx, _Tillingy) + float2(0, _Time.y) *_Speed) * _Color) * _EmissivePower;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
