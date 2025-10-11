using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ButtonSpike : MonoBehaviour
{
    public int waiter;
    private Animator animator;
    private bool clicked;
    private int currentTurn;
    private int waitTurn;
    private GameObject buttonTimer;
    void Start()
    {
        animator = GetComponent<Animator>();
        clicked = false;
        buttonTimer = GameObject.FindGameObjectWithTag("ButtonTimer");
        buttonTimer.GetComponent<TextMeshProUGUI>().text = "";
    }
    void Update()
    {
        currentTurn = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterMovement>().turnCount;
        if (clicked)
        {
            int difference = waiter - currentTurn + waitTurn;
            buttonTimer.GetComponent<TextMeshProUGUI>().text = "Button Time: " + difference.ToString();
        }
        else
        {
            waitTurn = currentTurn;
        }
        if (currentTurn - waitTurn >= waiter)
        {
            buttonTimer.GetComponent<TextMeshProUGUI>().text = "";
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
