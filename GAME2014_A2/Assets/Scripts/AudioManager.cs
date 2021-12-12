using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager audioManager;
    private AudioSource audioSource;

    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip coinSound;
    [SerializeField] private AudioClip starSound;
    [SerializeField] private AudioClip attackedSound;
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip shootSound;

    private void Awake()
    {
        audioManager = this;
        audioSource = GetComponent<AudioSource>();
    }
    public void PlaySound(AudioClip sfx)
    {
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(sfx);
        }
    }

    public void JumpSound()
    {
        PlaySound(jumpSound);
    }

    public void CoinSound()
    {
        PlaySound(coinSound);
    }

    public void StarSound()
    {
        PlaySound(starSound);
    }
    
    public void AttackedSound()
    {
        PlaySound(attackedSound);
    }    

    public void DeathSound()
    {
        PlaySound(deathSound);
    }

    public void ShootSound()
    {
        PlaySound(shootSound);
    }
}
