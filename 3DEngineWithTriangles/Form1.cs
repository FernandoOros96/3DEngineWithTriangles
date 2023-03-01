namespace _3DEngineWithTriangles
{
    public partial class Form1 : Form
    {
        Bitmap bmp;
        Graphics graphic;
        Scene scena;
        double[,] RotationX, RotationY, RotationZ;
        double angle = 0;
        double radianes;
        bool RX = false;
        bool RY = false;
        bool RZ = false;
        public Form1()
        {
            InitializeComponent();
            bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            graphic = Graphics.FromImage(bmp);
            pictureBox1.Image = bmp;
            scena = new Scene();
            timer1.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            radianes = convertirRadiantes(angle);

            if (RX == true)
            {
                RotationX = Matrix.RotationX(radianes);

                for (int i = 0; i < scena.malla.Triangles.Count; i++)
                {
                    Triangle triangle = scena.malla.Triangles[i];

                    for (int j = 0; j < 3; j++)
                    {
                        Vertex vertexX = triangle.Vec3D[j];

                        vertexX = Matrix.multiMatrix(vertexX, RotationX);

                        triangle.Vec3D[j] = vertexX;
                    }
                }
            }






                graphic.Clear(Color.Transparent);

            foreach (Triangle triangle in scena.malla.Triangles)
            {
                Draw(triangle.Vec3D[0], triangle.Vec3D[1]);
                Draw(triangle.Vec3D[1], triangle.Vec3D[2]);
                Draw(triangle.Vec3D[2], triangle.Vec3D[0]);
            }

            pictureBox1.Invalidate();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RX = true;
            RY = false;
            RZ = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            RX = false;
            RY = true;
            RZ = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            RX = false;
            RY = false;
            RZ = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            radianes = convertirRadiantes(angle);

            if (RX == true)
            {
                RotationX = Matrix.RotationX(radianes);

                for (int i = 0; i < scena.malla.Triangles.Count; i++)
                {
                    Triangle triangle = scena.malla.Triangles[i];

                    for (int j = 0; j < 3; j++)
                    {
                        Vertex vertexX = triangle.Vec3D[j];

                        vertexX = Matrix.multiMatrix(vertexX, RotationX);

                        triangle.Vec3D[j] = vertexX;
                    }
                }
            }else if(RY == true)
            {
                RotationY = Matrix.RotationY(radianes);

                for (int i = 0; i < scena.malla.Triangles.Count; i++)
                {
                    Triangle triangle = scena.malla.Triangles[i];

                    for (int j = 0; j < 3; j++)
                    {
                        Vertex vertexY = triangle.Vec3D[j];

                        vertexY = Matrix.multiMatrix(vertexY, RotationY);

                        triangle.Vec3D[j] = vertexY;
                    }
                }
            }
            else if(RZ == true)
            {
                RotationZ = Matrix.RotationZ(radianes);

                for (int i = 0; i < scena.malla.Triangles.Count; i++)
                {
                    Triangle triangle = scena.malla.Triangles[i];

                    for (int j = 0; j < 3; j++)
                    {
                        Vertex vertexZ = triangle.Vec3D[j];

                        vertexZ = Matrix.multiMatrix(vertexZ, RotationZ);

                        triangle.Vec3D[j] = vertexZ;
                    }
                }
            }

            graphic.Clear(Color.Transparent);

            foreach (Triangle triangle in scena.malla.Triangles)
            {
                Draw(triangle.Vec3D[0], triangle.Vec3D[1]);
                Draw(triangle.Vec3D[1], triangle.Vec3D[2]);
                Draw(triangle.Vec3D[2], triangle.Vec3D[0]);
            }

            pictureBox1.Invalidate();
        }

        public void Draw(Vertex uno, Vertex dos)
        {
            graphic.Clear(Color.Black);
            graphic.DrawLine(Pens.Gray, 0, pictureBox1.Height / 2, pictureBox1.Width, pictureBox1.Height / 2);
            graphic.DrawLine(Pens.Gray, pictureBox1.Width / 2, 0, pictureBox1.Width / 2, pictureBox1.Height);

            foreach (var triangle in scena.malla.Triangles)
            {
                var normal = CalculateNormal(triangle.Vec3D[0], triangle.Vec3D[1], triangle.Vec3D[2]);
                var cameraVector = new Vertex(0, 0, -1); // Camera is pointing towards +Z direction

                // Check if triangle is facing towards camera
                if (DotProduct(normal, cameraVector) < 0)
                {
                    // Convert vertex positions to 2D screen coordinates
                    var a = triangle.Vec3D[0].ConvertToPointF(triangle.Vec3D[0].X * 500 / (500 - triangle.Vec3D[0].Z), triangle.Vec3D[0].Y * 500 / (500 - triangle.Vec3D[0].Z));
                    var b = triangle.Vec3D[1].ConvertToPointF(triangle.Vec3D[1].X * 500 / (500 - triangle.Vec3D[1].Z), triangle.Vec3D[1].Y * 500 / (500 - triangle.Vec3D[1].Z));
                    var c = triangle.Vec3D[2].ConvertToPointF(triangle.Vec3D[2].X * 500 / (500 - triangle.Vec3D[2].Z), triangle.Vec3D[2].Y * 500 / (500 - triangle.Vec3D[2].Z));

                    // Move to screen center and draw
                    var center = new PointF(pictureBox1.Width / 2, pictureBox1.Height / 2);
                    graphic.DrawLine(Pens.White, new PointF(a.X + center.X, -a.Y + center.Y), new PointF(b.X + center.X, -b.Y + center.Y));
                    graphic.DrawLine(Pens.White, new PointF(b.X + center.X, -b.Y + center.Y), new PointF(c.X + center.X, -c.Y + center.Y));
                    graphic.DrawLine(Pens.White, new PointF(c.X + center.X, -c.Y + center.Y), new PointF(a.X + center.X, -a.Y + center.Y));
                }
            }
        }

        // Calculates the normal vector of a triangle given its three vertices
        private Vertex CalculateNormal(Vertex a, Vertex b, Vertex c)
        {
            var ab = new Vertex(b.X - a.X, b.Y - a.Y, b.Z - a.Z);
            var ac = new Vertex(c.X - a.X, c.Y - a.Y, c.Z - a.Z);
            var normal = new Vertex(ab.Y * ac.Z - ab.Z * ac.Y, ab.Z * ac.X - ab.X * ac.Z, ab.X * ac.Y - ab.Y * ac.X);
            return normal;
        }

        // Calculates the dot product of two vectors
        private double DotProduct(Vertex a, Vertex b)
        {
            return a.X * b.X + a.Y * b.Y + a.Z * b.Z;
        }


        private double convertirRadiantes(double angulo)
        {
            if (angulo == 0)
            {
                angulo += 1f / 57.2958f;
            }
            return angulo;
        }
    }
}