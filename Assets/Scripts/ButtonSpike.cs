using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSpike : MonoBehaviour
{
    public int waiter;
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
                }
            }
        }
    }
}
