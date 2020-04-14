using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuLevel : MonoBehaviour
{

    [SerializeField] GameObject[] buttons;

    AudioSource soundSource;

    int activeButton = 0;

    // Start is called before the first frame update
    void Start()
    {
        soundSource = GetComponent<AudioSource>();
        SetButton(activeButton);
        HandleLevels();
    }

    // Update is called once per frame
    void Update()
    {
        HandleButtons();
    }

    void HandleLevels()
    {
        for (int i = 0; i < 25; i++)
        {
            buttons[i].GetComponentInChildren<TextMeshPro>().color = Color.red;
        }

        for (int i = 0; i < GameInfo.unlockedLevels; i++)
        {
            buttons[i].GetComponentInChildren<TextMeshPro>().color = Color.white;
        }
    }

    void HandleButtons()
    {
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
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

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            soundSource.Play();

            if (activeButton > 0)
            {
                SetButton(activeButton - 1);
            }
            else
            {
                SetButton(buttons.Length-1);
            }
        }
        
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            soundSource.Play();

            if (activeButton + 5 < buttons.Length)
            {
                SetButton(activeButton + 5);
            }
            else
            {
                SetButton(activeButton - 20);
            }
        }

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            soundSource.Play();

            if (activeButton - 4 > 0)
            {
                SetButton(activeButton - 5);
            }
            else
            {
                SetButton(activeButton + 20);
            }
        }

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            soundSource.Play();

            UseButton(activeButton);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            soundSource.Play();

            Application.LoadLevel(0);
        }
    }

    void SetButton(int button)
    {
        buttons[activeButton].GetComponent<SpriteRenderer>().color = Color.white;
        activeButton = button;
        buttons[activeButton].GetComponent<SpriteRenderer>().color = Color.green;
    }

    void UseButton(int button)
    {
        if (GameInfo.unlockedLevels > button)
        {
            Application.LoadLevel(activeButton + 3);
        }
    }
}
