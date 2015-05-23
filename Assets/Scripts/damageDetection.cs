using UnityEngine;
using System.Collections;

public class damageDetection : MonoBehaviour {

	public float healthAmount;

	void Start () 
	{
		healthAmount = 100f;
	}
	
	void Update () 
	{
		
	}

	void OnCollisionEnter(Collision obj)
	{
		Debug.Log (obj.gameObject.name);
		if(obj.gameObject.layer == LayerMask.NameToLayer("Shield"))
		{
			Debug.Log ("HIT SHIELD!!!***");
			return;
		}else
		{
//			if()//obj.gameObject.GetComponent<>())
//			{
//
//			}
		}
	}

	void OnTriggerEnter(Collider obj)
	{
		if(obj.name == "Explosion")
		{
			Debug.Log(obj.name);
			obj.GetComponent<Collider>().enabled = false;

			if(obj.GetComponent<Shot1Damage>())
			{
				if((healthAmount - obj.GetComponent<Shot1Damage>().damage) <= 0f)
				{
					DestroyThis ();
				}
			}
		}
	}

	void DestroyThis()
	{
		if(this.name == "PA_ArchfireTank(Clone)")
		{
			Destroy (gameObject);
		}else if(this.name == "PA_DropPod(Clone)")
		{
			Destroy (gameObject);

		}else if(this.name == "PA_ArchlightBomber(Clone)")
		{
			Destroy (gameObject);

		}
	}
}
