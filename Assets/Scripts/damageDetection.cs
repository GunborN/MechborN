using UnityEngine;
using System.Collections;

public class damageDetection : MonoBehaviour {

	public float healthAmount;
	public GameObject theExplosion;
	private GameObject scoreObject;
	private GameObject healthBar;

	void Start () 
	{
		healthAmount = 100f;
		scoreObject = GameObject.Find ("TheScore");
		if(gameObject.tag == "Player")
		{
			healthBar = GameObject.Find("SlantBar");
			healthAmount = 1f;
		}
	}
	
	void Update () 
	{
		
	}

	void OnCollisionEnter(Collision obj)
	{
//		if(obj.gameObject.name == "EnemyShot1(Clone)" && this.gameObject.tag == "Player")
//		{
//			if(obj.gameObject.GetComponent<EnemyShot1Damage>())
//			{
//				float dmgAmount1 = obj.gameObject.GetComponent<EnemyShot1Damage>().damage;
//				if((healthAmount - dmgAmount1) <= 0f)
//				{
//					DestroyThis ();
//				}else
//				{
//					healthAmount -= dmgAmount1;
//					GameObject.Find ("SlantBar").GetComponent<GUIBarScript>().Value = healthAmount;
//				}
//			}
//		}

		if(obj.gameObject.layer == LayerMask.NameToLayer("Shield"))
		{
			//Debug.Log ("HIT SHIELD!!!***");
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
		if(LayerMask.LayerToName(this.gameObject.layer) != "Detector")
		{
			if(obj.name == "Explosion")
			{
				if(this.gameObject.tag == "Player")
				{
					//Debug.Log("GOT HERE!!!!!!!");
					if(obj.gameObject.GetComponent<EnemyShot1Damage>())
					{
						Debug.Log("GOT HERE!!!!!!!");

						float dmgAmount1 = obj.gameObject.GetComponent<EnemyShot1Damage>().damage;
						if((healthAmount - dmgAmount1) <= 0f)
						{
							DestroyThis ();
						}else
						{
							healthAmount -= dmgAmount1;
							GameObject.Find ("SlantBar").GetComponent<GUIBarScript>().Value = healthAmount;
						}
					}
				}

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
	}

	void DestroyThis()
	{

		if(this.tag == "Player")
		{
			Instantiate(theExplosion,gameObject.transform.position,Quaternion.identity);
			//GameObject.FindGameObjectWithTag("Game Over").SetActive(true);
			//GameObject.Find("EscapeMenu").SetActive(true);
			//GameObject.FindGameObjectWithTag("MainCamera").GetComponent<LookatTarget>();
			GameObject.Find ("SlantBar").GetComponent<GUIBarScript>().Value = 0;
			Destroy (gameObject);
		}
		else if(this.name == "PA_ArchfireTank(Clone)")
		{
			Instantiate(theExplosion,gameObject.transform.position,Quaternion.identity);
			scoreObject.SendMessage("UpdateScore",20,SendMessageOptions.RequireReceiver);
			Destroy (gameObject);
		}else if(this.name == "PA_DropPod(Clone)")
		{
			Instantiate(theExplosion,gameObject.transform.position,Quaternion.identity);
			scoreObject.SendMessage("UpdateScore",10,SendMessageOptions.RequireReceiver);
			Destroy (gameObject);

		}else if(this.name == "PA_ArchlightBomber(Clone)")
		{
			Instantiate(theExplosion,gameObject.transform.position,Quaternion.identity);
			scoreObject.SendMessage("UpdateScore",30,SendMessageOptions.RequireReceiver);
			Destroy (gameObject);
		}
	}
}
