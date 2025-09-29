using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SW.Utilities.LoadAsset;
using Unity.Mathematics;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace LA.AudioSystem
{
    public class SoundFXService : IDisposable
    {
        private AudioSource _audioSourcePrefab;
        private readonly Queue<AudioSource> _pool = new Queue<AudioSource>();
        private AudioContainer _audioContainer;


        [VContainer.Inject]
        public void Construct(PathConfig pathConfig)
        {
            _audioSourcePrefab = LoadAssetUtility.Load<AudioSource>(pathConfig.AudioSourcePrefab);
            _audioContainer = AudioContainer.Create();
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
            AudioSource audioSource = GetFromPool();

            audioSource.transform.position = position;
            audioSource.clip = clip;
            audioSource.volume = volume;
            audioSource.gameObject.SetActive(true);
            audioSource.Play();

            float clipLength = audioSource.clip.length;
            _audioContainer.StartCoroutine(ReturnToPoolAfter(audioSource, clip.length));
        }


        private AudioSource GetFromPool()
        {
            if (_pool.Count > 0)
            {
                return _pool.Dequeue();
            }

            AudioSource newSource = Object.Instantiate(_audioSourcePrefab, _audioContainer.transform);
            return newSource;
        }


        private IEnumerator ReturnToPoolAfter(AudioSource source, float delay)
        {
            yield return new WaitForSeconds(delay);
            source.Stop();
            source.gameObject.SetActive(false);
            _pool.Enqueue(source);
        }


        public void Dispose()
        {
            AudioSource o;
            _pool.TryDequeue(out o);
            while (o != null)
            {
                Object.Destroy(o.gameObject);
                _pool.TryDequeue(out o);
            }
        }
    }
}