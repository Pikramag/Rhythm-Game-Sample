using UnityEngine;

public class EnemyArrows : MonoBehaviour
{
    RaycastHit2D hit;
    GameObject note;
    Vector2 direction;
    bool isSet;

    void Update()
    {
        if(!isSet){
            if(SongManager.instance.isDownScroll){
                direction = new Vector2(0f, 1f);
            } else {
                direction = new Vector2(0f, -1f);
            }
            isSet = true;
        } else {
            if(note == null){
                hit = Physics2D.Raycast(transform.position, direction);
            }

            if(hit.collider != null){
                note = hit.collider.gameObject;
            }

            if(note != null && Vector2.Distance(transform.position, note.transform.position) < 0.25f){
                Destroy(note);
            }
        }
    }
}
