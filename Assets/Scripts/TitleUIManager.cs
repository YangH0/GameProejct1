using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleUIManager : MonoBehaviour
{
    public GameObject startButton;
    public GameObject ExitButton;
    public Image cutScene;
    public Sprite[] cutSceneImage;

    private int curCurSceneNum = 0;
    private bool bIsCutScene;

    private void Awake()
    {
        startButton.SetActive(false);
        ExitButton.SetActive(false);
        cutScene.gameObject.SetActive(true);
        bIsCutScene = true;
        curCurSceneNum = 0;
        cutScene.sprite = cutSceneImage[curCurSceneNum];
    }

    private void Update()
    {
        SetCutScene();
    }

    private void SetCutScene()
    {
        if (Input.anyKeyDown && bIsCutScene)
        {
            if(curCurSceneNum+1 >= 4)
            {
                bIsCutScene = false;
                startButton.SetActive(true);
                ExitButton.SetActive(true);
                cutScene.gameObject.SetActive(false);
                return;
            }
            cutScene.sprite = cutSceneImage[++curCurSceneNum];
        }
    }

    public void LoadInGameScene()
    {
        SceneManager.LoadScene("InGameScene");
    }

    public void GameExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
