using UnityEngine;
using System.Collections;

public class Laser_Reflection_2: MonoBehaviour {

	/****************
	 * Attached to the laser emitter gameobject. Control reflections of laser rays from any surface labelled 
	 * "Reflecting Surface" layer.
	 * Other opaque surface is labelled by BlockingSurface Mask, a changeable public variable.
	 * If the layer strikes a surface that is neither opaque nor reflecting, it will pass through unobstructed.*/

	public RaycastHit hit;
	public float range = 200f;
	public int max_reflection_number = 20;
	public LayerMask BlockingSurface;
	public float refractiveIndex = 1.5f;
	
	private int reflectionMask;
	private int refractionMask;
	private float criticalAngle;
	private float incidentAngle;
	private float refractAngle;
	private Ray laserRay1;
	private LineRenderer laser1;
	private float remainingRange;
	private int reflectionCount;
	private Vector3 newDir;
	private Vector3 oldDir;
	private Vector3[] ReflectionPts;
	private bool refracted = false;
	private float distanceInGlass;

	void Start () {
		laser1 = GetComponent<LineRenderer> ();
		//laser2 = GetComponent<LineRenderer> ();
		reflectionMask = LayerMask.GetMask ("Reflecting Surface");
		refractionMask = LayerMask.GetMask ("Refracting Surface");

		//create an array to hold all way points data. i.e. the dots to be joined by the line renderer's drawing.
		ReflectionPts = new Vector3[max_reflection_number];
		//RefractionPts = new Vector3[max_reflection_number]; 
		criticalAngle = Mathf.Asin(1.0f/refractiveIndex);
	}

	void Update () {

		//initialise the orientation and location of laser just emitted out of the emitter.
		laserRay1.origin = transform.position;

		//emit the laser in the y-axis' direction of the gameobject.
		laserRay1.direction = transform.up;

		//track how far the ray can still travel after reflections.
		remainingRange = range;
	
		reflectionCount = 0;
		while (remainingRange > 0f && (reflectionCount < max_reflection_number)) {
			if (Physics.Raycast(laserRay1, out hit, remainingRange, refractionMask)) {
				
				//store a waypoint. i.e. the place where a reflection takes place
				ReflectionPts[reflectionCount] = hit.point;
				
				//After one hit, minus off from total range the distance travelled by after the previous reflection 
				remainingRange -= Vector3.Distance(hit.point, laserRay1.origin);



				incidentAngle = Vector3.Angle (laserRay1.direction, hit.normal);
				if (incidentAngle > 90f) incidentAngle = 180f - incidentAngle;

				hit.collider.bounds.IntersectRay (laserRay1, out distanceInGlass);

				oldDir = laserRay1.direction;
				refractAngle = Mathf.Asin (Mathf.Sin (incidentAngle/180f*Mathf.PI)/refractiveIndex);
				refractAngle = refractAngle/Mathf.PI * 180f;

				newDir = oldDir;
				newDir = Quaternion.AngleAxis ((refractAngle - incidentAngle), Vector3.Cross (oldDir, hit.normal)) * newDir;  

				laserRay1.direction = newDir;

				laserRay1.origin = hit.point;
				refracted = true;
					
				reflectionCount ++;
				
			}
			
			if (refracted) {
				laserRay1.origin = laserRay1.GetPoint (distanceInGlass);
				remainingRange -= distanceInGlass;

				ReflectionPts[reflectionCount] = laserRay1.origin;
				laserRay1.direction = oldDir;

				refracted = false;
				reflectionCount ++;
			}

			if (Physics.Raycast(laserRay1, out hit, remainingRange, reflectionMask)) {
				
				//store a waypoint. i.e. the place where a reflection takes place
				ReflectionPts[reflectionCount] = hit.point;
				
				//After one hit, minus off from total range the distance travelled by after the previous reflection 
				remainingRange -= Vector3.Distance(hit.point, laserRay1.origin);
				
				//reset a new origin of the laser ray, starting from the reflecting point
				laserRay1.origin = hit.point;
				
				//reset a new direction of the ray by reflceting the incident vector3 from the plane hit.
				newDir = Vector3.Reflect(laserRay1.direction, hit.normal);
				laserRay1.direction = newDir;
				
				if (hit.collider.tag == "LaserTrigger")
				{
					//testing
					//hit.transform.position = hit.transform.position + hit.transform.up * speed * Time.deltaTime;
					hit.collider.GetComponent <LaserReceiver> ().TriggeredByLaser ();  // Activate triggered actions
				}
				
				reflectionCount++;
				
			}
			//stop the laser when it hits an opaque object
			else if (Physics.Raycast(laserRay1, out hit, remainingRange, BlockingSurface)) {
				ReflectionPts[reflectionCount] = hit.point;
				if (hit.collider.tag == "LaserTrigger")
				{
					hit.collider.GetComponent <LaserReceiver> ().TriggeredByLaser ();  // Activate triggered actions
				}
				break;
			}



			else {
				ReflectionPts[reflectionCount] = laserRay1.origin + laserRay1.direction.normalized * remainingRange;

				break;		
			}
		}
		//set the total number of waypoints for the renderer.
		laser1.SetVertexCount(reflectionCount + 2);
		//laser2.SetVertexCount (refractionCount +1);

		//initial point (tip of the laser pointer)
		laser1.SetPosition (0, transform.position);

		for(int i = 0; i < reflectionCount + 1; i++){
			laser1.SetPosition(i + 1, ReflectionPts[i]);
		}

	}
	
}
