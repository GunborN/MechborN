using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour 
{

    public float speed;

	// Use this for initialization
	void Start () 
    {
	    speed = 10.0f;
	}
	
	// Update is called once per frame
	void Update () 
    {
        Vector3 newPos = transform.position;

        

        newPos.x += Input.GetAxis("Horizontal") * speed * Time.deltaTime;

        newPos.z += Input.GetAxis("Vertical") * speed * Time.deltaTime;

        

        transform.position = newPos;
	}
}
