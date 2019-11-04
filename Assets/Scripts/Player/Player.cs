using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(JetpackManager))]
public class Player : MonoBehaviour
{
    // Variables
    public float runSpeed = 8f;
    public float walkSpeed = 6f;
    public float dashSpeed = 20f;
    public float dashTime = 2f;
    public float gravity = -100f;
    public float jumpHeight = 15f;
    public float groundRayDistance = 1.1f;
    private CharacterController controller; // Reference to character controller
    private Vector3 motion; // Is the movement offset per frame
    private bool isJumping;
    private float currentJumpHeight;
    private float currentSpeed;
    private float jetpackSpeed;
    private JetpackManager jetpackManager;
    public float initialGravity;
    public bool isDead = false;
    // Functions
    private void Start()
    {
        controller = GetComponent<CharacterController>();
        currentSpeed = walkSpeed;
        jetpackManager = GetComponent<JetpackManager>();
        jetpackSpeed = jetpackManager.jetpackSpeed;
        initialGravity = gravity;
    }
    private void Update()
    {
        // W A S D / Right Left Up Down Arrow Input
        float inputH = Input.GetAxis("Horizontal");
        float inputV = Input.GetAxis("Vertical");
        // Left Shift Input
        bool inputRun = Input.GetKeyDown(KeyCode.LeftShift);
        bool inputWalk = Input.GetKeyUp(KeyCode.LeftShift);
        bool inputDash = Input.GetKeyDown(KeyCode.E);
        // Space Bar Input
        bool inputJump = Input.GetButtonDown("Jump");
        bool inputJetpack = Input.GetKey(KeyCode.F);
        // Put Horizontal & Vertical input into vector
        Vector3 inputDir = new Vector3(inputH, 0f, inputV);
        // Rotate direction to Player's Direction
        inputDir = transform.TransformDirection(inputDir);
        // If input exceeds length of 1
        if (inputDir.magnitude > 1f)
        {
            // Normalize it to 1f!
            inputDir.Normalize();
        }

        if (inputDash)
        {
            Dash();
        }

        // If running
        if (inputRun)
        {
            currentSpeed = runSpeed;
        }

        if (inputWalk)
        {
            currentSpeed = walkSpeed;
        }

        Move(inputDir.x, inputDir.z, currentSpeed);

        // If is Grounded
        if (controller.isGrounded)
        {
            // .. And jump?
            if (inputJump)
            {
                Jump(jumpHeight);
            }

            // Cancel the y velocity
            motion.y = 0f;

            // Is jumping bool set to true
            if (isJumping)
            {
                // Set jump height
                motion.y = currentJumpHeight;
                // Reset back to false
                isJumping = false;
            }
        }
        
        if (inputJetpack)
        {
            JetpackON();
        }

        if (!inputJetpack)
        {
            gravity = initialGravity;
            motion.y += gravity * Time.deltaTime;
        }        
        controller.Move(motion * Time.deltaTime);
        //jetpackManager.speedText.text = jetpackManager.maxPower.ToString();
    }
    private void Move(float inputH, float inputV, float speed)
    {
        Vector3 direction = new Vector3(inputH, 0f, inputV);
        motion.x = direction.x * speed;
        motion.z = direction.z * speed;
    }
    IEnumerator SpeedBoost(float startDash, float endDash, float delay)
    {
        currentSpeed = startDash;

        yield return new WaitForSeconds(delay);

        currentSpeed = endDash;
    }
    public void Walk(float inputH, float inputV)
    {
        Move(inputH, inputV, walkSpeed);
    }
    public void Run(float inputH, float inputV)
    {
        Move(inputH, inputV, runSpeed);
    }
    public void Jump(float height)
    {
        isJumping = true; // We are jumping!
        currentJumpHeight = height;
    }
    public void Dash()
    {
        StartCoroutine(SpeedBoost(dashSpeed, walkSpeed, dashTime));
    }

    public void JetpackON()
    {
        motion.y += jetpackSpeed * Time.deltaTime;
        gravity = 0;
    }
}




//bool IsGrounded()
//{
// Raycast below the Player
//Ray groundRay = new Ray(transform.position, -transform.up);
//RaycastHit hit;
// If hitting something
//if (Physics.Raycast(groundRay, out hit, groundRayDistance))
//{
//return true;
//}
//return false;
//}