#version 400
uniform sampler2DArray uSampler;

in vec3 vTexturePos;
in vec4 vColor;

out vec4 color;

void main(void)
{
	vec4 tex = texture(uSampler, vTexturePos);
    color = vec4(tex.rgb*0.5+vColor.rgb*0.5,tex.a*vColor.a);
}