// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

vertexglobal
{
  uniform vec4 DiffuseColor;
  uniform mat4 WorldViewProj;
  uniform vec4 FogVector;
  
  attribute vec4 pos;
  attribute vec4 col;
  attribute vec2 tex;
  
  varying vec2 diffuseTexCoord;
  varying vec4 diffuseColor;
  varying vec4 specular;
}

vertexshaders
{
  shader "VSAlphaTest"
  {
    void main( )
    {
      gl_Position = WorldViewProj * pos;
      diffuseTexCoord = tex;
      diffuseColor = DiffuseColor;
      specular = vec4(0, 0, 0, saturate(dot(pos, FogVector)));
    }
  }
  
  shader "VSAlphaTestNoFog"
  {
    void main( )
    {
      gl_Position = WorldViewProj * pos;
      diffuseTexCoord = tex;
      diffuseColor = DiffuseColor;
    }
  }
  
  shader "VSAlphaTestVertexColor"
  {
    void main( )
    {
      gl_Position = WorldViewProj * pos;
      diffuseTexCoord = tex;
      diffuseColor = DiffuseColor * col;
      specular = vec4(0, 0, 0, saturate(dot(pos, FogVector)));
    }
  }
  
  shader "VSAlphaTestVertexColorNoFog"
  {
    void main( )
    {
      gl_Position = WorldViewProj * pos;
      diffuseTexCoord = tex;
      diffuseColor = DiffuseColor * col;
    }
  }
}

fragmentglobal
{
  uniform sampler2D Texture;
  uniform vec4 AlphaTest;
  uniform vec3 FogColor;
  
  varying vec2 diffuseTexCoord;
  varying vec4 diffuseColor;
  varying vec4 specular;
}

fragmentshaders
{
  shader "PSAlphaTestLtGt"
  {
    void main( )
    {
      vec4 color = texture2D(Texture, diffuseTexCoord) * diffuseColor;
      if(((abs(color.a - AlphaTest.x) < AlphaTest.y) ? AlphaTest.z : AlphaTest.w) < 0)
        discard;
      color.rgb = mix(color.rgb, FogColor * color.a, specular.w);
      gl_FragColor = color;
    }
  }
  
  shader "PSAlphaTestLtGtNoFog"
  {
    void main( )
    {
      vec4 color = texture2D(Texture, diffuseTexCoord) * diffuseColor;
      if(((abs(color.a - AlphaTest.x) < AlphaTest.y) ? AlphaTest.z : AlphaTest.w) < 0)
        discard;
      gl_FragColor = color;
    }
  }
  
  shader "PSAlphaTestEqNe"
  {
    void main( )
    {
      vec4 color = texture2D(Texture, diffuseTexCoord) * diffuseColor;
      if(((abs(color.a - AlphaTest.x) < AlphaTest.y) ? AlphaTest.z : AlphaTest.w) < 0)
        discard;
      color.rgb = mix(color.rgb, FogColor * color.a, specular.w);
      gl_FragColor = color;
    }
  }
  
  shader "PSAlphaTestEqNeNoFog"
  {
    void main( )
    {
      vec4 color = texture2D(Texture, diffuseTexCoord) * diffuseColor;
      if(((abs(color.a - AlphaTest.x) < AlphaTest.y) ? AlphaTest.z : AlphaTest.w) < 0)
        discard;
      gl_FragColor = color;
    }
  }
}

techniques
{
  technique "AlphaTestLtGt"
  {
		vertex "VSAlphaTest";
		fragment "PSAlphaTestLtGt";
  }

  technique "AlphaTestNoFogLtGt"
  {
		vertex "VSAlphaTestNoFog"
		fragment "PSAlphaTestLtGtNoFog"
  }

  technique "AlphaTestVertexColorLtGt"
  {
		vertex "VSAlphaTestVertexColor"
		fragment "PSAlphaTestLtGt"
  }

  technique "AlphaTestVertexColorNoFogLtGt"
  {
		vertex "VSAlphaTestVertexColorNoFog"
		fragment "PSAlphaTestLtGtNoFog"
  }

  technique "AlphaTestEqNe"
  {
		vertex "VSAlphaTest"
		fragment "PSAlphaTestEqNe"
  }

  technique "AlphaTestNoFogEqNe"
  {
		vertex "VSAlphaTestNoFog"
		fragment "PSAlphaTestEqNeNoFog"
  }

  technique "AlphaTestVertexColorEqNe"
  {
		vertex "VSAlphaTestVertexColor"
		fragment "PSAlphaTestEqNe"
  }

  technique "AlphaTestVertexColorNoFogEqNe"
  {
		vertex "VSAlphaTestVertexColorNoFog"
		fragment "PSAlphaTestEqNeNoFog"
  }
}