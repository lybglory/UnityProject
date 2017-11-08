using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AsyncLoad : MonoBehaviour {
    /// <summary>
    /// Slider句柄
    /// </summary>
    public Slider loadSlider;
    /// <summary>
    /// 间隔时间
    /// </summary>
    private float flInterval = 10;
    /// <summary>
    /// 异步加载操作
    /// </summary>
    //AsyncOperation asyncOperaNext;
    /// <summary>
    /// 进度值
    /// </summary>
    private float flProgress;
    private void Start()
    {
        StartCoroutine("LoadNextScene");
    }

    private void Update()
    {
        flInterval-= Time.deltaTime;
        
        loadSlider.value += Time.deltaTime;
    }

    IEnumerator LoadNextScene() {

        AsyncOperation asyncOperaNext = SceneManager.LoadSceneAsync("LevelOneScene");
            flProgress = asyncOperaNext.progress;
            yield return asyncOperaNext;
         
        //Debug.Log("异步加载进度值：=" + flProgress);
        
        

    }
}
