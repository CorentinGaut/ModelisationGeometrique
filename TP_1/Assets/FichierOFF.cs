using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Globalization;
using UnityEngine;

public class FichierOFF : MonoBehaviour
{
    Mesh mesh;
    string fileOFF;
    Vector3[] vertices;
    int[] triangles;

    public int size;

    void Start()
    {
        createMesh();
    }

    void createMesh()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        string[] lines = System.IO.File.ReadAllLines(@"Assets/buddha.off");
        string[] tmpVertex = lines[1].Split(' ');
        int nbvertex = int.Parse(tmpVertex[0]);
        int nbTriangles = int.Parse(tmpVertex[1]);

        vertices = new Vector3[nbvertex];
        triangles = new int[nbTriangles * 3];
        Vector3 somme = new Vector3(0, 0, 0);
        for (int i = 0; i < nbvertex; i++)
        {
            string[] coord = lines[2 + i].Split(' ');
            vertices[i] = new Vector3(float.Parse(coord[0],CultureInfo.InvariantCulture),
                                      float.Parse(coord[1],CultureInfo.InvariantCulture),
                                      float.Parse(coord[2],CultureInfo.InvariantCulture));
            somme += vertices[i];
        }

        Vector3 barycentre = somme / nbvertex;

        //rescale
        for (int i = 0; i < nbvertex; i++)
        {
            vertices[i] = (vertices[i] + barycentre) * size;
        }

        int triangle = 0;
        for(int i = 0; i < nbTriangles; i++)
        {
            string[] index = lines[2 + i + nbvertex].Split(' ');
            triangles[triangle] = int.Parse(index[1]);
            triangles[triangle + 1] = int.Parse(index[2]);
            triangles[triangle + 2] = int.Parse(index[3]); 

            triangle += 3;
        }

        //calcul des normals
        Vector3[] normals = new Vector3[nbvertex];
        triangle = 0;
        for (int j = 0; j < nbvertex; j++)
        {
            Vector3 normal = Vector3.zero;
            for (int i = 0; i < nbTriangles;i++)
            { 
                if (j == triangles[i * 3] || j == triangles[i * 3 + 1] || j == triangles[i * 3 + 2])
                {
                    Vector3 v1 = vertices[triangles[i * 3 + 1]] - vertices[triangles[i * 3]];
                    Vector3 v2 = vertices[triangles[i * 3 + 2]] - vertices[triangles[i * 3]];

                    normal += Vector3.Cross(v1, v2);
                }
                normal.Normalize();
            }
            normals[j] = normal;
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.normals = normals;
        //mesh.RecalculateNormals(); 
    }



}
