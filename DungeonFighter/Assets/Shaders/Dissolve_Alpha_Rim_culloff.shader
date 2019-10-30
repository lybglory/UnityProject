// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Leo shader/Dissolve_TexturCoords_Alpha_Rim" {
    Properties {
        _Color ("MainColor (RGB)", Color) = (0.3,0.3,0.3,1)
        _MainTex ("Base (RGB)", 2D) = "white" {}
		_MainTex_Alpha ("Trans (A)", 2D) = "white" {}
		_UseSecondAlpha("Is Use Second Alpha", int) = 0

		_NotVisibleColor ("NotVisibleColor (RGB)", Color) = (0.47,0.76,1,0.65)
		_NotVisiRim ("NotVisi Rim", Range(0.2, 8.0)) = 0.5

        _Amount ("Amount", Range (0, 1)) = 0.5
		_StartAmount("StartAmount", float) = 0.1
		_Illuminate ("Illuminate", Range (0, 1)) = 0.5
		_DissColor ("DissColor", Color) = (1,1,1,1)
		_ColorAnimate ("ColorAnimate", vector) = (1,0,0,0)
		_DissolveSrc ("DissolveSrc", 2D) = "white" {}
		_Cutoff ("Cutoff Value", Range(0,1)) = 0.5 
		_RimColor ("Rim Color", Color) = (0.26,0.19,0.16,0.0)
		_RimPower ("Rim Power", Range(0.5,8.0)) = 1.0
    }
    Subshader 
    {
		Tags {"IgnoreProjector"="True" "Queue"="AlphaTest" "RenderType"="Opaque" "LightMode" = "ForwardBase"}
        Fog { Mode Off }
		/*
        Pass 
		{
			ZTest Greater
			Blend SrcAlpha OneMinusSrcAlpha   
			ZWrite Off

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			uniform sampler2D _MainTex;
			uniform float4 _NotVisibleColor;
			uniform float _NotVisiRim;

			struct v2f {
				float4 pos : SV_POSITION;
				half2 uv : TEXCOORD0;
				fixed4 spec : COLOR;
			};
			  v2f vert (appdata_base v)
			  {
					v2f o;
					o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
					o.uv = v.texcoord;

					half3 viewNormal   = mul((float3x3)UNITY_MATRIX_MV, v.normal);
					half4 viewPos      = mul(UNITY_MATRIX_MV, v.vertex);
					half3 viewDir      = float3(0,0,1);
					half3 viewLightPos = float3(0,0,0);
					half3 dirToLight = viewPos.xyz - viewLightPos;
					half3 h = viewDir + normalize(-dirToLight);
					half rim = 1.0 - saturate(dot(viewNormal, normalize(h)));
					o.spec = pow(rim, _NotVisiRim);

					return o;
			  }
			  fixed4 frag (v2f i) : COLOR0 
			  { 
				  return _NotVisibleColor * i.spec; 
			  }
			  ENDCG
		}
		*/
        Pass 
        {
			//Blend SrcAlpha OneMinusSrcAlpha   
			//ZWrite Off
			CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma fragmentoption ARB_precision_hint_fastest
            #include "UnityCG.cginc"                
                 
            struct v2f 
            { 
                float4   pos : SV_POSITION;
                half2   uv : TEXCOORD0;
                fixed3  light : TEXCOORD1;
				fixed3  emission : TEXCOORD2;
            };
            fixed4 _Color;
            sampler2D _MainTex;
			sampler2D _MainTex_Alpha;
			int _UseSecondAlpha;
			sampler2D _DissolveSrc;
			fixed4 _RimColor;
			half _RimPower;
			fixed4 _DissColor;
			half _Amount;
			static half3 Color = float3(1,1,1);
			half4 _ColorAnimate;
			half _Illuminate;
			half _StartAmount;
			half _Cutoff;

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
				fixed3 worldN = mul((float3x3)unity_ObjectToWorld, SCALED_NORMAL);
				o.light.rgb = ShadeSH9 (float4(worldN, 1.0));
				  
				half3 viewNormal   = mul((float3x3)UNITY_MATRIX_MV, v.normal);
				half4 viewPos      = mul(UNITY_MATRIX_MV, v.vertex);
				half3 viewDir      = half3(0,0,1);
				half3 viewLightPos = half3(0,0,0);
				half3 dirToLight = viewPos.xyz - viewLightPos;
				half3 h = viewDir + normalize(-dirToLight);
				half rim = 1.0 - saturate(dot(viewNormal, normalize(h)));
				o.emission = _RimColor * pow(rim, _RimPower);
				return o;
			}
                 
            fixed4 frag (v2f i) : COLOR
            { 
                fixed4 c = tex2D_ETC1(_MainTex,_MainTex_Alpha,i.uv);

                c.rgb *= i.light;
				c.rgb += i.emission;
				half ClipTex = tex2D (_DissolveSrc, i.uv).r ;
				half ClipAmount = ClipTex - _Amount;
				half Clip = 0;
				if (_Amount > 0)
				{
					if (ClipAmount <0)
					{
						 Clip = 1; 
					}
					else
					{
						 if (ClipAmount < _StartAmount)
						 {
							  if (_ColorAnimate.x == 0)
								   Color.x = _DissColor.x;
							  else
								   Color.x = ClipAmount/_StartAmount;
							  if (_ColorAnimate.y == 0)
								   Color.y = _DissColor.y;
							  else
								   Color.y = ClipAmount/_StartAmount;
							  if (_ColorAnimate.z == 0)
								   Color.z = _DissColor.z;
							  else
								   Color.z = ClipAmount/_StartAmount;
							  c.rgb = (c.rgb * ((Color.x+Color.y + Color.z)) * Color * ((Color.x + Color.y + Color.z)))/(1 - _Illuminate);
						  }
					 }
				 }
				 c.a = c.a * _Color.a;
				 if (Clip == 1 || c.a <= _Cutoff)
				 {
					 clip(-0.1);
				 }
				 else
				 {
				     c.a = 1;
				 }
                return c;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
