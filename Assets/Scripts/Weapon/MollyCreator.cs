using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MollyCreator : MonoBehaviour
{
    [SerializeField] GameObject molly;
    [SerializeField] float mollySpawnInterval = .3f;

    private void Start()
    {
        StartCoroutine(ContinuouslyCreateMollies(mollySpawnInterval));
    }

    public IEnumerator ContinuouslyCreateMollies(float timeInterval)
    {
        while(true)
        {
            GameObject fire = Instantiate(molly, transform.position, Quaternion.identity);
            fire.transform.rotation = transform.rotation;
            yield return new WaitForSeconds(timeInterval);
        }
    }
}
