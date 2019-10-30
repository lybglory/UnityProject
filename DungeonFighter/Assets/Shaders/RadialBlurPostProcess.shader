// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/RadialBlurPostProcess" 
{
	Properties 
	{
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_BlurStrength   ("BlurStrength", float)   = 0.3
		_SampleDist ("SampleDist", float) = 0.4
		_SampleStrength("SampleStrength", float) = 3.0
		
	}

	SubShader 
	{
		Pass 
		{
			ZTest Always Cull Off ZWrite Off Fog { Mode Off }
		
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
	
			struct v2f 
			{
		    	float4 pos : SV_POSITION;
		    	half2 uv : TEXCOORD0;
			};
		
			float4 _MainTex_ST;
			float _BlurStrength;
			float _SampleDist;
			float _SampleStrength;
			sampler2D _MainTex;

			v2f vert (appdata_base v)
			{
		    	v2f o;
		    	o.pos = UnityObjectToClipPos (v.vertex);
		    	o.uv = TRANSFORM_TEX (v.texcoord, _MainTex);
		    	return o;
			}
	
			float4 frag (v2f i) : COLOR
			{
				float2 dir = 0.5 - i.uv;
				float dist = length(dir);
				dir /= dist;
				
				float4 color = tex2D (_MainTex, i.uv);
				
				float4 sum = color;
				
				sum += tex2D(_MainTex, i.uv + dir * -0.26 * _SampleDist * _BlurStrength);
				//sum += tex2D(_MainTex, i.uv + dir * -0.20 * _SampleDist * _BlurStrength);
				sum += tex2D(_MainTex, i.uv + dir * -0.15 * _SampleDist * _BlurStrength);
				//sum += tex2D(_MainTex, i.uv + dir * -0.11 * _SampleDist * _BlurStrength);
				sum += tex2D(_MainTex, i.uv + dir * -0.08 * _SampleDist * _BlurStrength);
				//sum += tex2D(_MainTex, i.uv + dir * -0.05 * _SampleDist * _BlurStrength);
				sum += tex2D(_MainTex, i.uv + dir * -0.03 * _SampleDist * _BlurStrength);
				//sum += tex2D(_MainTex, i.uv + dir * -0.02 * _SampleDist * _BlurStrength);
				sum += tex2D(_MainTex, i.uv + dir * -0.01 * _SampleDist * _BlurStrength);
				sum += tex2D(_MainTex, i.uv + dir * 0.01 * _SampleDist * _BlurStrength);
				//sum += tex2D(_MainTex, i.uv + dir * 0.02 * _SampleDist * _BlurStrength);
				sum += tex2D(_MainTex, i.uv + dir * 0.03 * _SampleDist * _BlurStrength);
				//sum += tex2D(_MainTex, i.uv + dir * 0.05 * _SampleDist * _BlurStrength);
				sum += tex2D(_MainTex, i.uv + dir * 0.08 * _SampleDist * _BlurStrength);
				//sum += tex2D(_MainTex, i.uv + dir * 0.11 * _SampleDist * _BlurStrength);
				sum += tex2D(_MainTex, i.uv + dir * 0.15 * _SampleDist * _BlurStrength);
				//sum += tex2D(_MainTex, i.uv + dir * 0.20 * _SampleDist * _BlurStrength);
				sum += tex2D(_MainTex, i.uv + dir * 0.26 * _SampleDist * _BlurStrength);
				
				
				sum /= 11.0;
				float t = saturate(dist * _SampleStrength);
		    	return lerp(color, sum, t);
			}
			
			ENDCG
    	}
	}
} 
