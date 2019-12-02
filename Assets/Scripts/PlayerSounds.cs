using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    public AudioClip ground;

    AudioSource audioSourceGround;

    void Start()
    {
        audioSourceGround = gameObject.AddComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            // Ground
            if (contact.normal.y == 1.0)
            {
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

            }
        }
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
}
