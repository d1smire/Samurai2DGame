using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCameraMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 0.25f; 
    [SerializeField] private Transform background; 
    
    [SerializeField] private float EndPositionRight = 1.93f;
    [SerializeField] private float EndPositionleft = -2.07f;
    
    private float backgroundEndPosition = 0f;

    private void Start()
    {
        backgroundEndPosition = EndPositionRight;
    }
    void Update()
    {
        Vector3 currentPosition = transform.position;

        if (currentPosition.x < backgroundEndPosition)
        {
            currentPosition.x += moveSpeed * Time.deltaTime;
            if (currentPosition.x >= backgroundEndPosition) 
            {
                backgroundEndPosition = EndPositionleft;
            }
            else 
            {
                backgroundEndPosition = EndPositionRight;
            }
        }
        else
        {
            currentPosition.x -= moveSpeed * Time.deltaTime;
        }

        transform.position = currentPosition;
    }
}
