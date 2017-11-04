using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StartScene : MonoBehaviour {

    private void OnGUI()
    {
        if (GUILayout.Button("下一关"))
        {
            SceneManager.LoadScene("");
        }
    }
}
