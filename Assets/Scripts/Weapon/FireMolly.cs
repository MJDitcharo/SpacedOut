using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireMolly : MonoBehaviour
{
    [SerializeField] GameObject fire;

    private void OnTriggerEnter(Collider other)
    {
        Instantiate(fire, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
