using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Radiation : MonoBehaviour
{
    private GameObject bar;
    public bool MaskAvailable;
    public bool InventoryAvailable;
    public bool MaskCurrent;
    public bool ArmorAvailable;
    public GameObject[] radioPuddles;
    private GameObject inventory;
    public Sprite emptyInventory;
    private Animator animator;
    private int waitTimeMask;
    private int waitTimeArmor;
    private int currentTurn;
    public bool ArmorCurrent;
    void Start()
    {
        waitTimeMask = 0;
        waitTimeArmor = 0;
        currentTurn = 0;
        animator = GetComponent<Animator>();
        MaskCurrent = false;
        inventory = GameObject.FindGameObjectWithTag("Inventory");
        radioPuddles = GameObject.FindGameObjectsWithTag("Radioactive");
        MaskAvailable = false;
        ArmorAvailable = false;
        InventoryAvailable = true;
        bar = GameObject.FindGameObjectWithTag("Bar");
    }
    void Update()
    {
        currentTurn = GetComponent<CharacterMovement>().turnCount;
        if (Input.GetKeyDown(KeyCode.E) && MaskAvailable && !ArmorCurrent)
        {
            foreach (GameObject puddle in radioPuddles)
            {
                puddle.GetComponent<RadioActive>().maskOn = true;
            }
            inventory.GetComponent<Image>().sprite = emptyInventory;
            MaskAvailable = false;
            InventoryAvailable = true;
            animator.SetBool("MaskCurrently", true);
            MaskCurrent = true;
            waitTimeMask = currentTurn;
        }
        if (Input.GetKeyDown(KeyCode.E) && ArmorAvailable && !MaskCurrent)
        {
            inventory.GetComponent<Image>().sprite = emptyInventory;
            ArmorAvailable = false;
            ArmorCurrent = true;
            waitTimeArmor = currentTurn;
            InventoryAvailable = true;
        }
        if (currentTurn - waitTimeMask == 5 && MaskCurrent)
        {
            MaskCurrent = false;
            foreach (GameObject puddle in radioPuddles)
            {
                puddle.GetComponent<RadioActive>().maskOn = false;
            }
            animator.SetBool("MaskCurrently", false);
        }
        if (currentTurn - waitTimeArmor == 5 && ArmorCurrent)
        {
            ArmorCurrent = false;
        }
        if (bar.GetComponent<Image>().fillAmount == 1f)
        {
            StartCoroutine(DelayedDestroy());
            MaskCurrent = true;
        }
        if (MaskCurrent == false)
        {
            bar.GetComponent<Image>().fillAmount = currentTurn / 100f;
        }
    }
    IEnumerator DelayedDestroy()
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (InventoryAvailable)
        {
            if (other.gameObject.tag == "Mask")
            {
                MaskAvailable = true;
                GetRid(other.gameObject);
            }
            if (other.gameObject.tag == "Armor")
            {
                ArmorAvailable = true;
                GetRid(other.gameObject);
            }
        }
    }
    private void GetRid(GameObject presentObject)
    {
        InventoryAvailable = false;
        inventory.GetComponent<Image>().sprite = presentObject.GetComponent<SpriteRenderer>().sprite;
        Destroy(presentObject);
    }
}
