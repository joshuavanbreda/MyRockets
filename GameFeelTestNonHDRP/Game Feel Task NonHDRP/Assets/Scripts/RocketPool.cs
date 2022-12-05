using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketPool : MonoBehaviour
{
    public int PoolSize = 5;
    public GameObject columnPrefab;
    public float spawnRate = 4f;
    public float columnMin = -1f;
    public float columnMax = 3.5f;

    private GameObject[] columns;
    private Vector3 objectPoolPosition = new Vector3(-15f, -25f, 100f);     //A holding position for our unused blocks offscreen.
    private float timeSinceLastSpawned;
    private float spawnXPosition = 211f;                                     //position in the distance to spawn and loop buildings/environment (forward x axis)\
    private float spawnZPosition = 0;                                     //position of the buildings to the side to line up next to the road
    private int currentColumn = 0;

    public GameObject rocket1Prefab;
    public GameObject rocket2Prefab;
    public GameObject rocket3Prefab;

    private GameObject[] rocket1Pool;
    private GameObject[] rocket2Pool;
    private GameObject[] rocket3Pool;

    public GameObject rocketPoolPos1;
    public Vector3 rocketPoolPosVec1;
    public GameObject rocketPoolPos2;
    public Vector3 rocketPoolPosVec2;
    public GameObject rocketPoolPos3;
    public Vector3 rocketPoolPosVec3;

    public GameObject rocketSpawnPos1;
    private Vector3 rocketSpawnPosVec1;
    public GameObject rocketSpawnPos2;
    private Vector3 rocketSpawnPosVec2;
    public GameObject rocketSpawnPos3;
    private Vector3 rocketSpawnPosVec3;

    private float timeSinceLastSpawned1;
    private float timeSinceLastSpawned2;
    private float timeSinceLastSpawned3;

    private int rocket1CurrentPoolIndex = 0;
    private int rocket2CurrentPoolIndex = 0;
    private int rocket3CurrentPoolIndex = 0;

    private bool canShoot1 = false;
    private bool canShoot2 = false;
    private bool canShoot3 = false;

    private bool canPress1 = true;
    private bool canPress2 = true;
    private bool canPress3 = true;

    // Start is called before the first frame update
    void Start()
    {
        rocketPoolPosVec1 = rocketPoolPos1.transform.position;
        rocketPoolPosVec2 = rocketPoolPos2.transform.position;
        rocketPoolPosVec3 = rocketPoolPos3.transform.position;

        rocketSpawnPosVec1 = rocketSpawnPos1.transform.position;
        rocketSpawnPosVec2 = rocketSpawnPos2.transform.position;
        rocketSpawnPosVec3 = rocketSpawnPos3.transform.position;

        rocket1Pool = new GameObject[PoolSize];
        for (int i = 0; i < PoolSize; i++)
        {
            rocket1Pool[i] = (GameObject)Instantiate(rocket1Prefab, rocketPoolPosVec1, rocket1Prefab.transform.rotation);
        }

        rocket2Pool = new GameObject[PoolSize];
        for (int i = 0; i < PoolSize; i++)
        {
            rocket2Pool[i] = (GameObject)Instantiate(rocket2Prefab, rocketPoolPosVec2, rocket2Prefab.transform.rotation);
        }

        rocket3Pool = new GameObject[PoolSize];
        for (int i = 0; i < PoolSize; i++)
        {
            rocket3Pool[i] = (GameObject)Instantiate(rocket3Prefab, rocketPoolPosVec3, rocket3Prefab.transform.rotation);
        }
    }




    // Update is called once per frame
    void Update()
    {
        
        if (canShoot1)
        {
            timeSinceLastSpawned1 += Time.deltaTime;
            if (canShoot1 /*&& timeSinceLastSpawned1 >= spawnRate*/)
            {
                timeSinceLastSpawned1 = 0;
                rocket1Pool[rocket1CurrentPoolIndex].transform.position = rocketSpawnPosVec1;
                rocket1CurrentPoolIndex++;
                if (rocket1CurrentPoolIndex >= PoolSize)
                {
                    rocket1CurrentPoolIndex = 0;
                }
            }
            canShoot1 = false;
        }

        if (canShoot2)
        {
            timeSinceLastSpawned2 += Time.deltaTime;
            if (canShoot2 /*&& timeSinceLastSpawned2 >= spawnRate*/)
            {
                timeSinceLastSpawned2 = 0;
                rocket2Pool[rocket2CurrentPoolIndex].transform.position = rocketSpawnPosVec2;
                rocket2CurrentPoolIndex++;
                if (rocket2CurrentPoolIndex >= PoolSize)
                {
                    rocket2CurrentPoolIndex = 0;
                }
            }
            canShoot2 = false;
        }

        if (canShoot3)
        {
            timeSinceLastSpawned3 += Time.deltaTime;
            if (canShoot3 /*&& timeSinceLastSpawned2 >= spawnRate*/)
            {
                timeSinceLastSpawned3 = 0;
                rocket3Pool[rocket3CurrentPoolIndex].transform.position = rocketSpawnPosVec3;
                rocket3CurrentPoolIndex++;
                if (rocket3CurrentPoolIndex >= PoolSize)
                {
                    rocket3CurrentPoolIndex = 0;
                }
            }
            canShoot3 = false;
        }
    }

    public void CanSpawnRocket1()
    {
        if (canPress1 == true)
        {
            canShoot1 = true;
            rocket1Pool[rocket1CurrentPoolIndex].GetComponent<Projectile>().enabled = true;
            rocket1Pool[rocket1CurrentPoolIndex].GetComponent<Projectile>().waitTime = 2;
            StartCoroutine(CanSpawn1());
        }
    }

    public void CanSpawnRocket2()
    {
        if (canPress2 == true)
        {
            canShoot2 = true;
            rocket2Pool[rocket2CurrentPoolIndex].GetComponent<Projectile>().enabled = true;
            rocket2Pool[rocket1CurrentPoolIndex].GetComponent<Projectile>().waitTime = 2;
            StartCoroutine(CanSpawn2());
        }
    }

    public void CanSpawnRocket3()
    {
        if (canPress3 == true)
        {
            canShoot3 = true;
            rocket3Pool[rocket3CurrentPoolIndex].GetComponent<Projectile>().enabled = true;
            rocket3Pool[rocket1CurrentPoolIndex].GetComponent<Projectile>().waitTime = 2;
            StartCoroutine(CanSpawn3());
        }
    }

    public IEnumerator CanSpawn1()
    {
        canPress1 = false;
        yield return new WaitForSeconds(3f);
        canPress1 = true;
    }

    public IEnumerator CanSpawn2()
    {
        canPress2 = false;
        yield return new WaitForSeconds(3f);
        canPress2 = true;
    }

    public IEnumerator CanSpawn3()
    {
        canPress3 = false;
        yield return new WaitForSeconds(3f);
        canPress3 = true;
    }
}
