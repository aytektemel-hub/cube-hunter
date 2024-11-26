using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootPrint : MonoBehaviour
{
    GameObject Player;
    ParticleSystem FootprintParticle;

    // Start is called before the first frame update
    void Start()
    {
        FootprintParticle = gameObject.GetComponent<ParticleSystem>();
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        var rot = FootprintParticle.main;
        gameObject.transform.rotation = new Quaternion(0, Player.transform.rotation.y, 0, 1);
    }
}
