using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour
{
    [SerializeField]
    float influenceRange, intensity, distanceToPlayer;
    bool attract;
    Vector3 pullForce;
    Rigidbody playerBody;
    GameObject playerRef;
    private void Start()
    {
        playerRef = GameObject.Find("testobjjjj");
        playerBody = playerRef.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        AttractPlayer();
    }

    private void AttractPlayer()
    {
            distanceToPlayer = Vector3.Distance(playerRef.transform.position, transform.position);
            if (distanceToPlayer <= influenceRange)
            {
                pullForce = (transform.position - playerRef.transform.position).normalized / distanceToPlayer * intensity;
                playerBody.AddForce(pullForce, ForceMode.Force);
            }
    }

}
