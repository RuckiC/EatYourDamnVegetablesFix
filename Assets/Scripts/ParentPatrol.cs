using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ParentPatrol : MonoBehaviour
{
    public GameObject player;
    public Transform[] points;
    private int destPoint = 0;
    private NavMeshAgent agent;
    public AISensor _AISensor;
    public float gameOverRange;

    public Animator anim;

    public GameObject gameOverScreenMom;

    public GameManager gameManager;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // Disabling auto-braking allows for continuous movement
        // between points (ie, the agent doesn't slow down as it
        // approaches a destination point).
        agent.autoBraking = false;

        GotoNextPoint();
    }

    void Update()
    {
        anim.SetBool("Chasing", false);
        // Choose the next destination point when the agent gets
        // close to the current one.
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
            GotoNextPoint();

        if (_AISensor.PlayerSeen.Count > 0)
        {
            Debug.Log("FOUND YOU TIMMY!");
            Chase();
        }

        Vector3 distanceFromPlayer = this.transform.position - player.transform.position;
        if(distanceFromPlayer.magnitude <= gameOverRange)
        {
            gameOverScreenMom.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
            if (Input.GetKeyDown(KeyCode.Return))
            {
                gameManager.restartGame();
            }
            Debug.Log("YOU LOSE!!!");
        }
    }

    void GotoNextPoint()
    {
        // Returns if no points have been set up
        if (points.Length == 0)
            return;

        // Set the agent to go to the currently selected destination.
        agent.destination = points[destPoint].position;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoint = Random.Range(0, 37);
    }

    private void Chase()
    {
        if(_AISensor.PlayerChase.Count > 0)
        {
            agent.SetDestination(player.transform.position);
            anim.SetBool("Chasing", true);
        }
    }
}
