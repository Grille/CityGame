#400


uniform sampler2D uSampler;
uniform vec2 uResolution;
uniform int uTime;

in vec2 vPosition;
in vec2 vTexturePos;
in vec4 vColor;


void main(void)
{
  vec4 texture = texture(uSampler, vTexturePos);
  vec4 color = (vColor/vec4(255,255,255,255));
  gl_FragColor = vec4(texture.rgb*0.5+color.rgb*0.5,texture.a*color.a);
}