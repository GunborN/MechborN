using UnityEngine;
using System.Collections;

public class Shooting : MonoBehaviour {

	public GameObject shot1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButton(0))
		{
			GameObject shot = Instantiate(shot1,transform.position,transform.rotation) as GameObject;
			shot.GetComponent<EffectSettings>().Target = GameObject.FindGameObjectWithTag("Target2");


		}
	}
}
