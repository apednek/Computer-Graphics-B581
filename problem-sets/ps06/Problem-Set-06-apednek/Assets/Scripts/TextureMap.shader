Shader "Custom/TextureMap"
{
    Properties
    {
        _MainTex("BaseMap", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100
        Pass
        {
            Cull Off

            CGPROGRAM
            
            #pragma vertex vertexFunc
            #pragma fragment fragmentFunc
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 pos : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vertexFunc(appdata IN) 
            {
                v2f o;
                float4 worldPos = mul(unity_ObjectToWorld, IN.vertex);
                worldPos.y += 10 * _Time;
                o.pos=UnityObjectToClipPos(IN.vertex);
                o.uv = IN.uv;
                UNITY_TRANSFER_FOG(o, o.vertex);
                return o;
            }

            fixed4 fragmentFunc(v2f IN) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, IN.uv);
                UNITY_APPLY_FOG(IN.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
