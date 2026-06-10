using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using static GameManager;

public class Inventory : MonoBehaviour
{
    private GameObject[] obj;
    [SerializeField] private GameObject itemDetail; //アイテムの詳細を表示するオブジェクト
    [SerializeField] private GameObject inventoryBox; //インベントリのボックス(プレハブ)
    private int itemCount; //インベントリ内にあるアイテムの個数

    private TrailRenderer highLight1; //強調エフェクト1
    private TrailRenderer highLight2; //強調エフェクト２

    private void Start()
    {
        obj = new GameObject[items.Count]; //配列を作成
        itemDetail.SetActive(false);
    }

    private void Update()
    {
        if (situation == Situation.NONE)
        {
            if (Mouse.current.rightButton.wasPressedThisFrame)
            {
                //インベントリを開く
                int i = 0;
                GameObject tmp;
                gachaSelect.SetActive(false);

                foreach (Item item in items)
                //for(int i = 0; i < items.Count; i++)
                {
                    if (item.GetQuantity() > 0) //アイテムの個数が１以上であれば表示する
                    {
                        //インベントリのアイテムを作成
                        tmp = Instantiate(inventoryBox, new Vector3((i % 5) * 2 - 7.75f, (int)(i / 5) * (-2) + 3.25f, 0.0f), Quaternion.identity); //インベントリボックス
                        obj[i] = Instantiate(item.GetPrefab(), new Vector3((i % 5) * 2 - 7.75f, (int)(i / 5) * (-2) + 3.25f, 0.0f), Quaternion.identity); //アイテム
                        obj[i].GetComponent<ItemDetail>().SetID(item.GetID()); //作ったアイテムのスクリプト(ItemDetail)にIDをコピーしておく

                        tmp.transform.Find("Canvas/Quantity").GetComponent<TextMeshProUGUI>().text = item.GetQuantity().ToString(); //アイテムの個数の表示

                        //new Vector3((i % 5) * 2 - 4, (int)(i / 5) * (-2) + 4, 0.0f)
                        obj[i].transform.SetParent(transform); //このオブジェクトをアイテムプレハブの親に設定
                        tmp.transform.SetParent(obj[i].transform);　//アイテムプレハブをインベントリボックスの親に設定
                        i++;
                    }
                }
                itemCount = i;

                if (i != 0) //インベントリ内にアイテムがあればインベントリを開く
                {
                    situation = Situation.INVENTORY;
                    itemDetail.SetActive(true);
                }
                else //アイテムがなければデフォルトの画面に戻る
                {
                    situation = Situation.NONE;
                    gachaSelect.SetActive(true);
                }
            }
        }
        else if (situation == Situation.INVENTORY)
        {
            if (Mouse.current.rightButton.wasPressedThisFrame)
            {
                //インベントリを閉じる
                foreach (GameObject tmp in obj)
                {
                    Destroy(tmp);
                }
                gachaSelect.SetActive(true);
                itemDetail.SetActive(false);
                situation = Situation.NONE;
                transform.position = Vector3.zero;
            }

            if (Mouse.current != null && itemCount > 20)
            {
                Vector2 scroll = Mouse.current.scroll.ReadValue(); //マウススクロールの値を取得

                if (scroll.y > 0 && transform.position.y > 0) //マウスを上にスクロール
                {
                    transform.position = new Vector2(transform.position.x, transform.position.y - 1); //このオブジェクトの位置をあげる
                    itemDetail.transform.position = new Vector2(itemDetail.transform.position.x, itemDetail.transform.position.y + 1); //詳細表示オブジェクトの位置を調整
                }
                else if (scroll.y < 0 && transform.position.y < (int)((itemCount + 4) / 5) - 3) //マウスを下にスクロール
                {
                    transform.position = new Vector2(transform.position.x, transform.position.y + 1); //このイブジェクトの位置を下げる
                    itemDetail.transform.position = new Vector2(itemDetail.transform.position.x, itemDetail.transform.position.y - 1); //詳細表示オブジェクトの位置を調整
                }
            }
        }
    }

    //選択したオブジェクトの詳細を表示する
    public void ChangeDetail(TrailRenderer trail1, TrailRenderer trail2, int itemID)
    {

        if(highLight1 != null || highLight2 != null) //強調エフェクトの中身があればエフェクトを停止させる
        {
            highLight1.emitting = false;
            highLight2.emitting = false;
        }

        //選択オブジェクトのエフェクトを開始
        highLight1 = trail1;
        highLight1.emitting = true;
        highLight2 = trail2;
        highLight2.emitting = true;


        Item targetItem = SearchItem(itemID); //IDからアイテムを検索

        itemDetail.transform.Find("Canvas/ItemText/Detail").GetComponent<TextMeshProUGUI>().text = targetItem.GetDetailBubble().transform.Find("Canvas/DetailText").GetComponent<TextMeshProUGUI>().text; //詳細を変更させる

        string quantity = targetItem.GetQuantity().ToString(); //アイテムの個数(int)をstringに変更
        itemDetail.transform.Find("Canvas/ItemText/Quantity").GetComponent<TextMeshProUGUI>().text = quantity; //アイテムの個数の表示を変更

        itemDetail.transform.Find("Canvas/ItemText/Name").GetComponent<TextMeshProUGUI>().text = targetItem.GetPrefab().name.Substring(5);
    }
}
