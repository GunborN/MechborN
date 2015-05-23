using UnityEngine;
using System.Collections;

public class ObjectBehaviour : MonoBehaviour {
    public float orbitSpeed = 20.0f;
	void Start () 
    {
	
	}
	
	void Update () 
    {
        transform.RotateAround(new Vector3(30, 0, 30), Vector3.up, orbitSpeed * Time.deltaTime);
	}
}
