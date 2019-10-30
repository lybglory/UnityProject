// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Mobile/Particles/Alpha BlendedColored" {
	Properties
	{
		_TintColor ("Tint Color", Color) = (0.5,0.5,0.5,0.5)
		_MainTex ("Base (RGB)", 2D) = "black" {}
		_MainTex_Alpha ("Trans (A)", 2D) = "white" {}
		_UseSecondAlpha("Is Use Second Alpha", int) = 0
		_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
	}
	
	SubShader
	{
		LOD 100
	Tags { "Queue"="Overlay" "IgnoreProjector"="True" "RenderType"="Transparent" }
	Blend SrcAlpha OneMinusSrcAlpha
	Cull Off Lighting Off ZWrite Off Fog { Color (0,0,0,0)}

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
					fixed4 color : COLOR;
				};
	
				struct v2f
				{
					float4 vertex : SV_POSITION;
					half2 texcoord : TEXCOORD0;
					fixed4 color : COLOR;
				};
	
				uniform sampler2D _MainTex;
			    uniform sampler2D _MainTex_Alpha;
			    uniform int _UseSecondAlpha;
				float4 _MainTex_ST;
				fixed4 _TintColor;
				uniform float _Cutoff;

			   fixed4 tex2D_ETC1(sampler2D sa,sampler2D sb,fixed2 v) 
			   {
			 	  fixed4 col = tex2D(sa,v);
			 	  fixed alp = tex2D(sb,v).r;
			 	  col.a = min(col.a,alp) ;
				  //col.rgb *= col.a;
				  return col;
			   }
				v2f vert (appdata_t v)
				{
					v2f o;
					o.vertex = UnityObjectToClipPos(v.vertex);
					o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
					o.color = v.color;
					return o;
				}
				
				fixed4 frag (v2f i) : COLOR
				{
					//fixed4 col = tex2D_ETC1(_MainTex,_MainTex_Alpha,i.texcoord);
				
					//col = col*_TintColor * 2 * i.color 
					//return col;
				
					return tex2D_ETC1(_MainTex,_MainTex_Alpha,i.texcoord)* _TintColor*2  * i.color ;

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
