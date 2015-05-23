using UnityEngine;
using System.Collections;

public class MasterExploder : MonoBehaviour {
    private const float MASTER_EXPLODE_TIME = 5.0f;
    private float explodeTime;
	// Use this for initialization

    void Awake()
    {
        explodeTime = Time.time;
    }
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (explodeTime <= Time.time)
        {
            Collider[] explosions = Physics.OverlapSphere(transform.position, 300.0f);

            foreach (Collider item in explosions)
            {
                item.gameObject.AddComponent<Rigidbody>();
                item.gameObject.GetComponent<Rigidbody>().AddExplosionForce(50f, transform.position, 100000.0f, 3.0f);
            }

            explodeTime = MASTER_EXPLODE_TIME + Time.time;
        }
	}
}
