using UnityEngine;

public class MusicaCombate : MonoBehaviour
{
    public AudioClip musicaFondo;
    private AudioSource audioSrc;

    void Start()
    {
        audioSrc = gameObject.AddComponent<AudioSource>();
        audioSrc.clip = musicaFondo;
        audioSrc.loop = true;
        audioSrc.volume = 0.5f;
        audioSrc.Play();
    }
}
