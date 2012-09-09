// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

struct VertexShaderInput
{
    float4 Position : POSITION0;
	float4 Color    : Color0;
};

struct VertexShaderOutput
{
    float4 Position : POSITION0;
};

struct PixelShaderOutput
{
	float4 Color0 : SV_TARGET0;
	float4 Color1 : SV_TARGET1;
	float4 Color2 : SV_TARGET2;
	float4 Color3 : SV_TARGET3;
};

VertexShaderOutput VertexShaderFunction(VertexShaderInput input)
{
    VertexShaderOutput output;
    output.Position = input.Position;
    return output;
}

PixelShaderOutput PixelShaderFunction(VertexShaderOutput input)
{
	PixelShaderOutput output;

	output.Color0 = float4(1, 0, 0, 1);
	output.Color1 = float4(0, 1, 0, 1);
	output.Color2 = float4(0, 0, 1, 1);
	output.Color3 = float4(0.5, 0, 0.5, 1);

	return output;
}

technique10 Technique1
{
    pass Pass1
    {
        // TODO: Stellen Sie Renderstates hier ein.

        VertexShader = compile vs_4_0 VertexShaderFunction();
        PixelShader = compile ps_4_0 PixelShaderFunction();
    }
}
