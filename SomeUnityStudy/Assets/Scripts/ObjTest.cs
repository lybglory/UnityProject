using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ObjTest : MonoBehaviour {

    private void Start()
    {   //加载场景的时候，目标物体不被销毁
        DontDestroyOnLoad(this.gameObject);
    }
    // Use this for initialization
    private void OnGUI()
    {
        if (GUILayout.Button("载入新场景")) {
            
            SceneManager.LoadScene("gui");
        }
    }
}
