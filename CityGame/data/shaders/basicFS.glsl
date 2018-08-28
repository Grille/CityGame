#400


uniform sampler2D uSampler;
uniform vec2 uResolution;
uniform float uTime;

uniform int uInt0;

in vec2 vPosition;
in vec2 vTexturePos;
in vec4 vColor;

bool equel(inout vec4 data,vec3 equel);
bool replace(inout vec4 data,vec3 equel,vec3 replace);
void dayTime(inout vec4 texture);

void main(void)
{
  vec4 textureColor = texture(uSampler, vTexturePos);
  vec4 color = (vColor/vec4(255,255,255,255));
  //dayTime(texture);
  //float durs = (float)(max(texture.r,max(texture.g,texture.b)))/3f+0.5f;
  gl_FragColor = (vec4(textureColor * color));
}

void dayTime(inout vec4 textureColor)
{
/*
  bool night = false;
  if (uInt0 >= 20|| uInt0 < 8=){
    //replace(textureColor,vec3(105,147,255),vec3(255,255,255));
    //replace(textureColor,vec3(74,125,255),vec3(240,240,222));
    if (replace(textureColor,vec3(77,93,119),vec3(255,255,200)));
    else if (replace(textureColor,vec3(62,75,96),vec3(128,128,100)));
    else textureColor = vec4(textureColor.r*0.6,textureColor.g*0.6,textureColor.b*0.8,textureColor.a);
  }
  */
}

bool equel(inout vec4 data,vec3 equel)
{
	if (int(data.r*255)==equel.r && int(data.g*255)==equel.g && int(data.b*255)==equel.b)
	return true;
	return false;
}
bool replace(inout vec4 data,vec3 equel,vec3 replace)
{
	if (int(data.r*255)==equel.r && int(data.g*255)==equel.g && int(data.b*255)==equel.b){
		data = vec4(vec3(replace.r/255,replace.g/255,replace.b/255),data.a);
		return true;
	}
	return false;
}