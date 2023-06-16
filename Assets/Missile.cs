using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip motorAudioClip;
    //public ParticleSystem particleSystem;

    [Header("FX")]
    [Tooltip("Particle System for Exhaust Smoke")]
    public GameObject ExhaustParticleSystem; //fix this and access the particle system properly
   // public GameObject ExplosionParticleSystem;

   [Tooltip("Prefab for Object Containing Explosion (instantiated on collision)")]
    public GameObject ExplosionPrefab;

    [Tooltip("Transform Where Explosion is Spawned")]
    public Transform ExplosionTransform;
    private Rigidbody rb;
    private bool audioIsPlaying = false;
    bool motorRunning = false;

    [Space(10)]
    [Header("Rocket Motor Configuration")]
    [Tooltip("Force Applied to Missile by Rocket Motor")]
    public float thrust;
    public float motorStartDelay;
    public float motorStopDelay;

    [Space(10)]
    [Header("Seeker Settings")]

    [Header("Testing (broken):")]
    public float attackRange;
      bool playerInAttackRange;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        Invoke("StartMotor", motorStartDelay);
        Invoke("StopMotor", motorStopDelay);
    }

    // Update is called once per frame
    void Update()
    {
        //playerInAttackRange = Physics.ch
        /*if(!audioIsPlaying ){
            if(motorRunning)
                Invoke("PlayAudio",audioClip.length);
        audioIsPlaying = true;
        }*/
        
    }
    void FixedUpdate(){
        RunMotor(thrust);
    }

    void PlayAudio(){
        //audioSource.pitch = rb.velocity.magnitude;
        audioSource.PlayOneShot(motorAudioClip,rb.velocity.magnitude);
        audioIsPlaying = false;
    }
    void RunMotor(float thrust){
        if(motorRunning){
            rb.AddRelativeForce(Vector3.up*thrust,ForceMode.Acceleration);
        }
    }

    void StartMotor(){
        PlayAudio();
        motorRunning = true;
        ExhaustParticleSystem.SetActive(true);
    }
    void StopMotor(){
        motorRunning = false;
    }

    void OnCollisionEnter(Collision col){
        if(rb.velocity.magnitude > 40){
           SpawnExplosion();
            DestroyMissile();
        }

    }

    public void SpawnExplosion(){
        GameObject spawnedExplosion = Instantiate(ExplosionPrefab, ExplosionTransform.position, ExplosionTransform.rotation);
            //spawnedExplosion.GetComponent<Rigidbody>().velocity = speed * barrel.up;
    }

    public void DestroyMissile(){
        Destroy(gameObject);
    }
}
