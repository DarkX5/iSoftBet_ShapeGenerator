using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioActions : MonoBehaviour
{
    [SerializeField] private GameObject onImage = null;
    [SerializeField] private GameObject offImage = null;
    // private bool isAudioEnabled = false;

    private void Start() {
        UpdateButtonUI();
    }

    public void ToggleAudio()
    {
        AudioListener.pause = !AudioListener.pause;
        UpdateButtonUI();
    }

    private void UpdateButtonUI()
    {
        if (AudioListener.pause)
        {
            // audio PAUSED 
            onImage.SetActive(false);
            offImage.SetActive(true);
        }
        else
        {
            // audio NOT paused
            onImage.SetActive(true);
            offImage.SetActive(false);
        }
    }
}
