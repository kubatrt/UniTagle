using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GraphVertice : MonoBehaviour 
{
	//public Material matDefault;
	//public Material matSelected;
	//public matHighlight;
	public 	float 	radius = 0.5f;
	public 	float 	moveSpeed = 1f;
	public	Vector3 targetPosition;
	public 	bool 	isSelected = false;
	
	//public List<GraphLine> 		lines = new List<GraphLine>();
	public List<GraphVertice>	neighbors = new List<GraphVertice>();


	//------------------------------------------------------------------------------------------------------------------
	void Start()
	{
		targetPosition = transform.position;
	}

	void Update() 
	{
		if(isSelected)	// dragging
		{
			Vector3 mousePos = Camera.main.ScreenToWorldPoint(
				new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
			targetPosition = new Vector3(mousePos.x, mousePos.y, transform.position.z);
		}
		Move();
	}

	//------------------------------------------------------------------------------------------------------------------
	void Move()
	{
		if(transform.position != targetPosition)
		{
			transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * moveSpeed);
		}
	}

	public GraphLine CreateNeighborLine(GraphVertice vert)
	{
		PlanarGraph graph = GameObject.FindObjectOfType<PlanarGraph>();

		if(neighbors.Contains(vert)) {
			return null;
		}
		neighbors.Add(vert);
		vert.neighbors.Add(this);	// add SELF to THEIR neighbors list, Code@.

		// create line renderers to each other vertice
		// this +--------------+ vert
		string name = this.name + "-line-" + vert;
		GraphLine newLine = new GameObject(name).AddComponent<GraphLine>();
		newLine.gameObject.transform.position = Vector3.zero;
		newLine.gameObject.transform.rotation = Quaternion.identity;
		newLine.gameObject.transform.localScale = Vector3.one;

		//newLine.A = transform.position; 
		newLine.A = this;

		//newLine.B = vert.transform.position; 
		newLine.B = vert;
		
		// add lineRenderer
		LineRenderer lr =  newLine.gameObject.AddComponent<LineRenderer>();
		lr.SetVertexCount(2);
		lr.SetWidth(0.1f, 0.1f);
		lr.SetPosition(0, newLine.A.transform.position);
		lr.SetPosition(1, newLine.B.transform.position);
		lr.sharedMaterial = graph.lineNormalMaterial;

		//lines.Add(newLine);
		return newLine;
	}
}
