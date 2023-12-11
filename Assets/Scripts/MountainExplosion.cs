using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountainExplosion : MonoBehaviour
{
    private GameObject player;
    private PlayerController playerControllerScript;
    private GameManager gameManagerScript;
    private bool onMountain;
    private static readonly int GROUND_TIME = 5;
    private int timeLeft = GROUND_TIME; // 5 seconds
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerControllerScript = player.GetComponent<PlayerController>();
        gameManagerScript = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && playerControllerScript.state == PlayerController.State.GROUNDED) // The player landed on the mountain
        {
            onMountain = true;
        }

        StartCoroutine(CountDown());
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            onMountain = false;
            timeLeft = GROUND_TIME;
        }
    }

    IEnumerator CountDown()
    {
        while (timeLeft > 0 && onMountain && !gameManagerScript.gameOver)
        {
            yield return new WaitForSeconds(1.0f);
            timeLeft--;
            //timerText.text = "Time: " + timeLeft;
            Debug.Log(timeLeft);
        }

        if (onMountain)
        {
            Debug.Log("Explosion, You're Dead");
        }
    }
}
