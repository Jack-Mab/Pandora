using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    #region Instance Variables
    public GameObject player;
    public Vector3 offset;
    private PlayerController playerControllerScript;
    #endregion

    #region Method Overrides

    private void Start()
    {
        playerControllerScript = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + offset;
        if (playerControllerScript.state == PlayerController.State.FLYING)
        {
            Vector3 playerOrientation = player.transform.rotation.eulerAngles;
            transform.rotation = Quaternion.Euler(0, playerOrientation.y, 0);
        }
    }
    #endregion
}
