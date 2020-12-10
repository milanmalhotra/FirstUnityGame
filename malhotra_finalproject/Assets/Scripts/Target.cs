using UnityEngine;
using UnityEngine.AI;


public class Target : MonoBehaviour
{
    public float health = 50f;

    public static int score;
    public static int totalEnemies;

    private void Start()
    {
        totalEnemies = 20;
        score = 0;
        
    }
    //decrease health by amount (gun's damage) and destroy object if it's health is 0
    public void TakeDamage(float amount)
    {
        health -= amount;

        if (health <= 0f)
        {
            Die();
        }
    }

    //once the enemy is dead delete the enemy and add 50 to the score
    void Die()
    {
        totalEnemies -= 1;
        score += 50;
        Destroy(gameObject);
    }
}
