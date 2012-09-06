// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

vertexglobal
{
	uniform vec4 DiffuseColor;
	uniform vec4 FogVector;
    uniform mat4 WorldViewProj;
}

vertexshaders
{
  shader "VSDualTexture"
  {
    attribute vec4 pos;
    attribute vec2 tex;
    attribute vec2 tex2;
	
    varying vec4 diffuse;
    varying vec4 specular;
    varying vec2 diffuseTexCoord;
    varying vec2 diffuseTexCoord2;
    void main( )
    {
      gl_Position = WorldViewProj * pos;
	  diffuse = DiffuseColor;
      specular = vec4(0, 0, 0, saturate(dot(pos, FogVector)));
      diffuseTexCoord = tex;
      diffuseTexCoord2 = tex2;
    }
  }
  
  shader "VSDualTextureNoFog"
  {
    attribute vec4 pos;
    attribute vec2 tex;
    attribute vec2 tex2;
	
    varying vec4 diffuse;
    varying vec2 diffuseTexCoord;
    varying vec2 diffuseTexCoord2;
    void main( )
    {
      gl_Position = WorldViewProj * pos;
	  diffuse = DiffuseColor;
      diffuseTexCoord = tex;
      diffuseTexCoord2 = tex2;
    }
  }
  
  shader "VSDualTextureVertexColor"
  {
    attribute vec4 pos;
    attribute vec2 tex;
    attribute vec2 tex2;
    attribute vec4 col;
	
    varying vec4 diffuse;
    varying vec4 specular;
    varying vec2 diffuseTexCoord;
    varying vec2 diffuseTexCoord2;
    void main( )
    {
      gl_Position = WorldViewProj * pos;
	  diffuse = DiffuseColor * col;
      specular = vec4(0, 0, 0, saturate(dot(pos, FogVector)));
      diffuseTexCoord = tex;
      diffuseTexCoord2 = tex2;
    }
  }
  
  shader "VSDualTextureVertexColorNoFog"
  {
    attribute vec4 pos;
    attribute vec2 tex;
    attribute vec2 tex2;
    attribute vec4 col;
	
    varying vec4 diffuse;
    varying vec2 diffuseTexCoord;
    varying vec2 diffuseTexCoord2;
    void main( )
    {
      gl_Position = WorldViewProj * pos;
	  diffuse = DiffuseColor * col;
      diffuseTexCoord = tex;
      diffuseTexCoord2 = tex2;
    }
  }
}

fragmentglobal
{
  uniform sampler2D Texture;
  uniform sampler2D Texture2;
  uniform vec3 FogColor;
}

fragmentshaders
{
  shader "PSDualTexture"
  {
    varying vec4 diffuse;
    varying vec4 specular;
    varying vec2 diffuseTexCoord;
    varying vec2 diffuseTexCoord2;
    void main( )
    {
	  vec4 color = texture2D(Texture, diffuseTexCoord);
	  vec4 overlay = texture2D(Texture2, diffuseTexCoord2);
	  color.rgb *= 2;
	  color *= overlay * diffuse;
	  color.rgb = lerp(color.rgb, FogColor * color.a, specular.w);
      gl_FragColor = color;
    }
  }
  
  shader "PSDualTextureNoFog"
  {
    varying vec4 diffuse;
    varying vec2 diffuseTexCoord;
    varying vec2 diffuseTexCoord2;
    void main( )
    {
	  vec4 color = texture2D(Texture, diffuseTexCoord);
	  vec4 overlay = texture2D(Texture2, diffuseTexCoord2);
	  color.rgb *= 2;
	  color *= overlay * diffuse;
      gl_FragColor = color;
    }
  }
}

techniques
{
  technique "DualTextureEffect"
  {
    vertex "VSDualTexture"
    fragment "PSDualTexture"
  }
  
  technique "DualTextureEffectNoFog"
  {
    vertex "VSDualTextureNoFog"
    fragment "PSDualTextureNoFog"
  }
  
  technique "DualTextureEffectVertexColor"
  {
    vertex "VSDualTextureVertexColor"
    fragment "PSDualTexture"
  }
  
  technique "DualTextureEffectNoFogVertexColor"
  {
    vertex "VSDualTextureVertexColorNoFog"
    fragment "PSDualTextureNoFog"
  }
}