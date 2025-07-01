using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostScript : MonoBehaviour
{
    private GameObject player;
    private int currentTurn;
    private int prevTurn;
    public int verticalForTurn;
    private Rigidbody2D rb;
    private Transform transform;
    private float yPos;
    private bool colliderInNext;
    void Start()
    {
        colliderInNext = false;
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        prevTurn = 0;
        transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        yPos = transform.position.y;
        currentTurn = player.GetComponent<CharacterMovement>().turnCount;
        verticalForTurn = player.GetComponent<CharacterMovement>().vertical;
        List<float> yPositions = new List<float>();
        GameObject[] allColliders = GameObject.FindGameObjectsWithTag("Collider");
        foreach (GameObject collider in allColliders)
        {
            yPositions.Add(collider.transform.position.y);
        }
        if (currentTurn != prevTurn && verticalForTurn != 0)
        {
            if (player.transform.position.y + verticalForTurn < transform.position.y)
            {
                colliderInNext = false;
                foreach (float yPos in yPositions)
                {
                    if (yPos == transform.position.y - 1f)
                    {
                        colliderInNext = true;
                    }
                }
                if (colliderInNext == false)
                {
                    yPos--;
                }
            }
            else if (player.transform.position.y + verticalForTurn > transform.position.y)
            {
                colliderInNext = false;
                foreach (float yPos in yPositions)
                {
                    if (yPos == transform.position.y + 1f)
                    {
                        colliderInNext = true;
                    }
                }
                if (colliderInNext == false)
                {
                    yPos++;
                }
            }
            rb.MovePosition(new Vector3(transform.position.x, yPos, 0f));
        }
        prevTurn = currentTurn;
    }
}
