using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class WaveUI : MonoBehaviour
{
    [SerializeField]
    wavespawner WaveSpawner;

    [SerializeField]
    Animator WaveAnimator;

    [SerializeField]
    Text WaveCountDownText;

    [SerializeField]
    Text WaveCountText;

    private wavespawner.spawnstate previousState;


    // Start is called before the first frame update
    void Start()
    {
        if (WaveSpawner == null)
        {
            Debug.LogError("Not found WaveSpawner");
        }
        if (WaveAnimator == null)
        {
            Debug.LogError("Not found WaveAnimator");
        }
        if (WaveCountDownText == null)
        {
            Debug.LogError("Not found WaveCountDownText");
        }
        if (WaveCountText == null)
        {
            Debug.LogError("Not found WaveCountText");

        }
    }

    // Update is called once per frame
    void Update()
    {
        switch(WaveSpawner.St)
        {
            case wavespawner.spawnstate.COUNTING:
                updateCountDown();
                break;
            case wavespawner.spawnstate.SPAWNING:
                updateSpawning();
                break;
        }

        previousState = WaveSpawner.St;
    }

    void updateCountDown()
    {
        if (previousState != wavespawner.spawnstate.COUNTING)
        {
            WaveAnimator.SetBool("WaveCountDown",true);
            WaveAnimator.SetBool("WaveInComming", false);
            //Debug.Log("Countdown");
        }
        WaveCountDownText.text = ((int)(WaveSpawner.wavecountdown)).ToString();
    }

    void updateSpawning()
    {
        if (previousState != wavespawner.spawnstate.SPAWNING)
        {
            WaveAnimator.SetBool("WaveCountDown", false);
            WaveAnimator.SetBool("WaveInComming", true);
            WaveCountText.text = (WaveSpawner.Nextwave+1).ToString();
            //Debug.Log("Spawning");
        }
        
    }
}
