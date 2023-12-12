using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    #region Instance Variables
    // Public Variables
    public float speed = 10;
    public MountainExplosion groundSensorScript;

    // Private Variables
    private Rigidbody enemyRb;
    private GameObject player;
    #endregion

    #region Overridden Methods
    void Start()
    {
        groundSensorScript =  transform.parent.GetComponentInChildren<MountainExplosion>(false);
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    void FixedUpdate()
    {
        if (groundSensorScript.onMountain)
        {
            enemyRb.isKinematic = false;

            // Move enemy towards player
            Vector3 direction = (player.transform.position - transform.position).normalized;
            enemyRb.MovePosition(transform.position + (speed * Time.deltaTime * direction));
        } else
        {
            enemyRb.isKinematic = true;
        }
    }
    #endregion
}
