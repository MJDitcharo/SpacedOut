using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] playerHealth ph;
    public float moveSpeed = 1f;
    [SerializeField] float pushbackFalloffSpeed = 4;
    [SerializeField] float rollSpeed = 4;
    [SerializeField] float rollCooldown = 1;
    [SerializeField] float rollInvulnerability = .5f;
    float rollTimer = 0;
    Vector3 movement;
    Vector3 mousePosition;
    public Vector3 pushback;
    Animator playerAnimation;

    public CharacterController cc;
    Vector3 warpedPositon;
    // Start is called before the first frame update
    void Start()
    {
        cc = gameObject.GetComponent<CharacterController>();
        playerAnimation = gameObject.GetComponent<Animator>();
    }
    void Update()
    {
        pushback = new Vector3(pushback.x, 0, pushback.z);
        pushback = Vector3.Lerp(pushback, Vector3.zero, pushbackFalloffSpeed * Time.deltaTime);
        movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized; //move the player with wasd
        playerAnimation.SetFloat("Velocity", Mathf.Lerp(playerAnimation.GetFloat("Velocity"), cc.velocity.magnitude, Time.deltaTime * 4));

        rollTimer += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space) && rollTimer >= rollCooldown)
        {
            rollTimer = 0f;
            pushback += transform.forward.normalized * rollSpeed;
            playerAnimation.SetTrigger("Roll");
            StartCoroutine(Rolling());
        }

      
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); //gets mouse position relative to camera
        mousePosition.y = transform.position.y; //prevent player from looking up
        Debug.DrawLine(transform.position, mousePosition, Color.red, 2f);
        //move the charcter towards the mouse position
        transform.LookAt(mousePosition);
        cc.Move(new Vector3(0, -1, 0) * Time.deltaTime);
        cc.Move(moveSpeed * Time.deltaTime * (movement + pushback));

    }
    
    void LateUpdate()
    {
        if(warpedPositon != Vector3.zero)
        {
            cc.enabled = false;
            transform.position = warpedPositon;
            warpedPositon = Vector3.zero;
            cc.enabled = true;
        }
    }

    public IEnumerator Rolling()
    {
        ph.isDamageable = false;
        yield return new WaitForSeconds(rollInvulnerability);
        ph.isDamageable = true;

    }

    public void WarpToPosition(Vector3 position)
    {
        warpedPositon = position;
    }

    
}
