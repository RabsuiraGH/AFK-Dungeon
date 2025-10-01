using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SW.Utilities.LoadAsset;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace LA.AudioSystem
{
    public class MusicService : IDisposable
    {
        private AudioSource _audioSourcePrefab;
        private readonly Queue<AudioSource> _pool = new Queue<AudioSource>();
        private AudioContainer _audioContainer;

        private AudioSource _nowPlaying;

        public event Action OnMusicClipEnded;


        [VContainer.Inject]
        public void Construct(PathConfig pathConfig)
        {
            _audioSourcePrefab = LoadAssetUtility.Load<AudioSource>(pathConfig.MusicPrefab);
            _audioContainer = AudioContainer.Create();
        }

        public void PlayMusicOnce(Music music, Vector2 position)
        {
            if(_nowPlaying.clip == music.Clip) return;

            PlayMusicClip(music.Clip, position, false, music.Volume);
        }


        public void PlayMusic(Music music, Vector2 position)
        {
            PlayMusicClip(music.Clip, position, music.Loop, music.Volume);
        }


        public void PauseMusic()
        {
            _nowPlaying?.Stop();
        }


        public void ContinueMusic()
        {
            _nowPlaying?.UnPause();
        }

        public void RestartMusic()
        {
            _nowPlaying?.Play();
        }


        private void PlayMusicClip(AudioClip clip, Vector2 position, bool loop, float volume = 1f)
        {
            ReturnToPool(_nowPlaying);
            AudioSource audioSource = GetFromPool();

            audioSource.transform.position = position;
            audioSource.clip = clip;
            audioSource.volume = volume;
            audioSource.gameObject.SetActive(true);
            audioSource.loop = loop;
            audioSource.Play();

            _nowPlaying = audioSource;
            if (!loop)
            {
                float clipLength = audioSource.clip.length;
                _audioContainer.StartCoroutine(ReturnToPoolAfter(audioSource, clipLength));
            }
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
            ReturnToPool(source);
        }


        private void ReturnToPool(AudioSource source)
        {
            if (source == null) return;
            source.Stop();
            source.gameObject.SetActive(false);
            _pool.Enqueue(source);
            OnMusicClipEnded?.Invoke();
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