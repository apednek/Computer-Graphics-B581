Shader "Custom/Terrain"
{
    Properties
    {
        _MainColor1("Color1", Color) = (0, 1, 0)
        _MainColor2("Color2", Color) = (1, 1, 0)
    }
    SubShader
    {
        Pass
        {
            Cull Off

            CGPROGRAM
            
            #pragma vertex vertexFunc
            #pragma fragment fragmentFunc


            #include "UnityCG.cginc"
            fixed4 _MainColor1;

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
            };

            v2f vertexFunc(appdata IN) 
            {
                v2f o;
                float4 worldPos = mul(unity_ObjectToWorld, IN.vertex);
                // IN.vertex -= float4(0,20.5,0,0);
                // //float drop = (sin(30) + sin(worldPos.x));
                float drop = (cos(worldPos.x));
                worldPos.y = worldPos.y + drop * 2;
                // o.pos = mul(UNITY_MATRIX_VP, worldPos);
                // return o;
                // IN.vertex -= float4(1.5,0,0,0);
                // o.pos=UnityObjectToClipPos(IN.vertex);
                o.pos=mul(UNITY_MATRIX_VP, worldPos);
                return o;
            }

            float4 fragmentFunc(v2f IN) : COLOR
            {
                return _MainColor1;
            }
            ENDCG
        }

    Pass
        {
            Cull Off

            CGPROGRAM
            
            #pragma vertex vertexFunc
            #pragma fragment fragmentFunc

            fixed4 _MainColor2;

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
            };

            v2f vertexFunc(appdata IN) 
            {
                v2f o;
                float4 worldPos = mul(unity_ObjectToWorld, IN.vertex);
                // IN.vertex += float4(0,20.5,0,0);
                // //float drop = (sin(30) + sin(worldPos.x));
                float drop = (-cos(worldPos.x));
                worldPos.y = worldPos.y + drop * 2;
                // o.pos = mul(UNITY_MATRIX_VP, worldPos);
                // return o;
                // IN.vertex -= float4(1.5,0,0,0);
                // o.pos=UnityObjectToClipPos(IN.vertex);
                o.pos=mul(UNITY_MATRIX_VP, worldPos);
                return o;
            }

            float4 fragmentFunc(v2f IN) : COLOR
            {
                return _MainColor2;
            }
            ENDCG
        }
    }
}
// float drop = 1 * exp(-(((worldPos.x - 0.1) * (worldPos.x - 0.1)) + ((worldPos.y - 0.2) * (worldPos.y - 0.2)) / 9)) + exp(-(((worldPos.x + 0.1) * (worldPos.x + 0.1)) + ((worldPos.y + 0.3) * (worldPos.y + 0.3)) / 1));