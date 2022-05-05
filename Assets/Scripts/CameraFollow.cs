using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public bool followPlayer = true;
    Vector3 offset;
    [SerializeField] int followSpeed;
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - GameManager.instance.player.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, GameManager.instance.player.transform.position + offset, followSpeed * Time.deltaTime);
    }
}
