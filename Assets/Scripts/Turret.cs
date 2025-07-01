using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private bool isActive;
    public bool startActive;
    private GameObject player;
    public int currentTurn;
    public int prevTurn;
    private Transform transform;
    private LayerMask layersToHit;
    private Ray2D rayFirst;
    private Ray2D rayMiddle;
    private Ray2D rayThird;
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        layersToHit = LayerMask.GetMask("Wall", "Player", "Enemy");
        transform = GetComponent<Transform>();
        currentTurn = 0;
        prevTurn = 0;
        player = GameObject.FindGameObjectWithTag("Player");
        isActive = false;
        startActive = false;
        Vector2 nextPos = transform.position + transform.up + transform.right;
        Vector2 otherPos = transform.position + transform.up - transform.right;
        Vector2 middlePos = transform.position + transform.up;
        rayMiddle = new Ray2D(middlePos, transform.up);
        rayFirst = new Ray2D(nextPos, transform.up);
        rayThird = new Ray2D(otherPos, transform.up);
    }
    void Update()
    {
        currentTurn = player.GetComponent<CharacterMovement>().turnCount;
        if (PlayerCheck() && startActive == false && isActive == false)
        {
            startActive = true;
            prevTurn = currentTurn;
        }
        if (currentTurn - prevTurn <= 3 && startActive)
        {
            if (PlayerCheck())
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                Vector2 direction = (Vector2)player.transform.position - (Vector2)transform.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
                rb.rotation = angle;
            }
            else
            {
                rb.rotation = 0f;
            }
        }
        if (currentTurn - prevTurn == 3 && startActive)
        {
            startActive = false;
            isActive = true;
        }
        if (isActive)
        {
            StartCoroutine(DelayedDestroy());
        }
    }
    IEnumerator DelayedDestroy()
    {
        isActive = false;
        yield return new WaitForSeconds(0.1f);
        if (PlayerCheck())
        {
            Debug.Log(GameObject.FindGameObjectWithTag("Player").GetComponent<Radiation>().ArmorCurrent);
            if (GameObject.FindGameObjectWithTag("Player").GetComponent<Radiation>().ArmorCurrent)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<Radiation>().ArmorCurrent = false;
            }
            else
            {
                Destroy(GameObject.FindGameObjectWithTag("Player"));
            }
        }
        rb.rotation = 0f;
        startActive = false;
    }
    private bool PlayerCheck()
    {
        RaycastHit2D hitMiddle = Physics2D.Raycast(rayMiddle.origin, rayMiddle.direction, 100f, layersToHit);
        RaycastHit2D hitFirst = Physics2D.Raycast(rayFirst.origin, rayFirst.direction, 100f, layersToHit);
        RaycastHit2D hitThird = Physics2D.Raycast(rayThird.origin, rayThird.direction, 100f, layersToHit);
        return (hitMiddle.collider != null && hitMiddle.collider.CompareTag("Player")) ||
                (hitFirst.collider != null && hitFirst.collider.CompareTag("Player")) ||
                (hitThird.collider != null && hitThird.collider.CompareTag("Player"));
    }
}
