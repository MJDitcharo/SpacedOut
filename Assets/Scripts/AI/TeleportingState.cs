using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportingState : State
{
    [SerializeField] State engageState;
    public GameObject[] tpLocations;
    [SerializeField] float timeBetweenTeleports = 2;
    [SerializeField] GameObject tpEffect;
    Coroutine teleportCoroutine;
    bool done = false;

    [SerializeField] AudioClip teleportAudio;
    Sound teleportSound;

    private void Start()
    {
        tpLocations = GameObject.FindGameObjectsWithTag("Teleport Location");

        teleportSound = new Sound();
        teleportSound.audio = teleportAudio;
        teleportSound.audioType = AudioStyle.sfx;
    }
    public override State RunCurrentState()
    {
        movement.GetAgent().isStopped = true;
        if (done)
        {
            movement.GetAgent().isStopped = false;
            done = false;
            animator.SetBool("Teleporting", false);
            return engageState;
        }
        if(teleportCoroutine == null)
        {
            teleportCoroutine = StartCoroutine(Teleport());
        }

        return this;  
    }

    public IEnumerator Teleport()
    {
        animator.SetBool("Teleporting", true);
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
                    Instantiate(tpEffect, transform.position, Quaternion.Euler(-90, 0, 0));
                    movement.GetAgent().Warp(tpLocations[locationIndex].transform.position);
                    AudioManager.Instance.PlaySFX(teleportSound);
                    Instantiate(tpEffect, transform.position, Quaternion.Euler(-90,0,0));
                }
            }
        }
        
        if(teleportCoroutine != null)
            done = true;
        teleportCoroutine = null;
    }
}
