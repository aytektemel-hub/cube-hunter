using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerControl : MonoBehaviour
{
    public PlatformControl _PlatformControl;
    public UIController _UIController;
    public GameObject CubePlayerGameObject;
    public GameObject FiredCubeGameObject;
    public Animator PlayerAnimator;
    public DynamicJoystick joystick;
    public GameObject CubeAddParticleGO;
    public GameObject ClosestEnemy;
    public GameObject ClosestGiftCube;
    public GameObject EnemiesGameObject;
    public GameObject FinishLineGameObject;
    public GameObject BonusCubesParentGameObject;

    [System.NonSerialized]
    public float[] PlayerSpeeds = new float[10] { 10, 11, 11.5f, 12, 12.5f, 13, 13.5f, 14, 14.5f, 15 };
    public float[] FireSpeeds = new float[10];
    public float PlayerSpeed;
    public float fireTime;
    public bool isThereEnemy;
    public bool isFiring;
    public GameObject LastFiredCube;
    public int StartCubes;
    public Animator GunAnim;
    Vector3 direction;

    Rigidbody PlayerRigidbody;
    GameObject[] GiftCubes;

    private void Awake()
    {
        Application.targetFrameRate = 300;
        PlayerRigidbody = gameObject.GetComponent<Rigidbody>();
        _PlatformControl = GameObject.FindGameObjectWithTag("Platform").GetComponent<PlatformControl>();
        CubePlayerGameObject = Resources.Load("Prefabs/cubePlayer") as GameObject;
        FiredCubeGameObject = Resources.Load("Prefabs/firedCube") as GameObject;
        CubeAddParticleGO = Resources.Load("Prefabs/cubeAddParticle") as GameObject;

        PlayerSpeed = PlayerSpeeds[PlayerPrefs.GetInt("RunLevel", 1) - 1];
        fireTime = FireSpeeds[PlayerPrefs.GetInt("FireLevel", 1) - 1];

        SetStartCubes();


    }
    // Start is called before the first frame update 
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (_UIController.isGameStart == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                PlayerAnimator.SetBool("run", true);
            }
            if (Input.GetMouseButton(0))
            {
                MovePlayer();
            }
            if (Input.GetMouseButtonUp(0))
            {
                PlayerRigidbody.velocity = Vector3.zero;
                PlayerAnimator.SetBool("run", false);
                if (_PlatformControl.isBonusPlatformActive == false)
                {
                    StartCoroutine(Fire());
                }
                else
                {
                    StartCoroutine(FireToGiftCubes());
                }

            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "CubeGround")
        {

            Destroy(other.gameObject.transform.parent.gameObject);
            AddCube();
            Vibration.VibrateBoth();
        }
        if (other.tag == "Enemy")
        {
            PlayerRigidbody.velocity = Vector3.zero;
            _UIController.SetActiveFailPanel();
            _UIController.isGameStart = false;
            PlayerAnimator.SetBool("run", false);
        }
        if (other.tag == "FinishBlock")
        {
            _PlatformControl.isBonusPlatformActive = true;
            if (isThereThrowableCube() == false)
            {
                _UIController.SetActiveSuccesPanel();
                _UIController.isGameStart = false;
            }
            other.enabled = false;
        }


    }
    void OnParticleCollision(GameObject other)
    {
        if (other.tag == "CubeGroundParticle")
        {
            Destroy(other.gameObject);
            AddCube();
        }

    }
    public void MovePlayer()
    {

        //transform.Translate(new Vector3(joystick.Horizontal * (PlayerSpeed / 2000), 0, joystick.Vertical * (PlayerSpeed / 2000)), Space.World);
        PlayerRigidbody.velocity = new Vector3(joystick.Horizontal * PlayerSpeed / 5, PlayerRigidbody.velocity.y, joystick.Vertical * PlayerSpeed / 5);

        direction = Vector3.forward * joystick.Vertical + Vector3.right * joystick.Horizontal;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = rotation;

        if (Mathf.Abs(joystick.Horizontal) >= Mathf.Abs(joystick.Vertical))
        {
            PlayerAnimator.SetFloat("WalkAnimSpeed", Mathf.Abs(joystick.Horizontal * 1.25f));
        }
        else
        {
            PlayerAnimator.SetFloat("WalkAnimSpeed", Mathf.Abs(joystick.Vertical * 1.25f));
        }
    }

    public void AddCube()
    {
        GameObject Clone;
        int cubePlayerCount;

        GameObject cubesParent = gameObject.transform.Find("Cubes").gameObject;
        GameObject mainCube = gameObject.transform.Find("Cubes").transform.GetChild(0).gameObject;
        cubePlayerCount = cubesParent.transform.childCount;

        Vector3 pos = new Vector3(mainCube.transform.position.x, 1.5f + (cubePlayerCount * 0.5f));

        Clone = Instantiate(CubePlayerGameObject, pos, Quaternion.identity);

        Clone.transform.parent = cubesParent.transform;
        Clone.transform.localPosition = new Vector3(mainCube.transform.localPosition.x, 1.5f + (cubePlayerCount * 0.5f), mainCube.transform.localPosition.z);
        Clone.transform.localScale = new Vector3(30, 30, 30);
        Clone.transform.localRotation = mainCube.transform.localRotation;

        GameObject CloneParticle;
        CloneParticle = Instantiate(CubeAddParticleGO, Clone.transform.position, Quaternion.identity);
        Destroy(CloneParticle, 1);
    }

    public IEnumerator Fire()
    {
        while (true)
        {
            if (gameObject.transform.GetChild(2).childCount > 1 && isFiring == false)
            {

                SetClosestEnemy();

                if (Input.GetMouseButton(0))
                {
                    yield break;
                }

                if (ClosestEnemy != null)
                {
                    if (ClosestEnemy.gameObject.GetComponent<Enemy>().shootable == true)
                    {
                        isFiring = true;
                        GameObject Clone;
                        Clone = Instantiate(FiredCubeGameObject, transform.GetChild(2).GetChild(0).position, Quaternion.identity);
                        Clone.GetComponent<FiredCube>().TargetedEnemy = ClosestEnemy;
                        ClosestEnemy.gameObject.GetComponent<Enemy>().shootable = false;
                        Destroy(transform.GetChild(2).GetChild(gameObject.transform.GetChild(2).childCount - 1).gameObject);
                        LastFiredCube = Clone;
                        GunAnim.Play("ArrowAnim", 0, 0);
                        transform.LookAt(Clone.GetComponent<FiredCube>().TargetedEnemy.transform.position);
                    }
                }
                else
                {
                    yield break;
                }


                yield return new WaitForSeconds(fireTime);
                isFiring = false;

            }
            else
            {
                yield break;
            }
        }
    }
    public void SetClosestEnemy()
    {
        float distance = 9999;
        float radius = 3.7f;
        Collider[] hitColliders = Physics.OverlapSphere(gameObject.transform.position, radius);
        //Quaternion rotation = Quaternion.LookRotation(direction);
        //Collider[] hitColliders = Physics.OverlapBox(gameObject.transform.GetChild(3).transform.position, new Vector3(2, 3, 3), gameObject.transform.rotation);

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.tag == "Enemy")
            {
                //print(hitCollider.gameObject);
                float newDistance = hitCollider.transform.gameObject.GetComponent<Enemy>().distanceToPlayer;
                if (newDistance < distance)
                {
                    if (hitCollider.gameObject.GetComponent<Enemy>().shootable == true)
                    {
                        ClosestEnemy = hitCollider.gameObject;
                        distance = newDistance;
                    }
                }
            }
        }
    }

    public void RotatePlayer()
    {
        transform.rotation = new Quaternion(transform.rotation.x, LastFiredCube.transform.rotation.y, transform.rotation.z, 1);
    }

    public void SetStartCubes()
    {
        for (int i = 0; i < StartCubes; i++)
        {
            AddCube();
        }

    }

    public IEnumerator FireToGiftCubes()
    {
        while (true)
        {
            if (gameObject.transform.GetChild(2).childCount > 1 && isFiring == false)
            {

                SetClosestCube();

                if (Input.GetMouseButton(0))
                {
                    yield break;
                }

                if (ClosestGiftCube != null)
                {
                    if (ClosestGiftCube.gameObject.GetComponent<GiftCube>().shootable == true)
                    {
                        isFiring = true;
                        GameObject Clone;
                        Clone = Instantiate(FiredCubeGameObject, transform.GetChild(2).GetChild(0).position, Quaternion.identity);
                        Clone.GetComponent<FiredCube>().TargetedEnemy = ClosestGiftCube;
                        ClosestGiftCube.gameObject.GetComponent<GiftCube>().shootable = false;
                        Destroy(transform.GetChild(2).GetChild(gameObject.transform.GetChild(2).childCount - 1).gameObject);
                        LastFiredCube = Clone;
                        GunAnim.Play("ArrowAnim", 0, 0);
                        transform.LookAt(Clone.GetComponent<FiredCube>().TargetedEnemy.transform.position);
                    }
                }
                else
                {
                    yield break;
                }


                yield return new WaitForSeconds(0.2f);
                isFiring = false;

            }
            else
            {
                yield break;
            }
        }
    }

    public void SetClosestCube()
    {
        float distance = 9999f;
        GiftCubes = new GameObject[BonusCubesParentGameObject.transform.childCount];
        for (int i = 0; i < BonusCubesParentGameObject.transform.childCount; i++)
        {
            GiftCubes[i] = BonusCubesParentGameObject.transform.GetChild(i).gameObject;
        }

        foreach (GameObject GiftCube in GiftCubes)
        {
            float newDistance = GiftCube.transform.gameObject.GetComponent<GiftCube>().distanceToPlayer;
            if (newDistance < 4f)
            {
                if (newDistance < distance)
                {
                    if (GiftCube.gameObject.GetComponent<GiftCube>().shootable == true)
                    {
                        ClosestGiftCube = GiftCube.gameObject;
                        distance = newDistance;
                    }
                }
            }

        }
    }

    public bool isThereThrowableCube()
    {
        if (gameObject.transform.GetChild(2).childCount > 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}




