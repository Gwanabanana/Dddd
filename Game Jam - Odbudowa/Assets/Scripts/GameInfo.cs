using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameInfo : MonoBehaviour
{
    [SerializeField] AudioClip mainSound;
    [HideInInspector] static public int musicLevel = 10;
    [HideInInspector] static public int soundLevel = 10;
    AudioSource audio;

    [HideInInspector] static public int unlockedLevels = 1;

    private void Awake()
    {
        int numberOfLevelManagers = FindObjectsOfType<GameInfo>().Length;
        if(numberOfLevelManagers > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        audio = GetComponent<AudioSource>();

        StartCoroutine(StartMusic());
    }

    IEnumerator StartMusic()
    {
        AudioSource audio = GetComponent<AudioSource>();

        audio.Play();
        yield return new WaitForSeconds(audio.clip.length * 0.943f);
        audio.clip = mainSound;
        audio.Play();
        audio.loop = true;
    }

    private void Update()
    {
        SetSoundLevel(soundLevel);
    }

    public void SetMusicLevel(int level)
    {
        musicLevel = level;

        audio = GetComponent<AudioSource>();
        audio.volume = 0.4f * musicLevel / 10f;
    }    
    void SetSoundLevel(int level)
    {
        audio.volume = 0.4f * musicLevel / 10f;
    }
}
