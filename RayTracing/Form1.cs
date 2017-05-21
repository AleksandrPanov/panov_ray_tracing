using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace RayTracing
{
    public partial class Form1 : Form
    {
        View view;
        public Form1()
        {           
            InitializeComponent();
            view = new View();
        }

        private void glControl1_Paint(object sender, PaintEventArgs e)
        {
            view.Update();
            glControl1.SwapBuffers();
            GL.UseProgram(0);
        }

        private void glControl1_Load(object sender, EventArgs e)
        {
            GL.Viewport(0, 0, glControl1.Width, glControl1.Height);
            view.Setup(glControl1.Width, glControl1.Height);
        }
    }
}
