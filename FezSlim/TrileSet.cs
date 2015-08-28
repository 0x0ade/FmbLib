using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

[System.Serializable]
public class TrileSet {

	public string name;

	public List<Trile> triles = new List<Trile>();

	public bool doneLoading=false;

	public void LoadTriles(XmlDocument doc){
		triles=TrileSetImporter.ImportXML(doc);
	}

}
