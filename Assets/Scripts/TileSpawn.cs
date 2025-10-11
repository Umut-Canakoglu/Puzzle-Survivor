using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TileSpawn : MonoBehaviour
{
    public GameObject tilePrefab;
    public Color secondaryColor;
    public Color primaryColor;
    public bool TestMode;
    public int yMax;
    public int yMin;
    public int xMax;
    public int xMin;
    private bool doorIsOpen;
    void Start()
    {
        doorIsOpen = false;
        for (int x = xMin; x <= xMax; x++)
        {
            for (int y = yMin; y <= yMax; y++)
            {
                GameObject newTile = Instantiate(tilePrefab, new Vector3(x, y), Quaternion.identity);
                SpriteRenderer rendererObject = newTile.GetComponent<SpriteRenderer>();
                bool isSecondary = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);
                if (isSecondary == true)
                {
                    rendererObject.color = secondaryColor;
                }
                else
                {
                    rendererObject.color = primaryColor;
                }
            }
        }
    }
    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null && TestMode == false)
        {
            string currentScene = SceneManager.GetActiveScene().name;
            Debug.Log(currentScene);
            GameManager.Instance.levelCompletes[currentScene] = doorIsOpen;
            SceneManager.LoadScene("Menu", LoadSceneMode.Single);
        }
        else
        {
            doorIsOpen = player.GetComponent<Gates>().doorOpened;
        }
    }
}
