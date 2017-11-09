using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneBaseCtr : MonoBehaviour {

    private float flProgress = 0;
    private AsyncOperation asyoper;

    public void LoadScene(EnumScene loadScene) {
        asyoper=SceneManager.LoadSceneAsync(EnumScene.LoadScene.ToString());
        if (asyoper.isDone) {
            GlobalParamater.GlScene=loadScene;
        }

        

    }
}
