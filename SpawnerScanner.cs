using UnityEngine;

public class SpawnerScanner : MonoBehaviour
{
    float ScrSpd;
    float bpmSongTime = 0f;
    int noteIndex = 0;
    int arrIndex;
    GameObject notePrefab;
    bool isEnemy, isPlayer, isSet;
    Sprite noteSprite;

    void Update()
    {
        if(!isSet){
            //SongManager.instance.AddSpawnListener(this.gameObject);
            ScrSpd = SongManager.instance.scrollSpeed;
            if(transform.parent.gameObject.name == "EnemySpawners"){
                isEnemy = true;
            } else {
                isEnemy = false;
            }
            for(int ID = 0; ID < transform.parent.transform.childCount; ID++){
                if(gameObject.name == transform.parent.transform.GetChild(ID).gameObject.name){
                    arrIndex = ID;
                }
            }

            notePrefab = SongManager.instance.prefab;
            noteSprite = SongManager.instance.Notes7K[arrIndex];
            isSet = true;
        } else {
            bpmSongTime = SongManager.instance.songPositionInBeats;
            noteIndex = SongManager.instance.nextIndex;
        }
    }

    //public void SpawnNote()
    //{
        
    //}
}