using UnityEngine;
using static GameManager;
using UnityEngine.UI;

public class GachaSelectButton : MonoBehaviour
{
    private Gacha Gacha;
    GameObject[] selectButton;

    private void Start()
    {
        Gacha = gameObject.GetComponentInParent<Gacha>();
        selectButton = new GameObject[2];


        selectButton[0] = gachaSelect.transform.Find("GachaSelectButton/Left").gameObject;
        selectButton[0].GetComponent<Button>().interactable = false;
        selectButton[1] = gachaSelect.transform.Find("GachaSelectButton/Right").gameObject;
        selectButton[1].GetComponent<Button>().interactable = true;

    }

    public void GachaButton(int num)
    {
        Situation situ;
        switch (num)
        {
            case 11:
                situ = Situation.GACHA1_SINGLE;
                break;
            case 12:
                situ = Situation.GACHA1_MULTI;
                break;
            case 21:
                situ = Situation.GACHA2_SINGLE;
                break;
            case 22:
                situ = Situation.GACHA2_MULTI;
                break;
            default:
                situ = Situation.NONE;
                break;
        }

        if (situation == Situation.NONE)
        {
            gachaSelect.SetActive(false);
            StartCoroutine(Gacha.ToGacha(situ, () =>
            {
                gachaSelect.SetActive(true);
            }));
        }
    }

    public void GachaSelectLeft()
    {
        gachaSelect.transform.position = new Vector3(-15f, 0f, 0);
        selectButton[0].GetComponent<Button>().interactable = true;
        selectButton[1].GetComponent<Button>().interactable = false;
    }

    public void GachaSelectRight()
    {
        gachaSelect.transform.position = Vector3.zero;
        selectButton[0].GetComponent<Button>().interactable = false;
        selectButton[1].GetComponent<Button>().interactable = true;
    }
}
