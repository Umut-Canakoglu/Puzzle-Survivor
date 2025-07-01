using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    private GameObject player;
    private int currentTurn;
    private int prevTurn;
    private Rigidbody2D rb;
    private Transform transform;
    private float yPos;
    private float xPos;
    private bool canMove;
    private GameObject[] allColliders;
    private bool waitForIt;
    void Start()
    {
        waitForIt = false;
        canMove = false;
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        prevTurn = 0;
        transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (waitForIt == false)
        {
            Vector2 playPos = player.transform.position;
            xPos = Mathf.Round(transform.position.x);
            yPos = Mathf.Round(transform.position.y);
            if (Mathf.Abs(playPos.x - xPos) > 3 || Mathf.Abs(playPos.y - yPos) > 3)
            {
                canMove = canMove;
            }
            else
            {
                canMove = true;
            }
            allColliders = GameObject.FindGameObjectsWithTag("Collider");
            currentTurn = player.GetComponent<CharacterMovement>().turnCount;
            if (canMove == true && currentTurn != prevTurn)
            {
                if (playPos.x - xPos != 0 && playPos.y - yPos == 0)
                {
                    if (playPos.x > xPos)
                    {
                        float newX = MoveRight(transform.position);
                        if (newX != 100f)
                        {
                            xPos = newX;
                        }
                        else
                        {
                            Debug.Log("OE1");
                            float newY = MoveUp(transform.position);
                            if (newY != 100f)
                            {
                                yPos = newY;
                            }
                            else
                            {
                                yPos = MoveDown(transform.position);
                            }
                        }
                    }
                    else
                    {
                        float newX = MoveLeft(transform.position);
                        if (newX != 100f)
                        {
                            xPos = newX;
                        }
                        else
                        {
                            Debug.Log("OE2");
                            float newY = MoveDown(transform.position);
                            if (newY != 100f)
                            {
                                yPos = newY;
                            }
                            else
                            {
                                yPos = MoveUp(transform.position);
                            }
                        }
                    }
                }
                else if (playPos.x - xPos == 0 && playPos.y - yPos != 0)
                {
                    if (playPos.y > yPos)
                    {
                        float newY = MoveUp(transform.position);
                        if (newY != 100f)
                        {
                            yPos = newY;
                        }
                        else
                        {
                            float newX = MoveRight(transform.position);
                            if (newX != 100f)
                            {
                                xPos = newX;
                            }
                            else
                            {
                                xPos = MoveLeft(transform.position);
                            }
                        }
                    }
                    else
                    {
                        float newY = MoveDown(transform.position);
                        if (newY != 100f)
                        {
                            yPos = newY;
                        }
                        else
                        {
                            float newX = MoveLeft(transform.position);
                            if (newX != 100f)
                            {
                                xPos = newX;
                            }
                            else
                            {
                                xPos = MoveRight(transform.position);
                            }
                        }
                    }
                }
                else if (playPos.x - xPos != 0 && playPos.y - yPos != 0)
                {
                    if (Mathf.Abs(playPos.x - xPos) >= Mathf.Abs(playPos.y - yPos))
                    {
                        if (playPos.y > yPos)
                        {
                            float newY = MoveUp(transform.position);
                            if (newY != 100f)
                            {
                                yPos = newY;
                            }
                            else
                            {
                                Debug.Log("OE4");
                                if (playPos.x > xPos)
                                {
                                    float newX = MoveRight(transform.position);
                                    if (newX != 100f)
                                    {
                                        xPos = newX;
                                    }
                                    else
                                    {
                                        xPos = MoveLeft(transform.position);
                                    }
                                }
                                else
                                {
                                    float newX = MoveLeft(transform.position);
                                    if (newX != 100f)
                                    {
                                        xPos = newX;
                                    }
                                    else
                                    {
                                        xPos = MoveRight(transform.position);
                                    }
                                }
                            }
                        }
                        else
                        {
                            float newY = MoveDown(transform.position);
                            if (newY != 100f)
                            {
                                yPos = newY;
                            }
                            else
                            {
                                Debug.Log("OE5");
                                if (playPos.x > xPos)
                                {
                                    float newX = MoveRight(transform.position);
                                    if (newX != 100f)
                                    {
                                        xPos = newX;
                                    }
                                    else
                                    {
                                        xPos = MoveLeft(transform.position);
                                    }
                                }
                                else
                                {
                                    float newX = MoveLeft(transform.position);
                                    if (newX != 100f)
                                    {
                                        xPos = newX;
                                    }
                                    else
                                    {
                                        xPos = MoveRight(transform.position);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (playPos.x > xPos)
                        {
                            float newX = MoveRight(transform.position);
                            if (newX != 100f)
                            {
                                xPos = newX;
                            }
                            else
                            {
                                Debug.Log("OE6");
                                if (playPos.y > yPos)
                                {
                                    float newY = MoveUp(transform.position);
                                    if (newY != 100f)
                                    {
                                        yPos = newY;
                                    }
                                    else
                                    {
                                        yPos = MoveDown(transform.position);
                                    }
                                }
                                else
                                {
                                    float newY = MoveDown(transform.position);
                                    if (newY != 100f)
                                    {
                                        yPos = newY;
                                    }
                                    else
                                    {
                                        yPos = MoveUp(transform.position);
                                    }
                                }
                            }
                        }
                        else
                        {
                            float newX = MoveLeft(transform.position);
                            if (newX != 100f)
                            {
                                xPos = newX;
                            }
                            else
                            {
                                Debug.Log("OE7");
                                if (playPos.y > yPos)
                                {
                                    float newY = MoveUp(transform.position);
                                    if (newY != 100f)
                                    {
                                        yPos = newY;
                                    }
                                    else
                                    {
                                        yPos = MoveDown(transform.position);
                                    }
                                }
                                else
                                {
                                    float newY = MoveDown(transform.position);
                                    if (newY != 100f)
                                    {
                                        yPos = newY;
                                    }
                                    else
                                    {
                                        yPos = MoveUp(transform.position);
                                    }
                                }
                            }
                        }
                    }
                }
                StartCoroutine(DelayedMovement(xPos, yPos));
            }
            prevTurn = currentTurn;
        }
    }
    private bool CanMoveTo(Vector2 position, GameObject[] colliders)
    {
        if (position.x > 8 || position.x < -8 || position.y > 4 || position.y < -4)
        {
            return false;
        }
        foreach (GameObject collider in colliders)
            {
                if (Vector2.Distance(collider.transform.position, position) < 0.01f)
                {
                    return false;
                }
            }
        return true;
    }
    private float MoveUp(Vector2 characterPosition)
    {
        if (CanMoveTo(new Vector2(characterPosition.x, characterPosition.y + 1f), allColliders))
        {
            return characterPosition.y + 1f;
        }
        else
        {
            return 100f;
        }
    }
    private float MoveDown(Vector2 characterPosition)
    {
        if (CanMoveTo(new Vector2(characterPosition.x, characterPosition.y - 1f), allColliders))
        {
            return characterPosition.y - 1f;
        }
        else
        {
            return 100f;
        }
    }
    private float MoveLeft(Vector2 characterPosition)
    {
        if (CanMoveTo(new Vector2(characterPosition.x - 1f, characterPosition.y), allColliders))
        {
            return characterPosition.x - 1f;
        }
        else
        {
            return 100f;
        }
    }
    private float MoveRight(Vector2 characterPosition)
    {
        if (CanMoveTo(new Vector2(characterPosition.x + 1f, characterPosition.y), allColliders))
        {
            return characterPosition.x + 1f;
        }
        else
        {
            return 100f;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (other.gameObject.GetComponent<Radiation>().ArmorCurrent)
            {
                transform.position = transform.position + transform.up;
                other.gameObject.GetComponent<Radiation>().ArmorCurrent = false;
            }
            else
            {
                rb.constraints = RigidbodyConstraints2D.FreezeAll;
                Destroy(other.gameObject);
            }
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag == "Player")
        {
            if (collision.collider.gameObject.GetComponent<Radiation>().ArmorCurrent)
            {
                transform.position = transform.position + transform.up*2;
                collision.collider.gameObject.GetComponent<Radiation>().ArmorCurrent = false;
            }
            else
            {
                rb.constraints = RigidbodyConstraints2D.FreezeAll;
                Destroy(collision.collider.gameObject);
            }
        }
    }
    IEnumerator DelayedMovement(float xPosition, float yPosition)
    {
        waitForIt = true;
        yield return new WaitForSeconds(0.1f);
        Vector2 direction = new Vector2(xPosition, yPosition) - (Vector2)transform.position;
        transform.position = new Vector3(xPosition, yPosition, 0f);
        rb.rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90;
        waitForIt = false;
    }
}
