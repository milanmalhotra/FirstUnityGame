using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float health = 200f;
    public float currentHealth;
    public float healthRegen;

    public HealthBar healthBar;
   // public GameEnding gameEnding;
    private void Start()
    {
        health = 200f;
        currentHealth = health;
        healthBar.SetMaxHealth(Mathf.RoundToInt(health));
      
    }

    private void Update()
    {
        currentHealth += (Time.deltaTime / 2) * healthRegen;
        if (currentHealth >= 200)
        {
            currentHealth = 200;
        }
        healthBar.SetHealth(Mathf.RoundToInt(currentHealth));
    }
    //decrease health by amount (gun's damage) and destroy object if it's health is 0
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        
        
        if (currentHealth <= 0f)
        {
            EndGame();
        }
    }

    void EndGame()
    {
        GameEnding.KilledPlayer();
    }
}
