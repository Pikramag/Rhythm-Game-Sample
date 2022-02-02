using UnityEngine.Events;
using System.Collections.Generic;
using UnityEngine;

public class SongManager : MonoBehaviour
{
    public string[] KeyCode7K = new string[7]{"s","d","f","space","j","k","l"};
    public Sprite[] Notes7K;
    public bool is7K = true;
    public static SongManager instance;
    public float scrollSpeed;
    public GameObject prefab;
    GameObject EnSpawner, PlSpawner, PlRemover, EnRemover;
    AudioSource MusicPlayer;
    AudioSource VoicesPlayer;
    public float bpm;
    float songPosition;
    public float songPositionInBeats;
    public float secPerBeat;
    float dspTimeSong;
    public List<Note> SN = new List<Note>();
    public int nextIndex;
    public bool isDownScroll;
    public int Score;
    public int Combo;
    UnityEvent m_SpawnNote;
    
    void Start()
    {
        m_SpawnNote = new UnityEvent();
        instance = this;
        MusicPlayer = GameObject.Find("MusicHolder").GetComponent<AudioSource>();
        VoicesPlayer = GameObject.Find("VoiceHolder").GetComponent<AudioSource>();
        secPerBeat = 60f / bpm;
        dspTimeSong = (float)AudioSettings.dspTime;
        EnSpawner = GameObject.Find("EnemySpawners");
        PlSpawner = GameObject.Find("PlayerSpawners");
        PlRemover = GameObject.Find("ArrowsHolder");
        EnRemover = GameObject.Find("EnemyArrowsHolder");
        MusicPlayer.Play();
        VoicesPlayer.Play();
        Vector2 EnSpawnPos = EnSpawner.transform.position;
        Vector2 PlRemPos = PlRemover.transform.position;
        Vector2 PlSpawnPos = PlSpawner.transform.position;
        Vector2 EnRemPos = EnRemover.transform.position;
        if(isDownScroll){
            PlRemover.transform.position = new Vector2(PlRemPos.x, -3.5f);
            EnRemover.transform.position = new Vector2(EnRemPos.x, -3.5f);
            EnSpawner.transform.position = new Vector2(EnSpawnPos.x, EnRemPos.y + Vector2.Distance(EnSpawnPos, EnRemPos));
            PlSpawner.transform.position = new Vector2(PlSpawnPos.x, PlRemPos.y + Vector2.Distance(PlSpawnPos, PlRemPos));
        } else {
            PlRemover.transform.position = new Vector2(PlRemPos.x, 3.5f);
            EnRemover.transform.position = new Vector2(EnRemPos.x, 3.5f);
            EnSpawner.transform.position = new Vector2(EnSpawnPos.x, EnRemPos.y - Vector2.Distance(EnSpawnPos, EnRemPos));
            PlSpawner.transform.position = new Vector2(PlSpawnPos.x, PlRemPos.y - Vector2.Distance(PlSpawnPos, PlRemPos));
        }

        if(is7K){
            for(int arr = 0; arr < 7; arr++){
                PlRemover.transform.GetChild(arr).transform.GetComponent<ArrowController>().keycode = KeyCode7K[arr];
            }
        } else {

        }
        GameObject opt = Instantiate(prefab, new Vector2(0f, -6f), Quaternion.identity);
        opt.GetComponent<NoteController>().enabled = false;
    }

    void Update()
    {
        songPosition = (float) (AudioSettings.dspTime - dspTimeSong);
        songPositionInBeats = songPosition / secPerBeat;

        for(int i = nextIndex; i < SN.Count && songPositionInBeats > SN[i].time - (4f / scrollSpeed); i++)
        {
            m_SpawnNote.Invoke();
            nextIndex++;
        }
    }

    public void AddSpawnListener(SpawnerScanner scriptToAdd)
    {
        //Very Dangerous if the scriptToAdd does not have the 
        //SpawnNote method you will get an exception
        m_SpawnNote.AddListener(scriptToAdd.SpawnNote);
    }
}

[System.Serializable]
public class Note
{
    public bool isEnemy, isPlayer;
    public int ArrID;
    public float time;
}
