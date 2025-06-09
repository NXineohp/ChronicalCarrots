Shader "Custom/SpotlightShader_Blackout"
{
    Properties
    {
        _Color ("Dark Color", Color) = (0, 0, 0, 0.8)

        _Radius1 ("Radius 1", Float) = 0.3
        _Center1 ("Center 1", Vector) = (0.5, 0.5, 0, 0)

        _Radius2 ("Radius 2", Float) = 0.3
        _Center2 ("Center 2", Vector) = (0.5, 0.5, 0, 0)
    }

    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Overlay" }
        LOD 100

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            Cull Off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            fixed4 _Color;

            float _Radius1;
            float4 _Center1;

            float _Radius2;
            float4 _Center2;

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 screenUV = i.vertex.xy / i.vertex.w;
                screenUV = 0.5 * (screenUV + 1.0); // von -1..1 auf 0..1

                float feather = 0.02;

                float dist1 = distance(screenUV, _Center1.xy);
                float dist2 = distance(screenUV, _Center2.xy);

                float light1 = smoothstep(_Radius1, _Radius1 + feather, dist1);
                float light2 = smoothstep(_Radius2, _Radius2 + feather, dist2);

                float mask = max(light1, light2); // additive Wirkung

                return lerp(fixed4(0,0,0,0), _Color, mask);
            }
            ENDCG
        }
    }
}
