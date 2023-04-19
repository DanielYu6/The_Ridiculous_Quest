Shader "Custom/SingleEyeShader"
{

    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _RenderRightEye ("Render to Right Eye", Range(0, 1)) = 1

    }
    SubShader
    {
        Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
        LOD 100
        
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha 
        Pass
        {
            Cull Off
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID

            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float stereoIndex : TEXCOORD1;

                UNITY_VERTEX_OUTPUT_STEREO

            };
            float _RenderRightEye;

            v2f vert (appdata v)
            {
                #if UNITY_SINGLE_PASS_STEREO
                    //if (unity_StereoEyeIndex == _RenderRightEye) discard;
                #endif

                
                v2f o;
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_INITIALIZE_OUTPUT(v2f, o);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
                o.stereoIndex = unity_StereoEyeIndex;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 tex = tex2D(_MainTex, i.uv);
                float4 col = float4(1, 1, 1, 1);
                
                col.a = abs(i.stereoIndex - _RenderRightEye);
                return tex * col;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}