using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Pool;

public enum AudioSourceType
{ 
    SFX,
    UI
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private AudioSource sfxAudioSourcePrefab;
    [SerializeField] private AudioSource uiAudioSourcePrefab;

    private AudioSource musicAudioSource;

    // Pool of audio sources for sound effects
    private IObjectPool<AudioSource> sfxPool;
    // Pool of audio sources for ui sounds
    private IObjectPool<AudioSource> uiPool;

    // throw an exception if we try to return an existing item,
    // already in the pool
    [SerializeField] private bool collectionCheck = true;
    // extra options to control the pool capacity and maximun size
    [SerializeField] private int defaultCapacity = 20;
    [SerializeField] private int maxSize = 100;

    private List<AudioSource> spawnSfxAudioSources;
    private List<AudioSource> sfxToRelease;

    private List<AudioSource> spawnUiAudioSources;
    private List<AudioSource> uiToRelease;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        musicAudioSource = GetComponent<AudioSource>();

        sfxPool = new ObjectPool<AudioSource>(
            CreateSfxAudioSource, OnGetFromPool, OnReleaseToPool, OnDestroyPooledObject,
            collectionCheck, defaultCapacity, maxSize);

        uiPool = new ObjectPool<AudioSource>(
            CreateUiAudioSource, OnGetFromPool, OnReleaseToPool, OnDestroyPooledObject,
            collectionCheck, defaultCapacity, maxSize);

        spawnSfxAudioSources = new List<AudioSource>();
        sfxToRelease = new List<AudioSource>();
        spawnUiAudioSources = new List<AudioSource>();
        uiToRelease = new List<AudioSource>();
    }

    private void Start()
    {
        // TODO: try to fix this ...
        // set the volume to mach the slider at start
        SetMasterVolume(1.0f);
        SetMusicVolume(1.0f);
        SetSoundEffectVolume(1.0f);
        SetUISoundVolume(1.0f);
    }

    private void Update()
    {
        foreach(AudioSource sfxAudioSource in spawnSfxAudioSources)
        {
            if (!sfxAudioSource.isPlaying)
            {
                sfxToRelease.Add(sfxAudioSource);
            }
        }
        foreach (AudioSource uiAudioSource in spawnUiAudioSources)
        {
            if (!uiAudioSource.isPlaying)
            { 
                uiToRelease.Add(uiAudioSource);
            }
        }

        foreach (AudioSource sfxAudioSource in sfxToRelease)
        {
            sfxPool.Release(sfxAudioSource);
            spawnSfxAudioSources.Remove(sfxAudioSource);
        }
        sfxToRelease.Clear();
        foreach (AudioSource uiAudioSource in uiToRelease)
        {
            uiPool.Release(uiAudioSource);
            spawnUiAudioSources.Remove(uiAudioSource);
        }
        uiToRelease.Clear();
    }

    public void PlayClip(AudioClip clip, AudioSourceType type)
    {
        switch (type)
        {
            case AudioSourceType.SFX: {
                    AudioSource sfxAudioSource = sfxPool.Get();
                    sfxAudioSource.clip = clip;
                    sfxAudioSource.Play();
                    spawnSfxAudioSources.Add(sfxAudioSource);
            } break;
            case AudioSourceType.UI:
            {
                    AudioSource uiAudioSource = uiPool.Get();
                    uiAudioSource.clip = clip;
                    uiAudioSource.Play();
                    spawnUiAudioSources.Add(uiAudioSource);
            } break;
        }
    }

    public void PlayMusic()
    {
        musicAudioSource.Play();
    }

    public void PauseMusic()
    {
        musicAudioSource.Pause();
    }

    public void StopMusic()
    {
        musicAudioSource.Stop();
    }

    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("MasterVolume", LinearToDecibel(volume));
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("MusicVolume", LinearToDecibel(volume));
    }

    public void SetSoundEffectVolume(float volume)
    {
        audioMixer.SetFloat("SoundEffectVolume", LinearToDecibel(volume));
    }

    public void SetUISoundVolume(float volume)
    {
        audioMixer.SetFloat("UISoundVolume", LinearToDecibel(volume));
    }

    private float LinearToDecibel(float linear)
    {
        float dB;
        if (linear != 0)
            dB = 20.0f * Mathf.Log10(linear);
        else
            dB = -144.0f;
        return dB;
    }


    private AudioSource CreateSfxAudioSource()
    {
        AudioSource sfxAudioSource = Instantiate(sfxAudioSourcePrefab);
        return sfxAudioSource;
    }

    private AudioSource CreateUiAudioSource()
    {
        AudioSource sfxAudioSource = Instantiate(uiAudioSourcePrefab);
        return sfxAudioSource;
    }

    private void OnReleaseToPool(AudioSource pooledObject)
    {
        pooledObject.enabled = false;
    }

    private void OnGetFromPool(AudioSource pooledObject)
    {
        pooledObject.enabled = true;
        pooledObject.Pause();
    }

    private void OnDestroyPooledObject(AudioSource pooledObject)
    {
        Destroy(pooledObject);
    }

}
