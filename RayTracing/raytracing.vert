in vec3 vPos;
out vec3 glPos;

void main(void)
{
	gl_pos = vec4(vPos, 1.0);
	glPos = vPos;
}