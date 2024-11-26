using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneCoin : MonoBehaviour
{
    UIController _UIController;
    GameObject UICoin;
    int CoinCollected;
    // Start is called before the first frame update
    private void Awake()
    {
        UICoin = GameObject.FindGameObjectWithTag("UICoin");
        _UIController = GameObject.FindGameObjectWithTag("Canvas").GetComponent<UIController>();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (UICoin.transform.position != transform.position)
        {
            MoveToUICoin();
        }
        else
        {
            Destroy(gameObject);
            _UIController.CoinText.text = PlayerPrefs.GetInt("Coin").ToString();
        }

    }

    void MoveToUICoin()
    {
        transform.position = Vector3.MoveTowards(transform.position, UICoin.transform.position, Time.deltaTime * 3000);
    }
}
