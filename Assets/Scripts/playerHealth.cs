using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerHealth : MonoBehaviour
{
    public int health;

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Bullet"))
        {
            health -= 10;
            if (health == 0)
            {
                Debug.Log("Dead");
            }
        }
    }
}
