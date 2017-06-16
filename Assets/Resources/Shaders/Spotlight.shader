﻿Shader "Custom/Planet" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Texture", any) = "" {}
	}

	SubShader {

		Tags { "ForceSupported" = "True" "RenderType"="Overlay" } 
		
		Lighting Off 
		Blend SrcAlpha OneMinusSrcAlpha 
		Cull Off 
		ZWrite Off 
		Fog { Mode Off } 
		ZTest Always 
		
		Pass {	
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest

			#include "UnityCG.cginc"

			struct appdata_t {
				float4 vertex : POSITION;
				float4 color : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f {
				float4 vertex : POSITION;
				float4 color : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			sampler2D _MainTex;

			uniform float4 _MainTex_ST;
			
			v2f vert (appdata_t v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.color = v.color;
				o.texcoord = TRANSFORM_TEX(v.texcoord,_MainTex);
				return o;
			}

			float4 frag (v2f i) : COLOR
			{
				const float pi = 3.14159;
				half x,y,r;
				float4 color;
				//color.r = i.color.r;
				//color.b = i.color.b;
				//color.g = i.color.g;
				//color.a = color.a * i.color.a;
				x = (6.0) * (i.texcoord.x - 0.5f + 0.25f * sin(_Time.y));
				y = (6.0) * (i.texcoord.y - 0.5f + 0.25f * cos(_Time.y));
				r = sqrt(x * x + y * y);
				if (r <= 1.0)
				{
					color = tex2D(_MainTex, i.texcoord);
					return color;
				}
				return half4(0.0,0.0,0.0,0.0f);
			}
			ENDCG 
		}
	} 	
}