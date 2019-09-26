
using UnityEngine;
using System.Collections;

public class CylindreGenerator : MonoBehaviour
{

    Mesh mesh;

    Vector3[] vertices;
    int[] triangles;

    public int nbMeridiens;
    public int nbParallele;
    public int hauteur;
    public int rayon;

    // Use this for initialization
    void Start()
    {

        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        vertices = new Vector3[(nbMeridiens + 1) * (nbParallele + 1)];
        triangles = new int[nbMeridiens * nbParallele * 6];

        //creation des points
        pointCylindre(nbMeridiens, rayon);

        //creations des triangle
        dessinTriangles();

        // mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;

        // mesh.RecalculateNormals();
    }

    private void pointCylindre(int meridiens, int r)
    {

        int index = 0;
        for (int j = 0; j < nbParallele; j++)
        {
            float h = (hauteur * j) / 2;
            for (int i = 0; i <= meridiens; i++)
            {
                float theta = (2 * Mathf.PI * i) / meridiens;
                vertices[index] = new Vector3(r * Mathf.Cos(theta),
                                              h,
                                              r * Mathf.Sin(theta));
                index++;
            }
        }

    }

    private void dessinTriangles()
    {
        int vert = 0;
        int tris = 0;

        for (int j = 0; j < nbParallele; j++)
        {
            for (int i = 0; i < nbMeridiens; i++)
            {
                triangles[tris] = vert;
                triangles[tris + 1] = (vert + 1) + nbMeridiens;
                triangles[tris + 2] = vert + 1;

                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = (vert + 1) + nbMeridiens;
                triangles[tris + 5] = (vert + 2) + nbMeridiens;

                vert++;
                tris += 6;
            }
            vert++;
        }
    }

    private void OnDrawGizmos()
    {
        if (vertices == null)
        {
            return;
        }
        for (int i = 0; i < vertices.Length; i++)
        {
            Gizmos.DrawSphere(vertices[i], .1f);
        }
    }
}

