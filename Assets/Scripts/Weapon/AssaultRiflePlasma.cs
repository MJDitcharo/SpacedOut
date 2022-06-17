using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssaultRiflePlasma : Rifle
{
    [SerializeField] LayerMask layerMask;
    [SerializeField] TrailRenderer bulletTrail;
    [SerializeField] float tracerTime = .1f;

    public override void Shoot()
    {
        RaycastHit hit;
        if(Physics.Raycast(firePoint[0].position, firePoint[0].forward, out hit, Mathf.Infinity, layerMask))
        {
            StartCoroutine(SpawnTrail(bulletTrail, hit.point, tracerTime));
            StartCoroutine(SpawnTrail(bulletTrail, hit.point, tracerTime));
            health hp = hit.collider.gameObject.GetComponent<health>();
            if(hp != null)
            {
                hp.DoDamage((int)damage);
            }
        }
        ammoCount--;
        AudioManager.Instance.PlaySFX(gunshotSound);

    }

    private IEnumerator SpawnTrail(TrailRenderer trail, Vector3 hitPoint, float speed)
    {
        GameObject tracer = Instantiate(bulletPrefab, firePoint[0].position, Quaternion.identity);
        Destroy(tracer, 2);

        float time = 0;

        while(time < tracerTime)
        {
            tracer.transform.position = Vector3.Lerp(firePoint[0].position, hitPoint, time / tracerTime);
            time += Time.deltaTime;

            yield return null;
        }
    }

    
}
