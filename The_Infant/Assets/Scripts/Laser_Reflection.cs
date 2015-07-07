using UnityEngine;
using System.Collections;

public class Laser_Reflection : MonoBehaviour {

	/****************
	 * Attached to the laser emitter gameobject. Control reflections of laser rays from any surface labelled 
	 * "Reflecting Surface" layer.
	 * Other opaque surface is labelled by BlockingSurface Mask, a changeable public variable.
	 * If the layer strikes a surface that is neither opaque nor reflecting, it will pass through unobstructed.*/

	public RaycastHit hit;
	public float range = 100f;
	public int max_reflection_number = 10;
	public LayerMask BlockingSurface;

	private int reflectionMask;
	private Ray laserRay;
	private LineRenderer laser;
	private float remainingRange;
	private int reflectionCount;
	private Vector3 newDir;
	private Vector3[] ReflectionPts;
	//testing
	//float speed = 9f;

	void Start () {
		laser = GetComponent<LineRenderer> ();
		reflectionMask = LayerMask.GetMask ("Reflecting Surface");

		//create an array to hold all way points data. i.e. the dots to be joined by the line renderer's drawing.
		ReflectionPts = new Vector3[max_reflection_number];
	}

	void Update () {

		//initialise the orientation and location of laser just emitted out of the emitter.
		laserRay.origin = transform.position;

		//emit the laser in the y-axis' direction of the gameobject.
		laserRay.direction = transform.up;

		//track how far the ray can still travel after reflections.
		remainingRange = range;

		reflectionCount = 0;
		while (remainingRange > 0f && reflectionCount < max_reflection_number) {
			if (Physics.Raycast(laserRay, out hit, remainingRange, reflectionMask)) {

				//store a waypoint. i.e. the place where a reflection takes place
				ReflectionPts[reflectionCount] = hit.point;

				//After one hit, minus off from total range the distance travelled by after the previous reflection 
				remainingRange -= Vector3.Distance(hit.point, laserRay.origin);
				
				//reset a new origin of the laser ray, starting from the reflecting point
				laserRay.origin = hit.point;
				
				//reset a new direction of the ray by reflceting the incident vector3 from the plane hit.
				newDir = Vector3.Reflect(laserRay.direction, hit.normal);
				laserRay.direction = newDir;

				if (hit.collider.tag == "LaserTrigger")
				{
					//testing
					//hit.transform.position = hit.transform.position + hit.transform.up * speed * Time.deltaTime;
					hit.collider.gameObject.SendMessage("TriggeredByLaser"); // Activate triggered actions
				}

				reflectionCount++;

			}
			//stop the laser when it hits an opaque object
			else if (Physics.Raycast(laserRay, out hit, remainingRange, BlockingSurface)) {
				ReflectionPts[reflectionCount] = hit.point;
				if (hit.collider.tag == "LaserTrigger")
				{
					hit.collider.gameObject.SendMessage("TriggeredByLaser");  // Activate triggered actions
				}
				break;
			}
			else {
				ReflectionPts[reflectionCount] = laserRay.origin + laserRay.direction.normalized * remainingRange;
				break;
			}
		}

		//set the total number of waypoints for the renderer.
		laser.SetVertexCount(reflectionCount + 2);

		//initial point (tip of the laser pointer)
		laser.SetPosition (0, transform.position);

		for(int i = 0; i < reflectionCount + 1; i++){
			laser.SetPosition(i + 1, ReflectionPts[i]);
		}
	}
}
