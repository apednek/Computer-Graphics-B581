Shader "Custom/Terrain"
{
    Properties
    {
        _MainColor1("Color1", Color) = (0, 0, 1)
        _MainColor2("Color2", Color) = (1, 1, 0)
        _DiffuseColor("Diffuse", Color) = (0, 1, 0)
    }
    SubShader
    {
        Pass
        {
            Cull Off

            CGPROGRAM
            
            #pragma vertex vertexFunc
            #pragma fragment fragmentFunc

            float3 lightPosition;
            float3 cameraPosition;
            float4 color;
            float3 _DiffuseColor;

            #include "UnityCG.cginc"
            fixed4 _MainColor1;

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float4 color : COLOR0;
            };

            v2f vertexFunc(appdata IN) 
            {
                v2f o;
                IN.normal = normalize(IN.normal);
                float4 worldPos = mul(unity_ObjectToWorld, IN.vertex);
                float drop = (cos(worldPos.x));
                worldPos.y = worldPos.y + drop * 2;
                o.pos=mul(UNITY_MATRIX_VP, worldPos);

                float3 lightDirection = normalize(o.pos - lightPosition);
                float diffuse = saturate(dot(IN.normal, lightDirection));
                color = float4(diffuse * float3(0,1,0), 1.0);
                o.color = color;

                return o;
            }

            float4 fragmentFunc(v2f IN) : COLOR
            {
                return IN.color;
            }
            ENDCG
        }

    // Pass
    //     {
    //         Cull Off

    //         CGPROGRAM
            
    //         #pragma vertex vertexFunc
    //         #pragma fragment fragmentFunc

    //         float3 lightPosition;
    //         float3 cameraPosition;
    //         float4 color;
    //         float3 _DiffuseColor;

    //         #include "UnityCG.cginc"
    //         fixed4 _MainColor2;

    //         struct appdata
    //         {
    //             float4 vertex : POSITION;
    //             float3 normal : NORMAL;
    //         };

    //         struct v2f
    //         {
    //             float4 pos : SV_POSITION;
    //             float4 color : COLOR0;
    //         };

    //         v2f vertexFunc(appdata IN) 
    //         {
    //             v2f o;
    //             // float3 albedo = UNITY_ACCESS_INSTANCED_PROP(Props, _DiffuseColor).rgb;
    //             IN.normal = normalize(IN.normal);
    //             float4 worldPos = mul(unity_ObjectToWorld, IN.vertex);
    //             float drop = (-cos(worldPos.x));
    //             worldPos.y = worldPos.y + drop * 2;
    //             o.pos=mul(UNITY_MATRIX_VP, worldPos);

    //             float3 lightDirection = normalize(o.pos - lightPosition);
    //             float diffuse = saturate(dot(IN.normal, lightDirection));
    //             color = float4(diffuse * float3(0,1,0), 1.0);
    //             o.color = color;

    //             return o;
    //         }

    //         float4 fragmentFunc(v2f IN) : COLOR
    //         {
    //             return IN.color + _MainColor2;
    //         }
    //         ENDCG
    //     }
    }
}
// float drop = 1 * exp(-(((worldPos.x - 0.1) * (worldPos.x - 0.1)) + ((worldPos.y - 0.2) * (worldPos.y - 0.2)) / 9)) + exp(-(((worldPos.x + 0.1) * (worldPos.x + 0.1)) + ((worldPos.y + 0.3) * (worldPos.y + 0.3)) / 1));