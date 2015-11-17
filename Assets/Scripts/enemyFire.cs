using UnityEngine;
using System.Collections;

public class enemyFire : MonoBehaviour {
	
	public GameObject shot1;
	private bool useShot1;
	
	private bool shooting;
	public bool canFire;

	public GameObject trans;
	
	
	void Start ()
	{
		shooting = false;
		useShot1 = true;
		canFire = false;
	}
	
	void Update () 
	{
		if(useShot1 && !shooting && canFire)
		{
			StartCoroutine(theShootingShot1 (shot1));
		}
	}

	IEnumerator theShootingShot1(GameObject shotName)
	{
		shooting = true;
		GameObject shot = Instantiate(shotName,trans.transform.position,transform.rotation) as GameObject;
		shot.GetComponent<EffectSettings>().Target = GameObject.FindGameObjectWithTag("Player");
		yield return new WaitForSeconds(1.2f);
		shooting = false;
	}
}
