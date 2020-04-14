using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public void LoadNextLevel(ParticleSystem winParticles, Vector3 pos, float gravity)
    {
        ParticleSystem particles = Instantiate(winParticles, pos, new Quaternion(0, 0, 0, 0), null);
        particles.gravityModifier *= gravity;

        StartCoroutine(Next());
    }


    public void RestartLevel()
    {
        StartCoroutine(Restart());
    }


    IEnumerator Restart()
    {
        yield return new WaitForSeconds(1.2f);
        Application.LoadLevel(Application.loadedLevel);
    }

    IEnumerator Next()
    {
        if ((GameInfo.unlockedLevels < Application.loadedLevel - 1) && (GameInfo.unlockedLevels < 25))
        {
            GameInfo.unlockedLevels = Application.loadedLevel - 1;
        }
        yield return new WaitForSeconds(1.2f);
        Application.LoadLevel(Application.loadedLevel + 1);
    }
}
