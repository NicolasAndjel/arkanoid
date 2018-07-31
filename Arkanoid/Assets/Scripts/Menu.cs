using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour {

    public AudioClip menuSound;
    private AudioSource source;
    private float volLowRange = 0.5f;
    private float volHighRange = 1.0f;

    void Start()
    {
        float vol = Random.Range(volLowRange, volHighRange);
        source = GetComponent<AudioSource>();
        source.PlayOneShot(menuSound, vol);
    }

    public void LoadScene(string sceneName)
    {
        Application.LoadLevel(sceneName);

    }
}
