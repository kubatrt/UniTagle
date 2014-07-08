using UnityEngine;
using System.Collections;


public class GraphLine : MonoBehaviour 
{
	// Vector3 lineA, lineB; Code@. which one to use?
	//public Vector3 A, B;
	public GraphVertice A;
	public GraphVertice B;
	public bool isIntersect;

	LineRenderer 	lineRenderer;
	PlanarGraph		planarGraph;

	//------------------------------------------------------------------------------------------------------------------
	void Start()
	{
		lineRenderer = GetComponent<LineRenderer>();
		planarGraph = GameObject.FindObjectOfType<PlanarGraph>();
		isIntersect = false;
	}

	void Update()
	{
		lineRenderer.SetPosition(0, A.transform.position);
		lineRenderer.SetPosition(1, B.transform.position);

		if(A.isSelected || B.isSelected)
		{
			//CheckIfLineIntersectOthers();
		}
	}

	public void CheckIfLineIntersectOthers()
	{
		for(int i=0; i < planarGraph.lines.Count; ++i)
		{
			Vector3 point = Vector3.zero;
			if(Geometry2DHelper.LinesIntersection(
				A.transform.position, B.transform.position,
				planarGraph.lines[i].A.transform.position, planarGraph.lines[i].B.transform.position, out point))
			{
				isIntersect = true;
				break;
			}
			else
			{
				isIntersect = false;
			}
		}

	}

}
