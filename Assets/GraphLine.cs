using UnityEngine;
using System.Collections;


public class GraphLine : MonoBehaviour 
{
	// Vector3 lineA, lineB; Code@. which one to use?
	public Vector3 A, B;
	public GraphVertice vertA, vertB;

	LineRenderer lineRenderer;

	void Start()
	{
		lineRenderer = GetComponent<LineRenderer>();
	}

	void Update()
	{
		lineRenderer.SetPosition(0, vertA.transform.position);
		lineRenderer.SetPosition(1, vertB.transform.position);
		//CheckIntersection();
	}

	public bool CheckIntersection(GraphLine other) 
	{
		Vector3 intersection = Vector3.zero;

		// TODO
		// http://en.wikipedia.org/wiki/Line%E2%80%93line_intersection

		return false;
	}
}
