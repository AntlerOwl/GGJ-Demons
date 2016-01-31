using UnityEngine;
using System.Collections;

public class Demon : MonoBehaviour
{
    public string demonName = "Demon";
    public Sprite icon;
    public int worth = 0;
    public AudioClip clip;
    private AudioSource source;

    void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    public void PlayAudio()
    {
        if (!source || !clip) return;

        source.PlayOneShot(clip);
    }
}
