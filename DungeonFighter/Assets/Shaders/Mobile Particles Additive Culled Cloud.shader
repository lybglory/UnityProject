// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge Beta 0.32 
// Shader Forge (c) Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:0.32;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:0,uamb:False,mssp:True,lmpd:False,lprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,hqsc:True,hqlp:False,blpr:2,bsrc:0,bdst:0,culm:2,dpts:2,wrdp:False,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.1568628,fgcg:0.1254902,fgcb:0.5647059,fgca:1,fgde:0.01,fgrn:20,fgrf:100,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:1,x:32719,y:32712|emission-72-OUT,custl-280-OUT;n:type:ShaderForge.SFN_Tex2d,id:5,x:33496,y:32770,ptlb:Diffuse,ptin:_Diffuse,tex:01fc1ca6a147b794dade94cbe8f6f8b3,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:12,x:33931,y:33123,ptlb:Alpha,ptin:_Alpha,tex:4bd48f64699b8fc42aaba68e46caeea6,ntxv:0,isnm:False;n:type:ShaderForge.SFN_VertexColor,id:66,x:33992,y:32670;n:type:ShaderForge.SFN_Multiply,id:72,x:33162,y:32692|A-66-RGB,B-5-RGB,C-273-OUT,D-5-A;n:type:ShaderForge.SFN_Step,id:189,x:33710,y:32964|A-66-A,B-12-R;n:type:ShaderForge.SFN_OneMinus,id:273,x:33481,y:32947|IN-189-OUT;n:type:ShaderForge.SFN_Multiply,id:280,x:33012,y:32970|A-72-OUT,B-281-OUT;n:type:ShaderForge.SFN_ValueProperty,id:281,x:33310,y:33155,ptlb:light-power,ptin:_lightpower,glob:False,v1:1;proporder:5-12-281;pass:END;sub:END;*/

Shader "Mobile/Particles/Alpha AdddCulled Cloud" {
    Properties {
        _Diffuse ("Diffuse", 2D) = "white" {}
        _Alpha ("Alpha", 2D) = "white" {}
        _lightpower ("light-power", Float ) = 1
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
            Blend One One
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
            uniform sampler2D _Diffuse; uniform float4 _Diffuse_ST;
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
                float2 node_2311 = i.uv0;
                float4 node_5 = tex2D(_Diffuse,TRANSFORM_TEX(node_2311.rg, _Diffuse));
                float3 node_72 = (node_66.rgb*node_5.rgb*(1.0 - step(node_66.a,tex2D(_Alpha,TRANSFORM_TEX(node_2311.rg, _Alpha)).r))*node_5.a);
                float3 emissive = node_72;
                float3 finalColor = emissive + (node_72*_lightpower);
/// Final Color:
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
