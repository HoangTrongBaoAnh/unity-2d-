using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound{
    public string name;

    public AudioClip clip;

    [Range(0f,1f)]
    public float volume = 0.7f;
    [Range(0.5f, 1.5f)]
    public float pitch = 1f;

    [Range(0f, 0.5f)]
    public float Randomvolume = 0.1f;
    [Range(0f, 0.5f)]
    public float Randomvpitch = 0.1f;

    private AudioSource source;

    public void setsource(AudioSource _source)
    {
        source = _source;
        source.clip = clip;
    }

    public void play()
    {
        source.volume = volume * (1 + Random.Range(-Randomvolume/2f, Randomvolume / 2f));
        source.pitch = pitch * (1 + Random.Range(-Randomvpitch / 2f, Randomvpitch / 2f));
        source.Play();
    }

}

public class Audio_Manager : MonoBehaviour
{
    public static Audio_Manager instance;

    [SerializeField]
    Sound[] sounds;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one audio manager");
        }
        else
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i=0;i<sounds.Length;i++)
        {
            GameObject _go = new GameObject("Sound_" + i + "_" + sounds[i].name);
            _go.transform.SetParent(this.transform);
            sounds[i].setsource(_go.AddComponent<AudioSource>());
        }
    }

    public void playsound(string _name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == _name)
            {
                sounds[i].play();
                return;
            }
        }

        Debug.LogWarning("not found sound on the list ," + _name );
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
