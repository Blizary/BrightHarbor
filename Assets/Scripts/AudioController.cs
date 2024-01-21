using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] float fadeDuration;
    [SerializeField] private AudioSource exploringAudio;
    [SerializeField] private AudioSource darknessAudio;
    [SerializeField] private AudioSource activeAudioPlayer;

    [SerializeField] private GameEventListener_String onMusicChange;

    private void Awake()
    {
        onMusicChange.Response.AddListener(OnMusicChange);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMusicChange(string _newAudio)
    {
        //Debug.Log("new music request for: "+_newAudio);
        if(_newAudio.Contains("Darkness"))
        {
            StartCoroutine( ChangeMusic(darknessAudio));
        }
        else if (_newAudio.Contains("Exploring"))
        {
            StartCoroutine(ChangeMusic(exploringAudio));
        }

      
    }

    IEnumerator ChangeMusic(AudioSource _newAudio)
    {
        //Debug.Log("NEW MUSIC Processing");
        float timer = 0.0f;
       
        if (activeAudioPlayer != _newAudio)
        {
            _newAudio.enabled = true;
            _newAudio.Play();
            while (timer < fadeDuration)
            {
                float t = timer / fadeDuration;

                // Decrease volume of the first source
                activeAudioPlayer.volume = Mathf.Lerp(1.0f, 0.0f, t);

                // Increase volume of the second source
                _newAudio.volume = Mathf.Lerp(0.0f, 1.0f, t);
                

                timer += Time.deltaTime;
                yield return null; // Wait for the next frame
            }

            // Ensure that volumes are set to their final values
            activeAudioPlayer.volume = 0.0f;
            _newAudio.volume = 1.0f;
            activeAudioPlayer.enabled = false;
            activeAudioPlayer.Stop();
            activeAudioPlayer = _newAudio;
            //Debug.Log("NEW MUSIC ON");
        }
    }


}
