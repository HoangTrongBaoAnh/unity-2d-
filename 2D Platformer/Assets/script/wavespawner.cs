using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wavespawner : MonoBehaviour
{
    public enum spawnstate {SPAWNING,WAITING,COUNTING };

    [System.Serializable]
    public class wave
    {
        public string name;
        public Transform enemy;
        public int count;
        public float rate;

    }

    public wave[] Wave;
    private int nextwave = 0;
    public int Nextwave
    {
        get { return nextwave; }
    }

    public float timeBetweenWave = 5f;
    public float wavecountdown;
    public float Wavecountdown
    {
        get { return wavecountdown; }
    }

    private spawnstate st = spawnstate.COUNTING;
    public spawnstate St
    {
        get { return st; }
    }

    private float searchcount = 5f;

    public Transform[] spawnpoint ;

    // Start is called before the first frame update
    void Start()
    {
        if (spawnpoint.Length == 0)
        {
            Debug.LogError("length == 0");
        }
        wavecountdown = timeBetweenWave;
    }

    // Update is called once per frame
    void Update()
    {
        if (st == spawnstate.WAITING)
        {
            if (!EnemyIsAlive())
            {
                waveCompleted();
            }
            else
            {
                return ;
            }
            
        }

        if(wavecountdown <= 0)
        {
            if (st != spawnstate.SPAWNING)
            {
                StartCoroutine(spawnwave(Wave[nextwave]));
            }
        }
        else
        {
            wavecountdown -= Time.deltaTime;
        }
    }

    public void waveCompleted()
    {
        Debug.Log("there is no enemy left");
        wavecountdown = timeBetweenWave;
        st = spawnstate.COUNTING;
        if (nextwave + 1 > Wave.Length - 1)
        {
            Debug.Log("Wave Completed!! Looping...");
            nextwave = 0;
        }
        else
        {
            nextwave++;
        }
    }

    bool EnemyIsAlive()
    {
        searchcount -= Time.deltaTime;
        if (searchcount <= 0f)
        {
            searchcount = 1f;
            if (GameObject.FindGameObjectWithTag("enemy") == null)
            {
                return false;
            }
        }

        return true;
    }

    IEnumerator spawnwave(wave _wave)
    {
        Debug.Log("wave " + _wave.name);
        st = spawnstate.SPAWNING;

        for(int i = 0; i < _wave.count; ++i)
        {
            spawnEnemy(_wave.enemy);

            yield return new WaitForSeconds(1f/_wave.rate);
        }

        st = spawnstate.WAITING;


        yield break;
    }

    void spawnEnemy(Transform enemy)
    {
        Transform sp = spawnpoint[Random.Range(0,spawnpoint.Length)];
        Vector3 x = new Vector3(sp.position.x, sp.position.y, 14);
        Instantiate(enemy, x  , sp.rotation);
        Debug.Log("spawning Enenmy");
    }
}
