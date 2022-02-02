using UnityEngine;

public class NoteController : MonoBehaviour
{
    public SpriteRenderer sr;
    public GameObject Spawner;
    public GameObject Remover;
    Vector2 StartPos;
    Vector2 EndPos;
    public float SetBeat, RealBeat, SongBeatPos;
    bool isSet;
    void Update()
    {
        if(!isSet){
            Vector2 SpawnPos = Spawner.transform.position;
            Vector2 RemoverPos = Remover.transform.position;
            StartPos = new Vector2(RemoverPos.x, SpawnPos.y);
            if(SongManager.instance.isDownScroll){
                EndPos = new Vector2(RemoverPos.x, RemoverPos.y - Vector2.Distance(SpawnPos, RemoverPos));
            } else {
                EndPos = new Vector2(RemoverPos.x, RemoverPos.y + Vector2.Distance(SpawnPos, RemoverPos));
            }
            if(StartPos != null && EndPos != null){
                isSet = true;
            }
        } else {
            SongBeatPos = SongManager.instance.songPositionInBeats;
            float PosSet = (SongBeatPos - RealBeat) / (SetBeat - RealBeat); // (обновляемое время - время от спавна = таймер) / (настроенное время - время от спавна = задержка)
            transform.position = Vector2.LerpUnclamped(StartPos, EndPos, PosSet);

            if(PosSet > 1f){
                Destroy(gameObject);
            }
        }
    }
}