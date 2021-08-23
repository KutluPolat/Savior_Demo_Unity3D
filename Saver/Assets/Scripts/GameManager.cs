using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static SpawnSavior Spawn;
    public static SaviorClass Savior;
   

    void Start()
    {
        Spawn = new SpawnSavior(); //I declared _spawn in Start() because I use GameObject.Find() in SpawnSavior() and it's parent classes.
        Savior = new SaviorClass(FindObjectOfType<SaviorObject>());
    }

    void Update()
    {
        Savior = new SaviorClass(FindObjectOfType<SaviorObject>());
        
    }
}
