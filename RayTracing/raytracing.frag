﻿#version 400
#define EPSILON = 0.001
#define BIG = 1000000.0
const int DIFFUSE = 1;
const int REFLECTION = 2;
const int REFRACTION = 3;

out vec4 FragColor;
in vec3 glPosition;


struct SSphere
{
 vec3 Center;
 float Radius;
 int MaterialIdx;
};
struct STriangle
{
 vec3 v1;
 vec3 v2;
 vec3 v3;
 int MaterialIdx;
};
struct SCamera
{
 vec3 Position;
 vec3 View;
 vec3 Up;
 vec3 Side;
 // отношение сторон выходного изображения
 vec2 Scale;
};
struct SRay
{
 vec3 Origin;
 vec3 Direction;
};
{
	vec2 coords = glPosition.xy * uCamera.Scale;
	vec3 direction = uCamera.View + uCamera.Side * coords.x + uCamera.Up * coords.y;
	return SRay ( uCamera.Position, normalize(direction) );
}
SCamera initializeDefaultCamera()

{	 SCamera camera;
	 camera.Position = vec3(0.0, 0.0, -8.0);
	 camera.View = vec3(0.0, 0.0, 1.0);
	 camera.Up = vec3(0.0, 1.0, 0.0);
	 camera.Side = vec3(1.0, 0.0, 0.0);
	 camera.Scale = vec2(1.0);
	 return camera;
}



void main( void )
{
	SCamera uCamera = initializeDefaultCamera();
	SRay ray = GenerateRay( uCamera);
	FragColor = vec4 ( abs(ray.Direction.xy), 0, 1.0);
}