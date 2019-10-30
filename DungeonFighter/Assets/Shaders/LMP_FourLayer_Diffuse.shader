Shader "PengLu/terrain/FourLayer_Diffuse" {
Properties {
		 _Color ("Main Color", Color) = (1,1,1,1)
		_RTexture("RTexture",2D)=""{}
		_GTexture("GTexture",2D)=""{}
		_BTexture("BTexture",2D)=""{}
		_BaseTexture("BaseTexture",2D)=""{}
	}
	SubShader {
		Tags { "RenderType"="Opaque" "Queue"="Transparent"}
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Lambert vertex:vert
#pragma target 4.0

		sampler2D _RTexture;
		sampler2D _GTexture;
		sampler2D _BTexture;
		sampler2D _BaseTexture;
		fixed4 _Color;

		struct Input 
		{
			fixed4 vertColor;
			fixed2 uv_BaseTexture;
			fixed2 uv_RTexture;
			fixed2 uv_GTexture;
			fixed2 uv_BTexture;
		};	
		
		void vert (inout appdata_full v, out Input o)
		{
			UNITY_INITIALIZE_OUTPUT(Input, o);
			o.vertColor = v.color;
			//o.uv_BaseTexture = v.texcoord;
		}

		void surf (Input IN, inout SurfaceOutput o) {
			fixed4 finalColor;
			finalColor = tex2D(_RTexture, IN.uv_RTexture) * IN.vertColor.r;
			finalColor += tex2D(_GTexture, IN.uv_GTexture) * IN.vertColor.g;
			finalColor += tex2D(_BTexture, IN.uv_BTexture) * IN.vertColor.b;
			finalColor += tex2D(_BaseTexture,IN.uv_BaseTexture) * (1-(IN.vertColor.r + IN.vertColor.g + IN.vertColor.b));
			o.Albedo = finalColor*_Color;
			o.Alpha = 1.0;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
