using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GiftCube : MonoBehaviour
{
    PlayerControl _PlayerControl;
    UIController _UIController;
    public float distanceToPlayer;
    public bool shootable;
    public GameObject player;
    public int StartCubesCount;
    public GameObject CubeEnemyGameObject;
    int CubeEnemyLastCount;
    Animator EnemyAnim;
    public GameObject IncomingFiredCube;
    GameObject CubeEnemyDestroy;
    GameObject ParticleGiftCube;
    public GameObject CloneCoin;

    // Start is called before the first frame update
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        shootable = true;
        distanceToPlayer = Vector3.Distance(gameObject.transform.position, player.transform.position);
        ParticleGiftCube = Resources.Load("Prefabs/ParticleGiftCube") as GameObject;
        _UIController = GameObject.FindGameObjectWithTag("Canvas").GetComponent<UIController>();
        _PlayerControl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (_UIController.isGameStart)
        {
            if (Input.GetMouseButton(0))
            {
                distanceToPlayer = Vector3.Distance(gameObject.transform.position, player.transform.position);
            }

        }
        if (IncomingFiredCube != null)
        {
            if (myApproximation(gameObject.transform.position.x, IncomingFiredCube.transform.position.x, 0.05f) == true && myApproximation(gameObject.transform.position.z, IncomingFiredCube.transform.position.z, 0.05f) == true)
            {

                DestroyGiftCube();

            }
        }

    }

    // private void OnTriggerEnter(Collider other)
    // {
    //     if (other.tag == "FiredCube" && other.transform.gameObject == IncomingFiredCube)
    //     {
    //          DestroyGiftCube();

    //     }
    // }


    public void DestroyGiftCube()
    {
        Vibration.VibrateBoth();
        _UIController.AddCoin(20);
        Destroy(IncomingFiredCube);
        Destroy(gameObject);
        GameObject CloneCoinClone;
        CloneCoinClone = Instantiate(CloneCoin, Camera.main.WorldToScreenPoint(gameObject.transform.position), Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);

        ParticleGiftCube.GetComponent<ParticleSystemRenderer>().material = gameObject.GetComponent<MeshRenderer>().material;
        GameObject CloneParticleEnemy;
        CloneParticleEnemy = Instantiate(ParticleGiftCube, transform.position, Quaternion.identity);
        Destroy(CloneParticleEnemy, 1);
        //isThereGiftCube();
        if (!isThereGiftCube() || !_PlayerControl.isThereThrowableCube())
        {
            _UIController.SetActiveSuccesPanel();
            _UIController.isGameStart = false;
        }

    }

    private bool myApproximation(float a, float b, float tolerance)
    {
        return (Mathf.Abs(a - b) < tolerance);
    }

    public bool isThereGiftCube()
    {
        if (gameObject.transform.parent.childCount > 1)
        {
            //print(gameObject.transform.parent.childCount);
            return true;
        }
        else
        {
            print(false);
            return false;
        }
    }
}
