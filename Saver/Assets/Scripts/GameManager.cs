using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static SpawnSavior Spawn;
    public static SaviorClass Savior;
    public TextMeshProUGUI Level, Saved;

    private int _baseRescuableCount;

    void Start()
    {
        Spawn = new SpawnSavior(); //I declared _spawn in Start() because I use GameObject.Find() in SpawnSavior() and it's parent classes.
        Savior = new SaviorClass(FindObjectOfType<SaviorObject>());
        _baseRescuableCount = Spawn.RescuableUnits.Count;
        Level.text = "Level: " + PlayerPrefs.GetInt("Level").ToString();
    }

    void Update()
    {
        Savior = new SaviorClass(FindObjectOfType<SaviorObject>());
        Saved.text = "Saved: " + (_baseRescuableCount - Spawn.RescuableUnits.Count).ToString();
    }
}
