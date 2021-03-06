﻿Shader "Unlit/MinimapLines"
{
	Properties
	{
		_MainTex ("Depth", 2D) = "white" {}
		_LookupTex ("Lookup", 2D) = "white" {}
		_HeightStretch ("MaxHeight", float) = 1
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Cull Off ZWrite Off ZTest Always

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv_depth : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			sampler2D_float _CameraDepthTexture;

			sampler2D _LookupTex;

			float _HeightStretch;
			float _CameraDistance;
			float4 _WSCameraPos;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv_depth = v.uv.xy;

				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				float height = 1 - DecodeFloatRG(tex2D(_MainTex, i.uv_depth));
				height = _WSCameraPos.y - height * _CameraDistance;
								
				fixed4 col = tex2D(_LookupTex, float2(height / _HeightStretch, 0.5));
				return col;
			}
			ENDCG
		}
	}
}
