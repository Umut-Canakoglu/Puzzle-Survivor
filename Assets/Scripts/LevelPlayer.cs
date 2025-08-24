using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelPlayer : MonoBehaviour
{
    public void Level1Load()
    {
        SceneManager.LoadScene("Level1", LoadSceneMode.Single);
    }
    public void Level2Load()
    {
        SceneManager.LoadScene("Level2", LoadSceneMode.Single);
    }
}
