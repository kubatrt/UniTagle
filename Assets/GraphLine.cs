using UnityEngine;
using System.Collections;


public class GraphLine : MonoBehaviour 
{
	private int intersectCall = 0;
	public bool init = false;
	public GraphVertice A;	// start
	public GraphVertice B;	// end
	public bool isIntersect;

	LineRenderer 	lineRenderer;
	PlanarGraph		planarGraph;

	Vector3 A1,A2,B1,B2 = Vector3.zero;

	//------------------------------------------------------------------------------------------------------------------
	void Start()
	{
		lineRenderer = GetComponent<LineRenderer>();
		planarGraph = GameObject.FindGameObjectWithTag("GameController").GetComponent<PlanarGraph>();
		isIntersect = false;
	}

	void OnDestroy()
	{
		Debug.Log("Debug count: " + intersectCall);
	}

	void Update()
	{

		if(!init) { 
			CheckIfLineIntersectOthers();
			init = true;
		}

		lineRenderer.SetPosition(0, A.transform.position);
		lineRenderer.SetPosition(1, B.transform.position);

		//if(A.isSelected || B.isSelected)
		CheckIfLineIntersectOthers();
	}

	// check every frame
	public void CheckIfLineIntersectOthers()
	{
		//Debug.Log("### CheckIfLineIntersectOthers FOR: " + this.name);
		intersectCall++;
		if(!planarGraph) {
			Debug.Log ("Graph not initialized!");
			return;
		}

		bool intersection = false;
		for(int i=0; i < planarGraph.lines.Count; ++i)
		{
			if(planarGraph.lines[i] == this) {
				continue;
			}

			// self
			Geometry2DHelper.ShortVectorBy(0.1f, A.transform.position, B.transform.position, out A1, out A2);
			//Debug.Log(i + "# SELF: A1" + A1 + " A2 " + A2);
			// other
			Geometry2DHelper.ShortVectorBy(0.1f, planarGraph.lines[i].A.transform.position, 
			                               planarGraph.lines[i].B.transform.position, out B1, out B2);
			//Debug.Log(i + "# OTHER B1 " + B1 + " B2 " + B2);

			Vector3 point;
			intersection = Geometry2DHelper.LinesIntersection(A1, A2, B1, B2, out point);
			//Debug.Log(i + "# RESULT: >>> " + intersection + " <<< " + planarGraph.lines[i].name + " # Point: " + point);

			isIntersect = intersection;
			if(isIntersect) {
				break;
			}
		}

	}

}
