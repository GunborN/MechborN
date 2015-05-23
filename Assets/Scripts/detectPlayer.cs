using UnityEngine;
using System.Collections;

public class detectPlayer : MonoBehaviour {

	private bool buffer;
	public float comeBackTimer = 4.0f;

	// Use this for initialization
	void Start () 
	{
		buffer = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(comeBackTimer <= Time.time && transform.GetComponentInParent<Flee>().dontFlee == false)
		{
			transform.GetComponentInParent<Flee>().dontFlee = true;
			comeBackTimer = Time.time +4.0f;
		}
	}

	void OnTriggerEnter(Collider obj)
	{
		if (obj.tag == "Player" && !buffer)
		{
			StartCoroutine(bufferTime ());
			transform.GetComponentInParent<Flee>().dontFlee = false;
		}
	}
	void OnTriggerExit(Collider obj)
	{
		if(obj.tag == "Player" && !buffer)
		{
			StartCoroutine(bufferTime ());
			transform.GetComponentInParent<Flee>().dontFlee = true;
		}
	}
	void OnTriggerStay(Collider obj)
	{
		if (obj.tag == "Player" && !buffer)
		{
			StartCoroutine(bufferTime ());
			transform.GetComponentInParent<Flee>().dontFlee = false;
		}
	}

	IEnumerator bufferTime()
	{
		buffer = true;
		yield return new WaitForSeconds (1f);
		buffer = false;
	}
}
