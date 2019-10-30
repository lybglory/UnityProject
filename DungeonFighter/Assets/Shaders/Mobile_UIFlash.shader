// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Mobile/UIFlash" {
	Properties
	{
		_MainTex ("Base (RGB), Alpha (A)", 2D) = "black" {}
	        _FlashColor ("Flash Color", Color) = (0.5,0.5,0.5,1)  
	        _Angle ("Flash Angle", Range(0, 180)) = 45  
	        _Width ("Flash Width", Range(0, 1)) = 0.8  
	        _LoopTime ("Loop Time", Float) = 1  
	        _Interval ("Time Interval", Float) = 3  
	}
	
	SubShader
	{
		LOD 200

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
			Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag			
			#include "UnityCG.cginc"

			sampler2D _MainTex;
			//float4 _MainTex_ST;
	        fixed4 _FlashColor;  
	        fixed _Angle;  
	        fixed _Width;  
	        fixed _LoopTime;  
	        fixed _Interval;  

			struct appdata_t
			{
				float4 vertex : POSITION;
				half2 texcoord : TEXCOORD0;
				fixed4 color : COLOR;
			};
	
			struct v2f
			{
				float4 vertex : SV_POSITION;
				half2 texcoord : TEXCOORD0;
				fixed4 color : COLOR;
			};
		    float inFlash(half2 uv)  
	        {     
	            fixed brightness = 0;  
	            fixed angleInRad = 0.0174444 * _Angle;  
	            fixed tanInverseInRad = 1.0 / tan(angleInRad);    
	            fixed currentTime = _Time.y;  
	            fixed totalTime = _Interval + _LoopTime;  
	            fixed currentTurnStartTime = (short)((currentTime / totalTime)) * totalTime;  
	            fixed currentTurnTimePassed = currentTime - currentTurnStartTime - _Interval;  
	            bool onLeft = (tanInverseInRad > 0);  
	            fixed xBottomFarLeft = onLeft? 0.0 : tanInverseInRad;  
	            fixed xBottomFarRight = onLeft? (1.0 + tanInverseInRad + tanInverseInRad) : 1.0;  
	            fixed percent = currentTurnTimePassed / _LoopTime;  
	            fixed xBottomRightBound = xBottomFarLeft + percent * (xBottomFarRight - xBottomFarLeft);  
	            fixed xBottomLeftBound = xBottomRightBound - _Width;  
	            fixed xProj = uv.x + uv.y * tanInverseInRad;  
	            if(xProj > xBottomLeftBound && xProj < xBottomRightBound)  
	            {  
	                brightness = 1.0 - abs(2.0 * xProj - (xBottomLeftBound + xBottomRightBound)) / _Width;  
	            }  
	            return brightness;  
	        }  
			v2f o;
			v2f vert (appdata_t v)
			{
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.texcoord = v.texcoord;
				o.color = v.color;
				return o;
			}
			fixed4 frag (v2f IN) : COLOR
			{
				fixed4 col = tex2D(_MainTex, IN.texcoord) * IN.color;
				fixed brightness = inFlash(IN.texcoord);  
				col.rgb = col.rgb + _FlashColor.rgb * brightness;  
				return col;
			}
			ENDCG
		}
	}

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
			Blend SrcAlpha OneMinusSrcAlpha
			ColorMaterial AmbientAndDiffuse
			
			SetTexture [_MainTex]
			{
				Combine Texture * Primary
			}
		}
	}
}
