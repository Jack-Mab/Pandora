using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountainExplosion : MonoBehaviour
{
    #region Instance Variables
    // Public Variables
    public bool onMountain;

    // Private Variables
    private static readonly int GROUND_TIME = 5;
    private int timeLeft = GROUND_TIME;
    private GameObject player;
    private PlayerController playerControllerScript;
    private GameManager gameManagerScript;
    #endregion

    #region Overridden Methods
    void Start()
    {
        onMountain = false;
        player = GameObject.Find("Player");
        playerControllerScript = player.GetComponent<PlayerController>();
        gameManagerScript = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && playerControllerScript.state == PlayerController.State.GROUNDED) // The player landed on the mountain
        {
            onMountain = true;
        }

        // TODO: Activate Countdown Text
        StartCoroutine(CountDown());
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            onMountain = false;
            timeLeft = GROUND_TIME;

            // TODO: Deactivate Countdown Text
        }
    }
    #endregion

    #region Custom Methods
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
            playerControllerScript.ChangeState(PlayerController.State.DEAD);
        }
    }
    #endregion
}
