using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmRotation : MonoBehaviour
{
    public int rotationOffset = 90;

    // Update is called once per frame
    void Update()
    {
        Vector3 different = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position ;

        different.Normalize();

        float rotaZ = Mathf.Atan2(different.y,different.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f,0f,rotaZ + rotationOffset);
    }
}
