using UnityEngine;
using System.Collections;

public class Flee : MonoBehaviour
{
    public GameObject player;
    private Vector3 targetPosition;

    public float MoveSpeed;
	public bool dontFlee;

	private Vector3 newPosition;

    void Awake()
    {
		dontFlee = true;
        //player = GameObject.FindGameObjectWithTag("Player");
        targetPosition = Vector3.zero;
    }

    void Update()
    {
        targetPosition = new Vector3(player.transform.position.x,player.transform.position.y,0.0f);

		if(dontFlee)
		{
        	newPosition = Vector3.MoveTowards(transform.position, player.transform.position, MoveSpeed * Time.deltaTime);
		}else{
			newPosition = Vector3.MoveTowards(transform.position, player.transform.position, -MoveSpeed * Time.deltaTime);
		}

        transform.position = newPosition;
		transform.position = new Vector3(newPosition.x,newPosition.y,2.0f);
		transform.LookAt (targetPosition);
    }
//	private Vector3 Separation (GameObject boid)
//	{
//		BoidInfo boidInfo = boid.GetComponent<BoidInfo> (); //current boid info
//		
//		Vector3 displacement = Vector3.zero;
//		
//		foreach (GameObject b in _zombieList)
//		{
//			
//			BoidInfo bInfo = b.GetComponent<BoidInfo> (); //neighbor
//			
//			if (b != boid) 
//			{
//				//if the distance between the current boid and his neighbor
//				//is less than 10 they are too close and must be seperated
//				
//				if (Vector3.Distance (bInfo.Position, boidInfo.Position) < _separationRadius) 
//				{		
//					//calculate a displacement to move them apart
//					//the displacement will result in a vector
//					//that when added to the original velocity vector will
//					//move them away from each other
//					displacement = displacement - (bInfo.Position - boidInfo.Position);			
//				}
//			}
//		}
//		
//		return displacement;
//	}
}
