using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public int playerHealth;
    public bool playerDead;

    [Header("Game Over Settings")]
    public GameObject gOManager;


    private void Awake()
    {
        playerDead = false;
        gOManager = GameObject.Find("SceneManager");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision");

        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Enemy Collision");
            
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();

            if (enemy == null)
            {
                Debug.Log("NO Enemy script found!");
            }
            
            else
            {
                Debug.Log("Player Take Damage");
                PlayerDamage(enemy.damage);
            }
        }
    }

    void PlayerDamage(int damage)
    {
        playerHealth -= damage;
        Debug.Log("Player took Damage");

        if (playerHealth <= 0)
        {
            GameOverManager manager = gOManager.GetComponent<GameOverManager>();
            manager.GameOver();
        }

        
    }



}
