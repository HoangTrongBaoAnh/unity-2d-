using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocket : MonoBehaviour
{
    public float firerate = 0;
    public int damage = 10;
    public LayerMask whatToHit;

    public Rigidbody2D bulletTrailPrefab;
    public Transform muzzleFlashPrefab;
    public Transform hiteffect;

    float timeToSpawnEffect = 0;
    public float effectSpawnRate = 10;

    public float camShakeAmt = 0.05f;
    public float camShakeLength = 0.1f;
    shakecamera camShake;

    float timetofire = 0;
    public Transform firePoint;

    

    // Start is called before the first frame update
    void Awake()
    {
        //firePoint = transform.Find("firePoint");
        if (firePoint == null)
        {
            Debug.LogError("Pistol not have firepoint ?");
        }


    }

    void Start()
    {
        camShake = GameMaster.gm.GetComponent<shakecamera>();
        if (camShake == null)
            Debug.LogError("No CameraShake script found on GM object.");
    }

    // Update is called once per frame
    void Update()
    {
        if (firerate == 0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }
        else
        {
            if (Input.GetButton("Fire1") && Time.time > timetofire)
            {

                timetofire = Time.time + 1 / firerate;
                Shoot();
            }
        }
    }

    void Shoot()
    {
        Rigidbody2D clone = Instantiate(bulletTrailPrefab,firePoint.position,Quaternion.Euler(firePoint.rotation.x,firePoint.rotation.y,-90));

        clone.velocity = transform.TransformDirection(Vector3.right * 100f);

        Transform clone_ = Instantiate(muzzleFlashPrefab, firePoint.position, firePoint.rotation) as Transform;
        clone_.parent = firePoint;
        float size = Random.Range(0.6f, 0.9f);
        clone_.localScale = new Vector3(size, size, size);
        Destroy(clone_.gameObject, 0.02f);
        camShake.shake(camShakeAmt, camShakeLength);

    }


    
}
