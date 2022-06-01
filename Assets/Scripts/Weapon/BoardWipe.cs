using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardWipe : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        WipeBoard();
    }
    public void WipeBoard()
    {

        for (int i = 0; i < GameManager.instance.bullets.Count; i++)
        {
            Destroy(GameManager.instance.bullets[i]);
        }
        GameManager.instance.bullets.Clear();
    }
}
