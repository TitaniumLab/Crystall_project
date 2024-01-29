using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private SpawnManager spawnManager;
    public List<Target> contactedObj;

    //prevents creating 2-3 objects from 3 objects
    public int objNumber;

    [SerializeField] private int objID;
    public int scoreToAdd;



    private void Start()
    {
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
    }

    private void OnCollisionStay(Collision collision)
    {
        if (gameObject.name == collision.gameObject.name &&
            spawnManager.objectsList[spawnManager.objectsList.Count - 1].name + "(Clone)" != gameObject.name)
        {
            contactedObj.Add(collision.gameObject.GetComponent<Target>());
            if (contactedObj.Count == 2 &&
                contactedObj[0].objNumber != contactedObj[1].objNumber)
            {
                if (contactedObj[0].objNumber < contactedObj[1].objNumber)
                {
                    Vector3 midPos = (contactedObj[0].gameObject.transform.position + gameObject.transform.position) / 2;
                    spawnManager.SpawnAndCount(midPos, objID + 1, true);
                    Destroy(contactedObj[0].gameObject);
                }
                if (contactedObj[0].objNumber > contactedObj[1].objNumber)
                {
                    Vector3 midPos = (contactedObj[1].gameObject.transform.position + gameObject.transform.position) / 2;
                    spawnManager.SpawnAndCount(midPos, objID + 1, true);
                    Destroy(contactedObj[1].gameObject);
                }
                Destroy(gameObject);

            }
        }
    }

    private void Update()
    {
        if (contactedObj.Count != 0 &&
            contactedObj[0] != null &&
            contactedObj[0].objNumber > objNumber)
        {
            Vector3 midPos = (contactedObj[0].gameObject.transform.position + gameObject.transform.position) / 2;
            spawnManager.SpawnAndCount(midPos, objID + 1, true);
            Destroy(contactedObj[0].gameObject);
            Destroy(gameObject);
        }
        contactedObj.Clear();
    }
}

