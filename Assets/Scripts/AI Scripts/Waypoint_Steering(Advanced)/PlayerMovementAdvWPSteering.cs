using UnityEngine;
using System.Collections;

public class PlayerMovementAdvWPSteering : MonoBehaviour 
{
    public const float MOVE_SPEED = 30; // The movement speed of the character
    private const float GRAVITY = -50; // The force due to gravity (Constant for the purposes of this assignment)

    private CharacterController _controller; // The controller that is told how to move based on vector calculations
    private Vector3 _moveDirection; // The the directional vector of the character movement

    void Awake()
    {
        _controller = GetComponent<CharacterController>(); // Get the controller component
        _moveDirection = Vector3.zero; // // Initialize to zero
    }

	void Update () 
    {
        if (_controller.isGrounded) // If the character is grounded.
        {
            _moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")); // Use input to determine the direction to move
        }

        /* Create a new force vector using the MOVE_SPEED constant to determine the x and z components and 
         * the GRAVITY constant to determine the y component. */
        Vector3 _moveForce = new Vector3(_moveDirection.x * MOVE_SPEED, GRAVITY, _moveDirection.z * MOVE_SPEED);
        _moveForce *= Time.deltaTime; // Get force relative to time.

        _controller.Move(_moveForce); // Apply force vector.
	}
}
