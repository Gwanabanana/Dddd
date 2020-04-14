using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CursorMenu : MonoBehaviour
{
    [SerializeField] GameObject[] buttons;

    int activeButton = 0;
    bool isContinue = false;
    AudioSource soundSource;

    // Start is called before the first frame update
    void Start()
    {
        soundSource = GetComponent<AudioSource>();

        SetButton(activeButton);
        if(GameInfo.unlockedLevels > 1)
        {
            buttons[0].GetComponentInChildren<TextMeshPro>().text = "Continue";
            isContinue = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        HandleButtons();
    }

    void HandleButtons()
    {
        if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
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

        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
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

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            soundSource.Play();

            UseButton(activeButton);
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
        if(button == 0)
        {
            if (!isContinue)
            {
                Application.LoadLevel(3);
            }
            else
            {
                Application.LoadLevel(GameInfo.unlockedLevels + 2);
            }
        }
        else if(button == 1)
        {
            Application.LoadLevel(1);
        }
        else if(button == 2)
        {
            Application.LoadLevel(2);
        }
        else if(button == 3)
        {
            Application.Quit();
        }
    }

}
