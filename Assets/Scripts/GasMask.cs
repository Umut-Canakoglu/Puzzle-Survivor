using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GasMask : MonoBehaviour
{
    private GameObject inventory;
    private Sprite selfSprite;
    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Inventory");
        selfSprite = GetComponent<SpriteRenderer>().sprite;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            inventory.GetComponent<Image>().sprite = selfSprite;
            other.gameObject.GetComponent<Radiation>().MaskAvailable = true;
            Destroy(gameObject);
        }
    }
}
