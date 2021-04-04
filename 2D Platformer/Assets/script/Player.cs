using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [System.Serializable]
    public class PlayerStats
    {
        public int maxhealth = 100;

        private int _curhealth;

        public int curhealth
        {
            get { return _curhealth; }
            set { _curhealth = Mathf.Clamp(value, 0, maxhealth); }
        }

        public void init()
        {
            curhealth = maxhealth;
        }
    }

    public PlayerStats Stats = new PlayerStats();

    [SerializeField]
    private statusIndicator si;

    public int fallBoundary = -20;

    // Start is called before the first frame update
    void Start()
    {
        Stats.init();
        if(si == null)
        {
            Debug.LogError("Not found stats");
        }
        else
        {
            si.setHelth(Stats.curhealth, Stats.maxhealth);
        }

    }
    

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < fallBoundary)
        {
            damagePlayer(9999);
        }
        si.setHelth(Stats.curhealth, Stats.maxhealth);
    }

    public void damagePlayer(int damage)
    {
        Stats.curhealth -= damage;
        if (Stats.curhealth <= 0)
        {
            GameMaster.killPlayer(this);
        }
    }
}
