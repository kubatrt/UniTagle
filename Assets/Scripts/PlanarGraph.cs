using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlanarGraph : MonoBehaviour 
{
	public int 		numOfVertices = 4;
	public float 	percentRemoveLines = 0.2f;
	public float	radius = 5f;
	
	public GraphVertice		prefab;
	public Material			defaultMaterial;
	public Material			highlightMaterial;
	public Material			selectedMaterial;
	public Material			lineNormalMaterial;
	public Material			lineCrossingMaterial;

	public List<GraphVertice> 	verticesList = new List<GraphVertice>();
	public List<GraphLine> 		linesList = new List<GraphLine>();


	// objects containers
	Transform	verticesContainer;
	Transform 	linesContainer;

	public bool restarting = false;

	//------------------------------------------------------------------------------------------------------------------
	void Start () 
	{
		if(numOfVertices < 4) {
			Debug.Log ("Minimum 4 vertices!");
			return;
		}
		restarting = true;
	}
	
	void Update()
	{
		if(restarting) {
			restarting = false;
			ClearGraphObjects();

			CreateVertices();
			CreateLines();
			PositionAroundCircle(Vector3.zero, radius);
			RemovePercentRandomLines(percentRemoveLines);
		}
		else
		{
			UpdateLines();
		}
	}

	void UpdateLines()
	{
		// (N2) for 120 lines = 120 * 120 - 120 = 14280 updates per frame
		foreach(GraphLine line in linesList)
		{
			LineRenderer lr = line.GetComponent<LineRenderer>(); 
			if(lr) {
				if(line.isIntersect) {
					lr.sharedMaterial = lineCrossingMaterial;
				} 
				else {
					if(lr.sharedMaterial != lineNormalMaterial)
						lr.sharedMaterial = lineNormalMaterial;
				}
			}
		}
	}

	//------------------------------------------------------------------------------------------------------------------
	void ClearGraphObjects()
	{
		foreach(var line in linesList)
			Destroy(line.gameObject);
		foreach(var vert in verticesList)
			Destroy(vert.gameObject);
		linesList.Clear();
		verticesList.Clear();
	}

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
			verticesList.Add(newVertice);
		}
	}

	// Create lines for vertices, from all to all 
	void CreateLines()
	{
		// create lines, iterate each with each
		for(int i=0; i < numOfVertices; ++i) 
		{
			// for current i line iterate through all others
			for(int j=0; j < numOfVertices; ++j) 
			{
				if(j == i) continue;	// skip this
				GraphLine newLine = verticesList[i].CreateNeighborLine(verticesList[j]);
				if(newLine != null) {
					newLine.transform.parent = linesContainer;
					linesList.Add(newLine);
				}
			}
		}
		Debug.Log ("Created lines: " + linesList.Count);
	}

	// Position vertices around circle path
	public void PositionAroundCircle(Vector3 center, float radius = 2.5f)
	{
		float angleStep = 360f / verticesList.Count;
		for(int i=0; i < verticesList.Count; ++i)
		{
			float angle = angleStep * i;
			float x = center.x + radius * Mathf.Cos ( Mathf.Deg2Rad * angle);
			float y = center.y + radius * Mathf.Sin ( Mathf.Deg2Rad * angle);
			verticesList[i].transform.position = new Vector3( x, y, 0);
		}
	}

	// Remove given percent of lines, percent from 0.0 to 1.0
	public void RemovePercentRandomLines(float percent)
	{
		Mathf.Clamp01(percent);
		int removeLines = Mathf.FloorToInt(linesList.Count * percent);
		RemoveRandomLines(removeLines);
	}

	public void RemoveRandomLines(int removeLines)
	{
		Debug.Log("RemoveRandomLines: " + removeLines + " from: " + linesList.Count);
		while(removeLines > 0)
		{
			int r = Random.Range(0, linesList.Count -1);
			Destroy( linesList[r].gameObject );
			linesList.RemoveAt(r);
			removeLines--;
		}
	}
}