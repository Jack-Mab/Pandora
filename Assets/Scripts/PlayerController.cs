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
    private Rigidbody playerRb;
    // <== TODO: Add public variable to Game Manager Script ==> //
    #endregion

    #region Method Overrides
    // Start is called before the first frame update
    void Start()
    {
        state = State.GROUNDED;
        playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
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
            
        }
    }
    #endregion
}
