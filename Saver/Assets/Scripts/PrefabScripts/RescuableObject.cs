using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RescuableObject : MonoBehaviour
{
    private Vector3 _initialPosition, _positionBeneathPlane;

    private void Start()
    {
        _initialPosition = gameObject.transform.position;
        _positionBeneathPlane = new Vector3(_initialPosition.x, _initialPosition.y - 2f, _initialPosition.z);
    }

    void Update()
    {
        GetSwiping();
        CheckForDestroy();
    }

    private void GetSwiping()
    {
        if (Input.GetKey(KeyCode.Space) && GameManager.Savior.Rescuable != null && GameManager.Savior.Rescuable.transform.position == gameObject.transform.position)
        {
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, _positionBeneathPlane, 0.05f);
        }
        else if(Vector3.Distance(gameObject.transform.position, _initialPosition) > 0.05f) 
        {
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, _initialPosition, 0.025f);
        }
    }

    private void CheckForDestroy()
    {
        if(Vector3.Distance(gameObject.transform.position, _positionBeneathPlane) < 0.1f)
        {
            GameManager.Spawn.SpawnSaviorObject();
            Destroy(gameObject);
        }
    }
}
