using UnityEngine;
using System.Collections;

public class Laser_Reflection : MonoBehaviour {
	
	int reflectionMask;
	//LayerMask opaqueMask = 9;
	Ray laserRay;
	RaycastHit hit;
	LineRenderer laser;
	//	Vector3 rayDirection;
	public float range = 100f;
	public int max_reflection_number = 10;
	private float remainingRange;
	private int reflectionCount;
	private Vector3 newDir;
	private Vector3[] ReflectionPts;
	public LayerMask BlockingSurface;
	
	// Use this for initialization
	void Start () {
		laser = GetComponent<LineRenderer> ();
		//laser.SetVertexCount(max_reflection_number + 1);
		reflectionMask = LayerMask.GetMask ("Reflecting Surface");
		ReflectionPts = new Vector3[max_reflection_number];
		//blockMask = LayerMask.GetMask ("Opaque");
	}
	
	// Update is called once per frame
	void Update () {
		
		laserRay.origin = transform.position;
		laserRay.direction = transform.up;

		
		remainingRange = range;
		reflectionCount = 0;
		while (remainingRange > 0f && reflectionCount < max_reflection_number) {
			if (Physics.Raycast(laserRay, out hit, remainingRange, reflectionMask)) {

				ReflectionPts[reflectionCount] = hit.point;

				//After one hit, minus off from total range the distance travelled by after the previous reflection 
				remainingRange -= Vector3.Distance(hit.point, laserRay.origin);

				//laser.SetVertexCount(++reflectionCount);

				//set the new turning point in the line renderer.
				//laser.SetPosition(reflectionCount, hit.point);
				
				//reset a new origin of the laser ray, starting from the reflecting point
				laserRay.origin = hit.point;
				
				//reset a new direction of the ray by reflceting the incident vector3 from the plane hit.
				newDir = Vector3.Reflect(laserRay.direction, hit.normal);
				laserRay.direction = newDir;



				reflectionCount++;

			}
			//stop the laser when it hits an opaque object
			else if (Physics.Raycast(laserRay, out hit, remainingRange, BlockingSurface)) {
				ReflectionPts[reflectionCount] = hit.point;
				break;
			}
			else {
				ReflectionPts[reflectionCount] = laserRay.origin + laserRay.direction.normalized * remainingRange;
				break;
			}
		}
		laser.SetVertexCount(reflectionCount + 2);

		laser.SetPosition (0, transform.position);

		for(int i = 0; i < reflectionCount + 1; i++){
			laser.SetPosition(i + 1, ReflectionPts[i]);
		}
	}
}
