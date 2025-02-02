using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.Windows;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource SFXSource;
    [SerializeField] private AudioSource WalkSource;
    [SerializeField] private AudioSource RunSource;
    public AudioClip deathRobot;
    public AudioClip walkSound;
    public AudioClip RunSound;
    public AudioClip background;
    public AudioClip BulletSound;
    public AudioClip EnnemyBulletSound;

    private StarterAssetsInputs _input;


    // Start is called before the first frame update
    void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
        _input = GetComponent<StarterAssetsInputs>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void NoFootstep()
    {
        WalkSource.gameObject.SetActive(false);
        RunSource.gameObject.SetActive(false);  
    }

    public void WalkFootstep()
    {
        WalkSource.gameObject.SetActive(true);
        RunSource.gameObject.SetActive(false);
    }

    public void RunFootstep()
    {
            WalkSource.gameObject.SetActive(false);
            RunSource.gameObject.SetActive(true);

    }
    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
