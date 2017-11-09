using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StartScene : SceneBaseCtr
{

    private void OnGUI()
    {
        if (GUILayout.Button("下一关"))
        {
            GlobalParamater.GlScene = EnumScene.LevelOneScene;
            LoadScene(GlobalParamater.GlScene);
        }
    }
}
