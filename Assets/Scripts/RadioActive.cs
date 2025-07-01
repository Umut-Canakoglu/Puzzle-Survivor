using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class RadioActive : MonoBehaviour
{
    public bool maskOn;
    private GameObject bar;
    void Start()
    {
        maskOn = false;
        bar = GameObject.FindGameObjectWithTag("Bar");
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (maskOn == false && other.gameObject.tag == "Player")
        {
            bar.GetComponent<Image>().fillAmount = 1f;
        }
    }
}
