using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleUIManager : MonoBehaviour
{


    public void LoadInGameScene()
    {
        SceneManager.LoadScene("inGameScene");
    }
}
