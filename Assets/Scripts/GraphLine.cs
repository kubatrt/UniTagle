using UnityEngine;
using System.Collections;


public class GraphLine : MonoBehaviour 
{
	static Material		defaultMaterial;
	static Material 	intersectMaterial;

	private int intersectCall = 0;
	public bool init = false;
	public GraphVertice A;	// start
	public GraphVertice B;	// end
	public bool isIntersect;

	LineRenderer 	lineRenderer;
	PlanarGraph		planarGraph;

	//------------------------------------------------------------------------------------------------------------------
	void Start()
	{
		planarGraph = GameObject.FindGameObjectWithTag("GameController").GetComponent<PlanarGraph>();
		lineRenderer = GetComponent<LineRenderer>();
		isIntersect = false;
	}

	void Update()
	{
		/*if(!init) { 
			CheckIfLineIntersectOthers();
			init = true;
		}*/

		lineRenderer.SetPosition(0, A.transform.position);
		lineRenderer.SetPosition(1, B.transform.position);

		CheckIfLineIntersectOthers();
	}

	// check every frame
	public void CheckIfLineIntersectOthers()
	{
		if(!planarGraph) {
			Debug.Log ("Graph not initialized!"); 
			return;
		}

		Vector3 A1,A2,B1,B2 = Vector3.zero;
		//bool intersection = false;

		for(int i=0; i < planarGraph.linesList.Count; ++i)
		{
			if(planarGraph.linesList[i] == this)
				continue;

			// self
			Geometry2DHelper.ShortVectorBy(0.1f, A.transform.position, B.transform.position, out A1, out A2);
			//Debug.Log(i + "# SELF: A1" + A1 + " A2 " + A2);

			// other
			Geometry2DHelper.ShortVectorBy(0.1f, planarGraph.linesList[i].A.transform.position, 
			                               planarGraph.linesList[i].B.transform.position, out B1, out B2);
			//Debug.Log(i + "# OTHER B1 " + B1 + " B2 " + B2);

			Vector3 point;
			isIntersect = Geometry2DHelper.LinesIntersection(A1, A2, B1, B2, out point);
			//Debug.Log(i + "# RESULT: >>> " + intersection + " <<< " + planarGraph.lines[i].name + " # Point: " + point);

			//isIntersect = intersection;
			if(isIntersect) {
				break;
			}
		}

	}

}
