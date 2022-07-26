Shader "Unlit/Warp"
{
    Properties
    {
        _Speed ("Speed", float) = 1.0
        _Color1 ("Color1",Color) = (1,1,1,1)
        _Color2 ("Color2",Color) = (1,0,0,1)
        _Edge ("Edge", Range(0,1)) = 0.5
        _Frequency ("Frequency",int) = 30
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

            float _Speed;
            float4 _Color1;
            float4 _Color2;
            float _Edge;
            int _Frequency;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv =  v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
	            float d = distance(i.uv,0.5);
	            d = abs(sin(d * _Frequency + _Time.y*_Speed));
                return lerp(_Color1,_Color2,step(d, _Edge));
            }
            ENDCG
        }
    }
}
