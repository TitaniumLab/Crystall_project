using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverCheck : MonoBehaviour
{
    GameManager gameManager;
    //will count time before loss
    [SerializeField] private float timeCount = 0f;

    private void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    private void FixedUpdate()
    {
        //check how many time passed
        if (timeCount > 1.5f)
        {
            gameManager.isGameOver = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //adding time to counter
        if (other.gameObject.CompareTag("Crystal"))
        {
            timeCount += Time.deltaTime;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        timeCount = 0;
    }
}
