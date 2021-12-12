using UnityEngine;

/*Source File Name: PlatformBehavior
 * Author's Name: Sojung (Serena) Lee
 * Student #: 101245044
 * Date Last Modified: December 12, 2021
 * Program Description: sets up behavior for moving (active) platforms
 * Revision History:
 * (December 10) Added platform behaviors from my personal Game2014_Lab8 project
 * (December 12) Added directional behaviors (e.g. which direction does it start going to - left or right)
 * (December 12) Added exploding platform behavior
 */

//different types of platform movement
public enum MovingPlatformDirection 
{
    HORIZONTAL,
    VERTICAL,
    DIAGONAL_UP,
    DIAGONAL_DOWN,
    NUM_OF_DIRECTIONS
}

//Class for platform movement behavior
public class PlatformBehavior : MonoBehaviour
{
    //sets up basic physics movement (e.g. speed, direction, distance)
    [Header("Movement")]
    public MovingPlatformDirection direction;
    [Range(0.1f, 10.0f)]
    public float speed;
    [Range(1, 20)]
    public float distance;
    [Range(0.05f, 0.1f)]
    public float distanceOffset;
    public bool isLooping;
    private bool isMoving;
    private Vector2 startingPosition;

    //sets up which direction it starts going towards
    [SerializeField] private bool StartingRight = false;

    //checks if this platform explodes or not
    [SerializeField] public bool explodePlatform = false;

    void Start()
    {
        //starts platform
        startingPosition = transform.position;
        isMoving = true;
    }

    void Update()
    {
        //moves platform every frame
        MovePlatform();
        //loops platform movement
        if (isLooping)
        {
            isMoving = true;
        }
    }

    //creates different types of movement for platforms
    private void MovePlatform()
    {
        //pingpong value (goes back and forth)
        float pingPongValue = (isMoving) ? Mathf.PingPong(Time.time * speed, distance) : distance;

        //if platform reaches its location & isn't looping, then stop moving platform
        if ((!isLooping) && (pingPongValue >= distance - distanceOffset))
        {
            isMoving = false;
        }

        //sets platforms direction
        switch (direction)
        {
            case MovingPlatformDirection.HORIZONTAL:
                //if starting right, then platform will start moving to the right...
                if (StartingRight) transform.position = new Vector2(startingPosition.x + pingPongValue, transform.position.y);
                //if not starting right (starting left), then start moving to the opposite direction (left)
                else transform.position = new Vector2(startingPosition.x - pingPongValue, transform.position.y);
                break;
            case MovingPlatformDirection.VERTICAL:
                //if starting right, then platform will start moving to the up...
                if (StartingRight) transform.position = new Vector2(transform.position.x, startingPosition.y + pingPongValue);
                //if not starting right (starting left), then start moving to the opposite direction (down)
                else transform.position = new Vector2(transform.position.x, startingPosition.y - pingPongValue);
                break;
            case MovingPlatformDirection.DIAGONAL_UP:
                //if starting right, then platform will start moving towards to the top right direction...
                if (StartingRight) transform.position = new Vector2(startingPosition.x + pingPongValue, startingPosition.y + pingPongValue);
                //if not starting right (starting left), then start moving to the opposite direction (top left)
                else transform.position = new Vector2(startingPosition.x - pingPongValue, startingPosition.y + pingPongValue);
                break;
            case MovingPlatformDirection.DIAGONAL_DOWN:
                //if starting right, then platform will start moving towards to the bottom right direction...
                if (StartingRight) transform.position = new Vector2(startingPosition.x + pingPongValue, startingPosition.y - pingPongValue);
                //if not starting right (starting left), then start moving to the opposite direction (bottom left)
                else transform.position = new Vector2(startingPosition.x - pingPongValue, startingPosition.y - pingPongValue);
                break;
        }
    }
    float time = 0;
    //checks collision between this platform and other game objects
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if platform collides with player, then make player jump away and destroy/explode platform
        if (collision.gameObject.tag == "Player" && explodePlatform)
        {
            collision.gameObject.GetComponent<PlayerBehavior>().rb.AddForce(new Vector2(0, 1000));
            AudioManager.audioManager.ExplodeSound();
            Destroy(this.gameObject);

        }
    }
}
