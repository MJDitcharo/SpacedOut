using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole2 : MonoBehaviour
{
    public float pullStrength = 0.5f;
    public float pullRange = 1f;

    [SerializeField] GameObject BlackHoleCenter;


    private void Start()
    {
        Vector3 size = new Vector3(pullRange, 0, pullRange);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Contact with player");
            GameManager.instance.movement.pushback += (-pullStrength * (GameManager.instance.player.transform.position - BlackHoleCenter.transform.position).normalized);
        }
        
        if (other.tag == "Enemy")
        {
            Debug.Log("Contact with Enemy");

            EnemyMovement movement = other.GetComponent<EnemyMovement>();
            movement.pushback += (-pullStrength * (other.transform.position - BlackHoleCenter.transform.position).normalized);
        }
    }
}
