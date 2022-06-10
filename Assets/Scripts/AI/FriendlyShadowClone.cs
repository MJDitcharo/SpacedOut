using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyShadowClone : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float bulletSpeed = 20;
    [SerializeField] float shootDelay = .5f;

    private void Start()
    {
        StartCoroutine(Shoot());
    }

    private void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 20);
        if (colliders.Length != 0)
        {
            GameObject closest = null;
            for(int i = 0; i < colliders.Length; i++)
            {
                if(colliders[i].gameObject.tag == "Enemy")
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
            transform.LookAt(closest.transform.position);
        }
    }

    IEnumerator Shoot()
    {
        while(true)
        {
            yield return new WaitForSeconds(shootDelay);

            GameObject bul = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bul.GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed;
        }
    }


}
