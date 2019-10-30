// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Mobile/SelfAlpha" {
	Properties {
		_Color ("Main Color", Color) = (1,1,1,1)
		_MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
		_MainTex_Alpha ("Trans (A)", 2D) = "white" {}
		_UseSecondAlpha("Is Use Second Alpha", int) = 1
	}

	SubShader {
		Tags {"IgnoreProjector"="True" "RenderType"="Transparent" "Queue"="Transparent" "LightMode" = "ForwardBase"}
        Fog { Mode Off }
		LOD 200
		Pass 
		{
			ZWrite Off
			Blend SrcAlpha OneMinusSrcAlpha
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			uniform fixed4 _Color;
			uniform sampler2D _MainTex;
			uniform sampler2D _MainTex_Alpha;
			uniform int _UseSecondAlpha;
			struct v2f {
				float4 pos : SV_POSITION;
				fixed2 uv : TEXCOORD0;
			};
			fixed4 tex2D_ETC1(sampler2D sa,sampler2D sb,fixed2 v) 
			 {
			 	fixed4 col = tex2D(sa,v);
			 	fixed alp = tex2D(sb,v).r;
			 	col.a = min(col.a,alp) ;
				//col.rgb *= col.a;
				return col;
			 }
			v2f vert (appdata_base v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.uv = v.texcoord;
				return o;
			}
			fixed4 frag (v2f i) : COLOR0 
			{ 
				fixed4 c = tex2D_ETC1(_MainTex,_MainTex_Alpha ,i.uv) * _Color;
				return c; 
			}
			ENDCG
		}
	}
}