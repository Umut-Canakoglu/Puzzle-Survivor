using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelPlayer : MonoBehaviour
{
    void Awake()
    {
        GameObject notification = GameObject.FindGameObjectWithTag("Notification");
        notification.GetComponent<TextMeshProUGUI>().color = Color.black;
        GameObject[] buttons = GameObject.FindGameObjectsWithTag("LevelButton");
        buttons = order2D(buttons);
        int i = 0;
        foreach (GameObject button in buttons)
        {
            i++;
            string buttonName = "Level" + i.ToString();
            string beforeName = "Level" + (i - 1).ToString();
            button.name = buttonName;
            Button buttonElement = button.GetComponent<Button>();
            TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = buttonName;
            if (GameManager.Instance.levelCompletes.ContainsKey(buttonName) == false)
            {
                GameManager.Instance.levelCompletes[buttonName] = false;
            }
            buttonElement.colors = baseBlock(GameManager.Instance.levelCompletes[buttonName]);
            if (i > 1 && GameManager.Instance.levelCompletes[beforeName] == false)
            {
                buttonElement.onClick.AddListener(() => UnlockError(beforeName));
            }
            else
            {
                buttonElement.onClick.AddListener(() => LevelLoad(buttonName));
            }
        }
    }
    public void LevelLoad(string levelName)
    {
        SceneManager.LoadScene(levelName, LoadSceneMode.Single);
    }
    public void UnlockError(string levelName)
    {
        GameObject notification = GameObject.FindGameObjectWithTag("Notification");
        notification.GetComponent<TextMeshProUGUI>().text = "You need to complete " + levelName + " first";
        notification.GetComponent<TextMeshProUGUI>().color = Color.green;
        float speed = 1f;
        notification.GetComponent<Rigidbody2D>().velocity = speed * Vector2.up;
        StartCoroutine(Stop(notification));
    }
    private GameObject[] order2D(GameObject[] objectArray)
    {
        GameObject[] arrayToEdit = objectArray;
        GameObject[] ordered2dArray = new GameObject[arrayToEdit.Length];
        for (int i = 0; i < ordered2dArray.Length; i++)
        {
            float maxY = -10000;
            foreach (GameObject gameObject in arrayToEdit)
            {
                if (gameObject != null && gameObject.GetComponent<RectTransform>().position.y > maxY)
                {
                    maxY = gameObject.GetComponent<RectTransform>().position.y;
                }
            }
            float minX = 10000;
            foreach (GameObject gameObject in arrayToEdit)
            {
                if (gameObject != null && gameObject.GetComponent<RectTransform>().position.y == maxY)
                {
                    if (gameObject.GetComponent<RectTransform>().position.x < minX)
                    {
                        minX = gameObject.GetComponent<RectTransform>().position.x;
                    }
                }
            }
            for (int j = 0; j < arrayToEdit.Length; j++)
            {
                if (arrayToEdit[j] != null &&
                arrayToEdit[j].GetComponent<RectTransform>().position.x == minX &&
                arrayToEdit[j].GetComponent<RectTransform>().position.y == maxY)
                {
                    ordered2dArray[i] = arrayToEdit[j];
                    arrayToEdit[j] = null;
                    break;
                }
            }
        }
        return ordered2dArray;
    }
    private ColorBlock baseBlock(bool isCompleted)
    {
        ColorBlock baseBlock = new ColorBlock();
        baseBlock.selectedColor = Color.white;
        baseBlock.disabledColor = Color.black;
        baseBlock.colorMultiplier = 1;
        baseBlock.fadeDuration = 0.1f;
        if (isCompleted)
        {
            baseBlock.normalColor = Color.green;
            baseBlock.highlightedColor = new Color32(r: 125, g: 255, b: 125, a: 255);
        }
        else
        {
            baseBlock.normalColor = Color.red;
            baseBlock.highlightedColor = new Color32(r: 255, g: 125, b: 125, a: 255);
        }
        baseBlock.pressedColor = baseBlock.highlightedColor;
        baseBlock.selectedColor = baseBlock.highlightedColor;
        return baseBlock;
    }
    IEnumerator Stop(GameObject stoppedObj)
    {
        yield return new WaitForSeconds(1f);
        stoppedObj.GetComponent<Rigidbody2D>().velocity = 2f * Vector2.down;
        yield return new WaitForSeconds(1f);
        stoppedObj.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        stoppedObj.GetComponent<TextMeshProUGUI>().color = Color.black;
    }
}
