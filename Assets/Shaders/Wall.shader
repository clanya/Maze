Shader "Custom/Wall"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color",Color) = (1,1,1,1)
        _Span ("Span",float) = 2.0
        _Speed ("Speed",Range(0,10.00)) = 1.00
        _BorderThickness("Border thickness",Range(0,1.00)) = 0.25
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
            float4 _Color;
            float _Span;
            float _Speed;
            float _BorderThickness;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                //2はuv.x + uv.y (1 + 1)の最大値
                // if (i.uv.x + i.uv.y < 2 - fmod(_Time.y * _Speed - _BorderThickness, _Span * 2) && i.uv.x + i.uv.y > 2 - fmod(_Time.y * _Speed, _Span * 2))
                // {
                    // col = lerp(col, _Color, _Color.a);
                // }
                
                float edge = step( i.uv.x + i.uv.y, 2 - fmod(_Time.y * _Speed - _BorderThickness, _Span * 2));
                float edge2 = step(2 - fmod(_Time.y * _Speed, _Span * 2),i.uv.x + i.uv.y);
                //edgeとedge2が両方とも条件を満たさなければ（両方とも1にならなければ、_MainTexの値を返す）
                col = lerp(col, lerp(col, _Color,_Color.a), max(0, min(edge,edge2)));

                return col;
            }
            ENDCG
        }
    }
}
