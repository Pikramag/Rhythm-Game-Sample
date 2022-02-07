using UnityEngine;

public class NoteController : MonoBehaviour
{
    public bool is7K;
    public SpriteRenderer sr;
    public Vector2 StartPos, EndPos;
    public float SetBeat, RealBeat, SongBeatPos;

    void Start(){
        if(is7K){
            transform.localScale = new Vector2(0.75f, 0.75f);
        }
    }

    void Update()
    {
        SongBeatPos = SongManager.instance.songPositionInBeats;
        float PosSet = (SongBeatPos - RealBeat) / (SetBeat - RealBeat); // (обновляемое время - время от спавна = таймер) / (настроенное время - время от спавна = задержка)
            transform.position = Vector2.LerpUnclamped(StartPos, EndPos, PosSet);

        if(PosSet > 1.25f){
            Destroy(gameObject);
            SongManager.instance.Score -= 50;
            SongManager.instance.Combo = 0;
        }
    }
}