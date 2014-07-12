using UnityEngine;
using UnityEditor;
using System.Collections;

[ExecuteInEditMode()]
//[CustomEditor(typeof(GeometryTest))]
public class GeometryTest : MonoBehaviour 
{	
	public bool test = false;
	public Vector3 point = new Vector3();

	// A
	public Vector3 lineA1 = new Vector3(-1, 1, 0);
	public Vector3 lineA2 = new Vector3( 1, 0, 0);

	// B
	public Vector3 lineB1 = new Vector3(1, 1, 0);
	public Vector3 lineB2 = new Vector3(1, -1, 0);
	
	public float SCALE_FACTOR = 0.2f;
	public Vector3 SCALE_FACTOR_VECTOR = new Vector3(0.2f, 0.2f, 1f);

	bool intersect = false;
	static int test_count = 0;
	Vector3 D = Vector3.zero;
	void RunTest()
	{


		// Short Vector WORKS!
		/* Vector3 DA = lineA1 - lineA2 ; 
		DA.Normalize();  
		DA.Scale(SCALE_FACTOR_VECTOR);
		lineA1 -= DA; 
		lineA2 += DA;
		
		Vector3 DB = lineB1 - lineB2;
		DB.Normalize();
		DB.Scale (SCALE_FACTOR_VECTOR);
		lineB1 -= D; 
		lineB2 += D;*/

		////////////////
		//Vector3 tempA1;Vector3 tempA2;

		Geometry2DHelper.ShortVectorBy(SCALE_FACTOR, lineA1, lineA2, out lineA1, out lineA2);
		Geometry2DHelper.ShortVectorBy(SCALE_FACTOR, lineB1, lineB2, out lineB1, out lineB2);



		Debug.Log ("##TEST## SCALING #####################################################################################");
		Debug.Log ("## " + lineA1 + "<A1------ LineA ------A2>" + lineA2 + " | " + lineB1 + "<A1 ------- LineB ------B2>" + lineB2);
		//Debug.Log ("## X: Max (1A,2A): " + Mathf.Max(lineA1.x, lineB1.x) +  " # X Max (1B, 2B): " + Mathf.Max (lineA2.x, lineB2.x));
		//Debug.Log ("## Y: Max (1A,2A): " + Mathf.Max(lineA1.y, lineB1.y) +  " # Y Max (1B, 2B): " + Mathf.Max (lineA2.y, lineB2.y));
		intersect = Geometry2DHelper.LinesIntersection(lineA1 , lineA2, lineB1, lineB2, out point);
		Debug.Log ("## [" + (test_count++) + "]RESULT: \t\t\t\t\t ### " + intersect + " ### Point: " + point);
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawLine(lineA1, lineA2);

		Gizmos.color = Color.blue;
		Gizmos.DrawLine(lineB1, lineB2);

		if(intersect) {
			Gizmos.color = Color.red;
			Gizmos.DrawSphere(point, 0.1f);
		}	
	}

	void Start()
	{
		test_count = 0;
	}
	
	void Update() 
	{
		if(test) {
			RunTest();
			test = false;
		}
	}
}
