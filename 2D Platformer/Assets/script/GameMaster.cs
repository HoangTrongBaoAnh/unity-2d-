using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    // Start is called before the first frame update

    
    public static GameMaster gm;
    void Awake()
    {
        if (gm == null)
        {
            gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        }
        
    }

    void start()
    {
        if (shakeCamera == null)
        {
            Debug.LogError("Not found camerashake");
        }

    }


    public Transform playerPrefab;

    public Transform spawnPoint;
    public Transform spawnPrefab;

    public shakecamera shakeCamera;
    public float spawnDelay = 2;
    public IEnumerator respawnPlayer()
    {
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(spawnDelay);
        Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
        Transform clone = Instantiate(spawnPrefab, new Vector3(spawnPoint.position.x, spawnPoint.position.y-1, spawnPoint.position.x), Quaternion.Euler(-90, 90, 0)) as Transform;
        Destroy(clone.gameObject, 3f);
    }
    public static void killPlayer(Player player)
    {
        Destroy(player.gameObject);
        gm.StartCoroutine (gm.respawnPlayer());
    }

    public static void killEnemy(Enemy enemy)
    {
        gm._killenemy(enemy);
    }

    public void _killenemy(Enemy _enemy)
    {
        Transform dp = Instantiate(_enemy.deathParticle,_enemy.transform.position,Quaternion.identity) as Transform;
        Destroy(dp.gameObject,0.5f);
        shakeCamera.shake(_enemy.shakeamount,_enemy.shakelength);
        Destroy(_enemy.gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
