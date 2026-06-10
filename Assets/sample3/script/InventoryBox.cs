using UnityEngine;
using static GameManager;

public class InventoryBox : MonoBehaviour
{
    private bool onMouse;
    private int onMouseFrame = 0;
    [SerializeField] private int detailBubleOutFrame = 180;
    private GameObject detailBubble;


    private void Start()
    {
        detailBubble = Empty;
    }
    private void Update()
    {
        if (situation != Situation.INVENTORY)
        {
            if (onMouse)
            {
                //マウスがこのオブジェクトの上にある時
                if (onMouseFrame < detailBubleOutFrame)
                {
                    onMouseFrame++;
                }
                else if (onMouseFrame == detailBubleOutFrame)
                {
                    detailBubble = Instantiate(SearchItem(GetComponentInParent<ItemDetail>().GetID()).GetDetailBubble(), new Vector3(transform.position.x - 1f, transform.position.y + 0.5f, 0.0f), Quaternion.identity); //吹き出しを作成
                    onMouseFrame++;
                }
            }
        }
    }
    private void OnMouseEnter() //マウスがこのオブジェクトの上に入ったときに実行
    {
        if (situation != Situation.INVENTORY)
        {
            onMouse = true;
        }
    }

    private void OnMouseExit() //マウスがこのオブジェクトから離れたときに実行
    {
        if (situation != Situation.INVENTORY)
        {
            if (detailBubble != Empty) //吹き出しがあれば吹き出しを消す
            {
                Destroy(detailBubble);
                detailBubble = Empty;
            }

            onMouse = false;
            onMouseFrame = 0;
        }
    }
}
