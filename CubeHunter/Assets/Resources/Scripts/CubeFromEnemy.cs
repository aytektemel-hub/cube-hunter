using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeFromEnemy : MonoBehaviour
{
    public float LifeTime;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, LifeTime);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
