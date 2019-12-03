using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    public AudioClip groundHit;
    public AudioClip rolling;

    AudioSource audioSourceGroundHit;
    AudioSource audioSourceRolling;

    void Start()
    {
        audioSourceGroundHit = gameObject.AddComponent<AudioSource>();
        audioSourceGroundHit.playOnAwake = false;
        audioSourceGroundHit.volume = 0.0f;

        audioSourceRolling = gameObject.AddComponent<AudioSource>();
        audioSourceRolling.loop = true;
        audioSourceRolling.playOnAwake = false;
        audioSourceRolling.volume = 0.0f;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Ground")
        {
            if (collision.relativeVelocity.magnitude > 4.0f)
                PlayHit(collision, -1.0f);
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            if (audioSourceRolling.isPlaying)
                audioSourceRolling.Pause();
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            float magnitude = GetComponent<Rigidbody>().velocity.magnitude;

            if (magnitude > 0.5f)
            {
                if (audioSourceRolling.isPlaying)
                {
                    if (magnitude > 4.0f)
                    {
                        audioSourceRolling.pitch = 1.0f;
                        audioSourceRolling.volume = 1.0f;
                    }
                    else
                    {
                        audioSourceRolling.pitch = 1.0f / 4.0f * magnitude + 0.5f;
                        audioSourceRolling.volume = 1.0f / 4.0f * magnitude;
                    }


                }
                else
                    audioSourceRolling.PlayOneShot(rolling);
            }
            else
            {
                if (audioSourceRolling.isPlaying)
                    audioSourceRolling.Pause();
            }
        }
    }

    void PlayHit(Collision collision, float volumeMod)
    {
        audioSourceGroundHit.pitch = Random.Range(0.200f, 0.204f);
        audioSourceGroundHit.PlayOneShot(groundHit, collision.relativeVelocity.magnitude / 4.0f + volumeMod);
    }

    void PlayCollisionMap(Collision collision)
    {
        PlayHit(collision, 0.0f);
    }
}
