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

        DamageOverTime(1, 1);
        healthBar.SetHealth(currentHealth);
    }

    void takeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    public void DamageOverTime(int damageAmount, int damageTime)
    {
        StartCoroutine(DamageOverTimeCoroutine(damageAmount, damageTime));
    }

    IEnumerator DamageOverTimeCoroutine(int damageAmount, int duration)
    {
        float amountDamaged = 0;
        int damagePerLoop = damageAmount / duration;
        while (amountDamaged < damageAmount)
        {
            currentHealth -= damagePerLoop;
            Debug.Log(currentHealth.ToString());
            amountDamaged += damagePerLoop;
            yield return new WaitForSeconds(5f);
        }
    }
}
