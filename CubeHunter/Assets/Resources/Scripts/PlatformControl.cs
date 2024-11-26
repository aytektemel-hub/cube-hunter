using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformControl : MonoBehaviour
{
    public PlayerControl _PlayerControl;
    public GameObject EnemiesParent;
    public GameObject FinishLineGameObject;
    Vector3 finalPosition;
    Vector3 StartPosition;
    public bool isThereEnemy;
    public bool isBonusPlatformActive;
    private void Awake()
    {
        finalPosition = FinishLineGameObject.transform.localPosition;
        StartPosition = new Vector3(FinishLineGameObject.transform.position.x, FinishLineGameObject.transform.position.y + 1, FinishLineGameObject.transform.position.z);
        FinishLineGameObject.transform.position = StartPosition;
        isBonusPlatformActive = false;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CheckIsThereEnemy();
        if (isThereEnemy == false)
        {
            SetFinishLinePos();
        }
    }

    public void CheckIsThereEnemy()
    {
        if (EnemiesParent.transform.childCount > 0)
        {
            isThereEnemy = true;
        }
        else
        {
            isThereEnemy = false;
        }
    }
    public void SetFinishLinePos()
    {
        FinishLineGameObject.transform.localPosition = Vector3.MoveTowards(FinishLineGameObject.transform.localPosition, finalPosition, Time.deltaTime * 3);
    }
}
