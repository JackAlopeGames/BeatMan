Shader "Custom/SP"
{
	Properties
	{


		_MainTex("Albedo", 2D) = "white" {}
		_Mettalic("Metallic", 2D) = "white" {}
		_NormalMap("Normal map", 2D) = "bump" {}
		_Emission("Emission", 2D) = "white" {}
		_Lines("Png Effect", 2D) = "white" {}
		_Tiling("Tilling", Range(1,100)) = 1
		_Color("Special Color", Color) = (0,0,1)
		_RimPower("Rim Power", Range(0,6)) = 0.5
		_RimColor("Rim Color", Color) = (1,1,1,1)
		_EmissionPower("EmissionPower", Range(0,1)) = 1
	    _ColorE("EmissiveColor",Color) = (0,1,0,1)
	}

		SubShader
		{
		Tags{ "RenderType" = "Transparency" }
		Cull Off

		CGPROGRAM
		#pragma surface surf Standard
		#pragma target 3.0

		sampler2D _MainTex,_Noise,_Lines, _Emission, _Metallic, _NormalMap;
		fixed4 _Color;
	
		float _RimPower, _Tiling, _Presence, _RimPresence, _EmissionPower;
		float4 _RimColor, _ColorE;

		struct Input
		{

			float2 uv_MainTex;
			float3 viewDir;
			float3 worldNormal;
			INTERNAL_DATA
		};


		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			fixed4 main = lerp((tex2D(_MainTex, IN.uv_MainTex) + tex2D(_Lines, IN.uv_MainTex * float2(_Tiling,_Tiling) + float2(0, _Time.x * 2)) * _Color), (tex2D(_MainTex, IN.uv_MainTex)), _Presence);
			o.Albedo = main;
			float NdotV = 1.0 - saturate(dot(IN.viewDir, IN.worldNormal));
			o.Emission =  lerp((tex2D(_Emission, IN.uv_MainTex).rgb * _ColorE.rgb *_EmissionPower), (_RimColor * pow(NdotV, _RimPower)), _RimPresence);
			o.Metallic = tex2D(_Metallic, IN.uv_MainTex).r;
			o.Normal = UnpackNormal(tex2D(_NormalMap, IN.uv_MainTex));
		}

		ENDCG
		}

		FallBack "Diffuse"
}
