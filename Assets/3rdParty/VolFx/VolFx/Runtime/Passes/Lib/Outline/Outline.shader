//  OutlineFx © NullTale - https://x.com/NullTale
Shader "Hidden/Vol/Outline"
{
	SubShader 
	{
		name "Outline"
		
		Tags { "RenderType"="Transparent" "RenderPipeline" = "UniversalPipeline" }
		LOD 200
		
        ZTest Always
        ZWrite Off
        ZClip false
        Cull Off
		
		Pass
		{
            HLSLPROGRAM
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/SurfaceInput.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
            
            #pragma multi_compile_local _LUMA _ALPHA _CHROMA _DEPTH
            #pragma multi_compile_local _SHARP _
            #pragma multi_compile_local _ADAPTIVE _
            #pragma multi_compile_local _SCREEN _
            
			#pragma vertex vert
			#pragma fragment frag
            
            sampler2D _GradientTex;
            
            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);
            
            TEXTURE2D(_CameraDepthTexture);
            SAMPLER(sampler_CameraDepthTexture);

            float4 _Data; // x - thickness, y - sensitive, z - depth space
            float4 _Fill;
            
            #define _Sensitive _Data.y
            #define _Thickness _Data.x
            #define _Depth _Data.z
            
            
            struct vert_in
            {
                float4 pos : POSITION;
                float2 uv  : TEXCOORD0;
            };

            struct frag_in
            {
                float2 uv     : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };
            
            float luma(float3 rgb)
            {
                return dot(rgb, float3(.299, .587, .114));
            }
            
            float luma(float4 rgba)
            {
                return dot(rgba.rgb, float3(.299, .587, .114)) + rgba.a;
            }
            
            float chroma(float3 rgb)
            {
                return max(rgb.r, max(rgb.g, rgb.b));
            }
            
            float chroma(float4 rgba)
            {
                return max(rgba.r, max(rgba.g, rgba.b)) + rgba.a;
            }
                        
            float SampleDepth(float2 uv)
            {
#ifdef _LUMA
				return luma(SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, uv).rgba);
#endif
#ifdef _ALPHA
            	return SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, uv).a;
#endif
#ifdef _CHROMA
				return chroma(SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, uv).rgba);
#endif
#ifdef _DEPTH
                return SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, sampler_CameraDepthTexture, uv);
#endif            	
            }
            
            float sobel(float2 uv) 
            {
                float2 delta = float2(_Thickness, _Thickness);
                
                float hr = 0;
                float vt = 0;
                
                hr += SampleDepth(uv + float2(-1.0, -1.0) * delta) *  1.0;
                hr += SampleDepth(uv + float2( 1.0, -1.0) * delta) * -1.0;
                hr += SampleDepth(uv + float2(-1.0,  0.0) * delta) *  2.0;
                hr += SampleDepth(uv + float2( 1.0,  0.0) * delta) * -2.0;
                hr += SampleDepth(uv + float2(-1.0,  1.0) * delta) *  1.0;
                hr += SampleDepth(uv + float2( 1.0,  1.0) * delta) * -1.0;
                
                vt += SampleDepth(uv + float2(-1.0, -1.0) * delta) *  1.0;
                vt += SampleDepth(uv + float2( 0.0, -1.0) * delta) *  2.0;
                vt += SampleDepth(uv + float2( 1.0, -1.0) * delta) *  1.0;
                vt += SampleDepth(uv + float2(-1.0,  1.0) * delta) * -1.0;
                vt += SampleDepth(uv + float2( 0.0,  1.0) * delta) * -2.0;
                vt += SampleDepth(uv + float2( 1.0,  1.0) * delta) * -1.0;
                
                return sqrt(hr * hr + vt * vt);
            }
            
            float sobel(float2 uv, float mul) 
            {
                float2 delta = float2(_Thickness, _Thickness) * mul;
                
                float hr = 0;
                float vt = 0;
                
                hr += SampleDepth(uv + float2(-1.0, -1.0) * delta) *  1.0;
                hr += SampleDepth(uv + float2( 1.0, -1.0) * delta) * -1.0;
                hr += SampleDepth(uv + float2(-1.0,  0.0) * delta) *  2.0;
                hr += SampleDepth(uv + float2( 1.0,  0.0) * delta) * -2.0;
                hr += SampleDepth(uv + float2(-1.0,  1.0) * delta) *  1.0;
                hr += SampleDepth(uv + float2( 1.0,  1.0) * delta) * -1.0;
                
                vt += SampleDepth(uv + float2(-1.0, -1.0) * delta) *  1.0;
                vt += SampleDepth(uv + float2( 0.0, -1.0) * delta) *  2.0;
                vt += SampleDepth(uv + float2( 1.0, -1.0) * delta) *  1.0;
                vt += SampleDepth(uv + float2(-1.0,  1.0) * delta) * -1.0;
                vt += SampleDepth(uv + float2( 0.0,  1.0) * delta) * -2.0;
                vt += SampleDepth(uv + float2( 1.0,  1.0) * delta) * -1.0;
                
                return sqrt(hr * hr + vt * vt);
            }
            
            frag_in vert(vert_in input)
            {
                frag_in output;
                output.vertex = input.pos;
                output.uv     = input.uv;
                
                return output;
            }
            
            half4 frag(frag_in input) : SV_Target 
            {
                UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input);

                half4 col = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, input.uv);
                //float s   = pow(1 - saturate(sobel(input.uv, 1 - luma(col))), _Sensitive);
            	
#ifdef _ADAPTIVE
	#ifdef _LUMA
            	float s = pow(1 - saturate(sobel(input.uv, luma(col))), _Sensitive);
	#elif  _ALPHA
            	float s = pow(1 - saturate(sobel(input.uv, col.a)), _Sensitive);
	#elif  _CHROMA
            	float s = pow(1 - saturate(sobel(input.uv, chroma(col))), _Sensitive);
	#elif  _DEPTH
                float s = pow(1 - saturate(sobel(input.uv, SampleDepth(input.uv) * _Depth)), _Sensitive);
	#endif
#else
            	float s = (pow(1 - saturate(sobel(input.uv)), _Sensitive));
#endif            	

#ifdef _SCREEN
            	half4 outline = tex2D(_GradientTex, input.uv.yx);
#else
            	half4 outline = tex2D(_GradientTex, float2(1.03 - s, 0));
#endif
            	
#ifdef _SHARP
#ifdef _DEPTH
            	half l = outline.a * (1 - floor(s + .03));
#else
            	half l = outline.a * (1 - round(s + .03));
#endif
#else
            	half l = outline.a * (1 - s);
#endif
            	
            	//half4 fill = tex2D(_FillTex, input.uv.yx);
            	col.rgb = lerp(lerp(col.rgb, _Fill.rgb, _Fill.a), outline.rgb, l);
            	col.a = saturate(col.a + l);
            	
            	return col;
            }
			
			ENDHLSL
		}
	}
}
