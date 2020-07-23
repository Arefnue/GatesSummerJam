using UnityEngine;

namespace Managers
{
    public class AudioManager : MonoBehaviour
    {
        #region Singleton

        public static AudioManager Manager;


        private void Awake()
        {
            if (Manager == null)
            {
                Manager = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }


        #endregion
        
        
        public AudioSource musicSource;
        public AudioSource sfxSource;
        public AudioSource sfxTunnelsSource;
        public AudioSource bulletSfx;

        public void PlaySfx(AudioClip clip)
        {
            sfxSource.PlayOneShot(clip);
        }

        public void PlayMusic(AudioClip clip)
        {
            musicSource.clip = clip;
            musicSource.Play();
        }
        
        
        public void PlaySfxTunnels(AudioClip clip)
        {
            sfxTunnelsSource.PlayOneShot(clip);
        }

        
        public void PlaySfxBullet(AudioClip clip)
        {
            bulletSfx.PlayOneShot(clip);
        }

        public void PlayConversationSound(AudioClip clip)
        {
            StopConversationSound();

            bulletSfx.clip = clip;
            bulletSfx.Play();
        }

        public void StopConversationSound()
        {
            if (bulletSfx.isPlaying)
            {
                bulletSfx.Stop();
            }
            
        }
        
        
    }
}
