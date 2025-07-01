using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalGhost : MonoBehaviour
{
    private GameObject player;
    private int currentTurn;
    private int prevTurn;
    public int horizontalForTurn;
    private Rigidbody2D rb;
    private Transform transform;
    private float xPos;
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
        xPos = transform.position.x;
        currentTurn = player.GetComponent<CharacterMovement>().turnCount;
        horizontalForTurn = player.GetComponent<CharacterMovement>().horizontal;
        List<float> xPositions = new List<float>();
        GameObject[] allColliders = GameObject.FindGameObjectsWithTag("Collider");
        foreach (GameObject collider in allColliders)
        {
            xPositions.Add(collider.transform.position.x);
        }
        if (currentTurn != prevTurn && horizontalForTurn != 0)
        {
            if (player.transform.position.x + horizontalForTurn < transform.position.x)
            {
                colliderInNext = false;
                foreach (float xPos in xPositions)
                {
                    if (xPos == transform.position.x - 1f)
                    {
                        colliderInNext = true;
                    }
                }
                if (colliderInNext == false)
                {
                    xPos--;
                }
            }
            else if (player.transform.position.x + horizontalForTurn > transform.position.x)
            {
                colliderInNext = false;
                foreach (float xPos in xPositions)
                {
                    if (xPos == transform.position.x + 1f)
                    {
                        colliderInNext = true;
                    }
                }
                if (colliderInNext == false)
                {
                    xPos++;
                }
            }
            rb.MovePosition(new Vector3(xPos, transform.position.y, 0f));
        }
        prevTurn = currentTurn;
    }
}
