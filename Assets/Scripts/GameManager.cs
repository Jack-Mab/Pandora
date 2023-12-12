using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Instance Variables
    // Public Variables
    public static readonly int MAX_HEALTH = 10;
    public int health;
    public int score;
    public bool gameOver;
    public List<GameObject> mountainPrefabs;
    public GameObject flyingMonsterPrefab;
    public GameObject lifePackPrefab;

    // Private Variables
    private PlayerController playerControllerScript;
    private GameObject player;
    private float mountainSpawnRate = 2.0f;
    private float monsterSpawnRate = 4.0f;
    private float lifePackSpawnRate = 6.0f;
    private bool spawning;
    private float xRange = 20;
    private float yRange = 20;
    private float zRange = 50;
    #endregion

    #region Overridden Methods
    void Start()
    {
        player = GameObject.Find("Player");
        playerControllerScript = player.GetComponent<PlayerController>();

        StartGame();
    }

    void Update()
    {
        if (playerControllerScript.state == PlayerController.State.FLYING && !spawning)
        {
            spawning = true;
            StartCoroutine(SpawnMountain());
            StartCoroutine(SpawnMonster());
            StartCoroutine(SpawnLifePack());
        } else if (playerControllerScript.state == PlayerController.State.GROUNDED && spawning)
        {
            spawning = false;
        }
    }
    #endregion

    #region Custom Methods
    public void StartGame()
    {
        gameOver = false;
        score = 0;
        health = MAX_HEALTH;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GameOver()
    {
        gameOver = true;
    }

    public void UpdateScore(int scoreIncrement)
    {
        score += scoreIncrement;
    }
    private Vector3 GetSpawnPosition()
    {
        Vector3 playerPosition = player.transform.position;
        Vector3 spawnPosition = new Vector3(playerPosition.x + Random.Range(-xRange, xRange), playerPosition.y + Random.Range(-yRange, yRange), playerPosition.z) + player.transform.rotation * Vector3.forward * zRange;
        return spawnPosition;
    }

    IEnumerator SpawnMountain()
    {
        while (playerControllerScript.state == PlayerController.State.FLYING && !gameOver) // player is flying and game not over
        {
            yield return new WaitForSeconds(mountainSpawnRate);
            Vector3 playerOrientation = player.transform.rotation.eulerAngles;
            int index = Random.Range(0, mountainPrefabs.Count);
            Instantiate(mountainPrefabs[index], GetSpawnPosition(), Quaternion.Euler(0, playerOrientation.y, 0));
        }
    }

    IEnumerator SpawnMonster()
    {
        while (playerControllerScript.state == PlayerController.State.FLYING && !gameOver) // player is flying and game not over
        {
            yield return new WaitForSeconds(monsterSpawnRate);
            Vector3 playerOrientation = player.transform.rotation.eulerAngles;
            Instantiate(flyingMonsterPrefab, GetSpawnPosition(), Quaternion.Euler(0, playerOrientation.y, 0));
        }
    }

    IEnumerator SpawnLifePack()
    {
        while (playerControllerScript.state == PlayerController.State.FLYING && !gameOver) // player is flying and game not over
        {
            yield return new WaitForSeconds(lifePackSpawnRate);
            Vector3 playerOrientation = player.transform.rotation.eulerAngles;
            Instantiate(lifePackPrefab, GetSpawnPosition(), Quaternion.Euler(0, playerOrientation.y, 0));
        }
    }
    #endregion
}
