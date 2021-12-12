using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Source File Name: PlayerBehavior
 * Author's Name: Sojung (Serena) Lee
 * Student #: 101245044
 * Date Last Modified: December 12, 2021
 * Program Description: Sets up player's movement behavior
 * Revision History:
 * (December 11) Added basic set up (e.g. animations, basic movement) from my own GAME2014_Lab8 project
 * (December 11) Renamed animation states (e.g. run -> walk) to reflect new player animations
 * (December 12) Added direction based on player's sprite's local sprite
 * (December 12) Added new animation state "Shoot" and base it on user's input
 * (December 12) Added "flinch" function -> player's movement feedback/response from when touching obstacles or enemies
 */

public enum PlayerAnimationState // different types of player animation states for animator controller
{
    IDLE,
    WALK,
    JUMP,
    SHOOT,
    NUM_OF_ANIMS
}

//Class for player movement behavior
public class PlayerBehavior : MonoBehaviour
{
    // for touch input - movement based on joystick's sensitivity
    [Header("Touch Input")]
    public Joystick joystick;
    [Range(0.01f, 1.0f)]
    public float sensitivity;

    // player's movement - creates basic physics behind it (e.g. walking force, jumping force)
    [Header("Movement")]
    public float horizontalForce;
    public float verticalForce;
    public bool isGrounded;
    public Transform groundOrigin;
    public float groundRadius;
    public LayerMask groundLayerMask;
    [Range(0.1f, 0.9f)]
    public float airControlFactor;
    private float direction = 0;
    private Rigidbody2D rb;

    // player's animation - shows animation state in editors & creates animator for player)
    [Header("Animation")]
    public PlayerAnimationState state;
    private Animator animatorController;

    void Start()
    {
        //gets component from game object
        rb = GetComponent<Rigidbody2D>();
        animatorController = GetComponent<Animator>();
    }

    private void Update()
    {
        //receives user's input (e.g. keyboard OR gamepad UI) to check if user is shooting or not (if so, play animation)
        if (Input.GetKeyDown(KeyCode.E) || UIController.shootButtonDown == true)
        {
            animatorController.SetInteger("AnimationState", (int)PlayerAnimationState.SHOOT); // SHOOT State
            state = PlayerAnimationState.SHOOT;
        }

        //always check what direction player is facing
        direction = transform.localScale.x;
    }

    void FixedUpdate()
    {
        //fixed update for better and smoother movement
        Move();
        CheckIfGrounded();
    }

    //Creates player's movement behavior using physics and user input
    private void Move()
    {
        //gets horizontal axis from user's input (e.g. keyboard OR joystick)
        float x = (Input.GetAxisRaw("Horizontal") + joystick.Horizontal) * sensitivity;

        if (isGrounded)
        {
            //gets vertical axis from user's input (e.g. keyboard OR joystick)
            float y = (Input.GetAxisRaw("Vertical") + joystick.Vertical) * sensitivity;
            //gets & checks jump
            float jump = Input.GetAxisRaw("Jump") + ((UIController.jumpButtonDown) ? 1.0f : 0.0f);

            if (x != 0) //if the player is moving or the horizontal input is active...
            {
                //...flip the player's animation & set the animation state to walking...
                x = FlipAnimation(x);
                animatorController.SetInteger("AnimationState", (int)PlayerAnimationState.WALK); // WALK State
                state = PlayerAnimationState.WALK;
            }
            else //... or if the player is staying still or there's no horizontal input...
            {
                //...set the player's animation to idle
                animatorController.SetInteger("AnimationState", (int)PlayerAnimationState.IDLE); // IDLE State
                state = PlayerAnimationState.IDLE;
            }

            //creates variables for jumping force
            float horizontalMoveForce = x * horizontalForce;
            float jumpMoveForce = jump * verticalForce;
            float mass = rb.mass * rb.gravityScale;

            //adds force to player's rigidbody to create jump force
            rb.AddForce(new Vector2(horizontalMoveForce, jumpMoveForce) * mass);
            rb.velocity *= 0.99f; // scaling / stopping hack
        }
        else // Air Control (Jump)
        {
            //set player's animation to 'Jump'
            animatorController.SetInteger("AnimationState", (int)PlayerAnimationState.JUMP); // JUMP State
            state = PlayerAnimationState.JUMP;

            if (x != 0) //...if the player is moving horizontally
            {
                //... flip the animation and update the jumpforce
                x = FlipAnimation(x);
                float horizontalMoveForce = x * horizontalForce * airControlFactor;
                float mass = rb.mass * rb.gravityScale;

                rb.AddForce(new Vector2(horizontalMoveForce, 0.0f) * mass);
            }
        }
    }

    //checks if the player is on the ground using raycast
    private void CheckIfGrounded()  
    {
        RaycastHit2D hit = Physics2D.CircleCast(groundOrigin.position, groundRadius, Vector2.down, groundRadius, groundLayerMask);

        isGrounded = (hit) ? true : false;
    }

    //flips the player's animation based on local scale
    private float FlipAnimation(float x) 
    {
        // depending on direction scale across the x-axis either 1 or -1
        x = (x > 0) ? 1 : -1;

        transform.localScale = new Vector3(x, 1.0f);
        return x;
    }

    //checks when this game object starts colliding with another game object
    private void OnCollisionEnter2D(Collision2D other)  
    {
        // checks collision between player and platform
        if (other.gameObject.CompareTag("Platform"))
        {
            //  sets the player's transform to platform that it collided with (good for when player lands on top of moving platforms)
            transform.SetParent(other.transform);
        }

        // checks collision between player and other hazards or enemies
        if (other.gameObject.CompareTag("Obstacle") || other.gameObject.CompareTag("Snail") || other.gameObject.CompareTag("Slime"))
        {
            //makes the player flinch away (direction of flinch depends on player's last movement direction)
            if (direction < 0) rb.AddForce(new Vector2(-150, 250));
            else rb.AddForce(new Vector2(150, 250));
        }
    }

    //checks when this game object doesn't start colliding with another game object
    private void OnCollisionExit2D(Collision2D other)
    {
        // if the player is no longer attached to platform, remove its parent transform to its own
        if (other.gameObject.CompareTag("Platform"))
        {
            transform.SetParent(null);

            //anytime player leaves platform while user input indicated to jump, then play jump sound
            if (UIController.jumpButtonDown || Input.GetButton("Jump")) AudioManager.audioManager.JumpSound();
        }
    }

    //draw gizmos for player's ground boundaries (easier to check ground collision)
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundOrigin.position, groundRadius);
    }

}
