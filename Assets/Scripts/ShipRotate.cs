using UnityEngine;
using System.Collections;

public class ShipRotate : MonoBehaviour {

	public float rotateSpeed = 0.1f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.RotateAround (transform.position, transform.forward, rotateSpeed*Time.deltaTime * 90f);
	}
}
