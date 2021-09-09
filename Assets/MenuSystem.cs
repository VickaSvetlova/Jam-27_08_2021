using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MenuSystem : MonoBehaviour
{
    [SerializeField] private Slider muzic;
    [SerializeField] private Slider sound;
    [SerializeField] private Toggle toggle;
    [SerializeField] private AudioMixerGroup _audioMixer;
    [SerializeField] private GameObject menu;
    private bool isActive = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isActive)
            {
                Time.timeScale = 0;
                menu.SetActive(true);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.Confined;
                isActive = true;
            }

            else
            {
                Time.timeScale = 1;
                menu.SetActive(false);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.Confined;
                isActive = false;
            }
        }
    }

    public void SetMuzic(float volume)
    {
        _audioMixer.audioMixer.SetFloat("Muzik", Mathf.Lerp(-80, 0, volume));
    }

    public void SetSound(float volume)
    {
        _audioMixer.audioMixer.SetFloat("Sfx", Mathf.Lerp(-80, 0, volume));
    }

    public void SetToggle(bool state)
    {
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}