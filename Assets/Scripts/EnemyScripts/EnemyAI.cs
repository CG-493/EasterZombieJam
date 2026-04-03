using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public GameObject player;          // Assign player in Inspector
    public Vector3 playerPos;
    public float speed;          // Movement speed
    public float stoppingDistance;
    public float attackRate;

    private float nextAttackTime;

    public void Awake()
    {
        player = GameObject.Find("PlayerCharacter");
        
    }


    // void Update()
    // {
    //    if (player == null) return;
    //   Vector3 playerPos = player.transform.position;

    //    float distance = Vector3.Distance(transform.position, playerPos);

    // Move toward player
    //   if (distance > stoppingDistance)
    //   {
    //        Vector3 direction = new Vector3(playerPos.x, 0, 0);
    //         transform.position += direction * speed * Time.deltaTime;
    //     }
    //     else
    //    {
    // Attack player
    //      if (Time.time >= nextAttackTime)
    //        {
    //           Attack();
    //            nextAttackTime = Time.time + 1f / attackRate;
    //       }
    //    }

    // Optional: face player
    //     transform.LookAt(playerPos);
    //  }

    void Attack()
    {
        Debug.Log("Enemy attacks!");

        PlayerHealth health = player.GetComponent<PlayerHealth>();
        Enemy dam = gameObject.GetComponent<Enemy>();

        if (health != null)
        {
            health.PlayerDamage(dam.damage);
        }
    }
}