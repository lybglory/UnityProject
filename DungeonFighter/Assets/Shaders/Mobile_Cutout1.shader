// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Mobile/Transparent Cutout1" {
	Properties {
		_Color ("Main Color", Color) = (1,1,1,1)
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_MainTex_Alpha ("Trans (A)", 2D) = "white" {}
		_UseSecondAlpha("Is Use Second Alpha", int) = 0
		_SpecColor ("Specular Color", Color) = (1.0,1.0,1.0,1.0)
		_Shininess ("Shininess", Range(0, 50)) = 10
		_SpecCut ("Spec cut", Range(0,1)) = 0.5
		_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
	}

	SubShader {
		Tags {"IgnoreProjector"="True" "Queue"="Transparent" "RenderType"="Opaque" "LightMode" = "ForwardBase"}
        Fog { Mode Off }
		LOD 200
		Pass 
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			uniform fixed4 _Color;
			uniform sampler2D _MainTex;
			uniform sampler2D _MainTex_Alpha;
			uniform int _UseSecondAlpha;
			uniform fixed4 _SpecColor;
			uniform fixed _Shininess;
			uniform fixed _Cutoff;
			uniform fixed _SpecCut;

			struct v2f {
				float4 pos : SV_POSITION;
				fixed2 uv : TEXCOORD0;
				fixed3 normal : TEXCOORD1;
				fixed3 posWorld : TEXCOORD2;
                fixed3 light : TEXCOORD3;
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
				o.normal = v.normal;
				o.posWorld = mul(unity_ObjectToWorld, v.vertex);
				half3 worldN = mul((float3x3)unity_ObjectToWorld, SCALED_NORMAL);
				o.light.rgb = ShadeSH9 (half4(worldN, 1.0)) * 1.35;
				return o;
			}
			fixed4 frag (v2f i) : COLOR0 
			{ 
				fixed4 c = tex2D_ETC1(_MainTex,_MainTex_Alpha, i.uv) *_Color;

				if(c.a <= _Cutoff) 
				{
				clip(-.1f);
				}
				else if(c.a <= _SpecCut)
				//if(c.a <= _SpecCut)
				{
					fixed3 viewDirection = normalize( _WorldSpaceCameraPos.xyz - i.posWorld );
					//fixed3 lightDirection = normalize( _WorldSpaceLightPos0.xyz);
					fixed3 lightDirection = normalize( _WorldSpaceCameraPos.xyz);
					fixed3 normalDirection = normalize( mul( fixed4( i.normal, 0.0), unity_WorldToObject ).xyz );
					fixed3 halfDir = normalize(viewDirection + lightDirection);
					fixed3 specularReflection = _SpecColor.xyz * pow( saturate( dot( reflect(-lightDirection, normalDirection), viewDirection) ), _Shininess) * _Shininess;
					c.rgb *= i.light + specularReflection;
					c.a = 1;
				}
				else
				{
					c.rgb *= i.light;
					c.a = 1;
				}
				return c; 
			}
			ENDCG
		}
	}

Fallback "Transparent/Cutout/VertexLit"
}