using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class HomingBullet : bullet
{
    public float speed;
    public float yOffset = 1;
    private Coroutine homingCoroutine;
    private Transform target;


    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Enemy").transform;
        if (homingCoroutine != null)
        {
            StopCoroutine(homingCoroutine);
        }

        homingCoroutine = StartCoroutine(FindTarget());

        Destroy(gameObject, 3);
    }

    private IEnumerator FindTarget()
    {
        Vector3 startPosition = transform.position;
        float time = 0;

        while(time < 1)
        {
            transform.position = Vector3.Lerp(startPosition, target.position + new Vector3(0 , yOffset, 0), time);
            transform.LookAt(target.position + new Vector3(0, yOffset, 0));

            time += Time.deltaTime * speed;
            yield return null;
        }
    }
}
