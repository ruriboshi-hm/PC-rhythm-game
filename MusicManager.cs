using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MusicManager : MonoBehaviour
{
    [SerializeField] SongDataBase dataBase;

    [SerializeField] Image songDataImage;

    [SerializeField] TextMeshProUGUI songNameText;

    AudioSource audio;
    AudioClip Music;
    string songName;
    bool played;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GManager.instance.Start = false;
        songName = dataBase.songData[GManager.instance.songID].songName;
        audio = GetComponent<AudioSource>();
        Music = (AudioClip)Resources.Load("Musics/" + songName);
        played = false;
        songNameText.text = songName;
        songDataImage.sprite = dataBase.songData[GManager.instance.songID].songImage;
        songNameText.gameObject.SetActive(true);
        songDataImage.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)&&!played)
        {
            GManager.instance.Start = true;
            GManager.instance.StartTime = Time.time;
            played = true;
            audio.PlayOneShot(Music);
            songNameText.gameObject.SetActive(false);
            songDataImage.gameObject.SetActive(false);
        }

        if(Input.GetKeyDown(KeyCode.Escape)){
            audio.Stop();
        }

        if(Input.GetKeyDown(KeyCode.Backspace)){
            audio.Stop();
        }
    }
}
