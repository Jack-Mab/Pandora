using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    #region Instance Variables
    // Public Variables
    public GameObject player;
    public float speed;

    // Private Variables
    private PlayerController playerControllerScript;
    private Rigidbody rb;
    #endregion

    #region Overridden Methods
    void Start()
    {
        player = GameObject.Find("Player");
        playerControllerScript = player.GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Move the object when the player is flying
        if (playerControllerScript.state == PlayerController.State.FLYING)
        {
            Vector3 direction = -(player.transform.forward.normalized);
            rb.MovePosition(transform.position + (speed * Time.deltaTime * direction));
        }
    }
    #endregion
}
