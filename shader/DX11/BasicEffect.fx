// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

Texture2D<float4> Texture : register(t0);
sampler TextureSampler : register(s0);

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
uniform extern float4x4 WorldViewProj;

struct VSInput
{
    float4 Position : POSITION;
};

struct VSInputVc
{
    float4 Position : POSITION;
    float4 Color    : COLOR;
};

struct VSInputTx
{
    float4 Position : POSITION;
    float2 TexCoord : TEXCOORD0;
};

struct VSInputTxVc
{
    float4 Position : POSITION;
    float2 TexCoord : TEXCOORD0;
    float4 Color    : COLOR;
};

struct VSInputNm
{
    float4 Position : POSITION;
    float3 Normal   : NORMAL;
};

struct VSInputNmVc
{
    float4 Position : POSITION;
    float3 Normal   : NORMAL;
    float4 Color    : COLOR;
};

struct VSInputNmTx
{
    float4 Position : POSITION;
    float3 Normal   : NORMAL;
    float2 TexCoord : TEXCOORD0;
};

struct VSInputNmTxVc
{
    float4 Position : POSITION;
    float3 Normal   : NORMAL;
    float2 TexCoord : TEXCOORD0;
    float4 Color    : COLOR;
};

struct VSOutput
{
    float4 Diffuse    : COLOR0;
    float4 Specular   : COLOR1;
    float4 PositionPS : SV_Position;
};

struct VSOutputNoFog
{
    float4 Diffuse    : COLOR0;
    float4 PositionPS : SV_Position;
};

struct VSOutputTx
{
    float4 Diffuse    : COLOR0;
    float4 Specular   : COLOR1;
    float2 TexCoord   : TEXCOORD0;
    float4 PositionPS : SV_Position;
};

struct VSOutputTxNoFog
{
    float4 Diffuse    : COLOR0;
    float2 TexCoord   : TEXCOORD0;
    float4 PositionPS : SV_Position;
};

struct VSOutputPixelLighting
{
    float4 PositionWS : TEXCOORD0;
    float3 NormalWS   : TEXCOORD1;
    float4 Diffuse    : COLOR0;
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

struct PSInput
{
    float4 Diffuse  : COLOR0;
    float4 Specular : COLOR1;
};

struct PSInputNoFog
{
    float4 Diffuse : COLOR0;
};

struct PSInputTx
{
    float4 Diffuse  : COLOR0;
    float4 Specular : COLOR1;
    float2 TexCoord : TEXCOORD0;
};

struct PSInputTxNoFog
{
    float4 Diffuse  : COLOR0;
    float2 TexCoord : TEXCOORD0;
};

struct PSInputPixelLighting
{
    float4 PositionWS : TEXCOORD0;
    float3 NormalWS   : TEXCOORD1;
    float4 Diffuse    : COLOR0;
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

ColorPair ComputeLights(float3 eyeVector, float3 worldNormal, uniform int numLights)
{
    float3x3 lightDirections = 0;
    float3x3 lightDiffuse = 0;
    float3x3 lightSpecular = 0;
    float3x3 halfVectors = 0;
    
    [unroll]
    for (int i = 0; i < numLights; i++)
    {
        lightDirections[i] = float3x3(DirLight0Direction, DirLight1Direction, DirLight2Direction)[i];
        lightDiffuse[i] = float3x3(DirLight0DiffuseColor, DirLight1DiffuseColor, DirLight2DiffuseColor)[i];
        lightSpecular[i] = float3x3(DirLight0SpecularColor, DirLight1SpecularColor, DirLight2SpecularColor)[i];
        halfVectors[i] = normalize(eyeVector - lightDirections[i]);
    }

    float3 dotL = mul(-lightDirections, worldNormal);
    float3 dotH = mul(halfVectors, worldNormal);
    
    float3 zeroL = step(0, dotL);

    float3 diffuse  = zeroL * dotL;
    float3 specular = pow(max(dotH, 0) * zeroL, SpecularPower);

    ColorPair result;
    result.Diffuse = mul(diffuse, lightDiffuse) * DiffuseColor.rgb + EmissiveColor;
    result.Specular = mul(specular, lightSpecular) * SpecularColor;
    return result;
}

VSOutput VSBasic(VSInput input)
{
    VSOutput output;
    output.PositionPS = mul(input.Position, WorldViewProj);
    output.Diffuse = DiffuseColor;
    output.Specular = float4(0, 0, 0, saturate(dot(input.Position, FogVector)));
    return output;
}

VSOutputNoFog VSBasicNoFog(VSInput input)
{
    VSOutputNoFog output;
    output.PositionPS = mul(input.Position, WorldViewProj);
    output.Diffuse = DiffuseColor;
    return output;
}

VSOutput VSBasicVertexColor(VSInputVc input)
{
    VSOutput output;
    output.PositionPS = mul(input.Position, WorldViewProj);
    output.Diffuse = DiffuseColor * input.Color;
    output.Specular = float4(0, 0, 0, saturate(dot(input.Position, FogVector)));
    return output;
}

VSOutputNoFog VSBasicVertexColorNoFog(VSInputVc input)
{
    VSOutputNoFog output;
    output.PositionPS = mul(input.Position, WorldViewProj);
    output.Diffuse = DiffuseColor * input.Color;
    return output;
}

VSOutputTx VSBasicTexture(VSInputTx input)
{
    VSOutputTx output;
    output.PositionPS = mul(input.Position, WorldViewProj);
    output.Diffuse = DiffuseColor;
    output.Specular = float4(0, 0, 0, saturate(dot(input.Position, FogVector)));
    output.TexCoord = input.TexCoord;
    return output;
}

VSOutputTxNoFog VSBasicTextureNoFog(VSInputTx input)
{
    VSOutputTxNoFog output;
    output.PositionPS = mul(input.Position, WorldViewProj);
    output.Diffuse = DiffuseColor;
    output.TexCoord = input.TexCoord;
    return output;
}

VSOutputTx VSBasicTextureVertexColor(VSInputTxVc input)
{
    VSOutputTx output;
    output.PositionPS = mul(input.Position, WorldViewProj);
    output.Diffuse = DiffuseColor * input.Color;
    output.Specular = float4(0, 0, 0, saturate(dot(input.Position, FogVector)));
    output.TexCoord = input.TexCoord;
    return output;
}

VSOutputTxNoFog VSBasicTextureVertexColorNoFog(VSInputTxVc input)
{
    VSOutputTxNoFog output;
    output.PositionPS = mul(input.Position, WorldViewProj);
    output.Diffuse = DiffuseColor * input.Color;
    output.TexCoord = input.TexCoord;
    return output;
}


// Vertex Shader - Vertex Lighting


VSOutput VSBasicVertexLighting(VSInputNm input)
{
    VSOutput output;
    float4 pos_ws = mul(input.Position, World);
    float3 eyeVector = normalize(EyePosition - pos_ws.xyz);
    float3 worldNormal = normalize(mul(input.Normal, WorldInverseTranspose));

    ColorPair lightResult = ComputeLights(eyeVector, worldNormal, 3);
    
    output.PositionPS = mul(input.Position, WorldViewProj);
    output.Diffuse = float4(lightResult.Diffuse, DiffuseColor.a);
    output.Specular = float4(lightResult.Specular, saturate(dot(input.Position, FogVector)));
    return output;
}

VSOutput VSBasicVertexLightingVertexColor(VSInputNmVc input)
{
    VSOutput output;
    float4 pos_ws = mul(input.Position, World);
    float3 eyeVector = normalize(EyePosition - pos_ws.xyz);
    float3 worldNormal = normalize(mul(input.Normal, WorldInverseTranspose));

    ColorPair lightResult = ComputeLights(eyeVector, worldNormal, 3);
    
    output.PositionPS = mul(input.Position, WorldViewProj);
    output.Diffuse = float4(lightResult.Diffuse, DiffuseColor.a) * input.Color;
    output.Specular = float4(lightResult.Specular, saturate(dot(input.Position, FogVector)));
    return output;
}

VSOutputTx VSBasicVertexLightingTexture(VSInputNmTx input)
{
    VSOutputTx output;
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

VSOutputTx VSBasicVertexLightingTextureVertexColor(VSInputNmTxVc input)
{
    VSOutputTx output;
    float4 pos_ws = mul(input.Position, World);
    float3 eyeVector = normalize(EyePosition - pos_ws.xyz);
    float3 worldNormal = normalize(mul(input.Normal, WorldInverseTranspose));

    ColorPair lightResult = ComputeLights(eyeVector, worldNormal, 3);
    
    output.PositionPS = mul(input.Position, WorldViewProj);
    output.Diffuse = float4(lightResult.Diffuse, DiffuseColor.a) * input.Color;
    output.Specular = float4(lightResult.Specular, saturate(dot(input.Position, FogVector)));
    output.TexCoord = input.TexCoord;
    return output;
}

VSOutput VSBasicOneLight(VSInputNm input)
{
    VSOutput output;
    float4 pos_ws = mul(input.Position, World);
    float3 eyeVector = normalize(EyePosition - pos_ws.xyz);
    float3 worldNormal = normalize(mul(input.Normal, WorldInverseTranspose));

    ColorPair lightResult = ComputeLights(eyeVector, worldNormal, 1);
    
    output.PositionPS = mul(input.Position, WorldViewProj);
    output.Diffuse = float4(lightResult.Diffuse, DiffuseColor.a);
    output.Specular = float4(lightResult.Specular, saturate(dot(input.Position, FogVector)));
    return output;
}

VSOutput VSBasicOneLightVertexColor(VSInputNmVc input)
{
    VSOutput output;
    float4 pos_ws = mul(input.Position, World);
    float3 eyeVector = normalize(EyePosition - pos_ws.xyz);
    float3 worldNormal = normalize(mul(input.Normal, WorldInverseTranspose));

    ColorPair lightResult = ComputeLights(eyeVector, worldNormal, 1);
    
    output.PositionPS = mul(input.Position, WorldViewProj);
    output.Diffuse = float4(lightResult.Diffuse, DiffuseColor.a) * input.Color;
    output.Specular = float4(lightResult.Specular, saturate(dot(input.Position, FogVector)));
    return output;
}

VSOutputTx VSBasicOneLightTexture(VSInputNmTx input)
{
    VSOutputTx output;
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

VSOutputTx VSBasicOneLightTextureVertexColor(VSInputNmTxVc input)
{
    VSOutputTx output;
    float4 pos_ws = mul(input.Position, World);
    float3 eyeVector = normalize(EyePosition - pos_ws.xyz);
    float3 worldNormal = normalize(mul(input.Normal, WorldInverseTranspose));

    ColorPair lightResult = ComputeLights(eyeVector, worldNormal, 1);
    
    output.PositionPS = mul(input.Position, WorldViewProj);
    output.Diffuse = float4(lightResult.Diffuse, DiffuseColor.a) * input.Color;
    output.Specular = float4(lightResult.Specular, saturate(dot(input.Position, FogVector)));
    output.TexCoord = input.TexCoord;
    return output;
}


// Vertex Shader - Pixel Lighting


VSOutputPixelLighting VSBasicPixelLighting(VSInputNm input)
{
    VSOutputPixelLighting output;
    output.PositionPS = mul(input.Position, WorldViewProj);
    output.PositionWS = float4(mul(input.Position, World).xyz, saturate(dot(input.Position, FogVector)));
    output.NormalWS = normalize(mul(input.Normal, WorldInverseTranspose));
    output.Diffuse = float4(1, 1, 1, DiffuseColor.a);
    return output;
}

VSOutputPixelLighting VSBasicPixelLightingVertexColor(VSInputNmVc input)
{
    VSOutputPixelLighting output;
    output.PositionPS = mul(input.Position, WorldViewProj);
    output.PositionWS = float4(mul(input.Position, World).xyz, saturate(dot(input.Position, FogVector)));
    output.NormalWS = normalize(mul(input.Normal, WorldInverseTranspose));
    output.Diffuse.rgb = input.Color.rgb;
    output.Diffuse.a = input.Color.a * DiffuseColor.a;
    return output;
}

VSOutputPixelLightingTx VSBasicPixelLightingTexture(VSInputNmTx input)
{
    VSOutputPixelLightingTx output;
    output.PositionPS = mul(input.Position, WorldViewProj);
    output.PositionWS = float4(mul(input.Position, World).xyz, saturate(dot(input.Position, FogVector)));
    output.NormalWS = normalize(mul(input.Normal, WorldInverseTranspose));
    output.Diffuse = float4(1, 1, 1, DiffuseColor.a);
    output.TexCoord = input.TexCoord;
    return output;
}

VSOutputPixelLightingTx VSBasicPixelLightingTextureVertexColor(VSInputNmTxVc input)
{
    VSOutputPixelLightingTx output;
    output.PositionPS = mul(input.Position, WorldViewProj);
    output.PositionWS = float4(mul(input.Position, World).xyz, saturate(dot(input.Position, FogVector)));
    output.NormalWS = normalize(mul(input.Normal, WorldInverseTranspose));
    output.Diffuse.rgb = input.Color.rgb;
    output.Diffuse.a = input.Color.a * DiffuseColor.a;
    output.TexCoord = input.TexCoord;
    return output;
}


// Pixel Shader


float4 PSBasic(PSInput input) : SV_Target0
{
    float4 color = input.Diffuse;
    color.rgb = lerp(color.rgb, FogColor * color.a, input.Specular.w);
    return color;
}

float4 PSBasicNoFog(PSInputNoFog input) : SV_Target0
{
    return input.Diffuse;
}

float4 PSBasicTexture(PSInputTx input) : SV_Target0
{
    float4 color = Texture.Sample(TextureSampler, input.TexCoord) * input.Diffuse;
    color.rgb = lerp(color.rgb, FogColor * color.a, input.Specular.w);
    return color;
}

float4 PSBasicTextureNoFog(PSInputTxNoFog input) : SV_Target0
{
    return Texture.Sample(TextureSampler, input.TexCoord) * input.Diffuse;
}

float4 PSBasicVertexLighting(PSInput input) : SV_Target0
{
    float4 color = input.Diffuse;
    color.rgb += input.Specular.rgb * color.a;
    color.rgb = lerp(color.rgb, FogColor * color.a, input.Specular.w);
    return color;
}

float4 PSBasicVertexLightingNoFog(PSInput input) : SV_Target0
{
    float4 color = input.Diffuse;
    color.rgb += input.Specular.rgb * color.a;
    return color;
}

float4 PSBasicVertexLightingTexture(PSInputTx input) : SV_Target0
{
    float4 color = Texture.Sample(TextureSampler, input.TexCoord) * input.Diffuse;
    color.rgb += input.Specular.rgb * color.a;
    color.rgb = lerp(color.rgb, FogColor * color.a, input.Specular.w);
    return color;
}

float4 PSBasicVertexLightingTextureNoFog(PSInputTx input) : SV_Target0
{
    float4 color = Texture.Sample(TextureSampler, input.TexCoord) * input.Diffuse;
    color.rgb += input.Specular.rgb * color.a;
    return color;
}

float4 PSBasicPixelLighting(PSInputPixelLighting input) : SV_Target0
{
    float4 color = input.Diffuse;

    float3 eyeVector = normalize(EyePosition - input.PositionWS.xyz);
    float3 worldNormal = normalize(input.NormalWS);
    
    ColorPair lightResult = ComputeLights(eyeVector, worldNormal, 3);

    color.rgb *= lightResult.Diffuse;
    color.rgb += lightResult.Specular * color.a;
    color.rgb = lerp(color.rgb, FogColor * color.a, input.PositionWS.w);
    return color;
}

float4 PSBasicPixelLightingTexture(PSInputPixelLightingTx input) : SV_Target0
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

technique10 BasicEffect
{
	pass Basic
	{
		SetVertexShader(CompileShader(vs_4_0, VSBasic()));
		SetPixelShader(CompileShader(ps_4_0, PSBasic()));
	}
}

technique10 BasicEffectNoFog
{
	pass Basic
	{
		SetVertexShader(CompileShader(vs_4_0, VSBasicNoFog()));
		SetPixelShader(CompileShader(ps_4_0, PSBasicNoFog()));
	}
}

technique10 BasicEffectVertexColor
{
	pass Basic
	{
		SetVertexShader(CompileShader(vs_4_0, VSBasicVertexColor()));
		SetPixelShader(CompileShader(ps_4_0, PSBasic()));
	}
}

technique10 BasicEffectVertexColorNoFog
{
	pass Basic
	{
		SetVertexShader(CompileShader(vs_4_0, VSBasicVertexColorNoFog()));
		SetPixelShader(CompileShader(ps_4_0, PSBasicNoFog()));
	}
}

technique10 BasicEffectTexture
{
	pass Basic
	{
		SetVertexShader(CompileShader(vs_4_0, VSBasicTexture()));
		SetPixelShader(CompileShader(ps_4_0, PSBasicTexture()));
	}
}

technique10 BasicEffectTextureNoFog
{
	pass Basic
	{
		SetVertexShader(CompileShader(vs_4_0, VSBasicTextureNoFog()));
		SetPixelShader(CompileShader(ps_4_0, PSBasicTextureNoFog()));
	}
}

technique10 BasicEffectTextureVertexColor
{
	pass Basic
	{
		SetVertexShader(CompileShader(vs_4_0, VSBasicTextureVertexColor()));
		SetPixelShader(CompileShader(ps_4_0, PSBasicTexture()));
	}
}

technique10 BasicEffectTextureVertexColorNoFog
{
	pass Basic
	{
		SetVertexShader(CompileShader(vs_4_0, VSBasicTextureVertexColorNoFog()));
		SetPixelShader(CompileShader(ps_4_0, PSBasicTextureNoFog()));
	}
}


// Vertex Lighting


technique10 BasicEffectVertexLighting
{
	pass Basic
	{
		SetVertexShader(CompileShader(vs_4_0, VSBasicVertexLighting()));
		SetPixelShader(CompileShader(ps_4_0, PSBasicVertexLighting()));
	}
}

technique10 BasicEffectVertexLightingNoFog
{
	pass Basic
	{
		SetVertexShader(CompileShader(vs_4_0, VSBasicVertexLighting()));
		SetPixelShader(CompileShader(ps_4_0, PSBasicVertexLightingNoFog()));
	}
}

technique10 BasicEffectVertexLightingVertexColor
{
	pass Basic
	{
		SetVertexShader(CompileShader(vs_4_0, VSBasicVertexLightingVertexColor()));
		SetPixelShader(CompileShader(ps_4_0, PSBasicVertexLighting()));
	}
}

technique10 BasicEffectVertexLightingVertexColorNoFog
{
	pass Basic
	{
		SetVertexShader(CompileShader(vs_4_0, VSBasicVertexLightingVertexColor()));
		SetPixelShader(CompileShader(ps_4_0, PSBasicVertexLightingNoFog()));
	}
}

technique10 BasicEffectVertexLightingTexture
{
	pass Basic
	{
		SetVertexShader(CompileShader(vs_4_0, VSBasicVertexLightingTexture()));
		SetPixelShader(CompileShader(ps_4_0, PSBasicVertexLightingTexture()));
	}
}

technique10 BasicEffectVertexLightingTextureNoFog
{
	pass Basic
	{
		SetVertexShader(CompileShader(vs_4_0, VSBasicVertexLightingTexture()));
		SetPixelShader(CompileShader(ps_4_0, PSBasicVertexLightingTextureNoFog()));
	}
}

technique10 BasicEffectVertexLightingTextureVertexColor
{
	pass Basic
	{
		SetVertexShader(CompileShader(vs_4_0, VSBasicVertexLightingTextureVertexColor()));
		SetPixelShader(CompileShader(ps_4_0, PSBasicVertexLightingTexture()));
	}
}

technique10 BasicEffectVertexLightingTextureVertexColorNoFog
{
	pass Basic
	{
		SetVertexShader(CompileShader(vs_4_0, VSBasicVertexLightingTextureVertexColor()));
		SetPixelShader(CompileShader(ps_4_0, PSBasicVertexLightingTextureNoFog()));
	}
}


// One Light


technique10 BasicEffectOneLight
{
	pass Basic
	{
		SetVertexShader(CompileShader(vs_4_0, VSBasicOneLight()));
		SetPixelShader(CompileShader(ps_4_0, PSBasicVertexLighting()));
	}
}

technique10 BasicEffectOneLightNoFog
{
	pass Basic
	{
		SetVertexShader(CompileShader(vs_4_0, VSBasicOneLight()));
		SetPixelShader(CompileShader(ps_4_0, PSBasicVertexLightingNoFog()));
	}
}

technique10 BasicEffectOneLightVertexColor
{
	pass Basic
	{
		SetVertexShader(CompileShader(vs_4_0, VSBasicOneLightVertexColor()));
		SetPixelShader(CompileShader(ps_4_0, PSBasicVertexLighting()));
	}
}

technique10 BasicEffectOneLightVertexColorNoFog
{
	pass Basic
	{
		SetVertexShader(CompileShader(vs_4_0, VSBasicOneLightVertexColor()));
		SetPixelShader(CompileShader(ps_4_0, PSBasicVertexLightingNoFog()));
	}
}

technique10 BasicEffectOneLightTexture
{
	pass Basic
	{
		SetVertexShader(CompileShader(vs_4_0, VSBasicOneLightTexture()));
		SetPixelShader(CompileShader(ps_4_0, PSBasicVertexLightingTexture()));
	}
}

technique10 BasicEffectOneLightTextureNoFog
{
	pass Basic
	{
		SetVertexShader(CompileShader(vs_4_0, VSBasicOneLightTexture()));
		SetPixelShader(CompileShader(ps_4_0, PSBasicVertexLightingTextureNoFog()));
	}
}

technique10 BasicEffectOneLightTextureVertexColor
{
	pass Basic
	{
		SetVertexShader(CompileShader(vs_4_0, VSBasicOneLightTextureVertexColor()));
		SetPixelShader(CompileShader(ps_4_0, PSBasicVertexLightingTexture()));
	}
}

technique10 BasicEffectOneLightTextureVertexColorNoFog
{
	pass Basic
	{
		SetVertexShader(CompileShader(vs_4_0, VSBasicOneLightTextureVertexColor()));
		SetPixelShader(CompileShader(ps_4_0, PSBasicVertexLightingTextureNoFog()));
	}
}


// Pixel Lighting


technique10 BasicEffectPixelLighting
{
	pass Basic
	{
		SetVertexShader(CompileShader(vs_4_0, VSBasicPixelLighting()));
		SetPixelShader(CompileShader(ps_4_0, PSBasicPixelLighting()));
	}
}

technique10 BasicEffectPixelLightingNoFog
{
	pass Basic
	{
		SetVertexShader(CompileShader(vs_4_0, VSBasicPixelLighting()));
		SetPixelShader(CompileShader(ps_4_0, PSBasicPixelLighting()));
	}
}

technique10 BasicEffectPixelLightingVertexColor
{
	pass Basic
	{
		SetVertexShader(CompileShader(vs_4_0, VSBasicPixelLightingVertexColor()));
		SetPixelShader(CompileShader(ps_4_0, PSBasicPixelLighting()));
	}
}

technique10 BasicEffectPixelLightingVertexColorNoFog
{
	pass Basic
	{
		SetVertexShader(CompileShader(vs_4_0, VSBasicPixelLightingVertexColor()));
		SetPixelShader(CompileShader(ps_4_0, PSBasicPixelLighting()));
	}
}

technique10 BasicEffectPixelLightingTexture
{
	pass Basic
	{
		SetVertexShader(CompileShader(vs_4_0, VSBasicPixelLightingTexture()));
		SetPixelShader(CompileShader(ps_4_0, PSBasicPixelLightingTexture()));
	}
}

technique10 BasicEffectPixelLightingTextureNoFog
{
	pass Basic
	{
		SetVertexShader(CompileShader(vs_4_0, VSBasicPixelLightingTexture()));
		SetPixelShader(CompileShader(ps_4_0, PSBasicPixelLightingTexture()));
	}
}

technique10 BasicEffectPixelLightingTextureVertexColor
{
	pass Basic
	{
		SetVertexShader(CompileShader(vs_4_0, VSBasicPixelLightingTextureVertexColor()));
		SetPixelShader(CompileShader(ps_4_0, PSBasicPixelLightingTexture()));
	}
}

technique10 BasicEffectPixelLightingTextureVertexColorNoFog
{
	pass Basic
	{
		SetVertexShader(CompileShader(vs_4_0, VSBasicPixelLightingTextureVertexColor()));
		SetPixelShader(CompileShader(ps_4_0, PSBasicPixelLightingTexture()));
	}
}
