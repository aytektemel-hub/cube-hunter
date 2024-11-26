using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    UIController UI_Controller;
    GameObject PlayerGameObject;
    public float CamMoveSpeed = 15f;


    private void Awake()
    {
        PlayerGameObject = GameObject.FindGameObjectWithTag("Player");
        UI_Controller = GameObject.FindGameObjectWithTag("Canvas").GetComponent<UIController>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        SetCameraPosition();

        if (UI_Controller.isGameStart)
        {
            if (Input.GetMouseButton(0))
            {
                Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 70, CamMoveSpeed * Time.deltaTime);
            }
            else
            {
                Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 90, CamMoveSpeed * Time.deltaTime);
            }
        }

    }

    public void SetCameraPosition()
    {
        //transform.position = Vector3.MoveTowards(transform.position, new Vector3(PlayerGameObject.transform.position.x, transform.position.y, PlayerGameObject.transform.position.z - 1f), CamMoveSpeed * Time.deltaTime);

        transform.position = new Vector3(PlayerGameObject.transform.position.x, transform.position.y, PlayerGameObject.transform.position.z - 1f);

    }


}
