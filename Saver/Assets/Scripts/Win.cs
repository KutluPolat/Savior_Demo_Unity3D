using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour
{
    public GameObject WinObject, NextLevelButton;
    private bool _win = true;

    void Update()
    {
        if (_win && GameManager.Spawn.RescuableUnits.Count == 0)
        {
            _win = false;
            StartCoroutine(Congratulations());
        }
    }

    private IEnumerator Congratulations()
    {
        //@ Konfeti animasyonu baslat.
        WinObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        NextLevelButton.SetActive(true);
    }

    public void NextLevel()
    {
        PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
        SceneManager.LoadScene("FirstLevel");
    }
}
