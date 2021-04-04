using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shakecamera : MonoBehaviour
{
    public Camera mainCam;

    public float shakeAmt = 0;

    void awake()
    {
        if (mainCam == null)
        {
            mainCam = Camera.main;
        }
    }

    public void shake(float amt , float length)
    {
        shakeAmt = amt;
        InvokeRepeating("doShake",0,0.01f);
        Invoke("stopShake", length);
    }
    void doShake()
    {
        if (shakeAmt > 0)
        {
            Vector3 camPos = mainCam.transform.position;

            float offsetX = Random.value * shakeAmt * 2 - shakeAmt;
            float offsetY = Random.value * shakeAmt * 2 - shakeAmt;

            camPos.x += offsetX;
            camPos.y += offsetY;

            mainCam.transform.position = camPos;
        }
    }

    void stopShake()
    {
        CancelInvoke("doShake");
        mainCam.transform.localPosition = Vector3.zero;
    }
    // Start is called before the first frame update
    

    // Update is called once per frame
}
