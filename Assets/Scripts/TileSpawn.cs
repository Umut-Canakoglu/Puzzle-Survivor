using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TileSpawn : MonoBehaviour
{
    public GameObject tilePrefab;
    public Color secondaryColor;
    public Color primaryColor;
    void Start()
    {
        for (int x = -8; x <= 8; x++)
        {
            for (int y = -4; y <= 4; y++)
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
        if (GameObject.FindGameObjectWithTag("Player") == null)
        {
            SceneManager.LoadScene("Menu", LoadSceneMode.Single);
        }
    }
}
