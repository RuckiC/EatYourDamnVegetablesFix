using System.Collections;
using UnityEngine;

public class VeggieFall : MonoBehaviour
{
    [SerializeField] private GameObject carrot;
    [SerializeField] private GameObject potato;
    [SerializeField] private GameObject onion;
    [SerializeField] private GameObject raddish;

    private GameObject currentVeggie;
    private int veggieIndex;

    void Start()
    {
        StartCoroutine("VeggieSpawner");
    }
    private GameObject rollVeggie()
    {
        veggieIndex = Random.Range(1, 5);
        
        switch(veggieIndex)
        {
            case 1:
                currentVeggie = carrot;
                break;
            case 2:
                currentVeggie = potato;
                break;
            case 3:
                currentVeggie = onion;
                break;
            case 4:
                currentVeggie = raddish;
                break;
            default:
                Debug.Log("Veggie index defaulted");
                break;
        }
        
        return currentVeggie;
    }

    IEnumerator VeggieSpawner()
    {
        Instantiate(rollVeggie());
        yield return new WaitForSeconds(1);
    }
}
