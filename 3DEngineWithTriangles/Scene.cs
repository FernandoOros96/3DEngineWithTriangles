using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DEngineWithTriangles
{
    public class Scene
    {
        public Mesh malla;

        //Cone
        Vertex center = new Vertex(0, 0, 0);
        double radius = 50;
        double height = 100;
        int numSegments = 20;
        int numLongitudes = 10;
        int numLatitudes = 10;

 

        public Scene()
        {
            malla = new Mesh();
            /*createCube(mesh);
            createCone(center, radius, height, numSegments,malla);
            createCylinder(radius, height, numSegments, malla);*/
            CreateSphere(radius, numLongitudes, numLatitudes, malla);

        }

        public void createCube(Mesh mesh)
        {
            Vertex[] vertices = new Vertex[8];
            vertices[0] = new Vertex(-50, -50, -50);
            vertices[1] = new Vertex(50, -50, -50);
            vertices[2] = new Vertex(-50, 50, -50);
            vertices[3] = new Vertex(50, 50, -50);
            vertices[4] = new Vertex(-50, -50, 50);
            vertices[5] = new Vertex(50, -50, 50);
            vertices[6] = new Vertex(-50, 50, 50);
            vertices[7] = new Vertex(50, 50, 50);

            Triangle[] triangles = new Triangle[12];
            triangles[0] = new Triangle { Vec3D = new Vertex[] { vertices[0], vertices[2], vertices[1] } };
            triangles[1] = new Triangle { Vec3D = new Vertex[] { vertices[1], vertices[2], vertices[3] } };
            triangles[2] = new Triangle { Vec3D = new Vertex[] { vertices[1], vertices[3], vertices[5] } };
            triangles[3] = new Triangle { Vec3D = new Vertex[] { vertices[3], vertices[7], vertices[5] } };
            triangles[4] = new Triangle { Vec3D = new Vertex[] { vertices[2], vertices[6], vertices[3] } };
            triangles[5] = new Triangle { Vec3D = new Vertex[] { vertices[3], vertices[6], vertices[7] } };
            triangles[6] = new Triangle { Vec3D = new Vertex[] { vertices[0], vertices[4], vertices[2] } };
            triangles[7] = new Triangle { Vec3D = new Vertex[] { vertices[2], vertices[4], vertices[6] } };
            triangles[8] = new Triangle { Vec3D = new Vertex[] { vertices[1], vertices[5], vertices[0] } };
            triangles[9] = new Triangle { Vec3D = new Vertex[] { vertices[0], vertices[5], vertices[4] } };
            triangles[10] = new Triangle { Vec3D = new Vertex[] { vertices[4], vertices[7], vertices[6] } };
            triangles[11] = new Triangle { Vec3D = new Vertex[] { vertices[4], vertices[5], vertices[7] } };

            mesh.Triangles.AddRange(triangles);
        }

        public void createCone(Vertex center, double radius, double height, int numSegments,Mesh mesh)
        {
            // Create the base of the cone
            for (int i = 0; i < numSegments; i++)
            {
                double angle1 = 2 * Math.PI * i / numSegments;
                double angle2 = 2 * Math.PI * (i + 1) / numSegments;

                Vertex v1 = new Vertex(center.X + radius * Math.Cos(angle1), center.Y + radius * Math.Sin(angle1), center.Z);
                Vertex v2 = new Vertex(center.X + radius * Math.Cos(angle2), center.Y + radius * Math.Sin(angle2), center.Z);
                Vertex v3 = center;

                mesh.Triangles.Add(new Triangle() { Vec3D = new Vertex[] { v1, v2, v3 } });
            }

            // Create the sides of the cone
            Vertex apex = new Vertex(center.X, center.Y, center.Z + height);
            for (int i = 0; i < numSegments; i++)
            {
                double angle1 = 2 * Math.PI * i / numSegments;
                double angle2 = 2 * Math.PI * (i + 1) / numSegments;

                Vertex v1 = new Vertex(center.X + radius * Math.Cos(angle1), center.Y + radius * Math.Sin(angle1), center.Z);
                Vertex v2 = new Vertex(center.X + radius * Math.Cos(angle2), center.Y + radius * Math.Sin(angle2), center.Z);
                Vertex v3 = apex;

                mesh.Triangles.Add(new Triangle() { Vec3D = new Vertex[] { v1, v2, v3 } });
            }
        }

        public void createCylinder(double radius, double height, int numSides, Mesh mesh)
        {
            double angleStep = 2 * Math.PI / numSides;

            // create the top cap
            Vertex topCenter = new Vertex(0, height / 2, 0);
            mesh.Triangles.Add(new Triangle()
            {
                Vec3D = new Vertex[] { topCenter,
        new Vertex(radius, height / 2, 0),
        new Vertex(radius * Math.Cos(angleStep), height / 2, radius * Math.Sin(angleStep)) }
            });

            for (int i = 1; i < numSides; i++)
            {
                mesh.Triangles.Add(new Triangle()
                {
                    Vec3D = new Vertex[] { topCenter,
            new Vertex(radius * Math.Cos((i - 1) * angleStep), height / 2, radius * Math.Sin((i - 1) * angleStep)),
            new Vertex(radius * Math.Cos(i * angleStep), height / 2, radius * Math.Sin(i * angleStep)) }
                });
            }

            // create the bottom cap
            Vertex bottomCenter = new Vertex(0, -height / 2, 0);
            mesh.Triangles.Add(new Triangle()
            {
                Vec3D = new Vertex[] { bottomCenter,
        new Vertex(radius * Math.Cos(angleStep), -height / 2, radius * Math.Sin(angleStep)),
        new Vertex(radius, -height / 2, 0) }
            });

            for (int i = 1; i < numSides; i++)
            {
                mesh.Triangles.Add(new Triangle()
                {
                    Vec3D = new Vertex[] { bottomCenter,
            new Vertex(radius * Math.Cos(i * angleStep), -height / 2, radius * Math.Sin(i * angleStep)),
            new Vertex(radius * Math.Cos((i - 1) * angleStep), -height / 2, radius * Math.Sin((i - 1) * angleStep)) }
                });
            }

            // create the side walls
            for (int i = 0; i < numSides; i++)
            {
                Vertex v1 = new Vertex(radius * Math.Cos(i * angleStep), height / 2, radius * Math.Sin(i * angleStep));
                Vertex v2 = new Vertex(radius * Math.Cos(i * angleStep), -height / 2, radius * Math.Sin(i * angleStep));
                Vertex v3 = new Vertex(radius * Math.Cos((i + 1) * angleStep), -height / 2, radius * Math.Sin((i + 1) * angleStep));
                Vertex v4 = new Vertex(radius * Math.Cos((i + 1) * angleStep), height / 2, radius * Math.Sin((i + 1) * angleStep));

                mesh.Triangles.Add(new Triangle() { Vec3D = new Vertex[] { v1, v2, v3 } });
                mesh.Triangles.Add(new Triangle() { Vec3D = new Vertex[] { v1, v3, v4 } });
            }
        }


        public void CreateSphere(double radius, int numLongitudes, int numLatitudes, Mesh mesh)
        {

            double latitudeAngleStep = Math.PI / (numLatitudes + 1);
            double longitudeAngleStep = (2 * Math.PI) / numLongitudes;

            for (int i = 0; i < numLatitudes; i++)
            {
                double latitudeAngle = (i + 1) * latitudeAngleStep;
                double cosLatitude = Math.Cos(latitudeAngle);
                double sinLatitude = Math.Sin(latitudeAngle);

                for (int j = 0; j < numLongitudes; j++)
                {
                    double longitudeAngle = j * longitudeAngleStep;
                    double cosLongitude = Math.Cos(longitudeAngle);
                    double sinLongitude = Math.Sin(longitudeAngle);

                    Vertex v1 = new Vertex(radius * sinLatitude * cosLongitude, radius * sinLatitude * sinLongitude, radius * cosLatitude);
                    Vertex v2 = new Vertex(radius * sinLatitude * Math.Cos(longitudeAngle + longitudeAngleStep), radius * sinLatitude * Math.Sin(longitudeAngle + longitudeAngleStep), radius * cosLatitude);
                    Vertex v3 = new Vertex(radius * Math.Sin(latitudeAngle + latitudeAngleStep) * Math.Cos(longitudeAngle + longitudeAngleStep), radius * Math.Sin(latitudeAngle + latitudeAngleStep) * Math.Sin(longitudeAngle + longitudeAngleStep), radius * Math.Cos(latitudeAngle + latitudeAngleStep));
                    Vertex v4 = new Vertex(radius * Math.Sin(latitudeAngle + latitudeAngleStep) * cosLongitude, radius * Math.Sin(latitudeAngle + latitudeAngleStep) * sinLongitude, radius * Math.Cos(latitudeAngle + latitudeAngleStep));

                    mesh.Triangles.Add(new Triangle() { Vec3D = new[] { v1, v2, v3 } });
                    mesh.Triangles.Add(new Triangle() { Vec3D = new[] { v1, v3, v4 } });
                }
            }
        }
    }
}
