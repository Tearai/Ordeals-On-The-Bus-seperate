using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class vrcameramovement : MonoBehaviour
{
    [SerializeField] Transform resetTransform;
    [SerializeField] GameObject player;
    [SerializeField] Camera playerHead;
    public bool shouldReset = true;

    [ContextMenu("Reset Position")]

    public void Start()
    {
        //ResetPosition();
        StartCoroutine(Count());
    }
    public void Update()
    {
        if (shouldReset == false)
        {
            ResetPosition();
        }
            
    }



    public void ResetPosition()
    {
        var rotationAngleY = resetTransform.rotation.eulerAngles.y -
            playerHead.transform.rotation.eulerAngles.y;
        player.transform.Rotate(0, rotationAngleY, 0);

        var distanceDiff = resetTransform.position -
                           playerHead.transform.position;
        player.transform.position += distanceDiff;

    }

    IEnumerator Count()
    {
        yield return new WaitForSeconds(2f);
        shouldReset = true;
    }
}
