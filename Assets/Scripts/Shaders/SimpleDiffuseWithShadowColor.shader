Shader "Custom/SimpleDiffuseWithShadowColor"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1,1,1,1)
        _ShadowColor ("Shadow Color", Color) = (0.5,0.5,0.5,1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf SimpleDiffuse

        // Используем стандартный SurfaceOutput (без specular)
        struct Input
        {
            float2 uv_MainTex;
        };

        sampler2D _MainTex;
        fixed4 _Color;
        fixed4 _ShadowColor;

        // Пользовательская модель освещения
        half4 LightingSimpleDiffuse (SurfaceOutput s, half3 lightDir, half atten)
        {
            // Диффузная составляющая (Lambert)
            half NdotL = max(0, dot(s.Normal, lightDir));
            half4 litColor = half4(s.Albedo * _LightColor0.rgb * NdotL, s.Alpha);

            // Цвет в тени – перемножаем с альбедо, чтобы сохранить текстуру
            half4 shadowColor = half4(_ShadowColor.rgb, s.Alpha);

            // Смешиваем lit и shadow в зависимости от аттенюации (освещённость + тени)
            // atten = 1 – полностью освещён, atten = 0 – полная тень
            half4 finalColor = litColor * atten + shadowColor * (1 - atten);

            // Добавляем ambient освещение (оно влияет и на lit, и на shadow)
            finalColor.rgb += s.Albedo * UNITY_LIGHTMODEL_AMBIENT.rgb;

            return finalColor;
        }

        void surf (Input IN, inout SurfaceOutput o)
        {
            fixed4 tex = tex2D(_MainTex, IN.uv_MainTex);
            o.Albedo = tex.rgb * _Color.rgb;
            o.Alpha = tex.a * _Color.a;


        }
        ENDCG
    }
    FallBack "Diffuse"
}