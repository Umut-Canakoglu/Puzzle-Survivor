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
    private Animator animator;
    private float startRotation;
    public GameObject turretArea;
    public GameObject currentTurretArea;
    private GameObject triangleTurretArea;

    public Color normalColor;
    public Color dangerousColor;

    void Start()
    {
        animator = GetComponent<Animator>();
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
        startRotation = transform.eulerAngles.z;
        createTurretArea(startRotation);
    }
    void Update()
    {
        currentTurn = player.GetComponent<CharacterMovement>().turnCount;
        triangleTurretArea = currentTurretArea.gameObject.transform.GetChild(0).gameObject;
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
                rb.rotation = startRotation;
            }
        }
        if(PlayerCheck() && currentTurn - prevTurn == 2){
            currentTurretArea.GetComponent<SpriteRenderer>().color = dangerousColor;
            triangleTurretArea.GetComponent<SpriteRenderer>().color = dangerousColor;
        } else{
            currentTurretArea.GetComponent<SpriteRenderer>().color = normalColor;
            triangleTurretArea.GetComponent<SpriteRenderer>().color = normalColor;
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
            animator.SetTrigger("Shoot");
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
        rb.rotation = startRotation;
        startActive = false;
    }
    private bool PlayerCheck()
    {
        RaycastHit2D hitMiddle = Physics2D.Raycast(rayMiddle.origin, rayMiddle.direction, 2f, layersToHit);
        RaycastHit2D hitFirst = Physics2D.Raycast(rayFirst.origin, rayFirst.direction, 2f, layersToHit);
        RaycastHit2D hitThird = Physics2D.Raycast(rayThird.origin, rayThird.direction, 2f, layersToHit);
        return (hitMiddle.collider != null && hitMiddle.collider.CompareTag("Player")) ||
                (hitFirst.collider != null && hitFirst.collider.CompareTag("Player")) ||
                (hitThird.collider != null && hitThird.collider.CompareTag("Player"));
    }

    private void createTurretArea(float rot)
    {
        if (rot == 0)
        {
            currentTurretArea = Instantiate(turretArea, new Vector3(transform.position.x, transform.position.y + 2, 0f), Quaternion.Euler(0, 0, 180));
        }
        else if (rot == 90)
        {
            currentTurretArea = Instantiate(turretArea, new Vector3(transform.position.x - 2, transform.position.y, 0f), Quaternion.Euler(0, 0, -90));
        }
        else if (rot == 270)
        {
            currentTurretArea = Instantiate(turretArea, new Vector3(transform.position.x + 2, transform.position.y, 0f), Quaternion.Euler(0, 0, 90));
        }
        else if (rot == 180)
        {
            currentTurretArea = Instantiate(turretArea, new Vector3(transform.position.x, transform.position.y - 2, 0f), Quaternion.Euler(0, 0, 0));
        }
    }
}
