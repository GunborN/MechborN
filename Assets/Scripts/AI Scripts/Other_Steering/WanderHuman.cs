using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WanderHuman : MonoBehaviour 
{
    public float MOVE_SPEED = 3; // The movement speed of the character
    public const float GRAVITY = -50; // The force due to gravity (Constant for the purposes of this assignment)
    public const float JITTER = 0.5f; // The interval to update the target position
    public const float ON_FIRE_JITTER = 0.01f; // The interval to update the target position
    public const float DISTANCE = 2f; // The distance from the character to the edge of the steering circle
    public const float RADIUS = 2f; // The radius of the steering circle

    private CharacterController _controller; // The controller that is told how to move based on vector calculations
    private Vector3 _targetPosition; // The position in world space the character will wander towards
    private Vector3 _steeringCirclePosition; // The position of the steering circle
    private float _timeSinceUpdate; // A counter used for updating target position
	private bool _wall = false;

    void Awake()
    {
        _controller = GetComponent<CharacterController>(); // Get the controller component
        _targetPosition = Vector3.zero; // Initialize to zero
        _steeringCirclePosition = Vector3.zero; // Initialize to zero
        _timeSinceUpdate = JITTER; // Initialize counter
    }

    void Update()
    {
        if (_controller.isGrounded) // If the character is grounded.
        {
            Collider[] enemies = Physics.OverlapSphere(transform.position, 10, 1 << LayerMask.NameToLayer("Enemy"));

            if (enemies.Length == 0)
            {
                MOVE_SPEED = 3;
                if (_timeSinceUpdate <= Time.time) // If it's been enough time since the last update
                {
                    Vector2 randomDirection = Random.insideUnitCircle.normalized * RADIUS; // Pick a random point on the edge of the circle
                    _targetPosition = new Vector3(randomDirection.x, 0, randomDirection.y) + _steeringCirclePosition; // Update the target position
                    _timeSinceUpdate = JITTER + Time.time; // Update the timer.
                }

                _steeringCirclePosition = transform.position + transform.forward * (DISTANCE + RADIUS); // Update steering circle position

                if (_wall)
                {
                    _steeringCirclePosition *= -1;
                    _wall = false;
                }
            }

            else
            {
                MOVE_SPEED = 8;
                GameObject closestEnemy = enemies[0].gameObject;

                foreach (Collider enemyCollider in enemies)
                {
                    if (Vector3.Distance(transform.position, enemyCollider.transform.position) < Vector3.Distance(transform.position, closestEnemy.transform.position))
                    {
                        closestEnemy = enemyCollider.gameObject;
                    }
                }
                
                _targetPosition = closestEnemy.transform.position;
                Vector3 vectorToTarget = transform.position - _targetPosition;
                _targetPosition = transform.position + vectorToTarget;
            }
        }

        transform.LookAt(new Vector3(_targetPosition.x, transform.position.y, _targetPosition.z)); // Update rotation so face target position

        Debug.DrawRay(transform.position, _targetPosition - transform.position, Color.blue); // Draw vector to target position

        /* Create a new force vector using the MOVE_SPEED constant to determine the x and z components and 
         * the GRAVITY constant to determine the y component. */
        Vector3 _moveForce = new Vector3(transform.forward.x * MOVE_SPEED, GRAVITY, transform.forward.z * MOVE_SPEED);
        _moveForce *= Time.deltaTime; // Get force relative to time

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
