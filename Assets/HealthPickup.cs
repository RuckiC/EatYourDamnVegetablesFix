using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
   public int healAmount = 100;
    public playerHealthTest playerHealth;

    private void OnTriggerEnter(Collider other)
    {
        Destroy(other);
        playerHealth.currentHealth += healAmount;
    }
}
