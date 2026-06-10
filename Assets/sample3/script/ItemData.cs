using UnityEngine;

//読み取りだけのファイルを作る
[CreateAssetMenu(menuName = "ItemData")]
public class ItemData : ScriptableObject
{
    [Header("アイテムの識別")]
    [SerializeField] private int itemID; //アイテムの固有ID
    [SerializeField] private GameObject itemPrefab; //アイテム自体のプレハブ
    [SerializeField] private GameObject itemDetailBubble; //アイテムの詳細吹きだし
    [SerializeField] private string itemDetail; //
    
    public int ID => itemID;
    public GameObject item => itemPrefab;
    public GameObject detailBubble => itemDetailBubble;
    public string detail => itemDetail;
    

    public void SetItemData(int id, GameObject prefab, GameObject detailBubble)
    {
        itemID = id;
        itemPrefab = prefab;
        itemDetailBubble = detailBubble;
        //itemDetail = detail;
    }
}
