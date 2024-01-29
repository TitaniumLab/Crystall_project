using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.ParticleSystem;

public class SpawnManager : MonoBehaviour
{
    //list of instantiated objects
    public List<GameObject> objectsList;
    //for compare on collision
    private int spawnCounter = 1;
    //ID of next obj to spawn
    private int nextSpawnedObj = 0;
    //shows next instantiated
    public List<GameObject> spawnPointObjs;
    //set position to instantiate
    private Vector3 spawnPos;
    private float xBounds = 3f;
    //spawn cooldown
    private bool canSpawn = true;

    private GameManager gameManager;


    private void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        //load list of previev objects
        foreach (GameObject obj in objectsList)
        {
            spawnPointObjs.Add(Instantiate(obj));
            Collider spawnPointCollider = spawnPointObjs[spawnPointObjs.Count - 1].GetComponent<Collider>();
            spawnPointCollider.enabled = false;
            spawnPointObjs[spawnPointObjs.Count - 1].gameObject.SetActive(false);
        }

    }

    private void Update()
    {
        if (!gameManager.isGameOver)
        {
            SetPosition();
        }
        else
        {
            spawnPointObjs[nextSpawnedObj].SetActive(false);
        }

        if (Input.GetMouseButtonDown(0) &&
            !gameManager.isGameOver &&
            gameManager.isGameStarted &&
            !gameManager.cursorOnUI)
        {
            if (canSpawn)
            {
                SpawnOnSpawnPoint();
            }
        }
    }

    //change spawn position
    private void SetPosition()
    {
        //set grabed object position
        spawnPos = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, 5.5f, 0);
        //stay object in bounds
        if (spawnPos.x > xBounds)
        {
            spawnPos.x = xBounds;
        }
        if (spawnPos.x < -xBounds)
        {
            spawnPos.x = -xBounds;
        }

        //set grabed object position
        foreach (GameObject obj in spawnPointObjs)
        {
            obj.transform.position = spawnPos;
        }
    }

    //spawn droping object 
    private void SpawnOnSpawnPoint()
    {

        SpawnAndCount(spawnPos, nextSpawnedObj, false);
        spawnPointObjs[nextSpawnedObj].SetActive(false);
        nextSpawnedObj = RandomObjId();
        ShowNextObj();
        canSpawn = false;
        StartCoroutine("DelayForSpawn");

    }

    //spawn on dropping and collision
    public void SpawnAndCount(Vector3 pos, int objectId, bool playParticle)
    {
        Target target = Instantiate(objectsList[objectId], pos, objectsList[objectId].transform.rotation).GetComponent<Target>();
        ParticleSystem particleSystem = target.GetComponent<ParticleSystem>();
        if (playParticle)
        {
            particleSystem.Play();
        }
        Debug.Log($"Object ¹{spawnCounter} spawned");
        target.objNumber = spawnCounter;
        spawnCounter++;
        gameManager.AddScore(target.scoreToAdd);
    }

    //next random obj to instantiate
    private int RandomObjId()
    {
        if (gameManager.score < 150)
        {
            return 0;
        }
        else if (gameManager.score >= 150 && gameManager.score < 500)
        {
            return Random.Range(0, 2);
        }
        else
        {
            return Random.Range(0, 3);
        }
    }

    //delay between clicks
    IEnumerator DelayForSpawn()
    {
        yield return new WaitForSeconds(0.3f);
        canSpawn = true;
    }

    //show next object
    public void ShowNextObj()
    {
        spawnPointObjs[nextSpawnedObj].SetActive(true);
    }
}
