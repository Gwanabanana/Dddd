using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicLevel : MonoBehaviour
{
    [SerializeField] GameObject[] musicLevel;
    
    [SerializeField] GameObject[] soundLevel;

    [SerializeField] GameObject[] buttons;

    AudioSource soundSource;

    int activeButton = 0;

    // Start is called before the first frame update
    void Start()
    {
        soundSource = GetComponent<AudioSource>();
        SetButton(activeButton);

        SetMusicLevel(GameInfo.musicLevel);
        SetSoundLevel(GameInfo.soundLevel);
    }


    // Update is called once per frame
    void Update()
    {
        HandleButtons();
    }

    void HandleButtons()
    {
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            soundSource.Play();
            if (activeButton + 1 < buttons.Length)
            {
                SetButton(activeButton + 1);
            }
            else
            {
                SetButton(0);
            }
        }

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            soundSource.Play();

            if (activeButton > 0)
            {
                SetButton(activeButton - 1);
            }
            else
            {
                SetButton(buttons.Length - 1);
            }
        }

        if (activeButton == 0)
        {
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                soundSource.Play();

                SetMusicLevel(GameInfo.musicLevel - 1);
            }
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                soundSource.Play();

                SetMusicLevel(GameInfo.musicLevel + 1);
            }
        }else if (activeButton == 1)
        {
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                soundSource.Play();

                SetSoundLevel(GameInfo.soundLevel - 1);
            }
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                soundSource.Play();

                SetSoundLevel(GameInfo.soundLevel + 1);
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.LoadLevel(0);
        }
    }

    void SetButton(int button)
    {
        buttons[activeButton].GetComponent<SpriteRenderer>().color = Color.white;
        activeButton = button;
        buttons[activeButton].GetComponent<SpriteRenderer>().color = Color.green;
    }

    void SetMusicLevel(int level)
    {
        if (level >= 0 && level <= 10)
        {
            GameInfo.musicLevel = level;

            for (int i = 0; i < 10; i++)
            {
                musicLevel[i].GetComponent<SpriteRenderer>().color = Color.white;
            }

            for (int i = 0; i < GameInfo.musicLevel; i++)
            {
                musicLevel[i].GetComponent<SpriteRenderer>().color = Color.green;
            }
        }
    }    
    
    void SetSoundLevel(int level)
    {
        if (level >= 0 && level <= 10)
        {
            GameInfo.soundLevel = level;

            for (int i = 0; i < 10; i++)
            {
                soundLevel[i].GetComponent<SpriteRenderer>().color = Color.white;
            }

            for (int i = 0; i < GameInfo.soundLevel; i++)
            {
                soundLevel[i].GetComponent<SpriteRenderer>().color = Color.green;
            }

            soundSource.volume = GameInfo.soundLevel / 10f;
        }
    }

}
