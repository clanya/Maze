Shader "Unlit/Warp01"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Speed("Speed",float) = 1
        _ShearX("ShearX",Range(0,2)) = 1
        _ShearY("ShearY",Range(0,1)) = 1
        _Center("Center",float) = 0.5
        _RadialScale("RadialScale",float) = 1
        _LengthScale("LengthScale",float) = 1
        _SampleCount ("Sample Count", int) = 5
        _RotateIntensity ("Rotate Intensity", float) = 0.05
        _RadialIntensity("Radial Intensity", Range(0.0, 1.0)) = 0.5
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
            #define PI 3.1415926

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
            float _Speed;
            float _ShearX;
            float _ShearY;
            float _Center;
            float _RadialScale;
            float _LengthScale;
            int _SampleCount;
            float _RotateIntensity;
            float _RadialIntensity;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            

            fixed4 frag (v2f i) : SV_Target
            {
                half angleRad = _RotateIntensity / _SampleCount;
                half3 rgb = tex2D(_MainTex, i.uv).rgb;
                // UVを-0.5～0.5に変換
                half2 symmetryUv = i.uv - 0.5;
                // 外側に行くほどこの値が大きくなる(0～0.707)
                half distance = length(symmetryUv);

                half factor = _RadialIntensity / _SampleCount * distance;
                for(int j = 1; j < _SampleCount; j++)
                {
                    // 回転行列を作る
                    half angle = angleRad * j;
                    half angleCos = cos(angle);
                    half angleSin = sin(angle);
                    half2x2 rotateMatrix = half2x2(angleCos, -angleSin, angleSin, angleCos);
                    // 放射状サンプリングのためのオフセット
                    half uvOffset = 1 - factor * j;

                    float2 uv = mul(symmetryUv * uvOffset, rotateMatrix) + 0.5;
                    
                    rgb += tex2D(_MainTex, uv).rgb;
                }
                rgb /= _SampleCount;
                return fixed4(rgb, 1);
                /*
                float2 position = i.uv - _Center;
                float len = length(position);
                if(len >= _RadialScale)
                {
                    return tex2D(_MainTex,i.uv);
                }
                float uzu = min(max(1.0 - ( len / _RadialScale), 0.0),1.0) * _ShearX;
                float x = position.x * cos(uzu) - position.y * sin(uzu) - distance(_ShearY, 0.5);
                float y = position.y * sin(uzu) + position.y * cos(uzu) - distance(_ShearY, 0.5);
                float2 retPosition = (float2(x,y) + _Center);
                return tex2D(_MainTex,retPosition);
                */
                /*
                float2 uv;
                float2 delta = i.uv - _Center;
                float radius = length(delta) * 2 * _RadialScale;
                float angle = atan2(delta.x, delta.y) * 1.0/6.28 * _LengthScale;    //12.56,6.28
                float2 Out = float2(radius, frac(angle));

                uv.x = lerp(Out.x + _ShearX,Out.x,Out.y);
                uv.y = lerp(Out.y + _ShearY,Out.y,Out.x);
                
                return tex2D(_MainTex,uv);
                */

                /*
                float2 uv;

                float2 delta = i.uv - _Center;
                float radius = length(delta) * 2 * _RadialScale;
                float angle = atan2(delta.x, delta.y) * 1.0/12.56 * _LengthScale;    //12.56,6.28
                float2 Out = float2(radius, frac(angle));
                uv = Out;

                //uv.x = lerp(Out.x + _ShearX,Out.x,Out.y);
                //uv.y = lerp(Out.y + _ShearY,Out.y,Out.x);
                //Out *= uv;

                
                return tex2D(_MainTex,Out);*/
                    
                /*
                float2 uv;
                float r = sqrt(i.uv.x * i.uv.x + i.uv.y * i.uv.y);
                
                uv.x = lerp(i.uv.x + _ShearX,i.uv.x,i.uv.y);
                uv.y = lerp(i.uv.y + _ShearY,i.uv.y,i.uv.x);

                
                float theta = atan2(uv.y, uv.x) * 1 / (2 * PI);
                uv.x = r * cos(theta);
                uv.y = r * sin(theta);
                return tex2D(_MainTex,uv);
                */
                
            }
            ENDCG
        }
    }
}
