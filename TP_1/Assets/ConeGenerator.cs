using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeGenerator : MonoBehaviour
{
    Mesh mesh;

    Vector3[] vertices;
    int[] triangles;

    public int nbMeridiens;
    public int hauteur;
    public int rayon;

    // Use this for initialization
    void Start()
    {

        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        vertices = new Vector3[(nbMeridiens + 1) + 2];
        triangles = new int[nbMeridiens * 3 + nbMeridiens * 3];

        //creation des points
        pointCone(nbMeridiens, rayon);

        //creations des triangle
        dessinTriangles();

        // mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;

        // mesh.RecalculateNormals();
    }

    private void pointCone(int meridiens, int r)
    {
        int index = 0;
        for (int i = 0; i <= meridiens; i++)
        {
            float theta = (2 * Mathf.PI * i) / meridiens;
            vertices[index] = new Vector3(r * Mathf.Cos(theta),
                                            0,
                                            r * Mathf.Sin(theta));
            index++;
        }

        vertices[index] = new Vector3(0, hauteur, 0);
        vertices[index + 1] = new Vector3(0, 0, 0);
    }

    private void dessinTriangles()
    {
        int vert = 0;
        int tris = 0;

        for (int i = 0; i < nbMeridiens; i++)
        {
            triangles[tris] = vert;
            triangles[tris + 1] = nbMeridiens + 1;
            triangles[tris + 2] = vert + 1;
            

            vert++;
            tris += 3;
        }

        vert = 0;
        for (int i = 0; i < nbMeridiens; i++)
        {
            triangles[tris] = vert;
            triangles[tris + 1] = vert + 1;
            triangles[tris + 2] = nbMeridiens + 2;
            

            vert++;
            tris += 3;
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
