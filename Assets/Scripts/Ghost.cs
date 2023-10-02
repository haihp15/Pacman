using UnityEngine;

public class Ghost : MonoBehaviour
{
    public Movement movement {get; private set;}

    public GhostHome1 home { get; private set; }

    public GhostScatter1 scatter { get; private set; }

    public GhostChase1 chase { get; private set; }

    public GhostFrightened2 frightened { get; private set; }

    public GhostBehavior1 initialBehavior;

    public Transform target;

    public int points = 200;

    private void Awake()
    {
        this.movement = GetComponent<Movement>();
        this.home = GetComponent<GhostHome1>();
        this.scatter = GetComponent<GhostScatter1>();
        this.frightened = GetComponent<GhostFrightened2>();
        this.chase = GetComponent<GhostChase1>();
    }

    private void Start()
    {
        ResetState();
    }

    public void ResetState()
    {
        this.gameObject.SetActive(true);
        this.movement.ResetState();

        this.frightened.Disable();
        this.chase.Disable();
        this.scatter.Enable();
        if (this.home != this.initialBehavior)
        {
            this.home.Disable();
        }
        if (this.initialBehavior != null)
        {
            this.initialBehavior.Enable();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            if (this.frightened.enabled)
            {
                FindObjectOfType<GameManager>().GhostEaten(this);
            }
            else
            {
                FindObjectOfType<GameManager>().PacmanEaten();
            }

        }
    }
}
 