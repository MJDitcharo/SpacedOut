using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngageState : State
{
    [SerializeField] State attackState;
    [SerializeField] SuicideAttackState suicideAttackState;
    

    [SerializeField] float attackDistance;

    // Start is called before the first frame update
    void Start()
    {

    }

    public override State RunCurrentState()
    {
        animator.SetBool("Running", true);
        RaycastHit hit = new RaycastHit();
        if (Vector3.Distance(transform.position, GameManager.instance.player.transform.position) <= attackDistance && Physics.Raycast(transform.position, GameManager.instance.player.transform.position + new Vector3(0, 1, 0) - transform.position, out hit, Mathf.Infinity) && hit.collider.gameObject == GameManager.instance.player)
        {
            if(GetComponent<SuicideAttackState>() != null)
            {
                return suicideAttackState;
            }
            return attackState;
        }
        Debug.DrawRay(transform.position, GameManager.instance.player.transform.position + new Vector3(0,1,0) - transform.position, Color.blue, .05f);
        
        movement.MoveToLocation(GameManager.instance.player.transform.position);

        return this;
    }
}
