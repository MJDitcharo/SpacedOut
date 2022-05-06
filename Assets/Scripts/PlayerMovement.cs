using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] playerHealth ph;
    public float moveSpeed = 1f;
    Vector3 movement;
    Vector3 mousePosition;
    Animator playerAnimation;

    public CharacterController cc;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        cc = gameObject.GetComponent<CharacterController>();
        playerAnimation = gameObject.GetComponent<Animator>();
    }
    void Update()
    {
        movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized; //move the player with wasd
        playerAnimation.SetFloat("Velocity", cc.velocity.magnitude);
        if (Input.GetKeyUp(KeyCode.Space))
        {
            playerAnimation.SetTrigger("Roll");
            StartCoroutine(Rolling());

        }

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); //gets mouse position relative to camera
        mousePosition.y = 0f; //prevent player from looking up
        Debug.DrawLine(transform.position, mousePosition, Color.red, 2f);
        //move the charcter towards the mouse position
        transform.LookAt(mousePosition);
        cc.Move(new Vector3(0, -1, 0));
        cc.Move(moveSpeed * Time.deltaTime * movement);
    }

    public IEnumerator Rolling()
    {
        ph.isDamageable = false;
        yield return new WaitForSeconds(1f);
        ph.isDamageable = true;

    }
}
