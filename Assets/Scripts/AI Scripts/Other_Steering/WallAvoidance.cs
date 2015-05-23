using UnityEngine;
using System.Collections;

public class WallAvoidance : MonoBehaviour {

	// Use this for initialization
	void Start () {
		_boundary = 5;	
		_direction = Vector3.zero;
		_direction = RandomDirection(gameObject);

	}
	
	// Update is called once per frame
	void Update () {
		Vector3 boundsOffset = Vector3.zero;

		boundsOffset = CheckBounds(gameObject);

		_velocity += boundsOffset + _direction;
		_speed = _velocity.magnitude;

		if(_speed > 5)
			_velocity = _velocity.normalized * 5;

		gameObject.transform.position += (_velocity * Time.deltaTime);

		_direction = _velocity.normalized;
		gameObject.transform.rotation = Quaternion.LookRotation (_velocity);

		Vector3 circlePosition = WanderDirection(gameObject);

		print ("circle is at " + circlePosition);

	
	}

	private Vector3 RandomDirection(GameObject node)
	{
		Vector3 randomDirection = new Vector3(1,0,0);
		return randomDirection;

	}

	private Vector3 WanderDirection(GameObject node)
	{
		int radius = 2;
		int circleDistance = 2;
		Vector3 circlePosition = _direction * circleDistance;



		//Vector2 randomPt = new Vector2(Mathf.Cos(),Mathf.Sin());
		return circlePosition;


	}

	private Vector3 CheckBounds (GameObject node)
	{	
		int Xmin = -(_boundary), 
		Xmax = _boundary, 
		Ymin = -(_boundary), 
		Ymax = _boundary, 
		Zmin = -(_boundary / 2), 
		Zmax = 5;
		
		
		Vector3 v = new Vector3 ();
		if (node.transform.position.x < Xmin) {						
			v.x = _boundsStr;
		} else if (node.transform.position.x > Xmax) {
			v.x = -(_boundsStr);
		}
		if (node.transform.position.y < Ymin) {
			v.y = _boundsStr;
		} else if (node.transform.position.y > Ymax) {
			//print ("boid out of y neg");
			v.y = -(_boundsStr);
		}
		if (node.transform.position.z < Zmin) {
			v.z = _boundsStr;
		} else if (node.transform.position.z > Zmax) {
			v.z = -(_boundsStr);
		}
	
		
		return v;
		
		
		
	}

	public int _boundary;
	public float _boundsStr;
	private Vector3 _direction;
	private Vector3 _velocity;
	private float _speed;
}
