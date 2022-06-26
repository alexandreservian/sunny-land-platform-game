using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private string scene;

    public void StartGame() => SceneManager.LoadScene(scene);
    public void QuitGame() => Application.Quit();
}
