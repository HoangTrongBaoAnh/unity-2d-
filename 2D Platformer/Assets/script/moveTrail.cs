using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveTrail : MonoBehaviour
{
    public int movespeed = 230;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * movespeed);
        Destroy(gameObject,1);
    }
}
