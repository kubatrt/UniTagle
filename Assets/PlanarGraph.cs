using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlanarGraph : MonoBehaviour 
{
	public int 		numOfVertices = 4;
	public float 	percentRemoveLines = 0.2f;
	
	public GraphVertice		prefab;
	public Material			defaultMaterial;
	public Material			highlightMaterial;
	public Material			selectedMaterial;
	public Material			lineNormalMaterial;
	public Material			lineCrossingMaterial;

	public List<GraphVertice> 	vertices = new List<GraphVertice>();
	public List<GraphLine> 		lines = new List<GraphLine>();


	// objects containers
	Transform	verticesContainer;
	Transform 	linesContainer;


	//------------------------------------------------------------------------------------------------------------------
	void Start () 
	{
		if(numOfVertices < 4) {
			Debug.Log ("Minimum 4 vertices!");
			return;
		}
		
		CreateContainers();
		CreateVertices();
		CreateLines();
		PositionAroundCircle(Vector3.zero, 5f);
		//RemovePercentRandomLines(percentRemoveLines);
	}
	
	void Update()
	{
		// TODO update lines material TODO: move
		foreach(GraphLine line in lines)
		{
			LineRenderer lr = line.GetComponent<LineRenderer>(); 
			if(line.isIntersect) {
				lr.sharedMaterial = lineCrossingMaterial;
			} else {
				if(lr.sharedMaterial != lineNormalMaterial)
					lr.sharedMaterial = lineNormalMaterial;
			}
		}
		
	}

	//------------------------------------------------------------------------------------------------------------------
	void CreateContainers()
	{
		verticesContainer = new GameObject("_vertices").GetComponent<Transform>();
		verticesContainer.parent = transform;
		linesContainer = new GameObject("_lines").GetComponent<Transform>();
		linesContainer.parent = transform;
	}
	
	void CreateVertices()
	{
		// create vertices
		for(int i=0; i < numOfVertices; ++i)
		{
			GraphVertice newVertice = GameObject.Instantiate(prefab, Vector3.zero, Quaternion.identity) as GraphVertice;
			newVertice.gameObject.renderer.sharedMaterial = defaultMaterial;
			newVertice.gameObject.name = newVertice.gameObject.name + "_" + i;
			newVertice.transform.parent = verticesContainer;
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

				if(j == numOfVertices-1) // skip last
					continue;
				if(Random.value > 0.75f)	// skip random lines
					continue;

				GraphLine newLine = vertices[i].CreateNeighborLine(vertices[j]);
				if(newLine != null) {
					newLine.transform.parent = linesContainer;
					lines.Add(newLine);
				}
			}
		}
	}
	
	public void Positioning4vers2D()
	{
		vertices[0].transform.position = new Vector3(-2.5f, 0, 0);
		vertices[1].transform.position = new Vector3(0f, 2.5f, 0);
		vertices[2].transform.position = new Vector3(2.5f, 0, 0);
		vertices[3].transform.position = new Vector3(0, -2.5f, 0);
	}

	// Position vertices around circle path
	public void PositionAroundCircle(Vector3 center, float radius = 2.5f)
	{
		float angleStep = 360f / vertices.Count;
		for(int i=0; i < vertices.Count; ++i)
		{
			float angle = angleStep * i;
			float x = center.x + radius * Mathf.Cos ( Mathf.Deg2Rad * angle);
			float y = center.y + radius * Mathf.Sin ( Mathf.Deg2Rad * angle);
			vertices[i].transform.position = new Vector3( x, y, 0);
		}
	}

	// Remove given percent of lines, percent from 0.0 to 1.0
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
		// TODO
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