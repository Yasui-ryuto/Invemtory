using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using static GameManager;

public class Gacha : MonoBehaviour
{
    [SerializeField] private GameObject[] prize;
    private bool skip = false;

    [Header("ガチャ２の確率")]
    
    [SerializeField] private int NRate;
    [SerializeField] private int RRate;
    [SerializeField] private int SRate;


    private void Start()
    {
        RateErrorCheck(); //それぞれの確率があっているかの確認
    }

    private void RateErrorCheck()
    {
        if (NRate < 0 || RRate < 0 || SRate < 0) //値がマイナスになっていないかの確認
        {
            Debug.LogError("値がマイナスになっている");
        }
        else if (NRate < RRate || RRate < SRate || NRate + RRate + SRate != 100)
        {
            Debug.LogError("ガチャ2の確率が不適切");
            //どこが間違っているか
            if (NRate < RRate)
            {
                Debug.LogError("NRate < RRate");
            }
            if (RRate < SRate)
            {
                Debug.LogError("RRate < SRate");
            }
            if (NRate + RRate + SRate != 100)
            {
                Debug.LogError("NRate + RRate + SRate != 100");
            }
        }
    }


    private void Update()
    {
        //ガチャ中に右クリックかスペースボタンを押すとスキップされる
        if ((Keyboard.current.spaceKey.wasPressedThisFrame || Mouse.current.leftButton.wasPressedThisFrame) && (situation != Situation.NONE && situation != Situation.INVENTORY))
        {
            skip = true;
        }
    }

    public IEnumerator ToGacha(Situation situ, Action finish)
    {
        GameObject[] item = new GameObject[10]; //ガチャ結果を入れる配列

        //ガチャの判別
        switch (situ)
        {
            case Situation.GACHA1_SINGLE:
                situation = Situation.GACHA1_SINGLE;
                yield return StartCoroutine(Gacha1_single(item, 0));
                break;
            case Situation.GACHA1_MULTI:
                situation = Situation.GACHA1_MULTI;
                yield return StartCoroutine(Gacha1_multi(item));
                break;
            case Situation.GACHA2_SINGLE:
                situation = Situation.GACHA2_SINGLE;
                yield return StartCoroutine(Gacha2_single(item, 0));
                break;
            case Situation.GACHA2_MULTI:
                situation = Situation.GACHA2_MULTI;
                yield return StartCoroutine(Gacha2_multi(item));
                break;
        }

        yield return new WaitForSeconds(3);

        //ガチャリザルトを消す
        foreach (GameObject obj in item)
        {
            Destroy(obj);
        }

        situation = Situation.NONE;
        skip = false;
        finish.Invoke();
    }

    private IEnumerator Gacha1_single(GameObject[] item, int i)
    {
        //ガチャで出てきたアイテムを１つずつ出す
        var prizeItem = items[UnityEngine.Random.Range(0, items.Count)]; //アイテムの中からランダムで選択する
        prizeItem.IncreaseQuantity(1); //選んだアイテムの所持数を1増やす
        item[i] = Instantiate(prizeItem.GetPrefab(), Vector3.zero, Quaternion.identity); //プレハブの生成
        item[i].GetComponent<Animator>().SetBool("animStart", true); //アニメーションのanimStartをtrueにする

        if (!skip)
        {
            yield return new WaitForSeconds(1);
        }

        item[i].GetComponent<Animator>().SetBool("animStart", false); //アニメーションのanimStartをfalseにする

    }
    private IEnumerator Gacha1_multi(GameObject[] item)
    {
        //ガチャを１０回引く
        for (int i = 0; i < 10; i++)
        {
            yield return StartCoroutine(Gacha1_single(item, i));

            item[i].SetActive(false);
        }

        //ガチャリザルトの表示
        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                item[i * 5 + j].SetActive(true);
                item[i * 5 + j].transform.position = new Vector3(j * 2 - 4, i * -2 + 1, 0.0f);
            }
        }
    }


    private IEnumerator Gacha2_single(GameObject[] item, int i)
    {
        int rand = UnityEngine.Random.Range(0, 100);
        Item prizeItem;

        if (rand <= NRate)
        {
            prizeItem = NItems[UnityEngine.Random.Range(0, NItems.Count)]; //アイテムの中からランダムで選択する
        }
        else if (rand <= NRate + RRate)
        {
            prizeItem = RItems[UnityEngine.Random.Range(0, RItems.Count)]; //アイテムの中からランダムで選択する
        }
        else
        {
            prizeItem = SItems[UnityEngine.Random.Range(0, SItems.Count)]; //アイテムの中からランダムで選択する
        }

        prizeItem.IncreaseQuantity(1); //選んだアイテムの所持数を1増やす
        item[i] = Instantiate(prizeItem.GetPrefab(), Vector3.zero, Quaternion.identity); //プレハブの生成
        item[i].GetComponent<Animator>().SetBool("animStart", true); //アニメーションのanimStartをtrueにする

        if (!skip)
        {
            yield return new WaitForSeconds(1); //１秒待つ
        }

        item[i].GetComponent<Animator>().SetBool("animStart", false);
    }
    public IEnumerator Gacha2_multi(GameObject[] item)
    {
        //ガチャを10回引く
        for (int i = 0; i < 10; i++)
        {
            yield return StartCoroutine(Gacha2_single(item, i));

            item[i].SetActive(false);
        }

        //ガチャリザルトの表示
        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                item[i * 5 + j].SetActive(true);
                item[i * 5 + j].transform.position = new Vector3(j * 2 - 4, i * -2 + 1, 0.0f);
            }
        }
    }
}
