using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplostionSound : MonoBehaviour
{

    public AudioClip hitSound;
    private AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        if (hitSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(hitSound);
        }
        Destroy(gameObject, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
