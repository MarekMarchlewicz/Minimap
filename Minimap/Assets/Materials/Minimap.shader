Shader "Unlit/Minimap"
{
	Properties
	{
		_MainTex ("Depth", 2D) = "white" {}
		_LookupTex ("Lookup", 2D) = "white" {}
		_MaxHeight ("MaxHeight", float) = 1
		_MinHeight ("MaxHeight", float) = 0
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

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
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;

			sampler2D _LookupTex;

			float _MaxHeight;
			float _MinHeight;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{				
				fixed height = tex2D(_MainTex, i.uv);
				fixed normalizedHeight = height - _MinHeight / _MaxHeight - _MinHeight;

				fixed4 col = tex2D(_LookupTex, float2(normalizedHeight, 0.5));
				return col;
			}
			ENDCG
		}
	}
}
