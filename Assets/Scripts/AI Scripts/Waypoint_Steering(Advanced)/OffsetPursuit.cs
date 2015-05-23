using UnityEngine;
using System.Collections;

public class OffsetPursuit : MonoBehaviour 
{
    private const float MOVE_SPEED = 20; // The movement speed of the character
    private const float GRAVITY = -50; // The force due to gravity
    private const float PURSUIT_OFFSET_DISTANCE = 5; // The distance away from the enemy the player must be before the enemy resumes pursuit

    private CharacterController _controller; // The controller that is told how to move based on vector calculations
    private CharacterController _pursuitTarget; // The target to pursue
    private Vector3 _moveDirection; // The the directional vector of the character movement


    void Awake()
    {
        _controller = GetComponent<CharacterController>(); // Get the controller component
        _moveDirection = Vector3.zero; // Initialize to zero
        _pursuitTarget = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>(); // Find the pursuit target
    }

	void Update () 
    {
        if (_controller.isGrounded) // If the character is grounded
        {
            // If the pursuit target is beyond the offset distance, calculate new force
            if (Vector3.Distance(transform.position, _pursuitTarget.transform.position) > PURSUIT_OFFSET_DISTANCE)
            {
                // Calculate the future position of the target using the distance between them and both characters' movement speeds
                float movementOffset = Vector3.Distance(transform.position, _pursuitTarget.transform.position) / (PlayerMovementAdvWPSteering.MOVE_SPEED + OffsetPursuit.MOVE_SPEED);
                
                // Calculate the position to seek using the target's position offset by its velocity and movementOffset
                Vector3 seekPosition = _pursuitTarget.transform.position + (_pursuitTarget.velocity * movementOffset);

                _moveDirection = (seekPosition - transform.position).normalized; // Get the unit vector direction to the seek position
            }

            else // Otherwise don't move
            {
                _moveDirection = Vector3.zero;
            }
        }

        /* Create a new force vector using the MOVE_SPEED constant to determine the x and z components and 
         * the GRAVITY constant to determine the y component. */
        Vector3 _moveForce = new Vector3(_moveDirection.x * MOVE_SPEED, GRAVITY, _moveDirection.z * MOVE_SPEED);
        _moveForce *= Time.deltaTime; // Get force relative to time.

        _controller.Move(_moveForce); // Apply force vector.
	}
}
