using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{

    AudioSource source;

    [SerializeField] AudioClip bulletSound;
    [SerializeField] AudioClip deathEnemySound;
    [SerializeField] AudioClip ambientSound;
    [SerializeField] AudioClip destructionRootSound;
    [SerializeField] AudioClip walkAcornSound;
    [SerializeField] AudioClip deathPlayerSound;
    [SerializeField] AudioClip lightFootSound;
    [SerializeField] AudioClip strongFootSound;
    [SerializeField] AudioClip pickupSound;
    [SerializeField] AudioClip rootGrowthSound;
    [SerializeField] AudioClip walkOnSticksSound;

    // Start is called before the first frame update
    void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    private void PlaySound(AudioClip _sound)
    {
        source.PlayOneShot(_sound);
    }

    public void PlayBulletSound()
    {
        PlaySound(bulletSound);
    }

    public void PlayDeathEnemySound()
    {
        PlaySound(deathEnemySound);
    }

    public void PlayAmbientSound()
    {
        PlaySound(ambientSound);
    }

    public void PlayDestructionRootSound()
    {
        PlaySound(destructionRootSound);
    }

    public void PlayWalkAcornSound()
    {
        PlaySound(walkAcornSound);
    }

    public void PlayDeathPlayerSound()
    {
        PlaySound(deathPlayerSound);
    }

    public void PlayLightFootSound()
    {
        PlaySound(lightFootSound);
    }

    public void PlayStrongFootSound()
    {
        PlaySound(strongFootSound);
    }

    public void PlayPickupSound()
    {
        PlaySound(pickupSound);
    }

    public void PlayRootGrowthSound()
    {
        PlaySound(rootGrowthSound);
    }

    public void PlayWalkOnSticksSound()
    {
        PlaySound(walkOnSticksSound);
    }

}
