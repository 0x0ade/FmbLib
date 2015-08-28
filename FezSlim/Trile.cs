using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Trile {

	public string name;

	public Vector3 size;
	public Vector3 offset;
	public Vector3 atlasOffset;

	public List<Vector3> verts = new List<Vector3>();
	public List<Vector2> uvs = new List<Vector2>();
	public List<int> tris = new List<int>();
	public List<int> normals = new List<int>();

	Mesh m;

	public Mesh getMesh{
		get{
			if(m==null){
				m=new Mesh();

				UpdateMesh();
			}
			return m;
		}
	}

	void UpdateMesh(){
		m.vertices=verts.ToArray();
		m.uv=uvs.ToArray();
		m.triangles=tris.ToArray();

		m.Optimize();
		m.RecalculateNormals();
	}
	
}
