using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubwayMove : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float leftBoundary = -2f;
    public float rightBoundary = 2f;

    void update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            MoveCharacter(-2f);
        }

        else if (Input.GetKeyDown(KeyCode.D))
        {
            MoveCharacter(2f);
        }
    }
       
    void MoveCharacter(float direction)
    {
            Vector3 newPosition = transform.position + new Vector3(direction * moveSpeed, 0f, 0f);
            newPosition.x = Mathf.Clamp(newPosition.x, leftBoundary, rightBoundary);
            transform.position = newPosition;
    }
 }
