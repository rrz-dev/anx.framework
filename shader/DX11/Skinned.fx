// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

uniform extern float4x4 WorldViewProj;

uniform extern float4 DiffuseColor;
uniform extern float3 EmissiveColor;
uniform extern float3 SpecularColor;
uniform extern float  SpecularPower;

uniform extern float3 DirLight0Direction;
uniform extern float3 DirLight0DiffuseColor;
uniform extern float3 DirLight0SpecularColor;
uniform extern float3 DirLight1Direction;
uniform extern float3 DirLight1DiffuseColor;
uniform extern float3 DirLight1SpecularColor;
uniform extern float3 DirLight2Direction;
uniform extern float3 DirLight2DiffuseColor;
uniform extern float3 DirLight2SpecularColor;

uniform extern float3 EyePosition;

uniform extern float3 FogColor;
uniform extern float4 FogVector;

uniform extern float4x4 World;
uniform extern float3x3 WorldInverseTranspose;

uniform extern float4x3 Bones[72];

Texture2D<float4> Texture : register(t0);
sampler TextureSampler : register(s0);

struct VSInputNmTxWeights
{
    float4 Position : POSITION;
    float3 Normal   : NORMAL;
    float2 TexCoord : TEXCOORD0;
    int4   Indices  : BLENDINDICES0;
    float4 Weights  : BLENDWEIGHT0;
};

struct VSOutputTx
{
    float4 Diffuse    : COLOR0;
    float4 Specular   : COLOR1;
    float2 TexCoord   : TEXCOORD0;
    float4 PositionPS : SV_Position;
};

struct VSOutputPixelLightingTx
{
    float2 TexCoord   : TEXCOORD0;
    float4 PositionWS : TEXCOORD1;
    float3 NormalWS   : TEXCOORD2;
    float4 Diffuse    : COLOR0;
    float4 PositionPS : SV_Position;
};

struct PSInputTx
{
    float4 Diffuse  : COLOR0;
    float4 Specular : COLOR1;
    float2 TexCoord : TEXCOORD0;
};

struct PSInputPixelLightingTx
{
    float2 TexCoord   : TEXCOORD0;
    float4 PositionWS : TEXCOORD1;
    float3 NormalWS   : TEXCOORD2;
    float4 Diffuse    : COLOR0;
};

struct ColorPair
{
    float3 Diffuse;
    float3 Specular;
};

void Skin(inout VSInputNmTxWeights input, uniform int boneCount)
{
    float4x3 skinning = 0;

    [unroll]
    for (int i = 0; i < boneCount; i++)
    {
        skinning += Bones[input.Indices[i]] * input.Weights[i];
    }

    input.Position.xyz = mul(input.Position, skinning);
    input.Normal = mul(input.Normal, (float3x3)skinning);
}

ColorPair ComputeLights(float3 eyeVector, float3 worldNormal, uniform int numLights)
{
    float3x3 lightDirections = 0;
    float3x3 lightDiffuse = 0;
    float3x3 lightSpecular = 0;
    float3x3 halfVectors = 0;
    
    [unroll]
    for (int i = 0; i < numLights; i++)
    {
        lightDirections[i] = float3x3(DirLight0Direction,     DirLight1Direction,     DirLight2Direction)    [i];
        lightDiffuse[i]    = float3x3(DirLight0DiffuseColor,  DirLight1DiffuseColor,  DirLight2DiffuseColor) [i];
        lightSpecular[i]   = float3x3(DirLight0SpecularColor, DirLight1SpecularColor, DirLight2SpecularColor)[i];
        
        halfVectors[i] = normalize(eyeVector - lightDirections[i]);
    }

    float3 dotL = mul(-lightDirections, worldNormal);
    float3 dotH = mul(halfVectors, worldNormal);
    
    float3 zeroL = step(0, dotL);

    float3 diffuse  = zeroL * dotL;
    float3 specular = pow(max(dotH, 0) * zeroL, SpecularPower);

    ColorPair result;
    result.Diffuse  = mul(diffuse,  lightDiffuse)  * DiffuseColor.rgb + EmissiveColor;
    result.Specular = mul(specular, lightSpecular) * SpecularColor;
    return result;
}

VSOutputTx VSSkinnedVertexLightingOneBone(VSInputNmTxWeights input)
{
    VSOutputTx output;
    Skin(input, 1);
    
    float4 pos_ws = mul(input.Position, World);
    float3 eyeVector = normalize(EyePosition - pos_ws.xyz);
    float3 worldNormal = normalize(mul(input.Normal, WorldInverseTranspose));
    ColorPair lightResult = ComputeLights(eyeVector, worldNormal, 3);

    output.PositionPS = mul(input.Position, WorldViewProj);
    output.Diffuse = float4(lightResult.Diffuse, DiffuseColor.a);
    output.Specular = float4(lightResult.Specular, saturate(dot(input.Position, FogVector)));
    output.TexCoord = input.TexCoord;
    return output;
}

VSOutputTx VSSkinnedVertexLightingTwoBones(VSInputNmTxWeights input)
{
    VSOutputTx output;
    Skin(input, 2);
    
    float4 pos_ws = mul(input.Position, World);
    float3 eyeVector = normalize(EyePosition - pos_ws.xyz);
    float3 worldNormal = normalize(mul(input.Normal, WorldInverseTranspose));
    ColorPair lightResult = ComputeLights(eyeVector, worldNormal, 3);

    output.PositionPS = mul(input.Position, WorldViewProj);
    output.Diffuse = float4(lightResult.Diffuse, DiffuseColor.a);
    output.Specular = float4(lightResult.Specular, saturate(dot(input.Position, FogVector)));
    output.TexCoord = input.TexCoord;
    return output;
}

VSOutputTx VSSkinnedVertexLightingFourBones(VSInputNmTxWeights input)
{
    VSOutputTx output;
    Skin(input, 4);
    
    float4 pos_ws = mul(input.Position, World);
    float3 eyeVector = normalize(EyePosition - pos_ws.xyz);
    float3 worldNormal = normalize(mul(input.Normal, WorldInverseTranspose));
    ColorPair lightResult = ComputeLights(eyeVector, worldNormal, 3);

    output.PositionPS = mul(input.Position, WorldViewProj);
    output.Diffuse = float4(lightResult.Diffuse, DiffuseColor.a);
    output.Specular = float4(lightResult.Specular, saturate(dot(input.Position, FogVector)));
    output.TexCoord = input.TexCoord;
    return output;
}

VSOutputTx VSSkinnedOneLightOneBone(VSInputNmTxWeights input)
{
    VSOutputTx output;
    Skin(input, 1);
	
    float4 pos_ws = mul(input.Position, World);
    float3 eyeVector = normalize(EyePosition - pos_ws.xyz);
    float3 worldNormal = normalize(mul(input.Normal, WorldInverseTranspose));
    ColorPair lightResult = ComputeLights(eyeVector, worldNormal, 1);

    output.PositionPS = mul(input.Position, WorldViewProj);
    output.Diffuse = float4(lightResult.Diffuse, DiffuseColor.a);
    output.Specular = float4(lightResult.Specular, saturate(dot(input.Position, FogVector)));
    output.TexCoord = input.TexCoord;
    return output;
}

VSOutputTx VSSkinnedOneLightTwoBones(VSInputNmTxWeights input)
{
    VSOutputTx output;
    Skin(input, 2);
	
    float4 pos_ws = mul(input.Position, World);
    float3 eyeVector = normalize(EyePosition - pos_ws.xyz);
    float3 worldNormal = normalize(mul(input.Normal, WorldInverseTranspose));
    ColorPair lightResult = ComputeLights(eyeVector, worldNormal, 1);

    output.PositionPS = mul(input.Position, WorldViewProj);
    output.Diffuse = float4(lightResult.Diffuse, DiffuseColor.a);
    output.Specular = float4(lightResult.Specular, saturate(dot(input.Position, FogVector)));
    output.TexCoord = input.TexCoord;
    return output;
}

VSOutputTx VSSkinnedOneLightFourBones(VSInputNmTxWeights input)
{
    VSOutputTx output;
    Skin(input, 4);
	
    float4 pos_ws = mul(input.Position, World);
    float3 eyeVector = normalize(EyePosition - pos_ws.xyz);
    float3 worldNormal = normalize(mul(input.Normal, WorldInverseTranspose));
    ColorPair lightResult = ComputeLights(eyeVector, worldNormal, 1);

    output.PositionPS = mul(input.Position, WorldViewProj);
    output.Diffuse = float4(lightResult.Diffuse, DiffuseColor.a);
    output.Specular = float4(lightResult.Specular, saturate(dot(input.Position, FogVector)));
    output.TexCoord = input.TexCoord;
    return output;
}

VSOutputPixelLightingTx VSSkinnedPixelLightingOneBone(VSInputNmTxWeights input)
{
    VSOutputPixelLightingTx output;
    Skin(input, 1);
	
    output.PositionPS = mul(input.Position, WorldViewProj);
    output.PositionWS = float4(mul(input.Position, World).xyz, saturate(dot(input.Position, FogVector)));
    output.NormalWS = normalize(mul(input.Normal, WorldInverseTranspose));
    output.Diffuse = float4(1, 1, 1, DiffuseColor.a);
    output.TexCoord = input.TexCoord;
    return output;
}

VSOutputPixelLightingTx VSSkinnedPixelLightingTwoBones(VSInputNmTxWeights input)
{
    VSOutputPixelLightingTx output;
    Skin(input, 2);
	
    output.PositionPS = mul(input.Position, WorldViewProj);
    output.PositionWS = float4(mul(input.Position, World).xyz, saturate(dot(input.Position, FogVector)));
    output.NormalWS = normalize(mul(input.Normal, WorldInverseTranspose));
    output.Diffuse = float4(1, 1, 1, DiffuseColor.a);
    output.TexCoord = input.TexCoord;
    return output;
}

VSOutputPixelLightingTx VSSkinnedPixelLightingFourBones(VSInputNmTxWeights input)
{
    VSOutputPixelLightingTx output;
    Skin(input, 4);
	
    output.PositionPS = mul(input.Position, WorldViewProj);
    output.PositionWS = float4(mul(input.Position, World).xyz, saturate(dot(input.Position, FogVector)));
    output.NormalWS = normalize(mul(input.Normal, WorldInverseTranspose));
    output.Diffuse = float4(1, 1, 1, DiffuseColor.a);
    output.TexCoord = input.TexCoord;
    return output;
}

float4 PSSkinnedVertexLighting(PSInputTx input) : SV_Target0
{
    float4 color = Texture.Sample(TextureSampler, input.TexCoord) * input.Diffuse;
    color.rgb += input.Specular.rgb * color.a;
    color.rgb = lerp(color.rgb, FogColor * color.a, input.Specular.w);
    return color;
}

float4 PSSkinnedVertexLightingNoFog(PSInputTx input) : SV_Target0
{
    float4 color = Texture.Sample(TextureSampler, input.TexCoord) * input.Diffuse;
    color.rgb += input.Specular.rgb * color.a;
    return color;
}

float4 PSSkinnedPixelLighting(PSInputPixelLightingTx input) : SV_Target0
{
    float4 color = Texture.Sample(TextureSampler, input.TexCoord) * input.Diffuse;
    
    float3 eyeVector = normalize(EyePosition - input.PositionWS.xyz);
    float3 worldNormal = normalize(input.NormalWS);
    
    ColorPair lightResult = ComputeLights(eyeVector, worldNormal, 3);
    
    color.rgb *= lightResult.Diffuse;
    color.rgb += lightResult.Specular * color.a;
    color.rgb = lerp(color.rgb, FogColor * color.a, input.PositionWS.w);
    return color;
}

technique10 OneBoneVertexLighting
{
	pass Skinned
	{
		SetVertexShader(CompileShader(vs_4_0, VSSkinnedVertexLightingOneBone()));
		SetPixelShader(CompileShader(ps_4_0, PSSkinnedVertexLighting()));
	}
}

technique10 OneBoneVertexLightingNoFog
{
	pass Skinned
	{
		SetVertexShader(CompileShader(vs_4_0, VSSkinnedVertexLightingOneBone()));
		SetPixelShader(CompileShader(ps_4_0, PSSkinnedVertexLightingNoFog()));
	}
}

technique10 TwoBonesVertexLighting
{
	pass Skinned
	{
		SetVertexShader(CompileShader(vs_4_0, VSSkinnedVertexLightingTwoBones()));
		SetPixelShader(CompileShader(ps_4_0, PSSkinnedVertexLighting()));
	}
}

technique10 TwoBonesVertexLightingNoFog
{
	pass Skinned
	{
		SetVertexShader(CompileShader(vs_4_0, VSSkinnedVertexLightingTwoBones()));
		SetPixelShader(CompileShader(ps_4_0, PSSkinnedVertexLightingNoFog()));
	}
}

technique10 FourBonesVertexLighting
{
	pass Skinned
	{
		SetVertexShader(CompileShader(vs_4_0, VSSkinnedVertexLightingFourBones()));
		SetPixelShader(CompileShader(ps_4_0, PSSkinnedVertexLighting()));
	}
}

technique10 FourBonesVertexLightingNoFog
{
	pass Skinned
	{
		SetVertexShader(CompileShader(vs_4_0, VSSkinnedVertexLightingFourBones()));
		SetPixelShader(CompileShader(ps_4_0, PSSkinnedVertexLightingNoFog()));
	}
}

technique10 OneBoneOneLight
{
	pass Skinned
	{
		SetVertexShader(CompileShader(vs_4_0, VSSkinnedOneLightOneBone()));
		SetPixelShader(CompileShader(ps_4_0, PSSkinnedVertexLighting()));
	}
}

technique10 OneBoneOneLightNoFog
{
	pass Skinned
	{
		SetVertexShader(CompileShader(vs_4_0, VSSkinnedOneLightOneBone()));
		SetPixelShader(CompileShader(ps_4_0, PSSkinnedVertexLightingNoFog()));
	}
}

technique10 TwoBonesOneLight
{
	pass Skinned
	{
		SetVertexShader(CompileShader(vs_4_0, VSSkinnedOneLightTwoBones()));
		SetPixelShader(CompileShader(ps_4_0, PSSkinnedVertexLighting()));
	}
}

technique10 TwoBonesOneLightNoFog
{
	pass Skinned
	{
		SetVertexShader(CompileShader(vs_4_0, VSSkinnedOneLightTwoBones()));
		SetPixelShader(CompileShader(ps_4_0, PSSkinnedVertexLightingNoFog()));
	}
}

technique10 FourBonesOneLight
{
	pass Skinned
	{
		SetVertexShader(CompileShader(vs_4_0, VSSkinnedOneLightFourBones()));
		SetPixelShader(CompileShader(ps_4_0, PSSkinnedVertexLighting()));
	}
}

technique10 FourBonesOneLightNoFog
{
	pass Skinned
	{
		SetVertexShader(CompileShader(vs_4_0, VSSkinnedOneLightFourBones()));
		SetPixelShader(CompileShader(ps_4_0, PSSkinnedVertexLightingNoFog()));
	}
}



technique10 OneBonePixelLighting
{
	pass Skinned
	{
		SetVertexShader(CompileShader(vs_4_0, VSSkinnedPixelLightingOneBone()));
		SetPixelShader(CompileShader(ps_4_0, PSSkinnedPixelLighting()));
	}
}

technique10 OneBonePixelLightingNoFog
{
	pass Skinned
	{
		SetVertexShader(CompileShader(vs_4_0, VSSkinnedPixelLightingOneBone()));
		SetPixelShader(CompileShader(ps_4_0, PSSkinnedPixelLighting()));
	}
}

technique10 TwoBonesPixelLighting
{
	pass Skinned
	{
		SetVertexShader(CompileShader(vs_4_0, VSSkinnedPixelLightingTwoBones()));
		SetPixelShader(CompileShader(ps_4_0, PSSkinnedPixelLighting()));
	}
}

technique10 TwoBonesPixelLightingNoFog
{
	pass Skinned
	{
		SetVertexShader(CompileShader(vs_4_0, VSSkinnedPixelLightingTwoBones()));
		SetPixelShader(CompileShader(ps_4_0, PSSkinnedPixelLighting()));
	}
}

technique10 FourBonesPixelLighting
{
	pass Skinned
	{
		SetVertexShader(CompileShader(vs_4_0, VSSkinnedPixelLightingFourBones()));
		SetPixelShader(CompileShader(ps_4_0, PSSkinnedPixelLighting()));
	}
}

technique10 FourBonesPixelLightingNoFog
{
	pass Skinned
	{
		SetVertexShader(CompileShader(vs_4_0, VSSkinnedPixelLightingFourBones()));
		SetPixelShader(CompileShader(ps_4_0, PSSkinnedPixelLighting()));
	}
}
