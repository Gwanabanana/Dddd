using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] GameObject playerJumpAudio;
    AudioSource playerJumpSound;
    
    [SerializeField] GameObject playerLandingAudio;
    AudioSource playerLandingSound;

    [SerializeField] GameObject playerDeathAudio;
    [HideInInspector] AudioSource playerDeathSound;
    
    [SerializeField] GameObject playerDeathPunchAudio;
    [HideInInspector] AudioSource playerDeathPunchSound;

    [SerializeField] GameObject enemySonarAudio;
    [HideInInspector] AudioSource enemySonarSound;

    [SerializeField] GameObject levelWinAudio;
    [HideInInspector] AudioSource levelWinSound;

    [SerializeField] GameObject blockPickupAudio;
    [HideInInspector] AudioSource blockPickupSound;
    
    [SerializeField] GameObject blockStickAudio;
    [HideInInspector] AudioSource blockStickSound;

    [SerializeField] GameObject blockGravityUpAudio;
    [HideInInspector] AudioSource blockGravityUpSound;

    [SerializeField] GameObject blockGravityDownAudio;
    [HideInInspector] AudioSource blockGravityDownSound;

    GameInfo info;

    private void Start()
    {
        info = FindObjectOfType<GameInfo>();

        playerJumpSound = playerJumpAudio.GetComponent<AudioSource>();
        playerLandingSound = playerLandingAudio.GetComponent<AudioSource>();
        playerDeathSound = playerDeathAudio.GetComponent<AudioSource>();
        playerDeathPunchSound = playerDeathPunchAudio.GetComponent<AudioSource>();

        enemySonarSound = enemySonarAudio.GetComponent<AudioSource>();

        levelWinSound = levelWinAudio.GetComponent<AudioSource>();

        blockPickupSound = blockPickupAudio.GetComponent<AudioSource>();
        blockStickSound = blockStickAudio.GetComponent<AudioSource>();
        blockGravityUpSound = blockGravityUpAudio.GetComponent<AudioSource>();
        blockGravityDownSound = blockGravityDownAudio.GetComponent<AudioSource>();
    }

    private void Update()
    {
        playerJumpSound.volume = 0.6f * GameInfo.soundLevel / 10f;
        playerLandingSound.volume = 0.6f * GameInfo.soundLevel / 10f;
        playerDeathSound.volume = GameInfo.soundLevel / 10f;
        playerDeathPunchSound.volume = 0.6f * GameInfo.soundLevel / 10f;
        enemySonarSound.volume = 0.6f * GameInfo.soundLevel / 10f;
        levelWinSound.volume = 0.2f * GameInfo.soundLevel / 10f;
        blockPickupSound.volume = 0.6f * GameInfo.soundLevel / 10f;
        blockStickSound.volume = 0.6f * GameInfo.soundLevel / 10f;
        blockGravityUpSound.volume = 0.6f * GameInfo.soundLevel / 10f;
        blockGravityDownSound.volume = 0.6f * GameInfo.soundLevel / 10f;
    }

    public void PlayPlayerJumpSound()
    {
        playerJumpSound.Play();
    }    
    public void PlayPlayerLandingSound()
    {
        playerLandingSound.Play();
    }    
    public void PlayPlayerDeathSound()
    {
        playerDeathSound.Play();
        playerDeathPunchSound.Play();
    }
    
    public void PlayEnemySonarSound()
    {
        enemySonarSound.Play();
    }    
    public void PlayLevelWinSound()
    {
        levelWinSound.Play();
    }    
    public void PlayBlockPickupSound()
    {
        blockPickupSound.Play();
    }
    public void PlayBlockStickSound()
    {
        blockStickSound.Play();
    }
    public void PlayBlockGravityUpSound()
    {
        blockGravityUpSound.Play();
    }
    public void PlayBlockGravityDownSound()
    {
        blockGravityDownSound.Play();
    }
}
