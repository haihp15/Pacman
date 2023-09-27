using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    public float speed = 8.0f;

    public float speedMultiplier = 1.0f;

    public Vector2 InitialDirection;

    public LayerMask obstacleLayer;
    public new Rigidbody2D rigidbody {get; private set;}

    public Vector2 direction { get; private set;}

    public Vector2 nextDirection { get; private set;}

    public Vector3 startingPosition { get; private set;}

    private void Awake()
    {
        this.rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Start()
    {
        ResetState();
    }
    public void ResetState()
    {
        
    }
}