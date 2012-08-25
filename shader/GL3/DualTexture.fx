// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

//TODO: dummy implementation / placeholder

vertexshaders
{
  shader "VSDualTexture"
  {
    uniform mat4 WorldViewProj;

    attribute vec4 pos;
    attribute vec2 tex;
    attribute vec2 tex2;

    varying vec2 diffuseTexCoord;
    varying vec2 diffuseTexCoord2;
    void main( )
    {
      gl_Position = WorldViewProj * pos;
      diffuseTexCoord = tex;
      diffuseTexCoord2 = tex2;
    }
  }
}

fragmentshaders
{
  shader "PSDualTexture"
  {
    uniform sampler2D Texture;
    uniform sampler2D Texture2;
	
    varying vec2 diffuseTexCoord;
    varying vec2 diffuseTexCoord2;
    void main( )
    {
	  vec4 color = texture2D(Texture, diffuseTexCoord);
	  vec4 overlay = texture2D(Texture2, diffuseTexCoord2);
	  
	  color.rgb *= 2;

	  // TODO
	  //color *= overlay * pin.Diffuse;
      //ApplyFog(color, pin.Specular.w);
    
      gl_FragColor = color;
    }
  }
}

techniques
{
  technique "DualTextureEffect"
  {
    vertex "SpriteVertexShader"
    fragment "SpriteFragmentShader"
  }
}