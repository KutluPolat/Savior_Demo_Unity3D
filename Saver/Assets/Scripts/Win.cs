using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour
{
    public ParticleSystem SphereConfetti1, SphereConfetti2, SphereConfetti3, ConeConfetti1, ConeConfetti2;
    public GameObject WinObject, NextLevelButton, Confettis;
    private bool _win = true;
    

    void Update()
    {
        if (_win && GameManager.Spawn.RescuableUnits.Count == 0)
        {
            _win = false;
            Confettis.SetActive(true);
            StartCoroutine(Congratulations());
            StartCoroutine(Confetti());
        }
    }

    private IEnumerator Congratulations()
    {
        //@ Confetti animation
        WinObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        NextLevelButton.SetActive(true);
    }

    private IEnumerator Confetti()
    {
        ConeConfetti1.Play();
        ConeConfetti2.Play();
        yield return new WaitForSeconds(0.3f);
        SphereConfetti1.Play();
        yield return new WaitForSeconds(0.1f);
        SphereConfetti2.Play();
        yield return new WaitForSeconds(0.1f);
        SphereConfetti3.Play();
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(Confetti());
    }

    public void NextLevel()
    {
        PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
        SceneManager.LoadScene("FirstLevel");
    }
}
