Shader "Custom/Test"
{
    Properties
    {
		_MainTex1 ("Main texture 1", 2D) = "white" {}
		_MainTex2 ("Main texture 2", 2D) = "white" {}
		_MainTex3 ("Main texture 3", 2D) = "white" {}
		_MaskTex ("Mask texture", 2D) = "black" {}
		
		_EmissionMaskTex ("Emission mask texture", 2D) = "black" {}
		_Color ("Emission color", Color) = (1,1,1,1)
    }
	
    SubShader
    {
        CGPROGRAM

		#pragma surface surf Lambert
		
		sampler2D _MainTex1, _MainTex2, _MainTex3, _MaskTex, _EmissionMaskTex;
		fixed3 _Color;
		
		struct Input {
			half2 uv_MainTex1;
			half2 uv_MaskTex;
		};
		
		void surf(Input IN, inout SurfaceOutput o){
			fixed3 masks = tex2D(_MaskTex, IN.uv_MaskTex);
			fixed3 clr = tex2D(_MainTex1, IN.uv_MainTex1) * masks.r +
			tex2D(_MainTex2, IN.uv_MainTex1) * masks.g +
			tex2D(_MainTex3, IN.uv_MainTex1) * masks.b; 
			o.Albedo = clr;
			o.Emission = tex2D(_EmissionMaskTex, IN.uv_MainTex1);
		}

		
        ENDCG
    }
	
    FallBack "Diffuse"
}
