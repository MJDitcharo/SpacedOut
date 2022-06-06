using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveBullet : MonoBehaviour
{
    [SerializeField] float scaleSpeed = 1;
    private void Update()
    {
        transform.localScale += new Vector3(1,1,1) * scaleSpeed * Time.deltaTime;
    }
}
