using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject MainMenuObject;
    public static bool FirstTime = true; //I declared this field as static because I don't want this to pop up every time when the player clicks the Next Level button.

    private void Start()
    {
        if (FirstTime)
        {
            FirstTime = false;
            MainMenuObject.SetActive(true);
        }
    }
    public void StartEvent()
    {
        GameManager.Spawn.SpawnSaviorObject();
        MainMenuObject.SetActive(false);
    }
}
