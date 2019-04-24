using UnityEngine;
using System.Collections;

public class BGScroller : MonoBehaviour
{
    public GameController gameController;
    public float scrollSpeed;
    public float tileSizeZ;

    private Vector3 startPosition;

    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        gameController = gameControllerObject.GetComponent<GameController>();
        startPosition = transform.position;
    }

    void Update()
    {
        if (gameController.accelerate == true)
        {
            if (scrollSpeed >= -20)
            {
                scrollSpeed -= Time.deltaTime;
            }

        }
        float newPosition = Mathf.Repeat(Time.time * scrollSpeed, tileSizeZ);
        transform.position = startPosition + Vector3.forward * newPosition;
    }


}