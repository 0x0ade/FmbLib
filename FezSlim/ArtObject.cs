using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

[System.Serializable]
public class ArtObject {

	public string name;

	public Vector3 size;

	public List<Vector3> verts = new List<Vector3>();
	public List<Vector2> uvs = new List<Vector2>();
	public List<int> tris = new List<int>();

	Mesh m;

	public Mesh GetMesh{
		get{
			if(m==null){
				m = new Mesh();

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

	public bool done;

	public void LoadModel(XmlDocument doc){
		ArtObject loaded = ArtObjectImporter.ImportXML(doc);
		verts=loaded.verts;
		uvs=loaded.uvs;
		tris=loaded.tris;
		size=loaded.size;
	}
	
}
