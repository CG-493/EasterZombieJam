using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 1; // 👈 set to 1 for testing
    public int scoreValue = 10;
 
   

    public void TakeDamage(int damage)
    {
        Debug.Log("Enemy took damage");

        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemy died, adding score");

        ScoreManager.instance.AddScore(scoreValue);
        Destroy(gameObject);
    }
}