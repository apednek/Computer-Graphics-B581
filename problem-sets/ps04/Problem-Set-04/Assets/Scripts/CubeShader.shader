Shader "CubeShader"
{
    // Properties
    // {
    //     _MainColor("Color", Color) = (0, 1, 0)
    // }
    SubShader
    {

        // Pass {
        //     CGPROGRAM
            
        //     #pragma vertex vertx
                
        //     #include "UnityCG.cginc"
        //     struct appdata {
        //         float4 vertex : POSITION;
        //     };
        //     struct v2f {
        //         float4 vertex : SV_POSITION;
        //         float3 color : COLOR;
        //     };

        //     float4x4 _ModelingMatrix;

        //     v2f vert (appdata v) {
                
        //         // the output to this shader is:
        //         v2f o;
        //         float4 worldPosition = v.vertex;
        //         o.vertex = mul(worldPosition, _ModelingMatrix);
        //         o.color = float3(0, 0, 0);
        //         return o;
        //     } // end of vert shader

        //     fixed4 Color;

        //     ENDCG
        // }

        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface ConfigureSurface Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float3 worldPos;
        };

        float _Smoothness;

        void ConfigureSurface (Input input, inout SurfaceOutputStandard surface) {
            surface.Albedo.rb = saturate(input.worldPos.xy * 0.5 + 0.5);
        }

        ENDCG
    }
}
