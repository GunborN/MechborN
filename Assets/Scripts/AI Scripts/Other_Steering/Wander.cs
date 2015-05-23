using UnityEngine;
using System.Collections;

public class Wander : MonoBehaviour 
{
    public float MOVE_SPEED = 3; // The movement speed of the character
    public const float GRAVITY = -50; // The force due to gravity (Constant for the purposes of this assignment)
    public float JITTER = 0.5f; // The interval to update the target position
    public float ON_FIRE_JITTER = 0.01f; // The interval to update the target position
    public float DISTANCE = 2f; // The distance from the character to the edge of the steering circle
    public float RADIUS = 2f; // The radius of the steering circle

    private CharacterController _controller; // The controller that is told how to move based on vector calculations
    private Vector3 _moveDirection; // The the directional vector of the character movement
    private Vector3 _targetPosition; // The position in world space the character will wander towards
    private Vector3 _steeringCirclePosition; // The position of the steering circle
    private float _timeSinceUpdate; // A counter used for updating target position
	private bool _wall = false;
    private float _SHIT_HITS_THE_FAN;
    private bool _SHIT_HAS_HIT_THE_FAN = false;

    void Awake()
    {
        _controller = GetComponent<CharacterController>(); // Get the controller component
        _moveDirection = Vector3.zero; // Initialize to zero
        _targetPosition = Vector3.zero; // Initialize to zero
        _steeringCirclePosition = Vector3.zero; // Initialize to zero
        _timeSinceUpdate = JITTER; // Initialize counter
        _SHIT_HITS_THE_FAN = 10f;

        transform.GetChild(0).gameObject.SetActive(false);
    }

    void Update()
    {
        if (_SHIT_HITS_THE_FAN <= 0.0f && _SHIT_HAS_HIT_THE_FAN == false)
        {
            transform.GetChild(0).gameObject.SetActive(true);


            _SHIT_HAS_HIT_THE_FAN = true;
        }

        if (_controller.isGrounded) // If the character is grounded.
        {
            if (_timeSinceUpdate <= Time.time) // If it's been enough time since the last update
            {
                Vector2 randomDirection = Random.insideUnitCircle.normalized * RADIUS; // Pick a random point on the edge of the circle
                _targetPosition = new Vector3(randomDirection.x, 0, randomDirection.y) + _steeringCirclePosition; // Update the target position

                if (_SHIT_HAS_HIT_THE_FAN == true)
                {
                    _timeSinceUpdate = ON_FIRE_JITTER + Time.time; // Update the timer.
                }

                else
                {
                    _timeSinceUpdate = JITTER + Time.time; // Update the timer.
                }
            }
        }

        transform.LookAt(new Vector3(_targetPosition.x, transform.position.y, _targetPosition.z)); // Update rotation so face target position
        _steeringCirclePosition = transform.position + transform.forward * (DISTANCE + RADIUS); // Update steering circle position

        Debug.DrawRay(transform.position, _targetPosition - transform.position, Color.blue); // Draw vector to target position

        /* Create a new force vector using the MOVE_SPEED constant to determine the x and z components and 
         * the GRAVITY constant to determine the y component. */
        Vector3 _moveForce = new Vector3(transform.forward.x * MOVE_SPEED, GRAVITY, transform.forward.z * MOVE_SPEED);
        _moveForce *= Time.deltaTime; // Get force relative to time.
		if(_wall){
			_steeringCirclePosition *= -1;
			_wall = false;

		}

        _SHIT_HITS_THE_FAN -= Time.deltaTime;

		_controller.Move(_moveForce); // Apply force vector.
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.tag == "HideObject")
        {
			_wall = true;
            
        }
    }

}
