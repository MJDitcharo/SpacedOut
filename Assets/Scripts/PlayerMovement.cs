using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    public CharacterController cc;
    public Camera cam;

    Vector2 movement;
    Vector2 mousePosition;
    private void Start()
    {
        cc = gameObject.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")); //move the player with wasd
        mousePosition = cam.ScreenToWorldPoint(Input.mousePosition); //get the mouse position
    }

    void FixedUpdate()
    {
        Vector2 lookDirection = new Vector2(mousePosition.x - cc.transform.position.x, mousePosition.y - cc.transform.position.y); //subtract two vectors to get a vector from the player and mouse position
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f; //gets the angle from the character position to the mouse position to look that way
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle)); //set it to the player

        cc.Move(moveSpeed * Time.deltaTime * movement); //move the player per frame
    }
}
