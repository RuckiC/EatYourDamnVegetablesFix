using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyPickup : MonoBehaviour
{
    public int sugarRushAmount = 100;
    private int candyCollected;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);
            candyCollected++;
            Debug.Log(candyCollected);
        }
    }
}
