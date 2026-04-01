using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health; // 👈 set to 1 for testing
    public int scoreValue;

    public int damage;

    public GameObject scoreManager;

    private void Awake()
    {
        scoreManager = GameObject.Find("ScoreManagerObject");
    }

    public void TakeDamage(int damage)
    {
        //Debug.Log("Enemy took damage");

        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemy died, adding score");
        ScoreManager manager = scoreManager.GetComponent<ScoreManager>();

        manager.AddScore(scoreValue);
        Destroy(gameObject);
    }
}