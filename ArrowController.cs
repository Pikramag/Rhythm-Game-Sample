using UnityEngine;

public class ArrowController : MonoBehaviour
{
    RaycastHit2D hit;
    Vector2 direction;
    Animator anim;
    public string keycode;
    GameObject note;
    bool isSet;

    void Start()
    {
        anim = GetComponent<Animator>();
    }
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
            if(Input.GetKey(keycode)){
                hit = Physics2D.Raycast(transform.position, direction);
                if(hit.collider != null && hit.collider.tag == "Note"){
                    note = hit.collider.gameObject;
                }
                if(note != null && Vector2.Distance(transform.position, note.transform.position) < 1.25f){
                    Destroy(note);
                    SongManager.instance.Score += 100;
                    SongManager.instance.Combo += 1;
                }
                anim.SetInteger("AnimState", 1);
            } else {
                anim.SetInteger("AnimState", 0);
            }
        }
    }
}