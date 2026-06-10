using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public static List<Item> items; //それぞれのアイテムのクラスを入れる
    [HideInInspector] public static List<Item> NItems;
    [HideInInspector] public static List<Item> RItems;
    [HideInInspector] public static List<Item> SItems;
    public enum Situation
    {
        NONE,
        INVENTORY,
        GACHASELECT,
        GACHA1_SINGLE,
        GACHA1_MULTI,
        GACHA2_SINGLE,
        GACHA2_MULTI
    }//状態
    /*
     * 0:何もなし
     * 1:インベントリ
     * 
     * 11:ガチャ１の単発
     * 12:ガチャ１の１０連
     * 21:ガチャ２の単発
     * 22:ガチャ２の１０連
     */

    [HideInInspector] public static Situation situation;

    [HideInInspector] public static GameObject gachaSelect;
    [HideInInspector] public static GameObject Empty;

    private void Awake()
    {
        Application.targetFrameRate = 60;

        items = new List<Item>();
        NItems = new List<Item>();
        RItems = new List<Item>();
        SItems = new List<Item>();

        ItemData[] itemDatas = Resources.LoadAll<ItemData>("ItemData"); //Resourcesファイルの中のItemDataをすべてロードする

        foreach (ItemData itemData in itemDatas)
        {
            Item item = new Item(itemData); //アイテム別にクラスを作る
            items.Add(item); //リストに入れる
            if (item.GetPrefab().name.StartsWith("N"))
            {
                NItems.Add(item);
            }
            else if (item.GetPrefab().name.StartsWith("R"))
            {
                RItems.Add(item);
            }
            else if (item.GetPrefab().name.StartsWith("S"))
            {
                SItems.Add(item);
            }
        }

        gachaSelect = GameObject.Find("GachaSelect");
        Empty = transform.Find("Empty").gameObject;
    }

    public static Item SearchItem(int id) //IDからアイテムの検索をする
    {
        foreach(Item item in items)
        {
            if(item.GetID() == id)
            {
                return item;
            }
        }

        return null;
    }
}
