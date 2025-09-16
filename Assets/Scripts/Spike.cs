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
    public int customWait;
    void Start()
    {
        customWait = 5;
        waitTurn = 0;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = spikeOn;
        player = GameObject.FindGameObjectWithTag("Player");
        active = true;
    }
    void Update()
    {
        currentTurn = player.GetComponent<CharacterMovement>().turnCount;
        if (active)
        {
            spriteRenderer.sprite = spikeOn;
            waitTurn = currentTurn;
        }
        else
        {
            spriteRenderer.sprite = spikeOff;
        }
        if (currentTurn - waitTurn >= customWait)
        {
            active = true;
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
                    StartCoroutine(destroyEnumerator(other.gameObject));
                }
            }
            if (other.gameObject.tag == "Zombie")
            {
                Instantiate(keyObjSpike, other.gameObject.transform.position, other.gameObject.transform.rotation);
                Destroy(other.gameObject);
            }
        }
    }
    IEnumerator destroyEnumerator(GameObject gameObject)
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }
}
