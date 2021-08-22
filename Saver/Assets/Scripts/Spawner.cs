using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Spawner : MonoBehaviour
{
    private SpawnSavior _spawn;
    
    void Start()
    {
        _spawn = new SpawnSavior(); //I declared _spawn in Start() because I use GameObject.Find() in SpawnSavior() and it's parent classes.
    }

    void Update()
    {
        
    }
}
