using UnityEngine.Events;
using System.Collections.Generic;
using UnityEngine;

public class SongManager : MonoBehaviour
{
    GameObject PlayerArrows, EnemyArrows;
    public string[] KeyCode7K = new string[7]{"s","d","f","space","j","k","l"};
    public Sprite[] Notes7K;
    public bool is7K = true;
    public static SongManager instance;
    public float scrollSpeed, bpm, songPositionInBeats;
    public GameObject prefab;
    AudioSource MusicPlayer;
    AudioSource VoicesPlayer;
    float songPosition;
    public float secPerBeat;
    float dspTimeSong;
    public List<Note> SN = new List<Note>();
    public bool isDownScroll;
    public int Score;
    public int Combo;
    float preloadTime;
    
    void Start()
    {
        EnemyArrows = GameObject.Find("EnemyArrowsHolder");
        PlayerArrows = GameObject.Find("ArrowsHolder");
        if(isDownScroll){
            EnemyArrows.transform.position = new Vector2(EnemyArrows.transform.position.x, -3.5f);
            PlayerArrows.transform.position = new Vector2(PlayerArrows.transform.position.x, -3.5f);
        } else {
            EnemyArrows.transform.position = new Vector2(EnemyArrows.transform.position.x, 3.5f);
            PlayerArrows.transform.position = new Vector2(PlayerArrows.transform.position.x, 3.5f);
        }

        for(int obj = 0; obj < 7; obj++){
            PlayerArrows.transform.GetChild(obj).GetComponent<ArrowController>().keycode = KeyCode7K[obj];
        }

        instance = this;
        MusicPlayer = GameObject.Find("MusicHolder").GetComponent<AudioSource>();
        VoicesPlayer = GameObject.Find("VoiceHolder").GetComponent<AudioSource>();
        secPerBeat = 60f / bpm;
        dspTimeSong = (float)AudioSettings.dspTime;
        MusicPlayer.Play();
        VoicesPlayer.Play();
        GameObject opt = Instantiate(prefab, new Vector2(0f, -6f), Quaternion.identity);
        opt.GetComponent<NoteController>().enabled = false;

        SongLoader.instance.CreateTextFile();
        SongLoader.instance.WriteChart();
    }

    void Update()
    {
        songPosition = (float) (AudioSettings.dspTime - dspTimeSong);
        songPositionInBeats = songPosition / secPerBeat;

        if(songPositionInBeats > preloadTime - 4f){
            preloadTime += 16f;
            PreloadNotes(preloadTime);
        }
    }

    void PreloadNotes(float preTime){
        for(int i = 0; i < SN.Count && SN[i].time < preTime; i++){
            NoteController script = prefab.GetComponent<NoteController>();
            script.sr.sprite = Notes7K[SN[i].ArrID];
            script.SetBeat = SN[i].time;
            script.RealBeat = SN[i].time - (4f / scrollSpeed);
            if(is7K){
                script.is7K = true;
            } else {
                script.is7K = false;
            }
            Vector2 Pos_Start;
            if(SN[i].isEnemy){
                if(isDownScroll){
                    Pos_Start = new Vector2(
                        EnemyArrows.transform.GetChild(SN[i].ArrID).transform.position.x,
                        EnemyArrows.transform.GetChild(SN[i].ArrID).transform.position.y + 9.5f);
                } else {
                    Pos_Start = new Vector2(
                        EnemyArrows.transform.GetChild(SN[i].ArrID).transform.position.x,
                        EnemyArrows.transform.GetChild(SN[i].ArrID).transform.position.y - 9.5f);
                }
                script.StartPos = Pos_Start;
                script.EndPos = new Vector2(
                    EnemyArrows.transform.GetChild(SN[i].ArrID).transform.position.x,
                    EnemyArrows.transform.GetChild(SN[i].ArrID).transform.position.y);
                Instantiate(prefab, Pos_Start, Quaternion.identity);
            }

            if(SN[i].isPlayer){
                if(isDownScroll){
                    Pos_Start = new Vector2(
                        PlayerArrows.transform.GetChild(SN[i].ArrID).transform.position.x,
                        PlayerArrows.transform.GetChild(SN[i].ArrID).transform.position.y + 9.5f);
                } else {
                    Pos_Start = new Vector2(
                        PlayerArrows.transform.GetChild(SN[i].ArrID).transform.position.x,
                        PlayerArrows.transform.GetChild(SN[i].ArrID).transform.position.y - 9.5f);
                }
                script.StartPos = Pos_Start;
                script.EndPos = new Vector2(
                    PlayerArrows.transform.GetChild(SN[i].ArrID).transform.position.x,
                    PlayerArrows.transform.GetChild(SN[i].ArrID).transform.position.y);
                Instantiate(prefab, Pos_Start, Quaternion.identity);
            }
        }
    }
}

[System.Serializable]
public class Note
{
    public bool isEnemy, isPlayer;
    public int ArrID;
    public float time;
}