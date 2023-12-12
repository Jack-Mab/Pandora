using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // <== TODO: Template ==> //
    #region Enumerations
    // The different states that the player can be in
    public enum State
    {
        DEAD,
        FLYING,
        GROUNDED
    }
    #endregion

    #region Instance Variables
    // Public Variables
    public State state;
    public float speed;
    public float rotationSpeed;
    public float rollingSpeed;
    public float rotationAngle;
    public GameObject focalPoint;

    // Private Variables
    private Rigidbody playerRb;
    private GameManager gameManagerScript;
    #endregion

    #region Method Overrides
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        gameManagerScript = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    void FixedUpdate()
    {

        // Get User input
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Check for game over condition
        if (gameManagerScript.health <= 0)
        {
            ChangeState(State.DEAD);
        }

        if (state == State.DEAD)
        {
            gameManagerScript.GameOver();
        } else if (state == State.FLYING)
        {
            // Change the player's pitch, yaw and roll
            Vector3 currentOrientation = transform.rotation.eulerAngles;
            float pitch = verticalInput * rotationAngle;
            float yaw = currentOrientation.y; // Fix the yaw
            float roll = horizontalInput * rotationAngle;

            Quaternion targetOrientation = Quaternion.Euler(pitch, yaw, -roll);

            // Roll player when moving sideways
            transform.rotation = Quaternion.Slerp(transform.rotation, targetOrientation, rotationSpeed);
            Vector3 newPosition = horizontalInput * rollingSpeed * Time.deltaTime * focalPoint.transform.right;

            playerRb.MovePosition(transform.position + newPosition); // Move player sideways

            // Land player
            if (Input.GetKeyDown(KeyCode.Q))
            {
                ChangeState(State.GROUNDED);
            }
        } else if (state == State.GROUNDED)
        {
            playerRb.AddForce(speed * verticalInput * Time.deltaTime * focalPoint.transform.forward); // Move the player forward
            focalPoint.transform.Rotate(focalPoint.transform.up, rotationSpeed * -horizontalInput); // Rotate camera around player

            // Player takes off
            if (Input.GetKeyDown(KeyCode.Q))
            {
                ChangeState(State.FLYING);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Unobtainium"))
        {
            Destroy(other.gameObject);
            gameManagerScript.score++;
        } else if (other.gameObject.CompareTag("Monster"))
        {
            gameManagerScript.health--;
        } else if (other.gameObject.CompareTag("Life Pack"))
        {
            Destroy(other.gameObject);
            if (gameManagerScript.health < GameManager.MAX_HEALTH)
            {
                gameManagerScript.health++;
            }
        }
    }
    #endregion

    #region Custom Methods
    public void ChangeState(State newState)
    {
        state = newState;
        switch (newState)
        {
            case State.GROUNDED:
                playerRb.useGravity = true;
                playerRb.constraints = RigidbodyConstraints.None;
                // <== TODO: Transition to Idle Animation ==> //
                break;
            case State.FLYING:
                playerRb.useGravity = false;
                playerRb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
                playerRb.constraints |= RigidbodyConstraints.FreezeRotationY;
                transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
                focalPoint.transform.rotation = transform.rotation; // Reset camera rotation
                // <== TODO: Transition to Flight Animation ==> //
                break;
        }
    }
    #endregion
}
