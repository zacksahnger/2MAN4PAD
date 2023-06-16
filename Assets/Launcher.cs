using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    public float rate = 5;
    public float speed = 40;
    public GameObject projectile;
    public Transform barrel;
    public AudioSource audioSource;
    public AudioClip audioClip;
    public AudioClip targetLockAudioClip;
    public GameObject Indicator;

    public CapsuleCollider capsuleCollider;

        private Coroutine _current;

    public void BeginFire()
    {
        if(_current != null) StopCoroutine(_current);

        _current = StartCoroutine (FireRoutine ());
    }

    public void StopFire()
    {
        if(_current != null) StopCoroutine(_current);
    }

    private IEnumerator FireRoutine()
    {
        while(true)
        {
            GameObject spawnedBullet = Instantiate(projectile, barrel.position, barrel.rotation);
            spawnedBullet.GetComponent<Rigidbody>().velocity = speed * barrel.up;
            audioSource.pitch = 1.0f;
            audioSource.PlayOneShot(audioClip);
          //  Destroy(spawnedBullet, 2);

            yield return new WaitForSeconds (1f / rate);
        }
    }
    // Start is called before the first frame update
    public void Update(){
            //if(targeting.OnCollisionStay())
    }
    void OnCollisionStay(Collision col){
        if(col.gameObject.tag == "Hitbox"){
            Debug.Log("Target Locked!");
            Indicator.SetActive(true);

        }

    }
    void OnTriggerStay(Collider col){
                if(col.gameObject.tag == "Hitbox"){
            Debug.Log("Target Locked!");
            Indicator.SetActive(true);
            Vector3 diff = col.transform.position - gameObject.transform.position;
            float signalStrength = diff.magnitude;
           // audioSource.pitch = 50 -  0.5f* signalStrength;
            audioSource.PlayOneShot(targetLockAudioClip,0.4f);
        }
    }
        void OnTriggerExit(Collider col){
                if(col.gameObject.tag == "Hitbox"){
            Debug.Log("Target Lost!");
            Indicator.SetActive(false);
        }
    }
    void onCollisionExit(Collision col){
                if(col.gameObject.tag == "Hitbox"){
            Debug.Log("Target Lost!");
            Indicator.SetActive(false);

        }
    }
}
