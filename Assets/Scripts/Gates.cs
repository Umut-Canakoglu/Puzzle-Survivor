using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gates : MonoBehaviour
{
    public bool KeyAvailable;
    public bool KeyCurrent;
    public GameObject gates;
    public Sprite gateOpen;
    public Sprite gateClose;
    void Start()
    {
        KeyAvailable = false;
        KeyCurrent = false;
        gates = GameObject.FindGameObjectWithTag("Gate");
        gates.GetComponent<SpriteRenderer>().sprite = gateClose;
        gates.GetComponent<SpriteRenderer>().flipX = true;
        gates.GetComponent<SpriteRenderer>().flipY = false;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && KeyAvailable && Vector2.Distance(transform.position, gates.transform.position) < 1.5f)
        {
            KeyAvailable = false;
            KeyCurrent = true;
            gates.transform.position = gates.transform.position - gates.transform.up * 0.4f - gates.transform.right * 0.4f;
            gates.GetComponent<SpriteRenderer>().sprite = gateOpen;
            gates.GetComponent<SpriteRenderer>().flipX = false;
            gates.GetComponent<SpriteRenderer>().flipY = true;
            StartCoroutine(DelayedDestroy());
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Key")
        {
            KeyAvailable = true;
            Destroy(other.gameObject);
        }
    }
    IEnumerator DelayedDestroy()
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }
}
