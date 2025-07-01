using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrow : MonoBehaviour
{
    public GameObject flame;
    private Transform transform;
    private bool onceEnter;
    private bool flameCurrentlyActive;
    private GameObject flameObj;
    // Start is called before the first frame update
    void Start()
    {
        onceEnter = false;
        flameCurrentlyActive = false;
        transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        int turnNum = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterMovement>().turnCount;
        if (turnNum % 2 == 0 && turnNum != 0 && onceEnter == false)
        {
            if (flameCurrentlyActive == false)
            {
                StartCoroutine(DelayedKill());
            }
            else
            {
                if (flameObj)
                {
                    Destroy(flameObj);
                }
                flameCurrentlyActive = false;
            }
            onceEnter = true;
        }
        if (turnNum % 2 == 1)
        {
            onceEnter = false;
        }
    }
    IEnumerator DelayedKill()
    {
        yield return new WaitForSeconds(0.1f); 
        Vector3 newPos = transform.position + transform.up;
        flameObj = Instantiate(flame, newPos, transform.rotation);
        flameCurrentlyActive = true;
    }
}
