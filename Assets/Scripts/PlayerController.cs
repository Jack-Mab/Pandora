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

            float pitch = verticalInput * rotationAngle;
            float roll = horizontalInput * rotationAngle;
            float yaw = 0;

            Quaternion targetOrientation = Quaternion.Euler(pitch, yaw, -roll);

            // rotate player
            transform.rotation = Quaternion.Slerp(transform.rotation, targetOrientation, rotationSpeed);
            //transform.Translate(horizontalInput * rotationSpeed * Time.deltaTime * focalPoint.transform.right);
            playerRb.AddForce(horizontalInput * rollingSpeed * Time.deltaTime * focalPoint.transform.right,
                ForceMode.Impulse);

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
                // <== TODO: Transition to Idle Animation ==> //
                // <== TODO: Transition to upright posture ==> //
                //transform.Rotate(Vector3.right, 90);
                break;
            case State.FLYING:
                playerRb.useGravity = false;
                // <== TODO: Transition to Flight Animation ==> //
                // <== TODO: Transition to flight posture ==> //
                transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
                focalPoint.transform.rotation = Quaternion.Euler(0, 0, 0);
                //transform.Rotate(Vector3.right, -90);
                break;
        }
    }
    #endregion
}
