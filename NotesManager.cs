using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Data
{
    public string name;
    public int maxBlock;
    public int BPM;
    public int offset;
    public Note[] notes;

}

[Serializable]
public class Note
{
    public int type;
    public int num;
    public int block;
    public int LPB;
}

public class NotesManager : MonoBehaviour
{
    public int noteNum;
    private string songName;

    public List<int> LaneNum = new List<int>();
    public List<int> NoteType = new List<int>();
    public List<float> NotesTime = new List<float>();
    public List<GameObject> NotesObj = new List<GameObject>();

    
    public List<int> LaneD = new List<int>();
    public List<int> LaneF = new List<int>();
    public List<int> LaneJ = new List<int>();
    public List<int> LaneK = new List<int>();


    private float NotesSpeed;
    [SerializeField] GameObject noteObj;

    [SerializeField] SongDataBase dataBase;

    void OnEnable()
    {
        NotesSpeed = GManager.instance.noteSpeed;
        noteNum = 0;
        songName = dataBase.songData[GManager.instance.songID].songName;
        Load(songName);
    }

    private void Load(string SongName)
    {
        string inputString = Resources.Load<TextAsset>(SongName).ToString();
        Data inputJson = JsonUtility.FromJson<Data>(inputString);

        noteNum = inputJson.notes.Length;
        GManager.instance.maxScore = noteNum * 5;

        //要修正
        for (int i = 0; i < inputJson.notes.Length; i++){
            float kankaku = 60 / (inputJson.BPM * (float)inputJson.notes[i].LPB);
            float beatSec = kankaku * (float)inputJson.notes[i].LPB;
            float time = (beatSec * inputJson.notes[i].num / (float)inputJson.notes[i].LPB) + inputJson.offset * 0.01f;
            NotesTime.Add(time);
            LaneNum.Add(inputJson.notes[i].block);

            if(inputJson.notes[i].block == 0){
                LaneD.Add(0);
            }else if(inputJson.notes[i].block == 1){
                LaneF.Add(1);
            }else if(inputJson.notes[i].block == 2){
                LaneJ.Add(2);
            }else if(inputJson.notes[i].block == 3){
                LaneK.Add(3);
            }

            NoteType.Add(inputJson.notes[i].type);

            float z = NotesTime[i] * NotesSpeed;
            NotesObj.Add(Instantiate(noteObj, new Vector3(inputJson.notes[i].block - 1.5f, 0.55f, z), Quaternion.identity));
        }
    }
}