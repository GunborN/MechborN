using UnityEngine;
using System.Collections;

public class followMouse : MonoBehaviour {

	public Transform mech;
	public Transform target;
	private Quaternion baseRot;

	public float speed;
	void Start ()
	{
		speed = 80f;
	}

	void Update()
	{
		//Screen.lockCursor = true;
		//Screen.showCursor = false;
	}
	
	void FixedUpdate () {
		//transform.position = Vector3.Lerp (transform.position,mech.position,.1f);
		transform.position = mech.position;

		transform.LookAt (target.position);
		//transform.rotation = Quaternion.Euler(transform.rotation.x + 29f,transform.rotation.y,transform.rotation.z);
		//transform.rotation = Quaternion.Euler(transform.eulerAngles.x + 29f,transform.eulerAngles.y,transform.eulerAngles.z);

		//transform.RotateAround (Vector3.zero,-Vector3.forward,speed*Time.deltaTime);
	}
}
