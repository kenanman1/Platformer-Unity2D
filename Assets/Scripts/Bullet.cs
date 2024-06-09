using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed = 10;

    Rigidbody2D rigidbody2D;
    Player player;
    CircleCollider2D circleCollider2D;

    void Start()
    {
        circleCollider2D = GetComponent<CircleCollider2D>();
        player = FindObjectOfType<Player>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        if (player.toRight)
        {
            rigidbody2D.velocity = new Vector2(speed, 0);
        }
        else
        {
            rigidbody2D.velocity = new Vector2(-speed, 0);
        }
    }

    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.hp -= 25;
            DestroyBullet();
        }
        Invoke("DestroyBullet", 3f);
    }

    void DestroyBullet()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Water")
        {
            InWater();
        }
    }
    void InWater()
    {
        if (circleCollider2D.IsTouchingLayers(LayerMask.GetMask("Water")))
        {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x / 1.5f, rigidbody2D.velocity.y * 1.2f);
            GetComponent<SpriteRenderer>().color = new Color(0.4f, 0.5f, 1);
        }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.white;
        }
    }
}
