using UnityEngine;
using static GameManager;

public class HighLightController : MonoBehaviour
{
    private TrailRenderer[] highLightEffect;
    private Vector2[] position;
    private int nextPointIndex0 = 0;
    private int nextPointIndex1 = 2;

    private void Start()
    {
        //エフェクトの通る地点の設定
        position = new Vector2[4];
        position[0] = new Vector2(transform.position.x - 0.75f, transform.position.y - 0.75f);
        position[1] = new Vector2(transform.position.x + 0.75f, transform.position.y - 0.75f);
        position[2] = new Vector2(transform.position.x + 0.75f, transform.position.y + 0.75f);
        position[3] = new Vector2(transform.position.x - 0.75f, transform.position.y + 0.75f);
    }
    private void Update()
    {
        if (situation == Situation.INVENTORY)
        {
            if (highLightEffect == null)
            {
                highLightEffect = new TrailRenderer[2];


                highLightEffect[0] = transform.GetChild(0).gameObject.GetComponent<TrailRenderer>(); //エフェクトを格納
                highLightEffect[0].emitting = false; //エフェクトの停止
                highLightEffect[0].Clear(); //これまで書いたエフェクトをけす
                highLightEffect[0].transform.position = position[0]; //エフェクトの初期位置の設定

                highLightEffect[1] = transform.GetChild(1).gameObject.GetComponent<TrailRenderer>();
                highLightEffect[1].emitting = false;
                highLightEffect[1].Clear();
                highLightEffect[1].transform.position = position[2];
            }

            if (highLightEffect[0].emitting)
            {
                highLightEffect[0].transform.position = Vector2.MoveTowards(highLightEffect[0].transform.position, position[nextPointIndex0], 0.05f); //エフェクトの移動

                if (Vector2.Distance(highLightEffect[0].transform.position, position[nextPointIndex0]) < 0.01f)
                {
                    nextPointIndex0 = (nextPointIndex0 + 1) % 4; //目的位置を変更
                }
            }

            if (highLightEffect[1].emitting)
            {
                highLightEffect[1].transform.position = Vector2.MoveTowards(highLightEffect[1].transform.position, position[nextPointIndex1], 0.05f); //エフェクトの移動

                if (Vector2.Distance(highLightEffect[1].transform.position, position[nextPointIndex1]) < 0.01f)
                {
                    nextPointIndex1 = (nextPointIndex1 + 1) % 4; //目的位置を変更
                }
            }
        }
    }

    public void ClicInventory()
    {
        transform.root.GetComponent<Inventory>().ChangeDetail(highLightEffect[0], highLightEffect[1], transform.parent.parent.GetComponent<ItemDetail>().GetID()); //アイテムの詳細当の変更
    }

}
