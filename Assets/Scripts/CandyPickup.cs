using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyPickup : MonoBehaviour
{
    public int sugarRushAmount = 5;
    public int candyCollected;

    private bool dirUp = true;
    private float speed = 0.3f;
    private float rotateSpeed = 0.2f;
    private Vector3 startPos;

    public CandyBar _candyBar;

    private void Start()
    {
        startPos = this.transform.position;
    }

    private void Update()
    {
        transform.Rotate(0, rotateSpeed, 0);
        if (dirUp)
            transform.Translate(Vector2.up * speed * Time.deltaTime);
        else
            transform.Translate(Vector2.down * speed * Time.deltaTime);

        if (transform.position.y >= startPos.y + 0.2f)
        {
            dirUp = false;
        }

        if (transform.position.y <= startPos.y - 0.2f)
        {
            dirUp = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);
            candyCollected++;
            Debug.Log(candyCollected);
            _candyBar.sugarHighTime += sugarRushAmount;
        }
    }
}
