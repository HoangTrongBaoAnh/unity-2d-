using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class LiveCounterUI : MonoBehaviour
{
    // Start is called before the first frame update

    private Text livescounter;
    void Awake()
    {
        livescounter = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        livescounter.text = "LIVES: " + GameMaster._remaininglives.ToString();
    }
}
