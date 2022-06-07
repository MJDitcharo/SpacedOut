using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildSplitBullet : bullet
{
    public Collider ignore;
    bool done = false;
    
    private void Start()
    {
        StartCoroutine(Wait());
    }


    public override void OnTriggerEnter(Collider other)
    {
        if (other == ignore && !done)
            return;
        Debug.Log("there");
        base.OnTriggerEnter(other);
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(.1f);
        done = true;
    }
}
