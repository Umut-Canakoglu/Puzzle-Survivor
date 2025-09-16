using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSpike : MonoBehaviour
{
    public int waiter;
    private Animator animator;
    private bool clicked;
    private int currentTurn;
    private int waitTurn;
    void Start()
    {
        animator = GetComponent<Animator>();
        clicked = false;
    }
    void Update()
    {
        currentTurn = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterMovement>().turnCount;
        if (!clicked)
        {
            waitTurn = currentTurn;
        }
        if (currentTurn - waitTurn >= waiter)
        {
            animator.SetBool("isClicked", false);
            clicked = false;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameObject[] spikes = GameObject.FindGameObjectsWithTag("Spike");
            foreach (GameObject spike in spikes)
            {
                bool activity = spike.GetComponent<Spike>().active;
                if (activity)
                {
                    spike.GetComponent<Spike>().active = false;
                    spike.GetComponent<Spike>().customWait = waiter;
                    if (!clicked)
                    {
                        animator.SetBool("isClicked", true);
                        clicked = true;
                    }
                }
            }
        }
    }
}
