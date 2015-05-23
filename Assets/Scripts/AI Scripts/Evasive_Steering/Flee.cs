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



//        if(    transform.position.y<=-8f 
//            || transform.position.y >= 8f 
//            || transform.position.x <= -15f
//            || transform.position.x >= 7f)
//        {
//            transform.position = Vector3.zero;
//        }
    }
}
