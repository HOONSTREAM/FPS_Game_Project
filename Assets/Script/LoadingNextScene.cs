using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LoadingNextScene : MonoBehaviour
{
    public int sceneNumber = 2;
    public Slider loadingbar;
    public Text loadingText;


    private void Start()
    {
        StartCoroutine(TransitionNextScene(sceneNumber));
    }

    IEnumerator TransitionNextScene(int number)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(number);

        ao.allowSceneActivation = false;

        while(!ao.isDone)
        {
            loadingbar.value = ao.progress;
            loadingText.text = (ao.progress * 100f).ToString() + "%"; 

            if(ao.progress >= 0.9f)
            {
                ao.allowSceneActivation = true;
            }

            yield return null; // 다음 프레임이 될 때 까지 기다린다.
        }

    }
}
