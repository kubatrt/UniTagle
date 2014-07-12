using UnityEngine;
using System.Collections;


public class MouseInputHandler : MonoBehaviour {

	PlanarGraph	graph;

	public GraphVertice	lastSelection;

	// Use this for initialization
	void Start () {
		graph = GetComponent<PlanarGraph>();
		lastSelection = null;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetMouseButtonDown(0))
		{
			Vector3 mousePos = Camera.main.ScreenToWorldPoint(
				new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
			Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

			RaycastHit hitInfo;
			if(Physics.Raycast(mouseRay, out hitInfo, float.MaxValue))
			{
				SphereCollider sc = hitInfo.collider as SphereCollider;
				if(sc != null)
				{
					Debug.Log("Selected: " + sc.name + " RAY: " + mouseRay + "MOUSE: " + mousePos);
					lastSelection = sc.gameObject.GetComponent<GraphVertice>();
					
					lastSelection.isSelected = true;
					lastSelection.renderer.sharedMaterial = graph.selectedMaterial;
				}
			}
		}
		else if(Input.GetMouseButtonUp(0))
		{
			if(lastSelection != null)
			{
				lastSelection.renderer.sharedMaterial = graph.defaultMaterial;
				lastSelection.isSelected = false;
				lastSelection = null;
			}
		}
	}
}
