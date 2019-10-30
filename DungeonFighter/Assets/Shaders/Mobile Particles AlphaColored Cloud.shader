// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge Beta 0.32 
// Shader Forge (c) Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:0.32;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:0,uamb:False,mssp:True,lmpd:False,lprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,hqsc:True,hqlp:False,blpr:1,bsrc:3,bdst:7,culm:2,dpts:2,wrdp:False,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.1568628,fgcg:0.1254902,fgcb:0.5647059,fgca:1,fgde:0.01,fgrn:20,fgrf:100,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:1,x:32719,y:32712|emission-72-OUT,custl-263-OUT,alpha-230-OUT;n:type:ShaderForge.SFN_Tex2d,id:5,x:33483,y:32455,ptlb:Diffuse,ptin:_Diffuse,tex:01fc1ca6a147b794dade94cbe8f6f8b3,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:12,x:33946,y:32852,ptlb:Alpha,ptin:_Alpha,tex:8abef1feb634e5649939b80da6764494,ntxv:0,isnm:False;n:type:ShaderForge.SFN_VertexColor,id:66,x:34266,y:32604;n:type:ShaderForge.SFN_Multiply,id:67,x:33135,y:32821|A-189-OUT,B-66-A,C-5-A,D-162-OUT;n:type:ShaderForge.SFN_Multiply,id:72,x:33203,y:32370|A-66-RGB,B-5-RGB,C-5-A;n:type:ShaderForge.SFN_Vector1,id:162,x:33457,y:33069,v1:3;n:type:ShaderForge.SFN_Step,id:189,x:33466,y:32731|A-205-OUT,B-256-OUT;n:type:ShaderForge.SFN_OneMinus,id:205,x:33864,y:32610|IN-66-A;n:type:ShaderForge.SFN_Clamp01,id:230,x:33072,y:33063|IN-67-OUT;n:type:ShaderForge.SFN_OneMinus,id:256,x:33671,y:32837|IN-12-R;n:type:ShaderForge.SFN_Multiply,id:263,x:32954,y:32516|A-72-OUT,B-264-OUT;n:type:ShaderForge.SFN_ValueProperty,id:264,x:33272,y:32740,ptlb:light-power,ptin:_lightpower,glob:False,v1:1;proporder:5-12-264;pass:END;sub:END;*/

Shader "Mobile/Particles/Alpha BlendedColored Cloud" {
    Properties {
        _MainTex ("Diffuse", 2D) = "white" {}
		_MainTex_Alpha ("Trans (A)", 2D) = "white" {}
		_UseSecondAlpha("Is Use Second Alpha", int) = 0

        _Alpha ("Alpha", 2D) = "white" {}
        _lightpower ("light-power", Float ) = 1
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Overlay"
            "RenderType"="Transparent"
        }
        Pass {
            Name "ForwardBase"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off
            ZWrite Off
            
            Fog {Mode Off}
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma exclude_renderers d3d11 opengl xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
			uniform sampler2D _MainTex_Alpha;
			uniform int _UseSecondAlpha;
            uniform sampler2D _Alpha; uniform float4 _Alpha_ST;
	
            uniform float _lightpower;
            struct VertexInput {
                float4 vertex : POSITION;
                float4 uv0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 uv0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };

			fixed4 tex2D_ETC1(sampler2D sa,sampler2D sb,fixed2 v) 
			 {
			 	fixed4 col = tex2D(sa,v);
			 	fixed alp = tex2D(sb,v).r;
			 	col.a = min(col.a,alp) ;
				//col.rgb *= col.a;
				return col;
			 }
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.uv0;
                o.vertexColor = v.vertexColor;
                o.pos = UnityObjectToClipPos(v.vertex);
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
////// Lighting:
////// Emissive:
                float4 node_66 = i.vertexColor;
                float2 node_279 = i.uv0;
                float4 node_5 = tex2D_ETC1(_MainTex,_MainTex_Alpha,TRANSFORM_TEX(node_279.rg, _MainTex));
                float3 node_72 = (node_66.rgb*node_5.rgb*node_5.a);
                float3 emissive = node_72;
                float3 finalColor = emissive + (node_72*_lightpower);
/// Final Color:
                return fixed4(finalColor,saturate((step((1.0 - node_66.a),(1.0 - tex2D(_Alpha,TRANSFORM_TEX(node_279.rg, _Alpha)).r))*node_66.a*node_5.a*3.0)));
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    //CustomEditor "ShaderForgeMaterialInspector"
}
