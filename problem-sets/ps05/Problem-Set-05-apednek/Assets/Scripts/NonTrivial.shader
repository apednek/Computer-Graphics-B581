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
                worldPos.y += 10 * _Time;
                o.pos=UnityObjectToClipPos(IN.vertex);
                return o;
            }

            float4 fragmentFunc(v2f IN) : COLOR
            {
                return _MainColor1;
            }
            ENDCG
        }
    }
}
