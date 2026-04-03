using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public int damage;
    public float lifetime;

    [SerializeField] float direction;
    public GameObject player;
    PlayerController playerInfo;

    [SerializeField] private BoxCollider2D bullCollider;
    private bool hasHit = false;
    [SerializeField] private Rigidbody2D rb;

    private void Awake()
    {
        bullCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("PlayerCharacter");
        playerInfo = player.GetComponent<PlayerController>();
    }

    void Start()
    {

        SetDirection(playerInfo.dire);
       
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        if (hasHit) return;
        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);

        rb.linearVelocity = new Vector2(direction, 0);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        hasHit = true;
        bullCollider.enabled = false;

        Debug.Log("Hit: " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Enemy"))
        {
            hasHit = true;

            Debug.Log("Enemy tag detected!");

            Enemy enemy = collision.gameObject.GetComponent<Enemy>();

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


    public void SetDirection(float _direction)
    {
        direction = _direction;

        gameObject.SetActive(true);

        hasHit = false;
        bullCollider.enabled = true;

        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != _direction)
            localScaleX = -localScaleX;

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }
}