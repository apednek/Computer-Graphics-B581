Shader "Custom/NonTrivial"
{
    Properties
    {
        _MainColor1("Color1", Color) = (0, 1, 0)
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
            int lightStatus;

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
                // float4 color : COLOR0;
                float3 normal : NORMAL;
                float3 worldPos : TEXCOORD0;
            };

            v2f vertexFunc(appdata IN) 
            {
                v2f o;
                float4 worldPos = mul(unity_ObjectToWorld, IN.vertex);
                worldPos.y += 10 * _Time;
                o.pos=UnityObjectToClipPos(IN.vertex);
                o.worldPos = worldPos;
                o.normal = normalize(IN.normal);

                // float3 lightDirection = normalize(o.pos - lightPosition);
                // float diffuse = saturate(dot(IN.normal, lightDirection));
                // float3 cameraDirection = normalize(o.pos - cameraPosition);
                // float3 reflective = reflect(lightDirection, IN.normal);
                // float specular = max(dot(cameraDirection, reflective), 0) * 2;
                // color = (float4( float3(1, 0, 0) + diffuse * float3(0,1,0) + float3(1, 1, 0) * specular, 1.0) );
                // o.color = color;

                return o;
            }

            float4 fragmentFunc(v2f IN) : COLOR
            {
                if(lightStatus == 1) {
                    IN.normal = normalize(IN.normal);
                    float3 lightDirection = normalize(lightPosition - IN.worldPos);
                    float diffuse = saturate(dot(IN.normal, lightDirection));
                    float3 cameraDirection = normalize(cameraPosition - IN.worldPos);
                    float3 reflective = reflect(-lightDirection, IN.normal);
                    float specular = saturate(dot(cameraDirection, reflective)) * 0.1;
                    color = float4(0, 0, 0, 1) + diffuse * float4(1, 1, 0, 1) + float4(1, 1, 0, 1) * specular;
                }
                else {
                    color = float4(0, 0, 0, 1);
                }

                return color;
            }
            ENDCG
        }
    }
}
