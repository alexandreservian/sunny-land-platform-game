using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public string scene;
    public LevelLoader levelLoader;

    public void StartGame() => levelLoader.TransitionScene(scene);
    public void QuitGame() => Application.Quit();
}