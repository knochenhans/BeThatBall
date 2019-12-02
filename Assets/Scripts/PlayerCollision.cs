using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    public AudioClip ground;
    public AudioClip rolling;

    AudioSource audioSourceGround;
    AudioSource audioSourceRolling;

    bool groundContact = false;

    void Start()
    {
        audioSourceGround = gameObject.AddComponent<AudioSource>();
        audioSourceRolling = gameObject.AddComponent<AudioSource>();
        audioSourceRolling.loop = true;
        audioSourceRolling.playOnAwake = false;
        audioSourceRolling.clip = rolling;
        audioSourceRolling.volume = 0.8f;
    }

    void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            // Ground
            if (contact.normal.y == 1.0)
            {
                groundContact = true;

                if (collision.relativeVelocity.magnitude > 4)
                    PlayCollisionMap(collision, -1.0f);
            }
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.contactCount > 1)
        {
            //if (collision.relativeVelocity.magnitude > 2)
            PlayCollisionMap(collision);

            foreach (ContactPoint contact in collision.contacts)
            {
                if (contact.normal.y == 1.0f)
                {
                    groundContact = true;
                }
            }
        }
    }

    void OnCollisionExit()
    {
        groundContact = false;
    }

    void PlayCollisionMap(Collision collision, float volumeMod)
    {
        audioSourceGround.pitch = Random.Range(0.200f, 0.204f);
        audioSourceGround.PlayOneShot(ground, 1.0f / 4.0f * collision.relativeVelocity.magnitude + Random.Range(-0.1f, 0.1f) + volumeMod - 0.5f);
    }

    void PlayCollisionMap(Collision collision)
    {
        PlayCollisionMap(collision, 0.0f);
    }

    void FixedUpdate()
    {
        if (GetComponent<Rigidbody>().velocity.magnitude > 0.1f)
        {
            if (!audioSourceRolling.isPlaying)
                audioSourceRolling.Play();

            audioSourceRolling.volume = GetComponent<Rigidbody>().velocity.magnitude / 4.0f;
        }
        else
        {
            if (audioSourceRolling.isPlaying)
                audioSourceRolling.Pause();
        }

        if (!groundContact)
            audioSourceRolling.Pause();
    }
}
