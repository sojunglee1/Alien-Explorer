using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Source File Name: AudioManager
 * Author's Name: Sojung (Serena) Lee
 * Student #: 101245044
 * Date Last Modified: December 12, 2021
 * Program Description: Sets up the audio for sfx
 * Revision History:
 * (December 12) Added basic audio system for sfx
 * (December 12) Added jump, coin, star, attacked, death, and shoot sounds
 */

//Class for audio management for sound effects played in game
public class AudioManager : MonoBehaviour
{
    //creates audio manager static variable (for easier access to other scripts)
    public static AudioManager audioManager;
    private AudioSource audioSource;

    //created multiple audio clips for sfx 
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip coinSound;
    [SerializeField] private AudioClip starSound;
    [SerializeField] private AudioClip attackedSound;
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip shootSound;

    private void Awake()
    {
        //sets up the static class
        audioManager = this;
        audioSource = GetComponent<AudioSource>();
    }

    //plays a specific sfx once
    public void PlaySound(AudioClip sfx) 
    {
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(sfx);
        }
    }

    public void JumpSound() //plays jump sound
    {
        PlaySound(jumpSound);
    }

    public void CoinSound() //plays coin collecting sound
    {
        PlaySound(coinSound);
    }

    public void StarSound() //plays star collecting sound
    {
        PlaySound(starSound);
    }
    
    public void AttackedSound() //plays sound for when player gets attacked
    {
        PlaySound(attackedSound);
    }    

    public void DeathSound() //plays sound for when player loses a life
    {
        PlaySound(deathSound);
    }

    public void ShootSound() //plays sound for when player shoots a bullet
    {
        PlaySound(shootSound);
    }
}
