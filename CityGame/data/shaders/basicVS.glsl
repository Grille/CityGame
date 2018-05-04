
#400
uniform int uTime;

in vec2 aPosition;
in vec2 aTexturePos;
in vec4 aColor;

varying vec2 vPosition;
varying vec2 vTexturePos;
varying vec4 vColor;


void main(void)
{
  vPosition = aPosition;
  vTexturePos = aTexturePos;
  vColor = aColor;
  gl_Position = vec4(vPosition,1, 1);
}