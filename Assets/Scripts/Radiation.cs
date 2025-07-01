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
    public bool KeyAvailable;
    public bool KeyCurrent;
    public GameObject gates;
    public Sprite gateOpen;
    public Sprite gateClose;
    void Start()
    {
        KeyAvailable = false;
        KeyCurrent = false;
        waitTimeMask = 0;
        waitTimeArmor = 0;
        currentTurn = 0;
        animator = GetComponent<Animator>();
        MaskCurrent = false;
        inventory = GameObject.FindGameObjectWithTag("Inventory");
        radioPuddles = GameObject.FindGameObjectsWithTag("Radioactive");
        gates = GameObject.FindGameObjectWithTag("Gate");
        MaskAvailable = false;
        ArmorAvailable = false;
        InventoryAvailable = true;
        bar = GameObject.FindGameObjectWithTag("Bar");
        gates.GetComponent<SpriteRenderer>().sprite = gateClose;
        gates.GetComponent<SpriteRenderer>().flipX = true;
        gates.GetComponent<SpriteRenderer>().flipY = false;
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
        if (other.gameObject.tag == "Key")
        {
            KeyAvailable = true;
            Destroy(other.gameObject);
        }
    }
    private void GetRid(GameObject presentObject)
    {
        InventoryAvailable = false;
        inventory.GetComponent<Image>().sprite = presentObject.GetComponent<SpriteRenderer>().sprite;
        Destroy(presentObject);
    }
}
