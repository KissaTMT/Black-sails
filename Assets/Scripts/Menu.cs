using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    private AsyncOperation _loadGame;
    public void Play()
    {
        ScreenFader.instance.Fade();//bad practice
        StartCoroutine(LoadRoutine());
    }
    private IEnumerator LoadRoutine()
    {
        yield return new WaitForSeconds(0.5f);
        _loadGame.allowSceneActivation = true;
    }

    private void Start()
    {
        _loadGame = SceneManager.LoadSceneAsync(1);
        _loadGame.allowSceneActivation = false;
    }
}
