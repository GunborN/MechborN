using UnityEngine;
using System.Collections;

public class swordOrbit : MonoBehaviour {

	public Transform mech;
	private GameObject sword;
	private Quaternion swordStep1;

	private Animator[] animators2;
	private Animator swordAnimator;
	private Animator gundam_sword;


	void Start () 
	{
		animators2 = GetComponentsInChildren<Animator> ();
		foreach(Animator thisAnim2 in animators2)
		{
			Debug.Log (thisAnim2.name);

			if(thisAnim2.name == "swordOrbit")
			{
				swordAnimator = thisAnim2;
			}else if(thisAnim2.name == "gundam_sword")
			{
				gundam_sword = thisAnim2;
			}
		}
		sword = transform.Find ("gundam_sword").gameObject;
	}


	void Update()
	{

		if(Input.GetKey(KeyCode.B))
		{
			StartCoroutine(SwingSword());
		}
	}
	
	void FixedUpdate () 
	{
		transform.position = mech.position;


	}

	IEnumerator SwingSword()
	{   
		swordAnimator.SetBool ("Swing", true);
		gundam_sword.SetBool ("Swing", true);


		yield return new WaitForSeconds(1f);

		swordAnimator.SetBool ("Swing", false);
		gundam_sword.SetBool ("Swing", false);

	}
}
