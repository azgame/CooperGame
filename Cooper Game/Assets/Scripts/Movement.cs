using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour{

    public float jumpVelocity;
    public Rigidbody rb;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    [Range(3,50)]
    public float speed;

    bool isGrounded;

    void Start(){
        isGrounded = true;
    }

    void Update(){
        DirectionalMovement();


        if (isGrounded){
            if (Input.GetButtonDown("Jump")){
                GetComponent<Rigidbody>().velocity = Vector3.up * jumpVelocity;
            }
        }
        
    }
    void FixedUpdate()
    {
        Jump();

    }
    

    void DirectionalMovement(){
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);
    }

    void Jump(){
        if (rb.velocity.y < 0){
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        } else if (rb.velocity.y > 0 && !Input.GetButton("Jump")){
            rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }
  
}
