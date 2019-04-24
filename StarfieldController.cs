using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class StarfieldController : MonoBehaviour
{

    public GameController gameController;
    private float hSliderValue = 1.0F;
    private ParticleSystem ps;

    void Start()
    {
       ps = GetComponent<ParticleSystem>();
       GameObject gameControllerObject = GameObject.FindWithTag("GameController");
       gameController = gameControllerObject.GetComponent<GameController>();
    }


    // Update is called once per frame
    void Update()
    {
        var main = ps.main;
        main.simulationSpeed = hSliderValue;

        if (gameController.accelerate == true)
        {
            if (hSliderValue <= 20)
            {
                hSliderValue += Time.deltaTime;
            }
        }


    }
}
