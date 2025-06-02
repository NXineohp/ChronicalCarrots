Shader "Custom/SpotlightShader"
{
    Properties
    {
        _Color ("Dark Color", Color) = (0, 0, 0, 0.8)
        _Radius ("Radius", Float) = 0.3
        _Center ("Center", Vector) = (0.5, 0.5, 0, 0)
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
            float _Radius;
            float4 _Center;

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

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float dist = distance(i.uv, _Center.xy);
                float feather = 0.02; // weichere Kante
                float alpha = smoothstep(_Radius, _Radius + feather, dist);
                return lerp(fixed4(0,0,0,0), _Color, alpha);
            }
            ENDCG
        }
    }
}
