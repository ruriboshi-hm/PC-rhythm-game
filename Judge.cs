using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Judge : MonoBehaviour
{
    [SerializeField] private GameObject[] MessageObj;
    [SerializeField] NotesManager notesManager;

    [SerializeField] TextMeshProUGUI comboText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] GameObject clear;

     AudioSource audio;
    [SerializeField] AudioClip hitSound;

    float endTime = 0;

    void Start()
    {
        audio = GetComponent<AudioSource>();
        endTime = notesManager.NotesTime[notesManager.NotesTime.Count-1];
    }

    void Update()
    {
        if (GManager.instance.Start){
            if (Input.GetKeyDown(KeyCode.D)){
                if(notesManager.LaneD[0] == 0){
                    Judgement(GetABS(Time.time - (notesManager.NotesTime[0] + GManager.instance.StartTime)),0);
                }
                // if (notesManager.LaneNum[0] == 0)
                // {
                //     Judgement(GetABS(Time.time - (notesManager.NotesTime[0] + GManager.instance.StartTime)),0,"D,0");
                // }else if (notesManager.LaneNum[1] == 0){
                //     Judgement(GetABS(Time.time - (notesManager.NotesTime[1] + GManager.instance.StartTime)),1,"D,1");
                // }
            }
            if (Input.GetKeyDown(KeyCode.F)){
                if(notesManager.LaneF[0] == 1){
                    Judgement(GetABS(Time.time - (notesManager.NotesTime[0] + GManager.instance.StartTime)),0);
                }
                // if (notesManager.LaneNum[0] == 1){
                //     Judgement(GetABS(Time.time - (notesManager.NotesTime[0] + GManager.instance.StartTime)),0,"F,0");
                // }else if (notesManager.LaneNum[1] == 1){
                //     Judgement(GetABS(Time.time - (notesManager.NotesTime[1] + GManager.instance.StartTime)),1,"F,1");
                // }
            }
            if (Input.GetKeyDown(KeyCode.J)){
                if(notesManager.LaneJ[0] == 2){
                    Judgement(GetABS(Time.time - (notesManager.NotesTime[0] + GManager.instance.StartTime)),0);
                }
                // if (notesManager.LaneNum[0] == 2){
                //     Judgement(GetABS(Time.time - (notesManager.NotesTime[0] + GManager.instance.StartTime)),0,"J,0");
                // }else if (notesManager.LaneNum[1] == 2){
                //     Debug.Log(notesManager.LaneNum[0]);
                //     Debug.Log(notesManager.LaneNum[1]);
                //     Judgement(GetABS(Time.time - (notesManager.NotesTime[1] + GManager.instance.StartTime)),1,"J,1");
                // }
            }
            if (Input.GetKeyDown(KeyCode.K)){
                if(notesManager.LaneK[0] == 3){
                    Judgement(GetABS(Time.time - (notesManager.NotesTime[0] + GManager.instance.StartTime)),0);
                }
                // if (notesManager.LaneNum[0] == 3){
                //     Judgement(GetABS(Time.time - (notesManager.NotesTime[0] + GManager.instance.StartTime)),0,"K,0");
                // }else if (notesManager.LaneNum[1] == 3){
                //     Judgement(GetABS(Time.time - (notesManager.NotesTime[1] + GManager.instance.StartTime)),1,"K,1");
                // }
            }

            if (Time.time > endTime + GManager.instance.StartTime){
                clear.SetActive(true);
                Invoke("ResultScene", 3f);
                return;
            }

            if (0 == notesManager.NotesTime.Count){
                return;
            }

            if(Input.GetKeyDown(KeyCode.Escape)){
                GManager.instance.perfect = 0;
                GManager.instance.great = 0;
                GManager.instance.good = 0;
                GManager.instance.miss = 0;
                GManager.instance.maxScore = 0;
                GManager.instance.ratioScore = 0;
                GManager.instance.score = 0;
                GManager.instance.combo = 0;
                SceneManager.LoadScene("MusicScene");
                return;
            }

            if (Time.time > notesManager.NotesTime[0] + 0.2f + GManager.instance.StartTime){
                message(3);
                deleteData(0);
                //Debug.Log("Miss");
                GManager.instance.miss++;
                GManager.instance.combo = 0;
                //ミス
            }
        }

        if(Input.GetKeyDown(KeyCode.Backspace)){
            GManager.instance.perfect = 0;
            GManager.instance.great = 0;
            GManager.instance.good = 0;
            GManager.instance.miss = 0;
            GManager.instance.maxScore = 0;
            GManager.instance.ratioScore = 0;
            GManager.instance.score = 0;
            GManager.instance.combo = 0;
            SceneManager.LoadScene("MusicSelect");
            return;
        }
        
    }
    void Judgement(float timeLag,int numOffset){
        audio.PlayOneShot(hitSound);
        if (timeLag <= 0.10){
            //Debug.Log("Perfect");
            //Debug.Log(getLog);
            message(0);
            GManager.instance.ratioScore += 5;
            GManager.instance.perfect++;
            GManager.instance.combo++;
            deleteData(numOffset);
        }else if(timeLag <= 0.15){
            //Debug.Log("Great");
            //Debug.Log(getLog);
            message(1);
            GManager.instance.ratioScore += 3;
            GManager.instance.great++;
            GManager.instance.combo++;
            deleteData(numOffset);
        }else if(timeLag <= 0.20){
            //Debug.Log("Good");
            //Debug.Log(getLog);
            message(2);
            GManager.instance.ratioScore += 1;
            GManager.instance.good++;
            GManager.instance.combo = 0;
            deleteData(numOffset);
        }
    }
    float GetABS(float num)//引数の絶対値を返す関数
    {
        if (num >= 0)
        {
            return num;
        }
        else
        {
            return -num;
        }
    }
    void deleteData(int numOffset)
    {
        notesManager.NotesTime.RemoveAt(0);
        notesManager.LaneNum.RemoveAt(0);
        notesManager.NoteType.RemoveAt(0);
        GManager.instance.score = (int)Math.Round(1000000 * Math.Floor(GManager.instance.ratioScore / GManager.instance.maxScore * 1000000) / 1000000);
        comboText.text = GManager.instance.combo.ToString();
        scoreText.text = GManager.instance.score.ToString();
    }

    void message(int judge)//判定を表示する
    {
        Instantiate(MessageObj[judge],new Vector3(notesManager.LaneNum[0]-1.5f,0.76f,0.15f),Quaternion.Euler(45,0,0));
    }

    void ResultScene()
    {
        SceneManager.LoadScene("Result");
    }
}