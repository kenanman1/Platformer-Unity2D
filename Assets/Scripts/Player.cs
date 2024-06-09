using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] float jumpSpeed = 300f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;

    public bool toRight = true;
    bool isAlive = true;
    float startGravity;
    Animator playerAnimation;
    Vector2 run;
    Rigidbody2D playerRigidBody;
    PolygonCollider2D polygonCollider;
    BoxCollider2D boxCollider;
    SpriteRenderer playerSpriteRenderer;

    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
        playerAnimation = GetComponent<Animator>();
        polygonCollider = GetComponent<PolygonCollider2D>();
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
        startGravity = playerRigidBody.gravityScale;
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (isAlive)
        {
            Run();
            Climb();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Water")
        {
            playerSpriteRenderer.color = new Color(0.4f, 0.5f, 1);
            speed /= 1.5f;
            climbSpeed /= 1.5f;
            jumpSpeed -= 25;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Water")
        {
            playerSpriteRenderer.color = Color.white;
            speed *= 1.5f;
            climbSpeed *= 1.5f;
            jumpSpeed += 25;
        }
    }

    public void OnMove(InputValue value)
    {
        if (isAlive)
        {
            run = value.Get<Vector2>();
            if (run.x > 0.5)
                run.x = 1;
            else if (run.x < -0.5)
                run.x = -1;

            if (run.y > 0.5)
                run.y = 1;
            else if (run.y < -0.5)
                run.y = -1;
        }
    }

    void Run()
    {
        FlipPlayer();
        Vector2 movement = new Vector2(run.x * speed, playerRigidBody.velocity.y);
        playerRigidBody.velocity = movement;
        playerAnimation.SetBool("IsRunning", run.x != 0);
    }

    void FlipPlayer()
    {
        if (run.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            toRight = false;
        }
        else if (run.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            toRight = true;
        }
    }

    void OnJump()
    {
        if (boxCollider.IsTouchingLayers(LayerMask.GetMask("platform")) && isAlive)
            playerRigidBody.AddForce(new Vector2(0, jumpSpeed));
    }

    void OnFire()
    {
        if (!isAlive)
            return;

        playerAnimation.SetTrigger("Shoot");
        if (toRight)
        {
            Instantiate(bullet, gun.position, Quaternion.Euler(0, 0, 315));
        }
        else
        {
            Instantiate(bullet, gun.position, Quaternion.Euler(0, 0, 130));
        }
        Invoke("StopShooting", 2f);
    }

    void StopShooting()
    {
        playerAnimation.ResetTrigger("Shoot");
    }


    void Climb()
    {
        if (boxCollider.IsTouchingLayers(LayerMask.GetMask("climbing")))
        {
            playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, run.y * climbSpeed);
            playerRigidBody.gravityScale = 0;
            playerAnimation.SetBool("IsClimbing", run.y != 0);
        }
        else
        {
            playerAnimation.SetBool("IsClimbing", false);
            playerRigidBody.gravityScale = startGravity;
        }
    }

    void Die()
    {
        playerRigidBody.AddForce(new Vector2(0, 200));
        isAlive = false;
        playerAnimation.SetTrigger("Dead");
        playerAnimation.SetBool("IsRunning", false);
        playerRigidBody.gravityScale = startGravity;

        gameObject.layer = LayerMask.NameToLayer("dead");

        GameSession.instance.TakeLife();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isAlive)
            return;

        if (collision.gameObject.tag == "enemy")
        {
            Die();
        }

        // Check for collision with specific collider on spikes, which has multiple colliders
        if (collision.gameObject.tag == "spikes" && collision.collider is PolygonCollider2D)
        {
            Die();
        }
    }
}