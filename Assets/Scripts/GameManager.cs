using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class GameManager : MonoBehaviour
{
    public GameObject mountainPrefab;
    private PlayerController playerControllerScript;
    private GameObject player;
    private float mountainSpawnRate = 1.0f;
    private bool gameOver;
    private bool spawning;
    private float xRange = 4;
    private float yRange = 2;
    private Vector3 takeoffPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerControllerScript = player.GetComponent<PlayerController>();
        takeoffPosition = player.transform.position;

        StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerControllerScript.state == PlayerController.State.FLYING && !spawning)
        {
            StartCoroutine(SpawnMountain());
            spawning = true;
            takeoffPosition = player.transform.position;
        } else if (playerControllerScript.state == PlayerController.State.GROUNDED && spawning)
        {
            spawning = false;
        }
    }

    private Vector3 GetSpawnPosition()
    {
        Vector3 spawnPosition = new Vector3(takeoffPosition.x + Random.Range(-xRange, xRange), takeoffPosition.y + Random.Range(-yRange, yRange), takeoffPosition.z) + player.transform.rotation * Vector3.forward * 4;
        return spawnPosition;
    }

    IEnumerator SpawnMountain()
    {
        Debug.Log(playerControllerScript.state == PlayerController.State.FLYING && !gameOver);
        while (playerControllerScript.state == PlayerController.State.FLYING && !gameOver) // player is flying and game not over
        {
            yield return new WaitForSeconds(mountainSpawnRate);
            //int targetIndex = Random.Range(0, targets.Count);
            //Instantiate(mountainPrefab);
            Vector3 playerOrientation = player.transform.rotation.eulerAngles;
            Instantiate(mountainPrefab, GetSpawnPosition(), Quaternion.Euler(0, playerOrientation.y, 0));
        }
    }

    public void StartGame()
    {

    }
}
