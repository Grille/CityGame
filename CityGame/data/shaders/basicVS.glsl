#version 400

in vec2 aPosition;
in vec2 aTexturePos;
in vec4 aTextureIndex;
in vec4 aColor;

out vec2 vPosition;
out vec2 vTexturePos;
out vec4 vTextureIndex;
out vec4 vColor;

void main(void)
{
  vPosition = aPosition;
  vTexturePos = aTexturePos;
  vTextureIndex = aTextureIndex;
  vColor = aColor;
  gl_Position = vec4(aPosition,0, 1);
}