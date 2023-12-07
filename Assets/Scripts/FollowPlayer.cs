using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    #region Instance Variables
    public GameObject player;
    public Vector3 offset;
    #endregion

    #region Method Overrides
    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + offset;
    }
    #endregion
}
