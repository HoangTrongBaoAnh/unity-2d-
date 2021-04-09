using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{

    bool hasexploded = false;

    public float radius = 5f;
    public float force = 600f;

    public GameObject explosion_effect;
    GameObject target;

    public int damage = 60;

    private Collider2D[] colliders;

    void start()
    {
        
        
    }

    public void Update()
    {
        
        
        Destroy(gameObject,3f);
    }

    void OnCollisionEnter2D(Collision2D _colinfo)
    {
        //_colinfo.collider.
        /*
        if (_colinfo.collider.gameObject.tag == "enemy")
        {
            _colinfo.collider.gameObject.GetComponent<Enemy>().damageEnemy(damage);
        }
        */
        //Debug.Log(_colinfo.contacts[0].point.ToString());
        explode(_colinfo.contacts[0].point);
        
    }

    public void explode(Vector3 explosionpoint)
    {
        
        GameObject d = Instantiate(explosion_effect,transform.position,transform.rotation) ;
        
        colliders = Physics2D.OverlapCircleAll(transform.position, radius);
        foreach (Collider2D nearbyobject in colliders)
        {
            Debug.Log("We hit " + nearbyobject.gameObject.name + " and did " + damage + " damage.");
            Rigidbody2D rb = nearbyobject.gameObject.GetComponent<Rigidbody2D>();
            Enemy enemy = nearbyobject.gameObject.GetComponent<Enemy>();
            if (rb != null && enemy != null)
            {
                enemy.damageEnemy(damage);
                rb.AddForce(transform.up * force);
            }
        }
        
        Destroy(d, 1f);
        Destroy(gameObject);
        //Debug.Log("BOMM!!!!!!!");
        
    }

}
