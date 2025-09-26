using System.Collections.Generic;
using System.Linq;
using SW.Utilities.LoadAsset;
using UnityEngine;

namespace LA.AudioSystem
{
    public class SoundFXService
    {
        private AudioSource _audioSourcePrefab;


        [VContainer.Inject]
        public void Construct(PathConfig pathConfig)
        {
            _audioSourcePrefab = LoadAssetUtility.Load<AudioSource>(pathConfig.AudioSourcePrefab);
        }

        public void PlaySoundFXByName(ICollection<Sound> sounds, Vector2 position, string name)
        {
            Sound firstOrDefault = sounds.FirstOrDefault(x => x.Name == name);
            PlaySoundFX(firstOrDefault, position);
        }
        public void PlayRandomSoundFX(ICollection<Sound> sounds, Vector2 position)
        {
            PlaySoundFX(sounds.ElementAt(Random.Range(0, sounds.Count)), position);
        }


        public void PlaySoundFX(Sound sound, Vector2 position)
        {
            PlaySoundFXClip(sound.Clip, position, sound.Volume);
        }


        public void PlayRandomSoundFXClip(ICollection<AudioClip> clips, Vector2 position, float volume = 1f)
        {
            PlaySoundFXClip(clips.ElementAt(Random.Range(0, clips.Count)), position, volume);
        }


        public void PlaySoundFXClip(AudioClip clip, Vector2 position, float volume = 1f)
        {
            AudioSource audioSource =
                Object.Instantiate(_audioSourcePrefab, position, Quaternion.identity);

            audioSource.clip = clip;
            audioSource.volume = volume;
            audioSource.Play();

            float clipLength = audioSource.clip.length;
            Object.Destroy(audioSource.gameObject, clipLength);
        }
    }
}