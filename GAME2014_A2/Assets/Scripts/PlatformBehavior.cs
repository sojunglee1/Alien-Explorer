using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MovingPlatformDirection
{
    HORIZONTAL,
    VERTICAL,
    DIAGONAL_UP,
    DIAGONAL_DOWN,
    NUM_OF_DIRECTIONS
}

public class PlatformBehavior : MonoBehaviour
{
    [Header("Movement")]
    public MovingPlatformDirection direction;
    [Range(0.1f, 10.0f)]
    public float speed;
    [Range(1, 20)]
    public float distance;
    [Range(0.05f, 0.1f)]
    public float distanceOffset;
    public bool isLooping;

    private Vector2 startingPosition;
    private bool isMoving;

    [SerializeField] private bool startingDirection = false;

    void Start()
    {
        startingPosition = transform.position;
        isMoving = true;
    }

    void Update()
    {
        MovePlatform();
        if (isLooping)
        {
            isMoving = true;
        }
    }

    private void MovePlatform()
    {
        float pingPongValue = (isMoving) ? Mathf.PingPong(Time.time * speed, distance) : distance;

        if ((!isLooping) && (pingPongValue >= distance - distanceOffset))
        {
            isMoving = false;
        }

        switch (direction)
        {
            case MovingPlatformDirection.HORIZONTAL:
                if (startingDirection) transform.position = new Vector2(startingPosition.x + pingPongValue, transform.position.y);
                else transform.position = new Vector2(startingPosition.x - pingPongValue, transform.position.y);
                break;
            case MovingPlatformDirection.VERTICAL:
                if (startingDirection) transform.position = new Vector2(transform.position.x, startingPosition.y + pingPongValue);
                else transform.position = new Vector2(transform.position.x, startingPosition.y - pingPongValue);
                break;
            case MovingPlatformDirection.DIAGONAL_UP:
                if (startingDirection) transform.position = new Vector2(startingPosition.x + pingPongValue, startingPosition.y + pingPongValue);
                else transform.position = new Vector2(startingPosition.x - pingPongValue, startingPosition.y + pingPongValue);
                break;
            case MovingPlatformDirection.DIAGONAL_DOWN:
                if (startingDirection) transform.position = new Vector2(startingPosition.x + pingPongValue, startingPosition.y - pingPongValue);
                else transform.position = new Vector2(startingPosition.x - pingPongValue, startingPosition.y - pingPongValue);
                break;
        }
    }
}
