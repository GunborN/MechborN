using UnityEngine;
using System.Collections;

public class PlayerBehaviour : MonoBehaviour 
{

    /// <summary>
    /// How fast the player should move
    /// </summary>
    public float speed = 0.5f;

	void Start () 
    {
	
	}
	
	void Update () 
    {
        Vector3 newPosition = new Vector3(transform.position.x + speed*(Input.GetAxis("Horizontal")), transform.position.y, transform.position.z + speed*(Input.GetAxis("Vertical")));
        transform.position = newPosition;
	}
}
