using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    GUIStyle _guiStyle = new GUIStyle();
    string _cubesCount;
    public GameObject endText;

    void Start()
    {
        endText.SetActive(false);
    }

    void Update()
    {
        _cubesCount = GameManager.gameManager.cubesCount.ToString();

        if (GameManager.gameManager.cubesCount <= 0) endText.SetActive(true); //show end message        
    }

    private void OnGUI()
    {
        int screenWidth = Screen.width;

        _guiStyle.fontSize = 50;
        _guiStyle.normal.textColor = Color.white;

        GUIContent uIContent = new GUIContent() { text = "Cubes left: " + _cubesCount };
        Vector3 size = _guiStyle.CalcSize(uIContent);
        
        GUI.Label(new Rect((screenWidth - size.x) -15, 10, 350, 50), "Cubes left: " + _cubesCount, _guiStyle);
    }
}
