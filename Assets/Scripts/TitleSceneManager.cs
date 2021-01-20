using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneManager : MonoBehaviour
{
    [SerializeField] AudioClip StartSE;
    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void OnStartButton()
    { 
        audioSource.PlayOneShot(StartSE);
        SceneManager.LoadScene("Battle");
        DontDestroyOnLoad(this);
    }
}
