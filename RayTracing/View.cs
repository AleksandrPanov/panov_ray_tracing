using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.IO;
namespace RayTracing
{
    class View
    {
        int BasicProgramID;
        int BasicVertexSheder;
        int BasicFragmentShader;
        Vector3 []vertdata;
        int vboPos;

        int uniform_pos;
        Vector3 campos;
        int uniform_aspect;
        Vector3 aspect;
        public int attribute_vpos;

        public void Setup(int w, int h)
        {
            string str = GL.GetString(StringName.ShadingLanguageVersion);
            GL.ClearColor(Color.DarkGray);
            GL.ShadeModel(ShadingModel.Smooth);

            Matrix4 perspMat = Matrix4.CreatePerspectiveFieldOfView(
                MathHelper.PiOver4,
                w / (float)h,
                1,
                64);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref perspMat);

            Matrix4 viewMat = Matrix4.LookAt(0, 0, 3, 0, 0, 0, 0, 1, 0);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref viewMat);


            InitSheders();
            InitBuffer();
        }
        public void Update()
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.Enable(EnableCap.DepthTest);
            GL.EnableVertexAttribArray(attribute_vpos);
            GL.DrawArrays(PrimitiveType.Quads, 0, 4);
            GL.DisableVertexAttribArray(attribute_vpos);

        }
        public View()
        {         
            vertdata = new Vector3[]{
                new Vector3(-1f,-1f,0f),
                new Vector3(1f,-1f,0f),
                new Vector3(1f,1f,0f),
                new Vector3(-1f,1f,0f)
            };
        }
        void loadShader(String filename, ShaderType type, int program, out int address)
        {
            address = GL.CreateShader(type);//создали объект шейдера типа type
            using (System.IO.StreamReader sr = new StreamReader(filename))
            {
                GL.ShaderSource(address, sr.ReadToEnd());//загружает исходный код в созданный шейдерный объект
            }
            GL.CompileShader(address);//compile
            GL.AttachShader(program, address);//link
           // Console.WriteLine(GL.GetShaderInfoLog(address));
        }
        private void InitBuffer()
        {
            GL.GenBuffers(1, out vboPos);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vboPos);
            GL.BufferData<Vector3>(BufferTarget.ArrayBuffer, (IntPtr)(vertdata.Length *
                Vector3.SizeInBytes), vertdata, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(attribute_vpos, 3, VertexAttribPointerType.Float, false, 0, 0);
            GL.Uniform3(uniform_pos,campos);
            GL.Uniform3(uniform_aspect, aspect);
            GL.UseProgram(BasicProgramID);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }
        private void InitSheders()
        {
            BasicProgramID = GL.CreateProgram();
            loadShader("..\\..\\raytracing.vert", ShaderType.VertexShader, 
                BasicProgramID, out BasicVertexSheder);
            loadShader("..\\..\\raytracing.frag", ShaderType.FragmentShader,
                BasicProgramID, out BasicFragmentShader);
            GL.LinkProgram(BasicProgramID);
            int status = 0;
            GL.GetProgram(BasicProgramID, GetProgramParameterName.LinkStatus, out status);
            System.Windows.Forms.MessageBox.Show(GL.GetProgramInfoLog(BasicProgramID));
        }
    }
}
