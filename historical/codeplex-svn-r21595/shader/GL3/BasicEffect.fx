// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

//TODO: dummy implementation / placeholder

vertexshaders
{
  shader "SpriteVertexShader"
  {
    uniform mat4 MatrixTransform;

    attribute vec4 pos;
    attribute vec4 col;
    attribute vec2 tex;

    varying vec4 diffuseColor;
    varying vec2 diffuseTexCoord;
    void main( )
    {
      gl_Position = MatrixTransform * pos;
      diffuseTexCoord = tex;
      diffuseColor = col;
    }
  }
}

fragmentshaders
{
  shader "SpriteFragmentShader"
  {
    uniform sampler2D Texture;

    varying vec4 diffuseColor;
    varying vec2 diffuseTexCoord;
    void main( )
    {
      gl_FragColor = texture2D(Texture, diffuseTexCoord) * diffuseColor;
    }
  }
}

techniques
{
  technique "SpriteTechnique"
  {
    vertex "SpriteVertexShader"
    fragment "SpriteFragmentShader"
  }
}