using System.Collections;
using UnityEngine;

namespace DefaultNamespace
{
    public class MuzicTrigger : MonoBehaviour
    {

        [SerializeField] private AudioClip clip;
        private AudioSource _audioSource;
        private MeshRenderer renderer;
        public SoundAmbientSystem.AreaSound AreaSound;
        [SerializeField] private float speed = 0.2f;

        private void OnEnable()
        {
            _audioSource = GetComponent<AudioSource>();

            renderer = GetComponent<MeshRenderer>();
            renderer.enabled = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            var temp = other.GetComponent<PlayerController>();
            if (temp != null)
            {
                //if (!_audioSource.isPlaying) {
                    _audioSource.clip = clip;
                    _audioSource.Play();
                    _audioSource.volume = 0;
                    StopAllCoroutines();
                    StartCoroutine(UpMuzic());
                //}
            }
        }

        private void OnTriggerExit(Collider other)
        {
            var temp = other.GetComponent<PlayerController>();
            if (temp != null)
            {
                _audioSource.volume = 1;
                StopAllCoroutines();
                StartCoroutine(HideMuzic());
            }
        }

        private IEnumerator HideMuzic()
        {
            while (_audioSource.volume > 0)
            {
                _audioSource.volume -= Time.deltaTime * speed;
                yield return null;
            }

            _audioSource.Stop();
        }

        private IEnumerator UpMuzic()
        {
            while (_audioSource.volume < 1)
            {
                _audioSource.volume += Time.deltaTime * speed;
                yield return null;
            }
        }
    }
}