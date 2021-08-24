using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI GameOverText, RestartsInText, LivesText;
    private float _time;
    private bool _onlyOnce = true;
    private void Start()
    {
        PlayerPrefs.SetInt("Lives", 3);
    }
    void Update()
    {
        SetLivesText();
        SetRestartInText();
        

        if (PlayerPrefs.GetInt("Lives") < 0 && _onlyOnce) // (base rule of this if statement) && (will make run this if statement only once.)
        {
            _onlyOnce = false;
            StartCoroutine(Restart());
        }
        
    }
    private void SetLivesText()
    {
        if (PlayerPrefs.GetInt("Lives") == 5)
        {
            LivesText.text = "♥♥♥♥♥";
        }
        else if (PlayerPrefs.GetInt("Lives") == 4)
        {
            LivesText.text = "♥♥♥♥";
        }
        else if(PlayerPrefs.GetInt("Lives") == 3)
        {
            LivesText.text = "♥♥♥";
        }
        else if(PlayerPrefs.GetInt("Lives") == 2)
        {
            LivesText.text = "♥♥";
        }
        else if(PlayerPrefs.GetInt("Lives") == 1)
        {
            LivesText.text = "♥";
        }
        else if(PlayerPrefs.GetInt("Lives") == 0)
        {
            LivesText.text = " ";
        }
    }
    private void SetRestartInText()
    {
        if (_time > 0)
        {
            _time -= Time.deltaTime;
            var intTime = (int)_time;
            RestartsInText.text = "The game will be restart in " + intTime.ToString() + " seconds.";
        }
    }

    private IEnumerator Restart()
    {
        GameOverText.gameObject.SetActive(true);
        AudioManager.PlayMusicOrEffect("_gameOver");
        RestartsInText.text = "The game will be restart in 3 seconds.";
        _time = 3f;

        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene("FirstLevel");
    }

    
}
