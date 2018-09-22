Shader "Custom/Hologram"
{
	Properties
	{
		_MainTex("Albedo", 2D) = "white" {}
		_Noise("Noise", 2D) = "white" {}
		_Lines("Lines", 2D) = "white" {}
		_Color("Hologram Color", Color) = (0,0,1)
		_RimPower("Rim Power", Range(0,6)) = 0.5
		_RimColor("Rim Color", Color) = (1,1,1,1)
		_Tilling("Tilling", Range(0,6)) = 1
		_Transparency("Transparency", Range(0.0,1)) = 0.25
	}

	SubShader
	{
	Tags{ "Queue" = "Transparent" "RenderType" = "Transparent" }
	ZWrite Off
	Blend SrcAlpha OneMinusSrcAlpha
	ZTest LEqual
	LOD 200
	CGPROGRAM
	#pragma surface surf StandardSpecular fullforwardshadows alpha:blend

	// Use shader model 3.0 target, to get nicer looking lighting
	#pragma target 3.0

	sampler2D _MainTex,_Noise,_Lines;
	fixed4 _Color;

	float _RimPower, _Transparency;
	float _Tilling;
	float4 _RimColor;

	struct Input
	{
		float2 uv_MainTex;
		float3 viewDir;
		float3 worldNormal;
	};


	void surf(Input IN, inout SurfaceOutputStandardSpecular  o)
	{
		fixed4 main = (tex2D(_MainTex, IN.uv_MainTex)  +( (tex2D(_Noise, IN.uv_MainTex + float2(_Time.x,0).r) + tex2D(_Lines, IN.uv_MainTex * float2(_Tilling, _Tilling) + float2(0, _Time.x * 2) ).r) * (tex2D(_MainTex, IN.uv_MainTex).r)) )* _Color;
		main.a = _Transparency;
		o.Albedo = main;
		float NdotV = 1.0 - saturate(dot(IN.viewDir, IN.worldNormal));
		o.Emission = _RimColor * pow(NdotV, _RimPower);
		o.Alpha = main.a;
	}

	ENDCG
	}

		FallBack "Diffuse"
}
