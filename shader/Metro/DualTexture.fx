// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

uniform extern float4 DiffuseColor;
uniform extern float3 FogColor;
uniform extern float4 FogVector;
uniform extern float4x4 WorldViewProj;

Texture2D<float4> Texture : register(t0);
   sampler TextureSampler : register(s0);

Texture2D<float4> Texture2 : register(t1);
   sampler Texture2Sampler : register(s1);

struct VSInput
{
    float4 pos  : POSITION;
    float2 tex  : TEXCOORD0;
    float2 tex2 : TEXCOORD1;
};

struct VSInputVertexColor
{
    float4 pos  : POSITION;
    float2 tex  : TEXCOORD0;
    float2 tex2 : TEXCOORD1;
    float4 col  : COLOR;
};

struct VSOutput
{
    float4 Diffuse    : COLOR0;
    float4 Specular   : COLOR1;
    float2 TexCoord   : TEXCOORD0;
    float2 TexCoord2  : TEXCOORD1;
    float4 PositionPS : SV_POSITION;
};

struct VSOutputNoFog
{
    float4 Diffuse    : COLOR0;
    float2 TexCoord   : TEXCOORD0;
    float2 TexCoord2  : TEXCOORD1;
    float4 PositionPS : SV_Position;
};

struct PSInput
{
    float4 Diffuse   : COLOR0;
    float4 Specular  : COLOR1;
    float2 TexCoord  : TEXCOORD0;
    float2 TexCoord2 : TEXCOORD1;
};

struct PSInputNoFog
{
    float4 Diffuse   : COLOR0;
    float2 TexCoord  : TEXCOORD0;
    float2 TexCoord2 : TEXCOORD1;
};

VSOutput VSDualTexture(VSInput input)
{
    VSOutput output;
    output.PositionPS = mul(input.pos, WorldViewProj);
    output.Diffuse = DiffuseColor;
    output.Specular = float4(0, 0, 0, saturate(dot(input.pos, FogVector)));
    output.TexCoord = input.tex;
    output.TexCoord2 = input.tex2;
    return output;
}

VSOutputNoFog VSDualTextureNoFog(VSInput input)
{
    VSOutputNoFog output;
    output.PositionPS = mul(input.pos, WorldViewProj);
    output.Diffuse = DiffuseColor;
    output.TexCoord = input.tex;
    output.TexCoord2 = input.tex2;
    return output;
}

VSOutput VSDualTextureVertexColor(VSInputVertexColor input)
{
    VSOutput output;
    output.PositionPS = mul(input.pos, WorldViewProj);
    output.Diffuse = DiffuseColor * input.col;
    output.Specular = float4(0, 0, 0, saturate(dot(input.pos, FogVector)));
    output.TexCoord = input.tex;
    output.TexCoord2 = input.tex2;
    return output;
}

VSOutputNoFog VSDualTextureVertexColorNoFog(VSInputVertexColor input)
{
    VSOutputNoFog output;
    output.PositionPS = mul(input.pos, WorldViewProj);
    output.Diffuse = DiffuseColor * input.col;
    output.TexCoord = input.tex;
    output.TexCoord2 = input.tex2;
    return output;
}

float4 PSDualTexture(PSInput input) : SV_Target0
{
    float4 color = Texture.Sample(TextureSampler, input.TexCoord);
    float4 overlay = Texture2.Sample(Texture2Sampler, input.TexCoord2);
    color.rgb *= 2;    
    color *= overlay * input.Diffuse;
    color.rgb = lerp(color.rgb, FogColor * color.a, input.Specular.w);
    return color;
}

float4 PSDualTextureNoFog(PSInputNoFog input) : SV_Target0
{
    float4 color = Texture.Sample(TextureSampler, input.TexCoord);
    float4 overlay = Texture2.Sample(Texture2Sampler, input.TexCoord2);
    color.rgb *= 2;
    color *= overlay * input.Diffuse;
    return color;
}

technique10 DualTextureEffect
{
	pass DualTexturePass
	{
		SetGeometryShader(0);
		SetVertexShader(CompileShader(vs_4_0, VSDualTexture()));
		SetPixelShader(CompileShader(ps_4_0, PSDualTexture()));
	}
}

technique10 DualTextureEffectVertexColor
{
	pass DualTexturePassVertexColor
	{
		SetGeometryShader(0);
		SetVertexShader(CompileShader(vs_4_0, VSDualTextureVertexColor()));
		SetPixelShader(CompileShader(ps_4_0, PSDualTexture()));
	}
}

technique10 DualTextureEffectNoFog
{
	pass DualTexturePassNoFog
	{
		SetGeometryShader(0);
		SetVertexShader(CompileShader(vs_4_0, VSDualTextureNoFog()));
		SetPixelShader(CompileShader(ps_4_0, PSDualTextureNoFog()));
	}
}

technique10 DualTextureEffectNoFogVertexColor
{
	pass DualTexturePassVertexColorNoFog
	{
		SetGeometryShader(0);
		SetVertexShader(CompileShader(vs_4_0, VSDualTextureVertexColorNoFog()));
		SetPixelShader(CompileShader(ps_4_0, PSDualTextureNoFog()));
	}
}
