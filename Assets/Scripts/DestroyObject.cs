using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    #region Instance Variables
    // Private Variables
    private static readonly float LIMIT = 75;
    private GameObject player;
    #endregion

    #region Overridden Methods
    void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        float distance = (player.transform.position - transform.position).magnitude;

        if (distance > LIMIT)
        {
            Destroy(gameObject);
        }
    }
    #endregion
}
