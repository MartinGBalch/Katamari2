using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPickup : MonoBehaviour
{
    bool hitTheBall = false;
    GameObject pickup;

    SphereCollider sphere;

    public float pickupableRatio = 1.5f;

    public Rigidbody rbody;
    public Transform target;
    Transform player;

    // Use this for initialization
    void Start ()
    {
        //target.localScale = target.localScale * rbody.mass;
        GetComponent();
    }
	
	// Update is called once per frame
	void Update () {}

    private void OnCollisionEnter(Collision RolledUp)
    {
        if (RolledUp.gameObject.tag != "pickup")
        {
            return;
        }

        foreach(ContactPoint contact in RolledUp.contacts)
        {
            if (contact.thisCollider == GetComponent<Collider>())
            {
                hitTheBall = true;
                break;
            }
        }

        if(hitTheBall)
        {
            if (RolledUp.rigidbody.mass <= rbody.mass * pickupableRatio && RolledUp.rigidbody.isKinematic)
            {
                float increase = (RolledUp.rigidbody.mass * .05f) / 1;
                rbody.mass += increase;
                //player.localScale = new Vector3(increase * 1, increase * 1, increase*1);
                sphere.radius += increase;
                Destroy(RolledUp.rigidbody);
                RolledUp.transform.parent = transform;
            }
        }
    }

    void GetComponent()
    {
        rbody = GetComponent<Rigidbody>();
        player = GetComponent<Transform>();
        target = GetComponent<Transform>();
        sphere = GetComponent<SphereCollider>();
    }
}
