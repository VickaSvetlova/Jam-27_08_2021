// Upgrade NOTE: replaced '_Projector' with 'unity_Projector'
// Upgrade NOTE: replaced '_ProjectorClip' with 'unity_ProjectorClip'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/ProjectorCircleShader" {
	Properties{
		[HDR]
		_Color("Tint Color", Color) = (1,1,1,1)
		_Radius("Radius", float) = 1
		_Thickness("Thickness", Range(0, 1)) = 1
	}
		Subshader{
			Tags {"Queue" = "Transparent"}
			Pass {
				ZWrite Off
				ColorMask RGB
				//Blend SrcAlpha One // Additive blending
				Blend SrcAlpha OneMinusSrcAlpha // Additive blending
				Offset -1, -1

				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#include "UnityCG.cginc"

				struct v2f {
					float4 uvShadow : TEXCOORD0;
					float4 pos : SV_POSITION;
				};

				float4x4 unity_Projector;
				float4x4 unity_ProjectorClip;

				v2f vert(float4 vertex : POSITION)
				{
					v2f o;
					o.pos = UnityObjectToClipPos(vertex);
					o.uvShadow = mul(unity_Projector, vertex);
					return o;
				}

				fixed4 _Color;
				float _Radius;
				float _Thickness;

				fixed4 frag(v2f i) : SV_Target
				{
					fixed4 c;
					float dis = sqrt(pow((0.5 - i.uvShadow.x), 2) + pow((0.5 - i.uvShadow.y), 2)) * _Radius;
					if (dis > 0.5 || dis < 0.5 - _Thickness * 0.5) {
						discard;
					} else {
						c = _Color;
					}
					return c;

				}
				ENDCG
			}
	}
}