// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

//TODO: dummy implementation / placeholder

Texture2D<float4> Texture : register(t0);
sampler Sampler : register(s0);

uniform extern float4x4 World;
uniform extern float4x4 View;
uniform extern float4x4 Projection;

float4 DiffuseColor;
float3 EmissiveColor;
float3 SpecularColor;
float  SpecularPower;

float3 LightDirection[3];
float3 LightDiffuseColor[3];
float3 LightSpecularColor[3];

float3 EyePosition;

float3 FogColor;
float4 FogVector;

#include "Structures.fxh"

struct VertexColorVSInput
{
	float4 Position		: POSITION;
	float4 Color		: COLOR;
};

struct NormalVSInput
{
	float4 Position		: SV_POSITION;
	float3 Normal		: NORMAL;
};

struct NormalColorVSInput
{
	float4 Position		: SV_POSITION;
	float3 Normal		: NORMAL;
	float4 Color		: COLOR;
};

struct NormalTexVSInput
{
	float4 Position		: SV_POSITION;
	float3 Normal		: NORMAL;
	float2 TexCoord		: TEXCOORD0;
};

struct PixelShaderInput
{
	float4 Position		: SV_POSITION;
	float4 Color		: COLOR;
	float2 TexCoord0	: TEXCOORD0;
};

PixelShaderInput VertexColorVertexShader( VertexColorVSInput input )
{
	PixelShaderInput output = (PixelShaderInput)0;
	
    float4 worldPosition = mul(input.Position, World);
    float4 viewPosition = mul(worldPosition, View);
    output.Position = mul(viewPosition, Projection);

	output.Color = input.Color;
	output.TexCoord0 = (float2)0;

	return output;
}

PixelShaderInput NormalTexShader( NormalTexVSInput input)
{
	PixelShaderInput output = (PixelShaderInput)0;
	
    float4 worldPosition = mul(input.Position, World);
    float4 viewPosition = mul(worldPosition, View);
    output.Position = mul(viewPosition, Projection);

	output.Color = float4(1,1,1,1);
	output.TexCoord0 = input.TexCoord;

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

technique10 NormalTex
{
	pass NormalTexPass
	{
		SetGeometryShader( 0 );
		SetVertexShader( CompileShader( vs_4_0, NormalTexShader() ) );
		SetPixelShader( CompileShader( ps_4_0, VertexColorPixelShader() ) );
	}
}
