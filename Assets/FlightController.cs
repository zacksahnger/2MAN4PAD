using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightController : MonoBehaviour
{

    //[Header("Object flight is applied to (do I need this?)")]
    //public GameObject obj;
    [Header("Set Target Transform")]
    public Vector3 targetTransform;
    public Quaternion targetQuat;
    public float thrust;
    public float dampening;
    private Rigidbody rb;
    private GameObject gm;
    private Vector3 velocity = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
       // gm = GetComponent<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        Vector3 diff = targetTransform - rb.transform.position;
        rb.transform.position = Vector3.SmoothDamp(rb.transform.position, targetTransform,ref velocity,0.3f);
        rb.AddForce(Vector3.up*9.8f,ForceMode.Acceleration);
        //rb.rotation.
        /*
        rb.AddForce(diff*thrust,ForceMode.Acceleration);
        rb.AddForce((diff*-1)*rb.velocity.magnitude*dampening,ForceMode.Acceleration);
        rb.AddForce(Vector3.up*9.8f,ForceMode.Acceleration);
        */

    }
}
