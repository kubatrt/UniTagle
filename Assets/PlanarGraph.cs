using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlanarGraph : MonoBehaviour 
{
	// settings
	public int 		numOfVertices = 4;
	public float 	percentRemoveLines = 0.2f;

	// prefabs and materials
	public GraphVertice		prefab;
	public Material			defaultMaterial;
	public Material			highlightMaterial;
	public Material			selectedMaterial;
	public Material			lineNormalMaterial;
	public Material			lineCrossing;

	//public GraphVertice lastSelection = null;

	List<GraphVertice> 	vertices = new List<GraphVertice>();
	//List<GraphLine> 	lines = new List<GraphLine>();


	void Start () {
		if(numOfVertices < 4)
		{
			Debug.Log ("Minimum 4 vertices!");
			return;
		}

		CreateVertices();
		CirclePositioning2D(Vector3.zero, 5f, 0f);
		CreateLines();
		//RemovePercentRandomLines(percentRemoveLines);
	}

	void CreateVertices()
	{
		// create vertices
		for(int i=0; i < numOfVertices; ++i)
		{
			GraphVertice newVertice = GameObject.Instantiate(prefab, Vector3.zero, Quaternion.identity) as GraphVertice;
			newVertice.gameObject.renderer.sharedMaterial = defaultMaterial;
			newVertice.gameObject.name = newVertice.gameObject.name + "_" + i;
			vertices.Add(newVertice);
		}
	}

	void CreateLines()
	{
		// create lines, iterate each with each
		for(int i=0; i < numOfVertices; ++i)
		{
			for(int j=0; j < numOfVertices; ++j)
			{
				if(j == i) continue;	// skip current element
				Vector3 vertPosA = vertices[i].transform.position;
				Vector3 vertPosB = vertices[j].transform.position;
				vertices[i].CreateNeighborLine(vertices[j]);
			}
		}
	}


	public void Positioning4vers2D(float depth = 0f)
	{
		vertices[0].transform.position = new Vector3(-2.5f, 0, depth);
		vertices[1].transform.position = new Vector3(0f, 2.5f, depth);
		vertices[2].transform.position = new Vector3(2.5f, 0, depth);
		vertices[3].transform.position = new Vector3(0, -2.5f, depth);
	}

	public void CirclePositioning2D(Vector3 center, float radius = 2.5f, float depth = 0f)
	{
		float angleStep = 360f / vertices.Count;
		for(int i=0; i < vertices.Count; ++i)
		{
			float angle = angleStep * i;
			float x = center.x + radius * Mathf.Cos ( Mathf.Deg2Rad * angle);
			float y = center.y + radius * Mathf.Sin ( Mathf.Deg2Rad * angle);
			vertices[i].transform.position = new Vector3( x, y, depth);
		}
	}

	// percent given from 0.0 to 1.0
	public void RemovePercentRandomLines(float percent)
	{
		// TODO...
		// numOfLines = GameObject.FindObjectsOfType().Count
		/*

		Mathf.Clamp01(percent);
		float linesToRemove = 1 * percent;
		Debug.Log("Lines percentF to remove: " + linesToRemove);

		RemoveRandomLines((int)linesToRemove);*/
	}

	public void RemoveRandomLines(int removeLines)
	{
		/*Debug.Log("Lines to remove: " + removeLines);

		List<GraphLine> tempLines  = lines;
		lines.Clear();

		for(int i=0; i < removeLines; ++i)
		{
			int r = Random.Range(0, lines.Count - 1 - i);
			lines.Add( tempLines[ r ] );
			tempLines.RemoveAt(r);
		}*/
	}
}