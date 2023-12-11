using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class GameManager : MonoBehaviour
{
    public List<GameObject> mountainPrefabs;
    private PlayerController playerControllerScript;
    private GameObject player;
    private float mountainSpawnRate = 2.0f;
    public bool gameOver;
    private bool spawning;
    private float xRange = 20;
    private float yRange = 20;
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
        Vector3 spawnPosition = new Vector3(takeoffPosition.x + Random.Range(-xRange, xRange), takeoffPosition.y + Random.Range(-yRange, yRange), takeoffPosition.z) + player.transform.rotation * Vector3.forward * 10;
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
            int index = Random.Range(0, mountainPrefabs.Count);
            Instantiate(mountainPrefabs[index], GetSpawnPosition(), Quaternion.Euler(0, playerOrientation.y, 0));
        }
    }

    public void StartGame()
    {

    }
}
