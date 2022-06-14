using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomForce : MonoBehaviour
{
    [SerializeField] float randomForce;
    private void Start()
    {
        gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-randomForce, randomForce), Random.Range(-randomForce, randomForce), Random.Range(-randomForce, randomForce)));
        gameObject.GetComponent<Rigidbody>().AddTorque(new Vector3(Random.Range(-randomForce, randomForce), Random.Range(-randomForce, randomForce), Random.Range(-randomForce, randomForce)));
    }
}
