using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public GameObject player;
    public float speed;
    private PlayerController playerControllerScript;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerControllerScript = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerControllerScript.state == PlayerController.State.FLYING)
        {
            Vector3 direction = -(player.transform.forward.normalized);
            transform.Translate(direction * speed * Time.deltaTime);
        }
    }
}
