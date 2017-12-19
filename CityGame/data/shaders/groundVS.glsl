in vec2 aPosition;
in vec2 aTexturePos;
in vec4 aColor;

out vec2 vTexturePos;
out vec4 vColor;

void main(void)
{
  vTexturePos = aTexturePos;
  vColor = aColor;
  gl_Position = vec4(aPosition,0, 1);
}