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
        GetComponent();
        player.localScale = new Vector3(rbody.mass , rbody.mass , rbody.mass );
        //target.localScale = target.localScale * rbody.mass;
       
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
                PlayerController playerC = GetComponent<PlayerController>();
                float increase = (RolledUp.rigidbody.mass * .05f) / 1;
                rbody.mass += increase;
                //player.localScale = Vector3.one * rbody.mass;
                
                sphere.radius += increase;
                //Destroy(RolledUp.rigidbody);
                RolledUp.transform.parent = transform;
                //newRadius();
                //sphere.radius = sum;
            }
        }
    }

    float sum;
    public void newRadius()
    {
        Renderer[] objs = GetComponentsInChildren<Renderer>();
        sum = 0;
        float xMax = Mathf.NegativeInfinity;
        float zMax = Mathf.NegativeInfinity;
        float yMax = Mathf.NegativeInfinity;

        
        

        foreach (Renderer r in objs)
        {
            if(r.bounds.extents.x + transform.position.x > xMax)
            {
                xMax = r.bounds.extents.x +transform.position.x;
            }
            if (r.bounds.extents.z + transform.position.z> zMax)
            {
                zMax = r.bounds.extents.z + transform.position.z;
            }
            if (r.bounds.extents.y + transform.position.y > yMax)
            {
                yMax = r.bounds.extents.y + transform.position.z;
            }
        }
        float maxD = Mathf.NegativeInfinity;
        for(int i =0; i < transform.childCount; i++)
        {
            float distance = Vector3.Distance(transform.GetChild(i).position, transform.position);
            if(maxD < distance)
            {
                maxD = distance;
            }
        }



        sum = (xMax + yMax + zMax) / 3;
        sum = maxD;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, sum);
    }

    void GetComponent()
    {
        rbody = GetComponent<Rigidbody>();
        player = GetComponent<Transform>();
        target = GetComponent<Transform>();
        sphere = GetComponent<SphereCollider>();
    }
}
