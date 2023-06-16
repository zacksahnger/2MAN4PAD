using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.AI;

public class HelicopterController : MonoBehaviour
{
   // public AudioSource;
   // public AudioClip;

   public NavMeshAgent agent;
   public GameObject CrashSmoke;
   public bool playerControlled;
   public bool destroyed;
   public InputAction action = null;
    public GameObject MainRotor;
    public GameObject TailRotor;

    public Vector3 MainRotorSpinDirection;
    public Vector3 TailRotorSpinDirection;

    public float MainRotorSpeed = 0.0f;
    public float TailRotorSpeed = 0.0f;

    [Range(-1, 1)]
    public float collective = 0.0f;
    // Start is called before the first frame update

    Rigidbody m_Rigidbody;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        m_Rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Invoke(nameof(AudioSource.PlayOneShot()),2);
        //transform.worldToLocalMatrix.MultiplyVector(transform.up) 
       // var localVector3Up = transform.InverseTransformDirection(transform.up);
       // var localVector3Forward = transform.InverseTransformDirection(transform.forward, ForceMode.Acceleration);
       if(playerControlled && !destroyed){
       m_Rigidbody.AddRelativeForce(Vector3.back * MainRotorSpeed*10*Input.GetAxis("Vertical"));
       m_Rigidbody.AddRelativeTorque(Vector3.up, ForceMode.Acceleration);
       }

       // m_Rigidbody.AddForce(localVector3Forward * collective * 100.0f);


        
        Spin(MainRotor,MainRotorSpinDirection,MainRotorSpeed*5);
        Spin(TailRotor,TailRotorSpinDirection,TailRotorSpeed*5);


    }
    void Spin(GameObject SpinObject, Vector3 spinDirection, float spinSpeed){
        SpinObject.transform.Rotate(spinDirection, spinSpeed*Time.deltaTime);
    }

       void OnCollisionEnter(Collision col) {
 
        if (col.gameObject.tag == "Projectile"){
           // Destroy(gameObject);
           CrashSmoke.SetActive(true);
           agent.enabled = false;
        }
    }
}
