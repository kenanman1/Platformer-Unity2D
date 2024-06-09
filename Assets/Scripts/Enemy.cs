using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float speed = 2f;
    bool isRightSide = true;
    Rigidbody2D rbEnemy;
    PolygonCollider2D pcEnemy;
    public float hp = 100f;

    void Start()
    {
        rbEnemy = GetComponent<Rigidbody2D>();
        pcEnemy = GetComponent<PolygonCollider2D>();
    }

    void Update()
    {
        Moving();
        DestroyEnemy();
    }

    void Moving()
    {
        if (isRightSide)
        {
            rbEnemy.velocity = new Vector2(speed, rbEnemy.velocity.y);
        }
        else
        {
            rbEnemy.velocity = new Vector2(-speed, rbEnemy.velocity.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (pcEnemy.IsTouchingLayers(LayerMask.GetMask("platform", "spikes")))
        {
            isRightSide = !isRightSide;
            Flip();
        }
    }

    void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void DestroyEnemy()
    {
        if (hp <= 0)
            Destroy(gameObject);
    }
}
