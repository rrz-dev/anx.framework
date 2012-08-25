// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

//TODO: dummy implementation / placeholder

uniform extern float4x4 World;
uniform extern float4x4 View;
uniform extern float4x4 Projection;

/*
Texture2D<float4> Texture : register(t0);
   sampler TextureSampler : register(s0);
*/

struct VertexColorVertexShaderInput
{
	float4 Position		: POSITION;
	float4 Color		: COLOR;
};

struct PixelShaderInput
{
	float4 Position		: SV_POSITION;
	float4 Color		: COLOR;
	float2 TexCoord0	: TEXCOORD0;
};

PixelShaderInput VertexColorVertexShader( VertexColorVertexShaderInput input )
{
	PixelShaderInput output = (PixelShaderInput)0;
	
    float4 worldPosition = mul(input.Position, World);
    float4 viewPosition = mul(worldPosition, View);
    output.Position = mul(viewPosition, Projection);

	output.Color = input.Color;
	output.TexCoord0 = (float2)0;

	return output;
}

float4 VertexColorPixelShader( PixelShaderInput input ) : SV_Target
{
	return input.Color;
}

technique10 VertexColor
{
	pass VertexColorPass
	{
		SetGeometryShader( 0 );
		SetVertexShader( CompileShader( vs_4_0, VertexColorVertexShader() ) );
		SetPixelShader( CompileShader( ps_4_0, VertexColorPixelShader() ) );
	}
}
