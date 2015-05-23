using UnityEngine;
using System.Collections;

public class Actions : MonoBehaviour {

	//Melee components
	public float MeleeCooldown = 1.5f;
	private float meleeTimer;
	private bool meleeCheck;
	private GameObject melee;

	// Use this for initialization
	void Start () 
	{
		//Melee initializers
		meleeCheck = false;
		meleeTimer = Time.time;
		melee = transform.Find ("Melee").transform.Find("Slasher").transform.Find("Slash").gameObject;


	}
	
	void Update ()
	{
		//MELEE //////////////////////////
		//////////////////////////////////
//		if(Input.GetKey(KeyCode.V) && (meleeTimer <= Time.time))
//		{
//			melee.SetActive(true);
//			meleeTimer = Time.time + MeleeCooldown;
//			meleeCheck = true;
//		}
//
//		if(meleeCheck && meleeTimer <= Time.time)
//		{
//			meleeCheck = false;
//			melee.SetActive(false);
//		}
		//////////////////////////////////
		//////////////////////////////////
	}
}
