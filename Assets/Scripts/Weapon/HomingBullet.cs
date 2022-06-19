using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class HomingBullet : bullet
{
    public float speed = 20f;
    public float yOffset = 1;
    Coroutine lookCoroutine;
    [SerializeField] float trackingStrength = 5;

    private void Update()
    {
        FindTarget();
    }

    private void FindTarget()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 20);
        if (colliders.Length != 0)
        {
            GameObject closest = null;
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject.tag == "Enemy")
                {
                    if (closest == null)
                        closest = colliders[i].gameObject;
                    if (Vector3.Distance(colliders[i].transform.position, transform.position) < Vector3.Distance(closest.transform.position, transform.position))
                    {
                        closest = colliders[i].gameObject;
                    }
                }
            }

            Debug.Log(closest);
            if (closest != null && lookCoroutine == null)
                StartCoroutine(TurnToTarget(closest.transform.position));
        }
        GetComponent<Rigidbody>().velocity = transform.forward * speed;
    }

    IEnumerator TurnToTarget(Vector3 target)
    {
        //Debug.Log("running coroutine");
        Quaternion lookRoation = Quaternion.LookRotation(target - new Vector3(transform.position.x, target.y, transform.position.z));
        Quaternion originalRotation = transform.rotation;

        float time = 0;

        while (time < 1)
        {
            //Debug.Log(time);
            transform.rotation = Quaternion.Slerp(originalRotation, lookRoation, time);

            time += Time.deltaTime * trackingStrength;

            yield return new WaitForEndOfFrame();
        }
        lookCoroutine = null;
    }
}
