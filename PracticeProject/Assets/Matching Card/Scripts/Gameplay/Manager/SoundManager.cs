using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;


    public AudioClip bg = null;
    public AudioClip buttonClick = null;
    public AudioClip cardMatch = null;
    public AudioClip win = null;
    public AudioClip fail = null;
    public AudioClip wrong = null;
    public AudioClip[] cardSounds = null;


    public AudioListener audioListener = null;
    public AudioSource bgSource = null;
    public AudioSource effect1 = null;
    public AudioSource effect2 = null;
    public AudioSource effect3 = null;
    public AudioSource effect4 = null;
    public AudioSource effect5 = null;
    public AudioSource effect6 = null;
    public AudioSource effect7 = null;
    public AudioSource repeatingSound = null;


    public void setBgVolume(float target)
    {
        if (GameConstants.isMusicOn())
        {
            target = Mathf.Clamp(target, 0.1f, 0.99f);
            if (bgSource)
            {
                bgSource.volume = target;
            }
        }
        else
        {
            bgSource.Stop();
        }
    }

    public void onCardClickSound() {
        playsound(cardSounds[Random.Range(0,cardSounds.Length)]);

    }

    public void UpdateBgSound(AudioClip target)
    {
        if (GameConstants.isMusicOn())
        {

            if (bgSource != null)
            {
                if (bgSource.clip != target)
                {
                    bgSource.clip = target;
                }
                //bgSource.volume =1f;

                if (bgSource.enabled)
                {
                    bgSource.Play();

                }
            }
        }
        else
        {
            bgSource.Stop();
        }
    }
    public void setBgSound(bool target)
    {
        if (GameConstants.isMusicOn())
        {

            if (bgSource != null)
            {
                if (target)
                {
                    bgSource.Play();
                }
                else
                {
                    bgSource.Stop();
                }
            }
        }
        else
        {
            bgSource.Stop();
        }
    }
    public void repeatsound(AudioClip b)
    {
        if (GameConstants.isSoundOn())
        {
            if (repeatingSound.isPlaying)
            {
                repeatingSound.Stop();
            }
            repeatingSound.clip = b;
            repeatingSound.Play();
        }
        else
        {
            stopalleffect();
        }
    }

    public void playsound(AudioClip b, float volume)
    {
       

        if (GameConstants.isSoundOn())
        {
            AudioSource target = effect1;
            if (!effect1.isPlaying)
            {
                target = effect1;
            }
            else if (!effect2.isPlaying)
            {
                target = effect2;
            }
            else if (!effect3.isPlaying)
            {
                target = effect3;
            }
            else if (!effect4.isPlaying)
            {
                target = effect4;
            }
            else if (!effect5.isPlaying)
            {
                target = effect5;
            }
            else if (!effect6.isPlaying)
            {
                target = effect6;
            }
            else if (!effect7.isPlaying)
            {
                target = effect7;
            }
            else
            {
                target = effect1;
                effect1.Stop();
            }
            target.clip = b;
            target.Play();
            target.volume = volume;





        }
        else
        {
            stopalleffect();
        }

    }
    public void playsound(AudioClip b)
    {


        if (GameConstants.isSoundOn())
        {
            AudioSource target = effect1;
            if (!effect1.isPlaying)
            {
                target = effect1;
            }
            else if (!effect2.isPlaying)
            {
                target = effect2;
            }
            else if (!effect3.isPlaying)
            {
                target = effect3;
            }
            else if (!effect4.isPlaying)
            {
                target = effect4;
            }
            else if (!effect5.isPlaying)
            {
                target = effect5;
            }
            else if (!effect6.isPlaying)
            {
                target = effect6;
            }
            else if (!effect7.isPlaying)
            {
                target = effect7;
            }
            else
            {
                target = effect1;
                effect1.Stop();
            }
            target.clip = b;
            target.Play();
            target.volume = 1;




        }
        else
        {
            stopalleffect();
        }

    }
    public void stopalleffect()
    {
        effect1.Stop();
        effect2.Stop();
        effect3.Stop();
        effect4.Stop();
        effect5.Stop();
        effect6.Stop();
        effect7.Stop();
        repeatingSound.Stop();
    }

    private void Awake()
    {

        instance = this;
        UpdateBgSound(bg);
    }


}
