using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
   public int healAmount = 100;
    public playerHealthTest playerHealth;

 

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("health pickup"))
        {
            Destroy(collision.gameObject);
            playerHealth.currentHealth += healAmount;
        }
    }
}
