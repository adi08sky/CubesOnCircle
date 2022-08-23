using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    private GUIStyle _guiStyle = new GUIStyle();
    private int _cubesCount;
    [SerializeField]
    private GameObject endText;

    void Start()
    {
        endText.SetActive(false);
    }

    void Update()
    {
        _cubesCount = PlayerPrefs.GetInt("cubesCount");

        if (_cubesCount <= 0) endText.SetActive(true); //show end message        
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
