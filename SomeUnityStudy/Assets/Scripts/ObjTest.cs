using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ObjTest : MonoBehaviour {

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    // Use this for initialization
    private void OnGUI()
    {
        if (GUILayout.Button("载入新场景",GUILayout.Width(200),GUILayout.Height(200))) {
            
            SceneManager.LoadScene("gui");
        }
    }
}
