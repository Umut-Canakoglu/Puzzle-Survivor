using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    public Sprite spikeOn;
    public Sprite spikeOff;
    public bool active;
    private SpriteRenderer spriteRenderer;
    private int waitTurn;
    private int currentTurn;
    private GameObject player;
    public GameObject keyObjSpike;
    void Start()
    {
        waitTurn = 0;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = spikeOff;
        player = GameObject.FindGameObjectWithTag("Player");
        active = false;
    }
    void Update()
    {
        currentTurn = player.GetComponent<CharacterMovement>().turnCount;
        if (active)
        {
            spriteRenderer.sprite = spikeOn;
        }
        else
        {
            spriteRenderer.sprite = spikeOff;
            waitTurn = currentTurn;
        }
        if (currentTurn - waitTurn >= 5)
        {
            active = false;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (active)
        {
            if (other.gameObject.tag == "Player")
            {
                if (other.gameObject.GetComponent<Radiation>().ArmorCurrent)
                {
                    other.gameObject.GetComponent<Radiation>().ArmorCurrent = true;
                }
                else
                {
                    Destroy(other.gameObject);
                }
            }
            if (other.gameObject.tag == "Zombie")
            {
                Instantiate(keyObjSpike, other.gameObject.transform.position, other.gameObject.transform.rotation);
                Destroy(other.gameObject);
            }
        }
    }
}
