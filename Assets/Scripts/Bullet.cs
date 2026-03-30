using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 1;
    public float lifetime = 3f;

    private bool hasHit = false;

    void Start()
    {
        // Destroy bullet after some time
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // Move bullet forward
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasHit) return;

        Debug.Log("Hit: " + collision.name);

        if (collision.CompareTag("Enemy"))
        {
            hasHit = true;

            Debug.Log("Enemy tag detected!");

            Enemy enemy = collision.GetComponent<Enemy>();

            if (enemy == null)
            {
                Debug.Log("NO Enemy script found!");
            }
            else
            {
                Debug.Log("Damaging enemy");
                enemy.TakeDamage(damage);
            }

            Destroy(gameObject);
        }
    }
}