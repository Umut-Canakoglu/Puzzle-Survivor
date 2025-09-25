using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSetUp : MonoBehaviour
{
    void Start()
    {
        GameObject wallUp = GameObject.FindGameObjectWithTag("WallUp");
        GameObject wallDown = GameObject.FindGameObjectWithTag("WallDown");
        GameObject wallLeft = GameObject.FindGameObjectWithTag("WallLeft");
        GameObject wallRight = GameObject.FindGameObjectWithTag("WallRight");
        GameObject cam = GameObject.FindGameObjectWithTag("MainCamera");
        TileSpawn positions = GetComponent<TileSpawn>();
        int yMax = positions.yMax;
        int yMin = positions.yMin;
        int xMax = positions.xMax;
        int xMin = positions.xMin;
        int xDiff = xMax - xMin;
        int yDiff = yMax - yMin;
        float xCenter = (xMax + xMin) / 2f;
        float yCenter = (yMax + yMin) / 2f;
        wallUp.transform.position = new Vector3(xCenter, yMax + 1f, 0f);
        wallDown.transform.position = new Vector3(xCenter, yMin - 1f, 0f);
        wallLeft.transform.position = new Vector3(xMin - 1f, yCenter, 0f);
        wallRight.transform.position = new Vector3(xMax + 1f, yCenter, 0);
        wallUp.transform.localScale = new Vector3(xDiff + 2f, 1, 1);
        wallDown.transform.localScale = new Vector3(xDiff + 2f, 1, 1);
        wallLeft.transform.localScale = new Vector3(1, yDiff + 2f, 1);
        wallRight.transform.localScale = new Vector3(1, yDiff + 2f, 1);
        cam.transform.position = new Vector3(xCenter, yCenter, -1f);
    }
}
