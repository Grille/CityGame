uniform sampler2D uSampler;

in vec2 vTexturePos;
in vec4 vColor;

void main(void)
{
  vec4 texture = texture2D(uSampler, vTexturePos);
  gl_FragColor = vec4(texture * vec4(1,0,0,1) );
}