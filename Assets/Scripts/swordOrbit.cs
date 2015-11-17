using UnityEngine;
using System.Collections;

public class swordOrbit : MonoBehaviour {

	public Transform mech;
	public Rigidbody mech2;

	private GameObject sword;
	private Quaternion swordStep1;

	private Animator[] animators2;
	private Animator swordAnimator;
	private Animator gundam_sword;

	public GameObject trail;
	public GameObject targetObject;

	private Vector2 mouse;
	private Vector2 playerScreenPoint;

	public float forceAmount = 9e+07f;

	public float swordCooldown = 3.0f;
	private float timer;

	public GameObject warpSystem;
	private GameObject feet;

	void Start () 
	{
		timer = Time.time;

		animators2 = GetComponentsInChildren<Animator> ();
		foreach(Animator thisAnim2 in animators2)
		{
			if(thisAnim2.name == "swordOrbit")
			{
				swordAnimator = thisAnim2;
			}else if(thisAnim2.name == "gundam_sword")
			{
				gundam_sword = thisAnim2;
			}
		}
		sword = transform.Find ("gundam_sword").gameObject;

		feet = GameObject.FindGameObjectWithTag ("FEET");
	}


	void Update()
	{
		if(timer <= Time.time)
		{
			mouse = new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y);
			playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);

			if(mouse.x < playerScreenPoint.x)
			{
				if(Input.GetKeyDown(KeyCode.V))
				{

					StartCoroutine(SwingSwordLeft());
					sword.GetComponent<BoxCollider>().enabled = true;
					mech2.AddForce((targetObject.transform.position - mech.position).normalized * forceAmount * Time.smoothDeltaTime);
					Instantiate(warpSystem,feet.transform.position,Quaternion.identity);
					timer = Time.time + swordCooldown;
				}
			}else{
				if(Input.GetKeyDown(KeyCode.V))
				{
					StartCoroutine(SwingSwordRight());
					sword.GetComponent<BoxCollider>().enabled = true;
					mech2.AddForce((targetObject.transform.position - mech.position).normalized * forceAmount * Time.smoothDeltaTime);
					Instantiate(warpSystem,feet.transform.position,transform.rotation);
					timer = Time.time + swordCooldown;
				}
			}
		}
	}
	
	void FixedUpdate () 
	{
		if(GameObject.FindGameObjectWithTag("Player"))
			transform.position = mech.position;
	}

	IEnumerator SwingSwordLeft()
	{   
		trail.SetActive (true);
		swordAnimator.SetBool ("Swing", true);
		gundam_sword.SetBool ("Swing", true);

		yield return new WaitForSeconds(1f);

		swordAnimator.SetBool ("Swing", false);
		gundam_sword.SetBool ("Swing", false);
		sword.GetComponent<BoxCollider> ().enabled = false;
		trail.SetActive (false);
	}

	IEnumerator SwingSwordRight()
	{   
		trail.SetActive (true);
		swordAnimator.SetBool ("SwingRight", true);
		gundam_sword.SetBool ("Swing", true);
		
		yield return new WaitForSeconds(1f);
		
		swordAnimator.SetBool ("SwingRight", false);
		gundam_sword.SetBool ("Swing", false);
		sword.GetComponent<BoxCollider> ().enabled = false;
		trail.SetActive (false);
	}
}
