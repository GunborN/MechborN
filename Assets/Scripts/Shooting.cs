using UnityEngine;
using System.Collections;

public class Shooting : MonoBehaviour {

	public GameObject shot1;
	public bool useShot1;

	public GameObject shot2;
	public bool useShot2;

	public GameObject shot3;
	public bool useShot3;

	public GameObject blueFire2;
	public bool useBlueFire2;
	
	public GameObject blueShot;
	public bool useBlueShot;
	
	public GameObject elecBall;
	public bool useElecBall;

	public GameObject elecBeam;
	public bool useElecBeam;
	
	public GameObject fireBeam;
	public bool useFireBeam;
	
	public GameObject fireBolt;
	public bool useFireBolt;

	public GameObject frostBolt;
	public bool useFrostBolt;
	
	public GameObject greenFireball;
	public bool useGreenFireball;
	
	public GameObject greenImpactWave;
	public bool useGreenImpactWave;

	public GameObject impactWave;
	public bool useImpactWave;
	
	public GameObject queueShotTrail;
	public bool useQueueShotTrail;
	
	public GameObject rocket2;
	public bool useRocket2;

	public GameObject whirlingShot;
	public bool useWhirlingShot;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButton(0))
		{
			if(useShot1)
			{
				GameObject shot = Instantiate(shot1,transform.position,transform.rotation) as GameObject;
				shot.GetComponent<EffectSettings>().Target = GameObject.FindGameObjectWithTag("Target2");
			}else if(useShot2)
			{
				GameObject shot = Instantiate(shot2,transform.position,transform.rotation) as GameObject;
				shot.GetComponent<EffectSettings>().Target = GameObject.FindGameObjectWithTag("Target2");
			}else if(useShot3)
			{
				GameObject shot = Instantiate(shot3,transform.position,transform.rotation) as GameObject;
				shot.GetComponent<EffectSettings>().Target = GameObject.FindGameObjectWithTag("Target2");
			}else if(useBlueFire2)
			{
				GameObject shot = Instantiate(blueFire2,transform.position,transform.rotation) as GameObject;
				shot.GetComponent<EffectSettings>().Target = GameObject.FindGameObjectWithTag("Target2");
			}else if(useBlueShot)
			{
				GameObject shot = Instantiate(blueShot,transform.position,transform.rotation) as GameObject;
				shot.GetComponent<EffectSettings>().Target = GameObject.FindGameObjectWithTag("Target2");
			}else if(useElecBall)
			{
				GameObject shot = Instantiate(elecBall,transform.position,transform.rotation) as GameObject;
				shot.GetComponent<EffectSettings>().Target = GameObject.FindGameObjectWithTag("Target2");
			}else if(useElecBeam)
			{
				GameObject shot = Instantiate(elecBeam,transform.position,transform.rotation) as GameObject;
				shot.GetComponent<EffectSettings>().Target = GameObject.FindGameObjectWithTag("Target2");
			}else if(useFireBeam)
			{
				GameObject shot = Instantiate(fireBeam,transform.position,transform.rotation) as GameObject;
				shot.GetComponent<EffectSettings>().Target = GameObject.FindGameObjectWithTag("Target2");
			}else if(useFireBolt)
			{
				GameObject shot = Instantiate(fireBolt,transform.position,transform.rotation) as GameObject;
				shot.GetComponent<EffectSettings>().Target = GameObject.FindGameObjectWithTag("Target2");
			}else if(useFrostBolt)
			{
				GameObject shot = Instantiate(frostBolt,transform.position,transform.rotation) as GameObject;
				shot.GetComponent<EffectSettings>().Target = GameObject.FindGameObjectWithTag("Target2");
			}else if(useGreenFireball)
			{
				GameObject shot = Instantiate(greenFireball,transform.position,transform.rotation) as GameObject;
				shot.GetComponent<EffectSettings>().Target = GameObject.FindGameObjectWithTag("Target2");
			}else if(useGreenImpactWave)
			{
				GameObject shot = Instantiate(greenImpactWave,transform.position,transform.rotation) as GameObject;
				shot.GetComponent<EffectSettings>().Target = GameObject.FindGameObjectWithTag("Target2");
			}else if(useImpactWave)
			{
				GameObject shot = Instantiate(impactWave,transform.position,transform.rotation) as GameObject;
				shot.GetComponent<EffectSettings>().Target = GameObject.FindGameObjectWithTag("Target2");
			}else if(useQueueShotTrail)
			{
				GameObject shot = Instantiate(queueShotTrail,transform.position,transform.rotation) as GameObject;
				shot.GetComponent<EffectSettings>().Target = GameObject.FindGameObjectWithTag("Target2");
			}else if(useRocket2)
			{
				GameObject shot = Instantiate(rocket2,transform.position,transform.rotation) as GameObject;
				shot.GetComponent<EffectSettings>().Target = GameObject.FindGameObjectWithTag("Target2");
			}else if(useWhirlingShot)
			{
				GameObject shot = Instantiate(whirlingShot,transform.position,transform.rotation) as GameObject;
				shot.GetComponent<EffectSettings>().Target = GameObject.FindGameObjectWithTag("Target2");
			}
		}
	}
}
