using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerHealthTest : MonoBehaviour
{
    public int maxhealth = 10000;
    public int currentHealth;

    public PlayerHealth healthBar;


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxhealth;
        healthBar.SetMaxHealth(maxhealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            takeDamage(10);
        }

        StartCoroutine(LoseHealthOverTime());
        
    }

    void takeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    

    IEnumerator LoseHealthOverTime()
    {
        while (currentHealth > 0)
        {
            currentHealth -= 1;
            healthBar.SetHealth(currentHealth);
            yield return new WaitForSeconds(3f);
        }
    }
}
