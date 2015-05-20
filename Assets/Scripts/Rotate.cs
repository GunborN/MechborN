using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
	
	}
	
	void Update () 
	{
		transform.RotateAround (transform.position, transform.up, Time.deltaTime * 90f);
	}
}
