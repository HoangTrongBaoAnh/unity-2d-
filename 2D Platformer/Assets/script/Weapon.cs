using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float firerate = 0;
    public int damage = 10;
    public LayerMask whatToHit;

    public Transform bulletTrailPrefab;
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
        if(firerate == 0)
        {
            if (Input.GetButtonDown ("Fire1"))
            {
                Shoot();
            }
        }
        else
        {
            if (Input.GetButton("Fire1") && Time.time > timetofire)
            {

                timetofire = Time.time + 1/firerate;
                Shoot();
            }
        }
    }

    void Shoot()
    {
        //firePoint = transform.Find("firePoint");
        Vector2 mousePosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        Vector2 firePointPosition = new Vector2(firePoint.position.x, firePoint.position.y);
        RaycastHit2D hit = Physics2D.Raycast(firePointPosition, mousePosition - firePointPosition, 100, whatToHit);

        Debug.DrawLine(firePointPosition, (mousePosition - firePointPosition) * 100, Color.cyan);
        if (hit.collider != null)
        {
            Debug.DrawLine(firePointPosition, hit.point, Color.red);
            Enemy enemy = hit.collider.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.damageEnemy(damage);
                Debug.Log("We hit " + hit.collider.name + " and did " + damage + " damage.");
            }
        }

        if (Time.time >= timeToSpawnEffect)
        {
            Vector3 hitpos ;
            Vector3 hitnormal ;

            if (hit.collider == null)
            {
                hitpos = (mousePosition - firePointPosition) * 30;
                hitnormal = new Vector3(9999,9999,9999);
            }
            else
            {
                hitpos = hit.point;
                hitnormal = hitpos;
            }
            bullet_effect(hitpos,hitnormal);
            timeToSpawnEffect = Time.time + 1 / effectSpawnRate;
            
        }
    }


    void bullet_effect(Vector3 hitpos, Vector3 hitnormal)
    {
        //firePoint = transform.Find("firePoint");
        Transform trail = Instantiate(bulletTrailPrefab,firePoint.position,firePoint.rotation) as Transform;

        LineRenderer lr = trail.GetComponent<LineRenderer>();

        if (lr != null)
        {
            lr.SetPosition(0, firePoint.position);
            lr.SetPosition(1,hitpos);
        }

        if (hitnormal != new Vector3(9999,9999,9999))
        {
            Transform hit = Instantiate(hiteffect,hitpos,Quaternion.FromToRotation(Vector3.forward,hitnormal)) as Transform;
            Destroy(hit.gameObject, 1f);
        }

        Destroy(trail.gameObject, 0.04f);
        

        Transform clone = Instantiate(muzzleFlashPrefab, firePoint.position, firePoint.rotation) as Transform;
        clone.parent = firePoint;
        float size = Random.Range(0.6f,0.9f);
        clone.localScale = new Vector3(size, size, size);
        Destroy(clone.gameObject, 0.02f);
        camShake.shake(camShakeAmt, camShakeLength);
    }

}
