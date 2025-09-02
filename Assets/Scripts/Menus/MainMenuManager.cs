using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private string levelName;

    public void Start()
    {
        Cursor.visible = true;
    }

    public void Play()
    {
        SceneManager.LoadScene(levelName);
    }
    

    public void Exit()
    {
        Debug.Log("Sair do jogo");
        Application.Quit();
    }
}
