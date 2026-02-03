using UnityEngine;
using UnityEngine.UI;

public class BackgroundManager : MonoBehaviour
{
    [SerializeField] SongDataBase dataBase;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(dataBase.songData[GManager.instance.songID].backgroundImage != null){
            GetComponent<Image>().sprite = dataBase.songData[GManager.instance.songID].backgroundImage;
            GetComponent<Image>().color = new Color(150, 150, 150, 255);
        }
    }
}
