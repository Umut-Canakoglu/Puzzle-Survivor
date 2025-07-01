using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public int turnCount;
    private Rigidbody2D rb;
    private Transform transform;
    public int horizontal;
    public int vertical;
    private bool movement;
    void Start()
    {
        horizontal = 1;
        vertical = 0;
        rb = GetComponent<Rigidbody2D>();
        transform = GetComponent<Transform>();
        turnCount = 0;
    }
    void Update()
    {
        List<Vector3> positions = new List<Vector3>();
        GameObject[] allColliders = GameObject.FindGameObjectsWithTag("Collider");
        foreach (GameObject collider in allColliders)
        {
            positions.Add(collider.transform.position);
        }
        float posY = transform.position.y;
        float posX = transform.position.x;
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            rb.rotation = 90f;
            horizontal = 0;
            vertical = 1;
        } else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            rb.rotation = 270f;
            horizontal = 0;
            vertical = -1;
        } else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            rb.rotation = 180f;
            horizontal = -1;
            vertical = 0;
        } else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            rb.rotation = 0;
            horizontal = 1;
            vertical = 0;
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            float newY = posY + vertical;
            float newX = posX + horizontal;
            if (newX < -8 || newX > 8 || newY < -4 || newY > 4)
            {
                newY = posY;
                newX = posX;
            } else {
                movement = true;
                foreach (Vector3 positionP in positions)
                {
                    Vector3 newPos = new Vector3(newX, newY, transform.position.z);
                    if(positionP == newPos)
                    {
                        movement = false;
                    }
                }
                if (movement == true)
                {
                    rb.MovePosition(new Vector3(newX, newY, 0f));
                    turnCount ++;
                }
            }
        }
    }
}
