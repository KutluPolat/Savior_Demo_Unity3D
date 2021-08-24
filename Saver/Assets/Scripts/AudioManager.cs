using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioClip _win, _gameOver, _saved, _punched, _stickedToRescuable, _stickedToNotRescuable;
    private static AudioSource _audioSource;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();

        _win = Resources.Load<AudioClip>("SFX/Win");
        _gameOver = Resources.Load<AudioClip>("SFX/GameOver");
        _saved = Resources.Load<AudioClip>("SFX/Saved");
        _punched = Resources.Load<AudioClip>("SFX/Punched");
        _stickedToRescuable = Resources.Load<AudioClip>("SFX/StickedToRescuable");
        _stickedToNotRescuable = Resources.Load<AudioClip>("SFX/StickedToNotRescuable");
    }

    public static void PlayMusicOrEffect(string value)
    {
        switch (value)
        {
            case "_win":
                _audioSource.PlayOneShot(_win);
                break;
            case "_gameOver":
                _audioSource.PlayOneShot(_gameOver);
                break;
            case "_saved":
                _audioSource.PlayOneShot(_saved);
                break;
            case "_punched":
                _audioSource.PlayOneShot(_punched);
                break;
            case "_stickedToRescuable":
                _audioSource.PlayOneShot(_stickedToRescuable);
                break;
            case "_stickedToNotRescuable":
                _audioSource.PlayOneShot(_stickedToNotRescuable);
                break;
        }
    }
}
