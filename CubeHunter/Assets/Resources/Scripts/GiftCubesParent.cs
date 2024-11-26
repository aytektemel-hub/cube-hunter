using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftCubesParent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool isThereGiftCube()
    {
        if (gameObject.transform.childCount > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
