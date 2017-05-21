#version 430

out vec4 FragColor;
in vec3 glPos;

void main(void)
{
	FlagColor = vec4 (abs(glPos.xy), 0, 1.0);
}