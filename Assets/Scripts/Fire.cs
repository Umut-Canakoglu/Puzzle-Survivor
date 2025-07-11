using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public GameObject keyObj;
    void Start()
    {
        transform.Rotate(0f, 0f, 90f, Space.Self); 
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (other.gameObject.GetComponent<Radiation>().ArmorCurrent)
            {
                other.gameObject.GetComponent<Radiation>().ArmorCurrent = false;
                StartCoroutine(DelayedGone());
            }
            else
            {
                Destroy(other.gameObject);
            }

        }
        if (other.gameObject.tag == "Zombie")
        {
            Instantiate(keyObj, other.gameObject.transform.position, other.gameObject.transform.rotation);
            Destroy(other.gameObject);
        }
    }
    IEnumerator DelayedGone()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
}
