
//adapted from Conrad Parker's pseudocode of Craig W. Reynold's Boid's algorithm
//for Unity3D 
//http://www.red3d.com/cwr
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public enum ZombieType
{
    normal,
    rage,
    magic
}

//adaptation of Boids pseudocode into Unity taken from http://www.kfish.org/boids/pseudocode.html
public class ZombieBoidsAlgorithm2D : MonoBehaviour
{
    

    private HashSet<GameObject> _zombieList;
    public GameObject _zombiePrefab;
    public GameObject _rageZombiePrefab;
    public GameObject _magicZombiePrefab;
    public GameObject _explosion;
    public float _explosionRadius;
    public float _explosionForce;
    public List<GameObject> _humanList;
    public GameObject _humanPrefab;


    public int _humanCount;

    public float _velocityLimit;

    [Range(0.0f, 5.0f)]
    public float _tendToStrength;

    [Range(0.0f, 1.0f)]
    public float _cohesionStrength;

    [Range(0.0f, 1.0f)]
    public float _separationStrength;

    [Range(0.0f, 1.0f)]
    public float _allignmentStrength;

    public int _separationRadius;
    public int _flockRadius;
    public GameObject _target;

    

    public GameObject _resetTarget;
    public GameObject _humans;

	//make boids
	private void Awake ()
	{
        _zombieList = new HashSet<GameObject>();
        _humanList = new List<GameObject>();
        SpawnHumans();
        SpawnZombie(_resetTarget, true);
        

        StartCoroutine("Retarget");
	}

	private GameObject SpawnZombie(GameObject spawnPoint, bool isStarting)
	{
        int ZombieSpawnRNG = Random.Range(0, 100);
        if (isStarting == true)
        {
            // spawn a normal zombie if this is the first spawn
            ZombieSpawnRNG = 0;
        }
        GameObject clone;
        
        if (ZombieSpawnRNG < 66)
        {
            clone = Instantiate(_zombiePrefab, spawnPoint.transform.position, transform.localRotation) as GameObject;
            clone.GetComponent<BoidInfo>().ZombieType = ZombieType.normal;
        }

        //else if (ZombieSpawnRNG >= 50 && ZombieSpawnRNG <= 90)
        else
        {
            
            clone = Instantiate(_rageZombiePrefab, spawnPoint.transform.position, transform.localRotation) as GameObject;
            clone.GetComponent<BoidInfo>().ZombieType = ZombieType.rage;
            clone.GetComponent<RageSeek>().target = _humanList[Random.Range(0, _humanList.Count)];
        }
        /*
        else
        {
            clone = Instantiate(_magicZombiePrefab, spawnPoint.transform.position, transform.localRotation) as GameObject;
            clone.GetComponent<BoidInfo>().ZombieType = ZombieType.magic;
        }
        */
        clone.GetComponent<BoidInfo>().Position = spawnPoint.transform.position;
        clone.GetComponent<BoidInfo>().Velocity = new Vector3(Random.Range(-10f, 10f), 0f, Random.Range(-10f, 10f));
        clone.name = "Zombie " + _zombieList.Count;
        clone.transform.parent = gameObject.transform;
        _zombieList.Add(clone);
        
        

        return clone;
	}

    private void SpawnHumans()
    {
		for (int i = 0; i < _humanCount; i++)
        {	
            Vector3 spawnPoint = new Vector3(0, 1.08f, 40) + new Vector3 (Random.Range (-5f, 5f), 0f, Random.Range (-5f, 5f));
			GameObject clone = Instantiate (_humanPrefab, spawnPoint, transform.localRotation) as GameObject;
			clone.name = "Human " + i;
			clone.transform.parent = _humans.transform;
			_humanList.Add(clone);		
		}	    
    }

	void Update ()
	{
        if (_target == null)
        {
            Retarget();
        }

        MoveBoidsToNewPosition();
	}

	private void MoveBoidsToNewPosition ()
	{
        Vector3 v1 = Vector3.zero,
        v2 = Vector3.zero,
        v3 = Vector3.zero,
        v4 = Vector3.zero;
				
		foreach (GameObject zombie in _zombieList) 
        {
            BoidInfo boidInfo = zombie.GetComponent<BoidInfo>();
            if (boidInfo.ZombieType == ZombieType.normal)
            {
                //normalizing these vectors will give the direction the boid should move to
                //the magnitude of these vectors will give how fast the boid should move there
                //relative to the timestep which is time.deltaTime;
                v1 = _cohesionStrength * Cohesion(zombie);  //Flock Centering (Cohesion)											
                v2 = _separationStrength * Separation(zombie);  //Collision Avoidance (Seperation)
                v3 = _allignmentStrength * Allignment(zombie);  //Velocity Matching (Alignment)						
                v4 = _tendToStrength * TendToPlace(_target, zombie);

                /*
                Debug.Log("v1: " + v1);
                Debug.Log("v2: " + v2);
                Debug.Log("v3: " + v3);
                Debug.Log("v4: " + v4);
                */

                //the boidInfo.Velocity is the amount of positional change 
                //resulting in the offset vectors
                boidInfo.Velocity = (boidInfo.Velocity + v1 + v2 + v3 + v4);

                zombie.transform.rotation = Quaternion.LookRotation(boidInfo.Velocity);

                LimitSpeed(zombie);

                boidInfo.Velocity = new Vector3(boidInfo.Velocity.x, 0.0f, boidInfo.Velocity.z);
                //Interpret the velocity as how far the boid moves per time step we add it to the current position
                boidInfo.Position = boidInfo.Position + (boidInfo.Velocity * Time.deltaTime);
                //the new position of the boid is calculated by adding the offset vectors (v1,v2...vn) to the position
            }
        }
	}

	#region Rule 1: Boids try to fly towards the centre of mass of neighbouring boids.
		
	private Vector3 Cohesion (GameObject boid) //(cohesion
	{
        if (_zombieList.Count > 1)
        {
            BoidInfo boidInfo = boid.GetComponent<BoidInfo>(); //current boid info

            Vector3 perceivedCenter = Vector3.zero;

            foreach (GameObject b in _zombieList)
            {
                BoidInfo bInfo = b.GetComponent<BoidInfo>(); //neighbors

                if (b != boid)
                {
                    //doing another calculation to see if I can get some of the boids in the same system to split up at times
                    //for randomness
                    if (Vector3.Distance(bInfo.Position, boidInfo.Position) < _flockRadius)
                    {
                        //neighborhood
                        perceivedCenter += bInfo.Position;
                    }
                }
            }

            perceivedCenter /= (_zombieList.Count - 1); //dividing by the size of the array -1
            //gives the average perceived center of mass

            perceivedCenter = (perceivedCenter - boidInfo.Position) / 100;
            //how strong the boid will move to the center
            //higher means less strength
            return new Vector3(perceivedCenter.x, 0, perceivedCenter.z);
        }

        else
        {
            return Vector3.zero;
        }
	}

	#endregion

	#region Rule 2: Boids try to keep a small distance away from other objects (including other boids).
		
	private Vector3 Separation (GameObject boid)
	{
		BoidInfo boidInfo = boid.GetComponent<BoidInfo> (); //current boid info

		Vector3 displacement = Vector3.zero;

        foreach (GameObject b in _zombieList)
        {

			BoidInfo bInfo = b.GetComponent<BoidInfo> (); //neighbor

			if (b != boid) 
            {
				//if the distance between the current boid and his neighbor
				//is less than 10 they are too close and must be seperated
								
				if (Vector3.Distance (bInfo.Position, boidInfo.Position) < _separationRadius) 
                {		
					//calculate a displacement to move them apart
					//the displacement will result in a vector
					//that when added to the original velocity vector will
					//move them away from each other
					displacement = displacement - (bInfo.Position - boidInfo.Position);			
				}
			}
		}

		return displacement;
	}
	#endregion
		
	#region Rule 3: Boids try to match velocity with near boids.

	private Vector3 Allignment (GameObject boid)
	{
        if (_zombieList.Count > 1)
        {
            BoidInfo boidInfo = boid.GetComponent<BoidInfo>(); //current boid info

            Vector3 perceivedVelocity = Vector3.zero;

            foreach (GameObject b in _zombieList)
            {
                BoidInfo bInfo = b.GetComponent<BoidInfo>();

                if (b != boid)
                {
                    //if the distance from this boid and another boid is less than a set amount they
                    if (Vector3.Distance(boidInfo.Position, bInfo.Position) < _flockRadius)
                    {
                        perceivedVelocity += bInfo.Velocity;//are in the same neighborhood				
                    }
                }
            }

            perceivedVelocity /= (_zombieList.Count - 1);
            perceivedVelocity = (perceivedVelocity - boidInfo.Velocity) / 8; //using conrad's magic /8 till i get a better handle on what the vectors are doing
            //how strong the boid will try to match velocity
            //higher means less strength

            return new Vector3(perceivedVelocity.x, 0, perceivedVelocity.z);
        }

        else
        {
            return Vector3.zero;
        }
	}

	#endregion

	#region Limiting the speed

	private void LimitSpeed (GameObject boid)
	{				
		BoidInfo boidInfo = boid.GetComponent<BoidInfo> ();
        if (boidInfo.Velocity.magnitude > _velocityLimit)//if the size of the velocity is greater than the limit set
        {
            //normalize it and scale it by the limit
            boidInfo.Velocity = boidInfo.Velocity.normalized * _velocityLimit;
        }


		//magnitude is the length of a vector
		//dat mag is given by dat pythag
		//a^2 + b^2 = c^2
		//or a length c is given by the sqrt(a^2 + b^2)
	}

	#endregion

	#region Tend to place
    private Vector3 TendToPlace (GameObject place, GameObject boid)
    {
		Vector3 tendTo;
		BoidInfo boidInfo = boid.GetComponent<BoidInfo> ();

		tendTo = place.transform.position;
				
		tendTo = tendTo - (boidInfo.Position);
		tendTo = tendTo / 10;
		return new Vector3(tendTo.x, 0, tendTo.z);	
    }


	
	#endregion

    #region fuckshitup

    public void TargetBitten(GameObject poorSap)
    {
        if (_humanList.Remove(poorSap))
        {
            SpawnZombie(poorSap, false);
            Destroy(poorSap);
            Retarget();
        }
    }

    public void TargetExplode(GameObject explodeToBits, GameObject rageZombie)
    {
        if (_humanList.Remove(explodeToBits))
        {
            
            _zombieList.Remove(rageZombie);
            /*
            Collider[] colliders = Physics.OverlapSphere(explodeToBits.transform.position, _explosionRadius);

            foreach (Collider item in colliders)
            {
                if (item.gameObject.rigidbody != null)
                {
                    Debug.Log("i explode u");
                    item.gameObject.rigidbody.AddExplosionForce(_explosionForce, explodeToBits.transform.position, _explosionRadius, 0.0f, ForceMode.Impulse);
                }
            }
            */
            Instantiate(_explosion, explodeToBits.transform.position, Quaternion.identity);

            Destroy(explodeToBits);
            Destroy(rageZombie);

        }
    }

    private void Retarget()
    {
        if (_humanList.Count > 0)
        {
            //_target = _humanList[Random.Range(0, _humanList.Count)];

            // pass one: just target closest human

            // find center of zombie pack

            Vector3 centerSummation = Vector3.zero;

            foreach(GameObject item in _zombieList)
            {
                centerSummation += item.transform.position;
            }

            Vector3 centerOfZombies = centerSummation / _zombieList.Count;

            // find closest human to center

            _humanList.Sort
            (
                delegate(GameObject object1, GameObject object2)
                {
                    // compare the distance from each object to our position, using the magnitude of a distance vector
                    return ((object1.transform.position - centerOfZombies).magnitude).CompareTo((object2.transform.position - centerOfZombies).magnitude);
                }
            );

            _target = _humanList[0];

        }

        else
        {
            _target = _resetTarget;
        }
    }

    #endregion
}
