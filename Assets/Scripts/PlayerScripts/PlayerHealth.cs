using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public int playerHealth;
    public bool playerDead;

    [Header("Game Over Settings")]
    public GameObject gOManager;

    [Header("Potion")]
    [SerializeField] int potionHealth;

    [Header("UI")]
    public TMP_Text healthUI;

    [Header("enemy")]
    GameObject en;

    private void Awake()
    {
        playerDead = false;
        gOManager = GameObject.Find("SceneManager");
        en = 
        UpdateUI();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            PlayerDamage()
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Potion"))
        {
            PlayerRecover(potionHealth);

            Destroy(collision);
        }
    }

    public void PlayerDamage(int damage)
    {
        playerHealth -= damage;
        Debug.Log("Player took Damage");

        if (playerHealth <= 0)
        {
            GameOverManager manager = gOManager.GetComponent<GameOverManager>();
            manager.GameOver();
        }

        UpdateUI();
    }

    void PlayerRecover(int recover)
    {
        playerHealth += recover;

        UpdateUI();
    }

    void UpdateUI()
    {

        healthUI.text = "Health " + playerHealth; 
    }

}
