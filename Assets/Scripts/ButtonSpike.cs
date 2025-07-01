using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSpike : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameObject[] spikes = GameObject.FindGameObjectsWithTag("Spike");
            foreach (GameObject spike in spikes)
            {
                spike.GetComponent<Spike>().active = !spike.GetComponent<Spike>().active;
            }
        }
    }
}
