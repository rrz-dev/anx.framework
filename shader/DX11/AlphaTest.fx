// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

uniform extern float4x4 WorldViewProj;
uniform extern float4 DiffuseColor;
uniform extern float4 AlphaTest;
uniform extern float3 FogColor;
uniform extern float4 FogVector;

Texture2D<float4> Texture : register(t0);
   sampler TextureSampler : register(s0);

struct VSInput
{
    float4 pos  : POSITION;
    float2 tex  : TEXCOORD0;
};

struct VSOutput
{
    float4 Diffuse    : COLOR0;
    float4 Specular   : COLOR1;
    float2 TexCoord   : TEXCOORD0;
    float4 PositionPS : SV_Position;
};

struct VSInputVertexColor
{
    float4 pos  : POSITION;
    float2 tex  : TEXCOORD0;
    float4 col  : COLOR;
};

struct VSOutputNoFog
{
    float4 Diffuse    : COLOR0;
    float2 TexCoord   : TEXCOORD0;
    float4 PositionPS : SV_Position;
};

struct PSInput
{
    float4 Diffuse  : COLOR0;
    float4 Specular : COLOR1;
    float2 TexCoord : TEXCOORD0;
};

struct PSInputNoFog
{
    float4 Diffuse  : COLOR0;
    float2 TexCoord : TEXCOORD0;
};

VSOutput VSAlphaTest(VSInput input)
{
    VSOutput output;
    output.PositionPS = mul(input.pos, WorldViewProj);
    output.Diffuse = DiffuseColor;
    output.Specular = float4(0, 0, 0, saturate(dot(input.pos, FogVector)));
    output.TexCoord = input.tex;
    return output;
}

VSOutputNoFog VSAlphaTestNoFog(VSInput input)
{
    VSOutputNoFog output;
    output.PositionPS = mul(input.pos, WorldViewProj);
    output.Diffuse = DiffuseColor;
    output.TexCoord = input.tex;
    return output;
}

VSOutput VSAlphaTestVertexColor(VSInputVertexColor input)
{
    VSOutput output;
    output.PositionPS = mul(input.pos, WorldViewProj);
    output.Diffuse = DiffuseColor * input.col;
    output.Specular = float4(0, 0, 0, saturate(dot(input.pos, FogVector)));
    output.TexCoord = input.tex;
    return output;
}

VSOutputNoFog VSAlphaTestVertexColorNoFog(VSInputVertexColor input)
{
    VSOutputNoFog output;
    output.PositionPS = mul(input.pos, WorldViewProj);
    output.Diffuse = DiffuseColor * input.col;
    output.TexCoord = input.tex;
    return output;
}

float4 PSAlphaTestLtGt(PSInput input) : SV_Target0
{
    float4 color = Texture.Sample(TextureSampler, input.TexCoord) * input.Diffuse;
    clip((color.a < AlphaTest.x) ? AlphaTest.z : AlphaTest.w);
	color.rgb = lerp(color.rgb, FogColor * color.a, input.Specular.w);
    return color;
}

float4 PSAlphaTestLtGtNoFog(PSInputNoFog input) : SV_Target0
{
    float4 color = Texture.Sample(TextureSampler, input.TexCoord) * input.Diffuse;
    clip((color.a < AlphaTest.x) ? AlphaTest.z : AlphaTest.w);
    return color;
}

float4 PSAlphaTestEqNe(PSInput input) : SV_Target0
{
    float4 color = Texture.Sample(TextureSampler, input.TexCoord) * input.Diffuse;
    clip((abs(color.a - AlphaTest.x) < AlphaTest.y) ? AlphaTest.z : AlphaTest.w);
	color.rgb = lerp(color.rgb, FogColor * color.a, input.Specular.w);
    return color;
}

float4 PSAlphaTestEqNeNoFog(PSInputNoFog input) : SV_Target0
{
    float4 color = Texture.Sample(TextureSampler, input.TexCoord) * input.Diffuse;
    clip((abs(color.a - AlphaTest.x) < AlphaTest.y) ? AlphaTest.z : AlphaTest.w);
    return color;
}

technique10 AlphaTestLtGt
{
	pass AlphaTestPass
	{
		SetGeometryShader(0);
		SetVertexShader(CompileShader(vs_4_0, VSAlphaTest()));
		SetPixelShader(CompileShader(ps_4_0, PSAlphaTestLtGt()));
	}
}

technique10 AlphaTestNoFogLtGt
{
	pass AlphaTestPass
	{
		SetGeometryShader(0);
		SetVertexShader(CompileShader(vs_4_0, VSAlphaTestNoFog()));
		SetPixelShader(CompileShader(ps_4_0, PSAlphaTestLtGtNoFog()));
	}
}

technique10 AlphaTestVertexColorLtGt
{
	pass AlphaTestPass
	{
		SetGeometryShader(0);
		SetVertexShader(CompileShader(vs_4_0, VSAlphaTestVertexColor()));
		SetPixelShader(CompileShader(ps_4_0, PSAlphaTestLtGt()));
	}
}

technique10 AlphaTestVertexColorNoFogLtGt
{
	pass AlphaTestPass
	{
		SetGeometryShader(0);
		SetVertexShader(CompileShader(vs_4_0, VSAlphaTestVertexColorNoFog()));
		SetPixelShader(CompileShader(ps_4_0, PSAlphaTestLtGtNoFog()));
	}
}

technique10 AlphaTestEqNe
{
	pass AlphaTestPass
	{
		SetGeometryShader(0);
		SetVertexShader(CompileShader(vs_4_0, VSAlphaTest()));
		SetPixelShader(CompileShader(ps_4_0, PSAlphaTestEqNe()));
	}
}

technique10 AlphaTestNoFogEqNe
{
	pass AlphaTestPass
	{
		SetGeometryShader(0);
		SetVertexShader(CompileShader(vs_4_0, VSAlphaTestNoFog()));
		SetPixelShader(CompileShader(ps_4_0, PSAlphaTestEqNeNoFog()));
	}
}

technique10 AlphaTestVertexColorEqNe
{
	pass AlphaTestPass
	{
		SetGeometryShader(0);
		SetVertexShader(CompileShader(vs_4_0, VSAlphaTestVertexColor()));
		SetPixelShader(CompileShader(ps_4_0, PSAlphaTestEqNe()));
	}
}

technique10 AlphaTestVertexColorNoFogEqNe
{
	pass AlphaTestPass
	{
		SetGeometryShader(0);
		SetVertexShader(CompileShader(vs_4_0, VSAlphaTestVertexColorNoFog()));
		SetPixelShader(CompileShader(ps_4_0, PSAlphaTestEqNeNoFog()));
	}
}
