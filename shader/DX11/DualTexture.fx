// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

//TODO: dummy implementation / placeholder

uniform extern float4x4 MatrixTransform;

Texture2D<float4> Texture : register(t0);
   sampler TextureSampler : register(s0);

struct VertexShaderInput
{
	float4 pos : POSITION;
	float4 col : COLOR;
	float2 tex : TEXCOORD0;
};

struct PixelShaderInput
{
	float4 pos : SV_POSITION;
	float4 col : COLOR;
	float2 tex : TEXCOORD0;
};

PixelShaderInput AlphaTestVertexShader( VertexShaderInput input )
{
	PixelShaderInput output = (PixelShaderInput)0;
	
	output.pos = mul(input.pos, MatrixTransform);
	output.col = input.col;
	output.tex = input.tex;

	return output;
}

float4 AlphaTestPixelShader( PixelShaderInput input ) : SV_Target
{
	return Texture.Sample(TextureSampler, input.tex) * input.col;
}

technique10 AlphaTest
{
	pass AlphaTestPass
	{
		SetGeometryShader( 0 );
		SetVertexShader( CompileShader( vs_4_0, AlphaTestVertexShader() ) );
		SetPixelShader( CompileShader( ps_4_0, AlphaTestPixelShader() ) );
	}
}
