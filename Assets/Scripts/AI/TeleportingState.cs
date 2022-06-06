using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportingState : State
{
    [SerializeField] State engageState;
    [SerializeField] GameObject[] tpLocations;
    [SerializeField] float timeBetweenTeleports = 2;
    Coroutine teleportCoroutine;
    bool done = false;

    private void Start()
    {
        tpLocations = GameObject.FindGameObjectsWithTag("Teleport Location");
    }
    public override State RunCurrentState()
    {
        movement.GetAgent().isStopped = true;
        if (done)
        {
            movement.GetAgent().isStopped = false;
            done = false;
            return engageState;
        }
        if(teleportCoroutine == null)
        {
            teleportCoroutine = StartCoroutine(Teleport());
        }

        return this;  
    }

    IEnumerator Teleport()
    {
        int numberOfTeleports = Random.Range(1, 4);
        
        for(int i = 0; i < numberOfTeleports; i++)
        {
            yield return new WaitForSeconds(timeBetweenTeleports);

            bool locationDecided = false;
            while(!locationDecided)
            {
                int locationIndex = Random.Range(0, tpLocations.Length);
                Debug.Log(locationIndex);

                if(Vector3.Distance(tpLocations[locationIndex].transform.position, GameManager.instance.player.transform.position) >= 15)
                {
                    locationDecided = true;
                    movement.GetAgent().Warp(tpLocations[locationIndex].transform.position);
                }
            }
        }
        teleportCoroutine = null;
        done = true;
    }
}
