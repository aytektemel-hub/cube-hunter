using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiredCube : MonoBehaviour
{
    public PlatformControl _PlatformControl;
    public PlayerControl _PlayerControl;
    public GameObject TargetedEnemy;
    GameObject CubeEnemyDestroy;
    GameObject ParticleEnemyDestroy;

    public float speed;

    bool isIncomingCubeSet;

    private void Awake()
    {
        _PlatformControl = GameObject.FindGameObjectWithTag("Platform").GetComponent<PlatformControl>();
        CubeEnemyDestroy = Resources.Load("Prefabs/cubeFromEnemy") as GameObject;
        ParticleEnemyDestroy = Resources.Load("Prefabs/ParticleEnemyDestroy") as GameObject;
        _PlayerControl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
        isIncomingCubeSet = false;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (TargetedEnemy != null)
        {
            moveToEnemy();
        }
        AssignFiredCubeToEnemy();

    }

    public void moveToEnemy()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(TargetedEnemy.transform.position.x, gameObject.transform.position.y, TargetedEnemy.transform.position.z), Time.deltaTime * speed);
        Vector3 relativePos = TargetedEnemy.transform.position - transform.position;

        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
        transform.rotation = rotation;
    }

    public void AssignFiredCubeToEnemy()
    {
        if (!isIncomingCubeSet)
        {
            if (!_PlatformControl.isBonusPlatformActive)
            {
                TargetedEnemy.GetComponent<Enemy>().IncomingFiredCube = gameObject;
                isIncomingCubeSet = true;
            }
            else
            {
                TargetedEnemy.GetComponent<GiftCube>().IncomingFiredCube = gameObject;
                isIncomingCubeSet = true;
            }

        }
    }

}
