using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    UIController UI_Controller;
    public float distanceToPlayer;
    public bool shootable;
    public GameObject player;
    public int StartCubesCount;
    public GameObject CubeEnemyGameObject;
    int CubeEnemyLastCount;
    public NavMeshAgent NavMeshEnemy;
    Animator EnemyAnim;
    public GameObject IncomingFiredCube;
    GameObject CubeEnemyDestroy;
    GameObject ParticleEnemyDestroy;

    // Start is called before the first frame update
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        shootable = true;
        distanceToPlayer = Vector3.Distance(gameObject.transform.position, player.transform.position);
        CubeEnemyGameObject = Resources.Load("Prefabs/cubeEnemy") as GameObject;
        CubeEnemyLastCount = 0;
        setCubes();
        NavMeshEnemy = gameObject.GetComponent<NavMeshAgent>();
        EnemyAnim = gameObject.GetComponent<Animator>();
        UI_Controller = GameObject.FindGameObjectWithTag("Canvas").GetComponent<UIController>();
        CubeEnemyDestroy = Resources.Load("Prefabs/cubeFromEnemy") as GameObject;
        ParticleEnemyDestroy = Resources.Load("Prefabs/ParticleEnemyDestroy") as GameObject;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (UI_Controller.isGameStart)
        {
            if (Input.GetMouseButton(0))
            {
                distanceToPlayer = Vector3.Distance(gameObject.transform.position, player.transform.position);
            }

            if (distanceToPlayer < 4)
            {
                transform.rotation = Quaternion.LookRotation(NavMeshEnemy.velocity, Vector3.up);
                //gameObject.transform.LookAt(player.transform);
                NavMeshEnemy.SetDestination(player.transform.position);
                if (EnemyAnim.GetBool("isWalk") == false)
                {
                    NavMeshEnemy.isStopped = false;
                    EnemyAnim.SetBool("isWalk", true);
                }
            }
            else
            {
                if (EnemyAnim.GetBool("isWalk") == true)
                {
                    NavMeshEnemy.isStopped = true;
                    EnemyAnim.SetBool("isWalk", false);
                }
            }
            StartDestroyEnemyParticles();
        }
        else
        {
            NavMeshEnemy.isStopped = true;
            EnemyAnim.SetBool("isWalk", false);
        }

    }

    // private void OnTriggerEnter(Collider other)
    // {
    //     if (other.tag == "FiredCube" && other.transform.gameObject == IncomingFiredCube)
    //     {
    //          StartDestroyEnemyParticles();

    //     }
    // }

    public void setCubes()
    {
        if (StartCubesCount == 0)
        {
            gameObject.transform.GetChild(1).gameObject.GetComponent<SkinnedMeshRenderer>().enabled = true;
        }
        else
        {
            for (int i = 0; i < StartCubesCount; i++)
            {
                GameObject Clone;

                GameObject cubesParent = gameObject.transform.GetChild(3).gameObject;
                CubeEnemyLastCount = cubesParent.transform.childCount;

                Clone = Instantiate(CubeEnemyGameObject, cubesParent.transform.position, Quaternion.identity);

                Clone.transform.parent = cubesParent.transform;
                Clone.transform.localPosition = new Vector3(0, CubeEnemyLastCount * 4f, 0);
                Clone.transform.localScale = new Vector3(220, 220, 220);
                //Clone.transform.localRotation = mainCube.transform.localRotation;
            }
        }

    }

    public void StartDestroyEnemyParticles()
    {
        if (IncomingFiredCube != null)
        {
            if (myApproximation(gameObject.transform.position.x, IncomingFiredCube.transform.position.x, 0.05f) == true && myApproximation(gameObject.transform.position.z, IncomingFiredCube.transform.position.z, 0.05f) == true)
            {
                Vibration.VibrateBoth();
                Destroy(IncomingFiredCube);
                GameObject Clone2;
                Clone2 = Instantiate(ParticleEnemyDestroy, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), Quaternion.identity);
                Destroy(Clone2, 1);

                for (int i = 0; i < StartCubesCount; i++)
                {
                    GameObject Clone;
                    Clone = Instantiate(CubeEnemyDestroy, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), Quaternion.identity);
                    Clone.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-1.5f, 1.5f), Random.Range(0.5f, 1.5f), Random.Range(-1.5f, 1.5f)), ForceMode.Impulse);
                }
                Destroy(gameObject);

            }
        }

    }

    private bool myApproximation(float a, float b, float tolerance)
    {
        return (Mathf.Abs(a - b) < tolerance);
    }
}
