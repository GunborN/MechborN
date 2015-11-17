using UnityEngine;
using System.Collections;

public class yesDome : MonoBehaviour {

	private Transform player;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
		if(GameObject.FindGameObjectWithTag("Player"))
			gameObject.transform.position = player.position;
	}

	void OnTriggerEnter(Collider obj)
	{
		if(obj.tag == "Enemy")
		{
			obj.GetComponent<enemyFire>().canFire = true;
		}
	}

	void OnTriggerExit(Collider obj)
	{
		if(obj.tag == "Enemy")
		{
			obj.GetComponent<enemyFire>().canFire = false;
		}
	}
}
