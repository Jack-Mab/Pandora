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
    public State state;
    public float speed;
    public float rotationSpeed;
    public float rollingSpeed;
    public float rotationAngle;
    public GameObject focalPoint;
    private Rigidbody playerRb;
    // <== TODO: Add public variable to Game Manager Script ==> //
    #endregion

    #region Method Overrides
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        // Get User input
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // <== TODO: Check for game over condition ==> //

        if (state == State.DEAD)
        {
            // <== TODO: Call GameOver() ==> //
        } else if (state == State.FLYING)
        {
            // Change the player's pitch, yaw and roll
            Vector3 currentOrientation = transform.rotation.eulerAngles;
            float pitch = verticalInput * rotationAngle;
            float yaw = currentOrientation.y;
            float roll = horizontalInput * rotationAngle;

            Quaternion targetOrientation = Quaternion.Euler(pitch, yaw, -roll);

            // roll player
            transform.rotation = Quaternion.Slerp(transform.rotation, targetOrientation, rotationSpeed);
            Vector3 newPosition = horizontalInput * rollingSpeed * Time.deltaTime * focalPoint.transform.right;

            playerRb.MovePosition(transform.position + newPosition); // Move player sideways

            // Land player
            if (Input.GetKeyDown(KeyCode.F))
            {
                ChangeState(State.GROUNDED);
            }
        } else if (state == State.GROUNDED)
        {
            playerRb.AddForce(speed * verticalInput * Time.deltaTime * focalPoint.transform.forward); // Move the player forward
            focalPoint.transform.Rotate(focalPoint.transform.up, rotationSpeed * -horizontalInput); // Rotate camera around player

            // Player takes off
            if (Input.GetKeyDown(KeyCode.F))
            {
                ChangeState(State.FLYING);
            }
        }
    }
    #endregion

    #region Custom Methods
    private void ChangeState(State newState)
    {
        state = newState;
        switch (newState)
        {
            case State.GROUNDED:
                playerRb.useGravity = true;
                playerRb.constraints = RigidbodyConstraints.None;
                // <== TODO: Transition to Idle Animation ==> //
                // <== TODO: Transition to upright posture ==> //
                //transform.Rotate(Vector3.right, 90);
                break;
            case State.FLYING:
                playerRb.useGravity = false;
                playerRb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
                playerRb.constraints |= RigidbodyConstraints.FreezeRotationY;
                // <== TODO: Transition to Flight Animation ==> //
                // <== TODO: Transition to flight posture ==> //
                transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
                focalPoint.transform.rotation = transform.rotation; //Quaternion.Euler(0, 0, 0);
                //transform.Rotate(Vector3.right, -90);
                break;
        }
    }
    #endregion
}
