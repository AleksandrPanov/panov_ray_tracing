#version 450

in vec3 vPosition;
out vec3 glPosition; //перед дальше по конвейеру

void main(void)//перекладывает вычисленные коорд вершин в выходную переменную
{
	gl_Position = vec4(vPosition, 1.0);
	glPosition = vPosition;
}