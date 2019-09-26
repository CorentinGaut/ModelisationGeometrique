using UnityEngine;
using System.Collections;

public class MeshGenerator : MonoBehaviour {


	Mesh mesh;

	Vector3[] vertices;
	int[] triangles;

	public int x = 20;
	public int y = 20;

	// Use this for initialization
	void Start () {
		mesh = new Mesh ();
		GetComponent<MeshFilter> ().mesh = mesh;

		vertices = new Vector3[(x + 1) * (y + 1)];

		int k = 0;
		//les coordonées
		for (int i = 0; i <= y; i++) {
			for (int j = 0; j <= x ;j++) {
				vertices[k] = new Vector3(j,i,0);
				k++;
			}
		}
			
		//les triangles
		triangles = new int[x * y * 6];

		int index = 0;
		int carre = 0;

		for (int j = 0; j < y; j++) {
			for (int i = 0; i < x; i++) {

				triangles [carre] = index;
				triangles [carre + 1] = index + x + 1;
				triangles [carre + 2] = index + 1;

				triangles [carre + 3] = index + 1;
				triangles [carre + 4] = index + x + 1;
				triangles [carre + 5] = index + x + 2;

				index++;
				carre += 6;
			}
            index++;
		}




		mesh.Clear ();
		mesh.vertices = vertices;
		mesh.triangles = triangles;

		mesh.RecalculateNormals();

	}


	
	// Update is called once per frame
	private void OnDrawGizmos () {

		if (vertices == null) {
			
			return;
		}

		for (int i = 0; i < vertices.Length; i++) {
			Gizmos.DrawSphere (vertices [i], .1f);
		}
	}
}
