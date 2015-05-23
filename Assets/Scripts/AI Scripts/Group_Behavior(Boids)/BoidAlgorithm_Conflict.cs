//notes: 3/3/2014 boids keep flying to flock even though they should be ignoring them
//written by Matthew Williamson
//adapted from Conrad Parker's pseudocode of Craig W. Reynold's Boid's algorithm
//for Unity3D 
//http://www.red3d.com/cwr
using UnityEngine;
using System.Collections;

//adaptation of Boids pseudocode into Unity taken from http://www.kfish.org/boids/pseudocode.html
public class BoidAlgorithm_Conflict : MonoBehaviour
{
		//make boids
		private void Awake ()
		{

				_boidsArray = new GameObject[_numBoids];
				SpawnBoids ();
		}
		private void SpawnBoids ()
		{
				for (int i = 0; i < _boidsArray.Length; i++) {
					
						GameObject clone = Instantiate (_boidPrefab, transform.localPosition, transform.localRotation) as GameObject;
						clone.GetComponent<BoidInfo> ().Position = new Vector3 (Random.Range (-35f, 35f), Random.Range (-35f, 35f), Random.Range (-10f, 10f));
						clone.GetComponent<BoidInfo> ().Velocity = new Vector3 (Random.Range (-10f, 10f), Random.Range (-10f, 10f), Random.Range (-10f, 10f));
						clone.name = "Boid " + i;
						clone.transform.parent = gameObject.transform;
						_boidsArray [i] = clone;
						
				}	

				
		}
		void Update ()
		{
				MoveBoidsToNewPosition ();
		}

		private void MoveBoidsToNewPosition ()
		{
				Vector3 v1 = Vector3.zero, 
				v2 = Vector3.zero, 
				v3 = Vector3.zero, 
				v4 = Vector3.zero,
				v5 = Vector3.zero,
				v6 = Vector3.zero;
				
				
				foreach (GameObject boid in _boidsArray) {		

						BoidInfo boidInfo = boid.GetComponent<BoidInfo> ();
                       

						//normalizing these vectors will give the direction the boid should move to
						//the magnitude of these vectors will give how fast the boid should move there
						//relative to the timestep which is time.deltaTime;
						v1 = _rule1Strength * Rule1 (boid);  //Flock Centering (Cohesion)											
						v2 = _rule2Strength * Rule2 (boid);  //Collision Avoidance (Seperation)
						v3 = _rule3Strength * Rule3 (boid);  //Velocity Matching (Alignment)						
						v5 = (-1 * _tendToStrength) * TendToPlace (_target, boid);
                        v6 = _avoidStrength * TendToPlace(_avoid, boid);
                    
						
						
						//the boidInfo.Velocity is the amount of positional change 
						//resulting in the offset vectors
						boidInfo.Velocity = (boidInfo.Velocity + v1 + v2 + v3 + v5 + v6);
						
						boid.transform.rotation = Quaternion.LookRotation (boidInfo.Velocity);

						LimitSpeed (boid);
						//Interpret the velocity as how far the boid moves per time step we add it to the current position
						boidInfo.Position = boidInfo.Position + (boidInfo.Velocity * Time.deltaTime);
                        boidInfo.Position = new Vector3(boidInfo.Position.x, boidInfo.Position.y, 0);
		
						//the new position of the boid is calculated by adding the offset vectors (v1,v2...vn) to the position
				}
		}

	#region Rule 1: Boids try to fly towards the centre of mass of neighbouring boids.
		
		private Vector3 Rule1 (GameObject boid) //(cohesion
		{
				BoidInfo boidInfo = boid.GetComponent<BoidInfo> (); //current boid info

				Vector3 perceivedCenter = Vector3.zero;

				foreach (GameObject b in _boidsArray) {
						BoidInfo bInfo = b.GetComponent<BoidInfo> (); //neighbors


						if (b != boid) {	
								//doing another calculation to see if I can get some of the boids in the same system to split up at times
								//for randomness
								if (Vector3.Distance (bInfo.Position, boidInfo.Position) < _flockRadius) //neighborhood
										perceivedCenter += bInfo.Position;
						}
				}

				perceivedCenter /= (_boidsArray.Length - 1); //dividing by the size of the array -1
				//gives the average perceived center of mass
				
				perceivedCenter = (perceivedCenter - boidInfo.Position) / 100;
				//how strong the boid will move to the center
				//higher means less strength
				return perceivedCenter;
		}
	#endregion

	#region Rule 2: Boids try to keep a small distance away from other objects (including other boids).
		
		private Vector3 Rule2 (GameObject boid)
		{
				BoidInfo boidInfo = boid.GetComponent<BoidInfo> (); //current boid info

				Vector3 displacement = Vector3.zero;

				foreach (GameObject b in _boidsArray) {

						BoidInfo bInfo = b.GetComponent<BoidInfo> (); //neighbor

						if (b != boid) {
								//if the distance between the current boid and his neighbor
								//is less than 10 they are too close and must be seperated
								
								if (Vector3.Distance (bInfo.Position, boidInfo.Position) < _rule2Radius) {
									
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

		private Vector3 Rule3 (GameObject boid)
		{
				BoidInfo boidInfo = boid.GetComponent<BoidInfo> (); //current boid info

				Vector3 perceivedVelocity = Vector3.zero;

				foreach (GameObject b in _boidsArray) {
						BoidInfo bInfo = b.GetComponent<BoidInfo> ();
						if (b != boid) {
								if (Vector3.Distance (boidInfo.Position, bInfo.Position) < _flockRadius) {//if the distance from this boid and another boid is less than a set amount they
										perceivedVelocity += bInfo.Velocity;//are in the same neighborhood
										
								}
						}
						
				}

				perceivedVelocity /= (_boidsArray.Length - 1);
				perceivedVelocity = (perceivedVelocity - boidInfo.Velocity) / 8; //using conrad's magic /8 till i get a better handle on what the vectors are doing
				//how strong the boid will try to match velocity
				//higher means less strength

				
				
				return perceivedVelocity;
		}

	#endregion


	#region Limiting the speed
		

		private void LimitSpeed (GameObject boid)
		{				
				BoidInfo boidInfo = boid.GetComponent<BoidInfo> ();

				if (boidInfo.Velocity.magnitude > _velocityLimit) //if the size of the velocity is greater than the limit set
						//normalize it and scale it by the limit
						boidInfo.Velocity = boidInfo.Velocity.normalized * _velocityLimit;
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
				return tendTo;	
		}


	
	#endregion
	
		private IEnumerator Wait (float time)
		{

				yield return new WaitForSeconds (time);
		}

			
		
		private GameObject[] _boidsArray;
		public GameObject _boidPrefab;
		public int _numBoids;	
		public float _velocityLimit;
		public float _rule1Strength;
		public float _rule2Strength;
		public int _rule2Radius;
		public float _rule3Strength;
		public int _flockRadius;
		public GameObject _target;
		public float _tendToStrength;
        public GameObject _avoid;
        public float _avoidStrength;

		

		
























	
}
