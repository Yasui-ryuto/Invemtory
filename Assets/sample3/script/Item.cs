using UnityEngine;

public class Item
{
    private ItemData itemData;
    private int quantity = 0;

    public Item(ItemData itemData) //デフォルトコンストラクタ
    {
        this.itemData = itemData;
    }

    public int GetID() //このアイテムのIDを返す
    {
        return itemData.ID;
    }

    public GameObject GetPrefab() //このアイテムのプレハブを返す
    {
        return itemData.item;
    }
   
    public GameObject GetDetailBubble() //このアイテムの吹き出しのプレハブを返す
    {
        return itemData.detailBubble;
    }

    public void IncreaseQuantity(int increse) //アイテムの個数を増やす
    {
        this.quantity += increse;
    }
    public int GetQuantity() //アイテムの個数を返す
    {
        return quantity;
    }
}
