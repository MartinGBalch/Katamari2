using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed;
  
    Rigidbody rbody;
   
	// Use this for initialization
	void Start () { GetComponents(); }
	
	// Update is called once per frame
	void Update ()
    {
        Move();
	}

    void Move()
    {
        Vector2 movementInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        rbody.AddForce(movementInput.x * speed, 0, movementInput.y * speed, ForceMode.Acceleration);
    }

    void GetComponents()
    {
        rbody = GetComponent<Rigidbody>();
    }
}
