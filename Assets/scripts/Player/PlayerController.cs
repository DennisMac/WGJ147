//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class PlayerController : MonoBehaviour
//{
//    Animator animator;    
//    Vector3 movement = Vector3.zero;
//    Rigidbody rigidbody;

//    public float speedMult = 0.1f;
//    public float forceMult = 100f;
//    public float rotateMult = 10f;

//    // Start is called before the first frame update
//    void Start()
//    {
//        animator = GetComponentInChildren<Animator>();
//        rigidbody = GetComponent<Rigidbody>();
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (Global.IsPaused)
//        {
//            movement = Vector3.zero;
//        }
//        else
//        {
//            movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

//            if (Mathf.Abs(movement.z) > Mathf.Epsilon)
//            {
//                animator.SetBool("Running", true);
//                animator.SetFloat("Speed", (Vector3.Dot(transform.forward, rigidbody.velocity) * speedMult));
//            }
//            else
//            {
//                animator.SetBool("Running", false);
//                animator.SetFloat("Speed", 0f);
//            }
            
//        }
//    }
//    private void FixedUpdate()
//    {
//        transform.Rotate(Vector3.up, movement.x * rotateMult);
//        movement.x = 0f;
//        //transform.position += transform.rotation * (movement * Global.PlayerSpeed * speedMult);
//        rigidbody.AddForce(transform.rotation * (movement * Global.PlayerSpeed * forceMult));

//    }
//}
