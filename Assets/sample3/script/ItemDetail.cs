using UnityEngine;

public class ItemDetail : MonoBehaviour
{
    private int id;
    [SerializeField] private string detail; //ƒAƒCƒeƒ€‚ÌÚ×

    public void SetDetail(string detail)
    {
        this.detail = detail;
    }
    public string GetDetail()
    {
        return detail;
    }
    public void SetID(int id)
    {
        this.id = id;
    }

    public int GetID()
    {
        return id;
    }
}
