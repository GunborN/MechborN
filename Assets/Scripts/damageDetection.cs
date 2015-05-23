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
			obj.GetComponent<SphereCollider>().enabled = false;

			if(obj.GetComponent<Shot1Damage>())
			{
				float dmgAmount1 = obj.GetComponent<Shot1Damage>().damage;
				if((healthAmount - dmgAmount1) <= 0f)
				{
					DestroyThis ();
				}else
				{
					healthAmount -= dmgAmount1;
				}
			}else if(obj.GetComponent<Shot2Damage>())
			{
				float dmgAmount2 = obj.GetComponent<Shot2Damage>().damage;
				if((healthAmount - dmgAmount2) <= 0f)
				{
					DestroyThis ();
				}else
				{
					healthAmount -= dmgAmount2;
				}
			}else if(obj.GetComponent<BlueParticleDamage>())
			{
				float dmgAmount3 = obj.GetComponent<BlueParticleDamage>().damage;
				if((healthAmount - dmgAmount3) <= 0f)
				{
					DestroyThis ();
				}else
				{
					healthAmount -= dmgAmount3;
				}
			}
		}else if(obj.name == "gundam_sword")
		{
			if(obj.GetComponent<SwordDamage>())
			{
				Debug.Log("SWORD HIT ME!!!");
				float dmgAmount1 = obj.GetComponent<SwordDamage>().damage;
				if((healthAmount - dmgAmount1) <= 0f)
				{
					DestroyThis ();
				}else
				{
					healthAmount -= dmgAmount1;
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
