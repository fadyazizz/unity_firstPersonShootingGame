
using UnityEngine;
using System;
using UnityEngine.Audio;


public class AudioManager : MonoBehaviour
{
    public SoundEffects[] soundEffects;
    public Music[] music;
    private AudioSource source;
    public static AudioManager instance;
    public bool mute;
    // void start(){
    // source = GetComponent<AudioSource>();
    // }
    void Awake()
    {
if(instance==null){
    instance=this;
}else{
    Destroy(gameObject);
    return;
}
        DontDestroyOnLoad(gameObject);

        float musicValue = PlayerPrefs.GetFloat("musicValue");
        float effectsValue = PlayerPrefs.GetFloat("effectsValue");

        foreach (SoundEffects s in soundEffects)
       {
           s.source = gameObject.AddComponent<AudioSource>();
           s.source.clip = s.clip;
           s.source.volume= effectsValue;
           //s.source.pitch=s.pitch;
           s.source.loop = s.loop;
       }
        foreach (Music m in music)
        {
            m.source = gameObject.AddComponent<AudioSource>();
            m.source.clip = m.clip;
            m.source.volume = musicValue;
            //s.source.pitch = s.pitch;
            m.source.loop = m.loop;
        }
    }

    public void AdjustVolume()
    {
        float musicValue = PlayerPrefs.GetFloat("musicValue");
        float effectsValue = PlayerPrefs.GetFloat("effectsValue");
        foreach (SoundEffects s in soundEffects)
        {
            s.source.volume = effectsValue;
            if (s.name == "EnemyFoot")
            {
                s.source.volume = effectsValue / 10;
            }
        }
        foreach (Music m in music)
        {
            m.source.volume = musicValue;
        }
    }

    void Start() {
       // Play("menu");
    }

    // Update is called once per frame
    public void Play (string name){

        SoundEffects s = Array.Find(soundEffects, sound => sound.name==name);
        Music m = Array.Find(music, mu => mu.name == name);
        if (s==null){
            if (m == null)
            {
                Debug.Log("play not found:" + name);
                return;
            }
            else
            {
                m.source.PlayOneShot(m.clip);
            }

        }
        else
        {
            s.source.PlayOneShot(s.clip);
        }
            
        
    }
    public void Play2(string name)
    {

        SoundEffects s = Array.Find(soundEffects, sound => sound.name == name);
        Music m = Array.Find(music, mu => mu.name == name);
        if (s == null)
        {
            if (m == null)
            {
                Debug.Log("play not found:" + name);
                return;
            }
            else
            {
                m.source.PlayOneShot(m.clip);
            }

        }
        else
        {
            s.source.PlayOneShot(s.clip);
        }


    }
    public void Stop (string name){

        SoundEffects s = Array.Find(soundEffects, sound => sound.name==name);
        Music m = Array.Find(music, mu => mu.name == name);
        if (s == null)
        {
            if (m == null)
            {
                Debug.Log("play not found:" + name);
                return;
            }
            else
            {
                m.source.Stop();
            }

        }
        else
        {
            s.source.Stop();
        }
        
    }

    /*
    public void Mute (){


            if(mute){
            mute=false;
                    foreach (SoundEffects s in soundEffects)
       {
           s.source.volume=s.volume;

       }
        }else{
        foreach (SoundEffects s in soundEffects)
       {

           s.source.volume=0;

       }
            mute=true;
        }
    }


        public void MuteOne (string name){


        SoundEffects s = Array.Find(soundEffects, sound => sound.name==name);
        if(s==null){
            Debug.Log("play not found:" + name);

            return;
        }
            s.source.volume=0;
    }

    public void UnmuteOne (string name){


        SoundEffects s = Array.Find(soundEffects, sound => sound.name==name);
        if(s==null){
            Debug.Log("play not found:" + name);

            return;
        }
            s.source.volume=s.volume;
    }
    */

}
