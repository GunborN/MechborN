using UnityEngine;
using System.Collections;

public class gunLook2 : MonoBehaviour {
	
	private GameObject target;
	//public float theAngle;
	
	void Start ()
	{
		target = GameObject.FindGameObjectWithTag ("Target2");
	}
	
	void Update () 
	{
		//float angle = theAngle * Mathf.Deg2Rad;
		//Vector3 targetDir = new Vector3(Mathf.Cos(angle),0, 0);
		transform.LookAt (target.transform.position);// + targetDir);
		
		//transform.rotation = Quaternion.Euler(transform.rotation.x + 29f,transform.rotation.y,transform.rotation.z);
		transform.rotation = Quaternion.Euler(transform.eulerAngles.x + 90f,transform.eulerAngles.y,transform.eulerAngles.z);	
	}
}
