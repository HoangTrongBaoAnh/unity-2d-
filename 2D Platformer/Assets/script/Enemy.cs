using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [System.Serializable]
    public class EnemyStats
    {
        public int maxHealth = 100;

        private int _curHelth;

        public int damage = 20;

        public int curHelth
        {
            get { return _curHelth; }
            set { _curHelth = Mathf.Clamp(value, 0, maxHealth); }
        }

        public void init()
        {
            curHelth = maxHealth;
        }

    }

    public float shakeamount = 0.1f;
    public float shakelength = 0.1f;

    public EnemyStats stats = new EnemyStats();

    public Transform deathParticle;

    public Transform halfHealth;
    public Transform lowHealth;

    private bool isCreatedHH = false;

    private GameObject[] CardClones = new GameObject[1];

    [Header("optional: ")]
    [SerializeField]
    private statusIndicator si;

    // Start is called before the first frame update
    void Start()
    {
        stats.init();

        if (si == null)
        {
            si.setHelth(stats.curHelth,stats.maxHealth);
        }
    }

    public void damageEnemy(int damage)
    {
        
        stats.curHelth -= damage;
        if (stats.curHelth <= 0)
        {
            GameMaster.killEnemy(this);
        }

        if (si != null)
        {
            si.setHelth(stats.curHelth, stats.maxHealth);
        }
    }

    void OnCollisionEnter2D(Collision2D _colinfo)
    {
        Player pl = _colinfo.collider.GetComponent<Player>();
        if(pl != null)
        {
            Debug.Log("damageplayer");
            pl.damagePlayer(stats.damage);
            damageEnemy(9999);
        }
    }
    

    // Update is called once per frame
    void Update()
    {
        
        if (stats.curHelth <= stats.maxHealth * 0.25)
        {
            //Debug.Log("low health");
            if (isCreatedHH)
            {
                Transform hh = Instantiate(lowHealth, transform.position, Quaternion.identity) as Transform;
                hh.transform.parent = transform;
                //Destroy(CardClones[0]);
                isCreatedHH = false;
            }
        }
        else if(stats.curHelth <= stats.maxHealth * 0.5)
        {
            
            if (!isCreatedHH)
            {
                Transform hh = Instantiate(halfHealth, transform.position, Quaternion.Euler(0, 90, 90)) as Transform;
                CardClones[0] = hh.gameObject;
                hh.transform.parent = transform;

                isCreatedHH = true;

                //Destroy(hh.gameObject, 1f);
            }
        }

    }
}
