// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Mobile/Particles/Alpha BlendedColored Scrolled" {
	Properties
	{
		_TintColor ("Tint Color", Color) = (0.5,0.5,0.5,0.5)
		_MainTex ("Base (RGB)", 2D) = "black" {}
		_MainTex_Alpha ("Trans (A)", 2D) = "white" {}
		_UseSecondAlpha("Is Use Second Alpha", int) = 0
		_ScrolledSpeed("MainTex ScrolledSpeed",float)=3
		_Tex2Color("Tex2Color",Color) = (1,1,1,1)
		_Tex2("Tex2",2D) = "black"{}
		_Tex2_Alpha ("Trans (A)", 2D) = "white" {}
		_Power("Power",range(0,1)) = 0.8
		_Tex2Power("Tex2Power",range(0,2)) =0.35
		_Tex2ScrollSpeed("Tex2ScrollSpeed",float) = 0.5
		_frequency("Tex2 frequency",range(0,40)) = 15
		
	}
	
	SubShader
	{
		LOD 100

	Tags { "Queue"="overlay" "IgnoreProjector"="True" "RenderType"="Transparent" }
	Blend SrcAlpha OneMinusSrcAlpha
	AlphaTest Greater .01
	ColorMask RGB
	Cull off Lighting Off ZWrite Off Fog { Mode Off }

		Pass
		{
			CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				
				#include "UnityCG.cginc"
	
				struct appdata_t
				{
					float4 vertex : POSITION;
					float2 texcoord : TEXCOORD0;
					float2 texcoord1 : TEXCOORD1;
					fixed4 color : COLOR;
				};
	
				struct v2f
				{
					float4 vertex : SV_POSITION;
					half2 texcoord : TEXCOORD0;
					half2 texcoord1 : TEXCOORD1;
					fixed4 color : COLOR;
				};
	
				uniform sampler2D _MainTex;
			    uniform sampler2D _MainTex_Alpha;
			    uniform int _UseSecondAlpha;
				sampler2D _Tex2;
				sampler2D _Tex2_Alpha;
				float4 _MainTex_ST;
				float4 _Tex2_ST;
				fixed4 _TintColor,_Tex2Color;
				half _Power,_Tex2Power,_Tex2ScrollSpeed,_ScrolledSpeed,_frequency;

			    fixed4 tex2D_ETC1(sampler2D sa,sampler2D sb,fixed2 v) 
			   {
			 	  fixed4 col = tex2D(sa,v);
			 	  fixed alp = tex2D(sb,v).r;
			 	  col.a = min(col.a,alp) ;
				 // col.rgb *= col.a;
				  return col;
			   }

				v2f vert (appdata_t v)
				{
					v2f o;
					o.vertex = UnityObjectToClipPos(v.vertex);
					o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
					o.texcoord1 = TRANSFORM_TEX(v.texcoord1, _Tex2);
					
					o.color = v.color;
					return o;
				}
				
				fixed4 frag (v2f i) : COLOR
				{
				    i.texcoord += float2(0 , _Time.x*_ScrolledSpeed);
				    
				    i.texcoord1 += float2(0, _Time.x*_Tex2ScrollSpeed);
				    fixed4 t = tex2D_ETC1(_Tex2,_Tex2_Alpha ,i.texcoord1) * _Tex2Color;
				    t*= half(sin(_Time.x*_frequency)*0.5 +0.5)*_Tex2Power *i.color;
					fixed4 col = tex2D_ETC1(_MainTex,_MainTex_Alpha, i.texcoord)  * i.color * _TintColor * 2;
					//col.a =  sin(_Time.x*20)*0.5 +0.5;
					return (col + t) *_Power;
				}
			ENDCG
		}
}
/*
	SubShader
	{
		LOD 100

		Tags
		{
			"Queue" = "Transparent"
			"IgnoreProjector" = "True"
			"RenderType" = "Transparent"
		}
		
		Pass
		{
			Cull Off
			Lighting Off
			ZWrite Off
			Fog { Mode Off }
			Offset -1, -1
			ColorMask RGB
			AlphaTest Greater .01
			Blend SrcAlpha OneMinusSrcAlpha
			ColorMaterial AmbientAndDiffuse
			
			SetTexture [_MainTex]
			{
				Combine Texture * Primary
			}
		}
	}
	*/
}