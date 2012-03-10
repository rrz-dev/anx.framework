//
// This file is part of the ANX.Framework created by the "ANX.Framework developer group".
//
// This file is released under the Ms-PL license.
//
//
//
// Microsoft Public License (Ms-PL)
//
// This license governs use of the accompanying software. If you use the software, you accept this license. 
// If you do not accept the license, do not use the software.
//
// 1.Definitions
//   The terms "reproduce," "reproduction," "derivative works," and "distribution" have the same meaning 
//   here as under U.S. copyright law.
//   A "contribution" is the original software, or any additions or changes to the software.
//   A "contributor" is any person that distributes its contribution under this license.
//   "Licensed patents" are a contributor's patent claims that read directly on its contribution.
//
// 2.Grant of Rights
//   (A) Copyright Grant- Subject to the terms of this license, including the license conditions and limitations 
//       in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free copyright license to 
//       reproduce its contribution, prepare derivative works of its contribution, and distribute its contribution
//       or any derivative works that you create.
//   (B) Patent Grant- Subject to the terms of this license, including the license conditions and limitations in 
//       section 3, each contributor grants you a non-exclusive, worldwide, royalty-free license under its licensed
//       patents to make, have made, use, sell, offer for sale, import, and/or otherwise dispose of its contribution 
//       in the software or derivative works of the contribution in the software.
//
// 3.Conditions and Limitations
//   (A) No Trademark License- This license does not grant you rights to use any contributors' name, logo, or trademarks.
//   (B) If you bring a patent claim against any contributor over patents that you claim are infringed by the software, your 
//       patent license from such contributor to the software ends automatically.
//   (C) If you distribute any portion of the software, you must retain all copyright, patent, trademark, and attribution 
//       notices that are present in the software.
//   (D) If you distribute any portion of the software in source code form, you may do so only under this license by including
//       a complete copy of this license with your distribution. If you distribute any portion of the software in compiled or 
//       object code form, you may only do so under a license that complies with this license.
//   (E) The software is licensed "as-is." You bear the risk of using it. The contributors give no express warranties, guarantees,
//       or conditions. You may have additional consumer rights under your local laws which this license cannot change. To the
//       extent permitted under your local laws, the contributors exclude the implied warranties of merchantability, fitness for a 
//       particular purpose and non-infringement.

//TODO: dummy implementation / placeholder

Texture2D<float4> Texture : register(t0);
sampler Sampler : register(s0);

uniform extern float4x4 World;
uniform extern float4x4 View;
uniform extern float4x4 Projection;

struct VSInput
{
	float4 Position		: SV_POSITION;
};

struct VertexColorVSInput
{
	float4 Position		: SV_POSITION;
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
