// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

uniform extern float4x4 WorldViewProj;

uniform extern float3 EnvironmentMapSpecular;
uniform extern float FresnelFactor;
uniform extern float EnvironmentMapAmount;

uniform extern float4 DiffuseColor;
uniform extern float3 EmissiveColor;

uniform extern float3 DirLight0Direction;
uniform extern float3 DirLight0DiffuseColor;
uniform extern float3 DirLight1Direction;
uniform extern float3 DirLight1DiffuseColor;
uniform extern float3 DirLight2Direction;
uniform extern float3 DirLight2DiffuseColor;

uniform extern float3 EyePosition;

uniform extern float3 FogColor;
uniform extern float4 FogVector;

uniform extern float4x4 World;
uniform extern float3x3 WorldInverseTranspose;

Texture2D<float4> Texture : register(t0);
sampler TextureSampler : register(s0);

textureCUBE EnvironmentMap : register(t1);
sampler EnvironmentMapSampler : register(s1);

struct VSInput
{
    float4 Position : POSITION;
    float3 Normal   : NORMAL;
    float2 TexCoord : TEXCOORD0;
};

struct VSOutput
{
    float4 Diffuse    : COLOR0;
    float4 Specular   : COLOR1;
    float2 TexCoord   : TEXCOORD0;
    float3 EnvCoord   : TEXCOORD1;
    float4 PositionPS : SV_Position;
};

struct PSInput
{
    float4 Diffuse  : COLOR0;
    float4 Specular : COLOR1;
    float2 TexCoord : TEXCOORD0;
    float3 EnvCoord : TEXCOORD1;
};

float ComputeFresnelFactor(float3 eyeVector, float3 worldNormal)
{
    float viewAngle = dot(eyeVector, worldNormal);
    return pow(max(1 - abs(viewAngle), 0), FresnelFactor) * EnvironmentMapAmount;
}

float3 ComputeLights(float3 eyeVector, float3 worldNormal, uniform int numLights)
{
    float3x3 lightDirections = 0;
    float3x3 lightDiffuse = 0;
    
    [unroll]
    for (int i = 0; i < numLights; i++)
    {
        lightDirections[i] = float3x3(DirLight0Direction, DirLight1Direction, DirLight2Direction)[i];
        lightDiffuse[i] = float3x3(DirLight0DiffuseColor, DirLight1DiffuseColor, DirLight2DiffuseColor)[i];
    }

    float3 dotL = mul(-lightDirections, worldNormal);
    float3 zeroL = step(0, dotL);
    float3 diffuse  = zeroL * dotL;
    return mul(diffuse,  lightDiffuse)  * DiffuseColor.rgb + EmissiveColor;
}

VSOutput ComputeEnvMapVSOutput(VSInput input, uniform bool useFresnel, uniform int numLights)
{
    VSOutput output;
    
    float4 pos_ws = mul(input.Position, World);
    float3 eyeVector = normalize(EyePosition - pos_ws.xyz);
    float3 worldNormal = normalize(mul(input.Normal, WorldInverseTranspose));

    float3 lightDiffuse = ComputeLights(eyeVector, worldNormal, numLights);
    
    output.PositionPS = mul(input.Position, WorldViewProj);
    output.Diffuse = float4(lightDiffuse, DiffuseColor.a);
    
    if (useFresnel)
        output.Specular.rgb = ComputeFresnelFactor(eyeVector, worldNormal);
    else
        output.Specular.rgb = EnvironmentMapAmount;
    
    output.Specular.a = saturate(dot(input.Position, FogVector));
    output.TexCoord = input.TexCoord;
    output.EnvCoord = reflect(-eyeVector, worldNormal);

    return output;
}

VSOutput VSEnvMap(VSInput input)
{
    return ComputeEnvMapVSOutput(input, false, 3);
}

VSOutput VSEnvMapFresnel(VSInput input)
{
    return ComputeEnvMapVSOutput(input, true, 3);
}

VSOutput VSEnvMapOneLight(VSInput input)
{
    return ComputeEnvMapVSOutput(input, false, 1);
}

VSOutput VSEnvMapOneLightFresnel(VSInput input)
{
    return ComputeEnvMapVSOutput(input, true, 1);
}

float4 PSEnvMap(PSInput input) : SV_Target0
{
    float4 color = Texture.Sample(TextureSampler, input.TexCoord) * input.Diffuse;
    float4 envmap = EnvironmentMap.Sample(EnvironmentMapSampler, input.EnvCoord) * color.a;
    color.rgb = lerp(color.rgb, envmap.rgb, input.Specular.rgb);

    color.rgb = lerp(color.rgb, FogColor * color.a, input.Specular.w);
    return color;
}

float4 PSEnvMapNoFog(PSInput input) : SV_Target0
{
    float4 color = Texture.Sample(TextureSampler, input.TexCoord) * input.Diffuse;
    float4 envmap = EnvironmentMap.Sample(EnvironmentMapSampler, input.EnvCoord) * color.a;
    color.rgb = lerp(color.rgb, envmap.rgb, input.Specular.rgb);

    return color;
}

float4 PSEnvMapSpecular(PSInput input) : SV_Target0
{
    float4 color = Texture.Sample(TextureSampler, input.TexCoord) * input.Diffuse;
    float4 envmap = EnvironmentMap.Sample(EnvironmentMapSampler, input.EnvCoord) * color.a;
    color.rgb = lerp(color.rgb, envmap.rgb, input.Specular.rgb);

    color.rgb += EnvironmentMapSpecular * envmap.a;
    color.rgb = lerp(color.rgb, FogColor * color.a, input.Specular.w);
    return color;
}

float4 PSEnvMapSpecularNoFog(PSInput input) : SV_Target0
{
    float4 color = Texture.Sample(TextureSampler, input.TexCoord) * input.Diffuse;
    float4 envmap = EnvironmentMap.Sample(EnvironmentMapSampler, input.EnvCoord) * color.a;
    color.rgb = lerp(color.rgb, envmap.rgb, input.Specular.rgb);

    color.rgb += EnvironmentMapSpecular * envmap.a;
    return color;
}

technique10 EnvironmentMapEffect
{
	pass EnvironmentMap
	{
		SetVertexShader(CompileShader(vs_4_0, VSEnvMap()));
		SetPixelShader(CompileShader(ps_4_0, PSEnvMap()));
	}
}

technique10 EnvironmentMapEffectNoFog
{
	pass EnvironmentMap
	{
		SetVertexShader(CompileShader(vs_4_0, VSEnvMap()));
		SetPixelShader(CompileShader(ps_4_0, PSEnvMapNoFog()));
	}
}

technique10 EnvironmentMapEffectFresnel
{
	pass EnvironmentMap
	{
		SetVertexShader(CompileShader(vs_4_0, VSEnvMapFresnel()));
		SetPixelShader(CompileShader(ps_4_0, PSEnvMap()));
	}
}

technique10 EnvironmentMapEffectFresnelNoFog
{
	pass EnvironmentMap
	{
		SetVertexShader(CompileShader(vs_4_0, VSEnvMapFresnel()));
		SetPixelShader(CompileShader(ps_4_0, PSEnvMapNoFog()));
	}
}

technique10 EnvironmentMapEffectSpecular
{
	pass EnvironmentMap
	{
		SetVertexShader(CompileShader(vs_4_0, VSEnvMap()));
		SetPixelShader(CompileShader(ps_4_0, PSEnvMapSpecular()));
	}
}

technique10 EnvironmentMapEffectSpecularNoFog
{
	pass EnvironmentMap
	{
		SetVertexShader(CompileShader(vs_4_0, VSEnvMap()));
		SetPixelShader(CompileShader(ps_4_0, PSEnvMapSpecularNoFog()));
	}
}

technique10 EnvironmentMapEffectFresnelSpecular
{
	pass EnvironmentMap
	{
		SetVertexShader(CompileShader(vs_4_0, VSEnvMapFresnel()));
		SetPixelShader(CompileShader(ps_4_0, PSEnvMapSpecular()));
	}
}

technique10 EnvironmentMapEffectFresnelSpecularNoFog
{
	pass EnvironmentMap
	{
		SetVertexShader(CompileShader(vs_4_0, VSEnvMapFresnel()));
		SetPixelShader(CompileShader(ps_4_0, PSEnvMapSpecularNoFog()));
	}
}

// One Light techniques

technique10 EnvironmentMapEffectOneLight
{
	pass EnvironmentMap
	{
		SetVertexShader(CompileShader(vs_4_0, VSEnvMapOneLight()));
		SetPixelShader(CompileShader(ps_4_0, PSEnvMap()));
	}
}

technique10 EnvironmentMapEffectNoFogOneLight
{
	pass EnvironmentMap
	{
		SetVertexShader(CompileShader(vs_4_0, VSEnvMapOneLight()));
		SetPixelShader(CompileShader(ps_4_0, PSEnvMapNoFog()));
	}
}

technique10 EnvironmentMapEffectFresnelOneLight
{
	pass EnvironmentMap
	{
		SetVertexShader(CompileShader(vs_4_0, VSEnvMapOneLightFresnel()));
		SetPixelShader(CompileShader(ps_4_0, PSEnvMap()));
	}
}

technique10 EnvironmentMapEffectFresnelNoFogOneLight
{
	pass EnvironmentMap
	{
		SetVertexShader(CompileShader(vs_4_0, VSEnvMapOneLightFresnel()));
		SetPixelShader(CompileShader(ps_4_0, PSEnvMapNoFog()));
	}
}

technique10 EnvironmentMapEffectSpecularOneLight
{
	pass EnvironmentMap
	{
		SetVertexShader(CompileShader(vs_4_0, VSEnvMapOneLight()));
		SetPixelShader(CompileShader(ps_4_0, PSEnvMapSpecular()));
	}
}

technique10 EnvironmentMapEffectSpecularNoFogOneLight
{
	pass EnvironmentMap
	{
		SetVertexShader(CompileShader(vs_4_0, VSEnvMapOneLight()));
		SetPixelShader(CompileShader(ps_4_0, PSEnvMapSpecularNoFog()));
	}
}

technique10 EnvironmentMapEffectFresnelSpecularOneLight
{
	pass EnvironmentMap
	{
		SetVertexShader(CompileShader(vs_4_0, VSEnvMapOneLightFresnel()));
		SetPixelShader(CompileShader(ps_4_0, PSEnvMapSpecular()));
	}
}

technique10 EnvironmentMapEffectFresnelSpecularNoFogOneLight
{
	pass EnvironmentMap
	{
		SetVertexShader(CompileShader(vs_4_0, VSEnvMapOneLightFresnel()));
		SetPixelShader(CompileShader(ps_4_0, PSEnvMapSpecularNoFog()));
	}
}
