using UnityEngine;
using System.Collections;

/* REFERENCES
 * http://en.wikipedia.org/wiki/Line%E2%80%93line_intersection
 * http://blog.ivank.net/basic-geometry-functions.html
 * http://www.wyrmtale.com/blog/2013/115/2d-line-intersection-in-c
 */


public class Geometry2DHelper 
{
	public static readonly Vector3 INVALID2D = new Vector3(9999f,9999f,0);
	public static readonly Vector3 INVALID3D = new Vector3(9999f,9999f,9999f);

	public static float SCALE_FACTOR = 0.2f; // with 0 lines starts and ends at the same position which result with collision

	public static float Distance(Vector3 a, Vector3 b)
	{
		return Mathf.Sqrt( (b.x-a.x)*(b.x-a.x) + (b.y-a.y)*(b.y-a.y)); 
	}
	
	public static bool InRect(Vector3 point, Vector3 topLeft, Vector3 bottomRight)
	{
		//topLeft *= 0.9f; bottomRight *= 0.9f; // - make lines shorter
		if(point.x >= Mathf.Min(topLeft.x, bottomRight.x ) && 
		   point.x <= Mathf.Max(topLeft.x, bottomRight.x ) && 
		   point.y >= Mathf.Min(topLeft.y, bottomRight.y) && 
		   point.y <= Mathf.Max(topLeft.y, bottomRight.y )) 
			return true;
		return false;
	}

	public static float Round2DP(float value)
	{
		return Mathf.Round(value * 100f) / 100f;
	}

	public static void ShortVectorBy(float factor, Vector3 v1, Vector3 v2, out Vector3 o1, out Vector3 o2)
	{

		o1 = v1; 
		o2 = v2;
		Vector3 dv = v1 - v2;
		//Debug.Log("### o1: " + o1 + " o2: " + o2);
		//Debug.Log("## Shorty by: " + factor + " dv: " + dv + " mag: " + dv.magnitude);


		if(dv.magnitude >= 1f) {
			dv.Normalize();  
			dv.Scale( new Vector3(factor, factor, 1f));
			o1 -= dv; 
			o2 += dv;
			//Debug.Log("# o1: " + o1 + " o2: " + o2);
		}

		//o1 = RoundDP(o1); o2 = RoundDP(o2);
	}

	public static bool LinesIntersection(Vector3 a1, Vector3 a2, Vector3 b1, Vector3 b2, out Vector3 point) 
	{	
		point = INVALID2D;
		// a - starting point b - ending points
		float dax = (a1.x-a2.x); float dbx = (b1.x-b2.x);
		float day = (a1.y-a2.y); float dby = (b1.y-b2.y);
		//Debug.Log("## dAx: " + dax + " # dBx: " + dbx);
		//Debug.Log("## dAy: " + day + " # dBy: " + dby);

		float delta = dax*dby - day*dbx;
		//Debug.Log("## Delta { dax*dby - day*dbx } : " + delta);

		if (delta == 0) 
			return false;	// parallel

		float A = (a1.x * a2.y - a1.y * a2.x);
		float B = (b1.x * b2.y - b1.y * b2.x);
		//Debug.Log("## A: " + A + " ### B: " + B );	

		point.x = ( A*dbx - dax*B ) / delta;
		point.y = ( A*dby - day*B ) / delta;

		// round 2DP
		//point.x = Round2DP(point.x);
		//point.y = Round2DP(point.y);
		//Debug.Log("## Point: " + point);
		//Debug.Log("## InRECT( 1a: " + a1 + " 2a " + a2 + ") && InRECT( 1b: " + b1 + " 2b: " + b2 + ")");
	
		// intersection point is located between starting and ending point of each line.
 		if( InRect(point, a1, a2) && InRect(point, b1, b2)) 
			return true;

		return false;
	}

	/*
	 * Circumcenter 
	 * Let us have three points (a, b, c). We are looking for a circle (center and radius), which intersects all three points. 
	 * This circumcenter is the intersection of normals (a,b) and (b,c). Radius is distance between circumcenter and a (or b, or c).
	 */
	/*
	public static Vector2 GetCircumcenter(Vector2 a, Vector2  b, Vector2  c)
	{
		// m1 - center of (a,b), the normal goes through it
		float f1 = (b.x - a.x) / (a.y - b.y);
		Vector2 m1 = new Vector2((a.x + b.x)/2f, (a.y + b.y)/2f);
		float g1 = m1.y - f1*m1.x;

		float f2 = (c.x - b.x) / (b.y - c.y);
		Vector2 m2 = new Vector2((b.x + c.x)/2f, (b.y + c.y)/2f);
		float g2 = m2.y - f2*m2.x;

		// degenerated cases
		// - 3 points on a line
		if(f1 == f2)   
			return Vector2.zero; // throw new Exception

		// - a, b have the same height -> slope of normal of |ab| = infinity
		else if(a.y == b.y) return new Vector2(m1.x, f2*m1.x + g2);
		else if(b.y == c.y) return new Vector2(m2.x, f1*m2.x + g1);

	    float x = (g2-g1) / (f1 - f2);
		return new Vector2(x, f1*x + g1);
	}
	*/
}
