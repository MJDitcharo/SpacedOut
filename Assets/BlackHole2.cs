using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole2 : MonoBehaviour
{
    public float pullStrength = 0.5f;
    public float pullRange = 1f;
    public int damage = 1;

    public float tickTime = 1;
    private float tick = 0;

    [SerializeField] GameObject BlackHoleCenter;


    private void Start()
    {
        Vector3 size = new Vector3(pullRange, 5, pullRange);
        transform.localScale = size;
    }

    private void Update()
    {
        tick += Time.deltaTime;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            GameManager.instance.movement.pushback += (-pullStrength * (GameManager.instance.player.transform.position - BlackHoleCenter.transform.position).normalized);
        }
        
        if (other.tag == "Enemy")
        {

            EnemyMovement movement = other.GetComponent<EnemyMovement>();
            if (movement != null)
            {
                movement.pushback += (-pullStrength * (other.transform.position - BlackHoleCenter.transform.position).normalized);
                if (tick >= tickTime)
                {
                    other.GetComponent<EnemyHealth>().DoDamage(damage);
                    tick = 0;
                }
            }
        }
    }
}
