// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

uniform extern float4x4 World;
uniform extern float4x4 View;
uniform extern float4x4 Projection;

uniform extern float4x4 WorldViewProj;
uniform extern float4x4 WorldInverseTranspose;

Texture2D<float4> Texture : register(t0);
   sampler TextureSampler : register(s0);

struct VertexShaderInput
{
	float4 Position : POSITION0;
	float3 Normal	: NORMAL;
	float2 TexCoord : TEXCOORD0;
};

struct VertexShaderOutput
{
    float4 Position : SV_POSITION;
	float3 Normal	: NORMAL;
	float2 TexCoord : TEXCOORD0;
};

VertexShaderOutput VertexShaderFunction(VertexShaderInput input)
{
    VertexShaderOutput output;

	output.Position = mul(input.Position, WorldViewProj);
	output.Normal = normalize(mul(input.Normal.xyz, (float3x3)WorldInverseTranspose));
	output.TexCoord = input.TexCoord;

    return output;
}

float4 PixelShaderFunction(VertexShaderOutput input) : SV_TARGET
{
	return Texture.Sample(TextureSampler, input.TexCoord);
}

technique10 Technique1
{
    pass Pass1
    {
        VertexShader = compile vs_4_0 VertexShaderFunction();
        PixelShader = compile ps_4_0 PixelShaderFunction();
    }
}
