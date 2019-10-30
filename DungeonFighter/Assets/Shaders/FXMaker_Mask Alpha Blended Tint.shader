// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

//Shader "FXMaker/Mask Alpha Blended Tint" {
//	Properties {
//		_TintColor ("Tint Color", Color) = (0.5,0.5,0.5,0.5)
//		_MainTex ("Particle Texture", 2D) = "white" {}
//		_Mask ("Mask", 2D) = "white" {}
//	}

//	Category {
//		Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
//		Blend SrcAlpha OneMinusSrcAlpha
//// 		AlphaTest Greater .01
//// 		ColorMask RGB
//		Cull Off Lighting Off ZWrite Off Fog { Color (0,0,0,0) }
//		BindChannels {
//			Bind "Color", color
//			Bind "Vertex", vertex
//			Bind "TexCoord", texcoord
//		}
		
//		// ---- Dual texture cards
//		SubShader {
//			Pass {
//				SetTexture [_MainTex] {
//					constantColor [_TintColor]
//					combine constant * primary
//				}
//	 			SetTexture [_Mask] {combine texture * previous}
//				SetTexture [_MainTex] {
//					combine texture * previous DOUBLE
//				}
//			}
//		}
		
//		// ---- Single texture cards (does not do color tint)
//		SubShader {
//			Pass {
//				SetTexture [_Mask] {combine texture * primary}
////				SetTexture [_Mask] {combine texture DOUBLE}
//				SetTexture [_MainTex] {
//					combine texture * previous
//				}
//			}
//		}
//	}
//}
Shader "FXMaker/Mask Alpha Blended Tint" {
    Properties {
        _TintColor ("Tint Color", Color) = (0.5,0.5,0.5,0.5)
        _MainTex ("Particle Texture", 2D) = "white" {}
		_MainTex_Alpha ("Trans (A)", 2D) = "white" {}
		_UseSecondAlpha("Is Use Second Alpha", int) = 0
        _Mask ("Mask", 2D) = "white" {}
		//_Mask_A ("Mask (A)", 2D) = "white" {}
		_SpeedX("SpeedX",float)=0
		_SpeedY("SpeedY",float)=1
		_OffsetX("OffsetX",float)=0
		_OffsetY("OffsetY",float)=-4
    }

    SubShader {
        Tags { "Queue"="Overlay" "IgnoreProjector"="True" "RenderType"="Transparent" }
	
		LOD 200
		Pass
		{
		    Cull Off
			Lighting Off
			ZWrite Off
			Fog{Color(0,0,0,0)}
			//Offset -1 ,-1
            Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag			
			#include "UnityCG.cginc"

			uniform sampler2D _MainTex;
			uniform float4 _MainTex_ST;
			uniform sampler2D _MainTex_Alpha;
			uniform sampler2D _Mask;
			//uniform sampler2D _Mask_A;

			uniform float4 _TintColor;
			uniform float _SpeedX;
			uniform float _SpeedY;
			uniform float _OffsetX;
			uniform float _OffsetY;

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
				half2 texcoord2 : TEXCOORD1;
				fixed4 color : COLOR;
			};

			v2f o;
			fixed4 tex2D_ETC1(sampler2D sa,sampler2D sb,fixed2 v) 
			 {
			 	fixed4 col = tex2D(sa,v) ;
			 	fixed alp = tex2D(sb,v).r;
			 	col.a = min(col.a,alp) ;
				return col;
			 }

			v2f vert (appdata_t v)

			{
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.texcoord = v.texcoord;
				o.texcoord2 = v.texcoord;
				o.texcoord += float2(_OffsetX*_SpeedX,_OffsetY*_SpeedY)*_Time.x;
				o.color = v.color;
				return o;
			}

			fixed4 frag (v2f IN) : COLOR
			{

			    fixed4 c = tex2D_ETC1(_MainTex,_MainTex_Alpha,IN.texcoord);
				fixed4 mask =  tex2D(_Mask,IN.texcoord2);
				IN.color *= mask.r;
			    fixed4 final = c * _TintColor*IN.color*2;
				return final;
			}
			ENDCG
		}
	}
}