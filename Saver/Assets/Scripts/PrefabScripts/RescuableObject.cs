using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RescuableObject : MonoBehaviour
{
    private Vector3 _normalYPos, _underPlaneYPos;

    private Vector3 _rightPoint, _leftPoint; // These variables are used for making the object move slightly.
    private bool _moveTowardsRight, _moveTowardsLeft; // These variables are used for making the object move slightly.

    private void Start()
    {
        var random = Random.Range(0f, 1f);

        if (random >= 0.5f)
            _moveTowardsRight = true;
        else
            _moveTowardsLeft = true;

        random += 0.5f; // Providing at least 0.5f of movement.

        _rightPoint = new Vector3(transform.position.x + random, transform.position.y, transform.position.z);
        _leftPoint = new Vector3(transform.position.x - random, transform.position.y, transform.position.z);
    }

    void Update()
    {
        GetSwiping();
        CheckForDestroy();
        Move();
    }

    private void GetSwiping()
    {
        _normalYPos = new Vector3(transform.position.x, 1f, transform.position.z);
        _underPlaneYPos = new Vector3(_normalYPos.x, -1.5f, _normalYPos.z);

        if (Input.GetKey(KeyCode.Space) && GameManager.Savior.Rescuable != null && GameManager.Savior.Rescuable.transform.position == gameObject.transform.position)
        {
            gameObject.transform.position = new Vector3 (transform.position.x, transform.position.y - 0.05f, transform.position.z);
        }
        else if (Vector3.Distance(gameObject.transform.position, _normalYPos) > 0.05f)
        {
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, _normalYPos, 0.025f);
        }
    }

    private void CheckForDestroy()
    {
        if (Vector3.Distance(transform.position, _underPlaneYPos) < 0.2f)
        {
            GameManager.Spawn.SpawnSaviorObject();

            GameManager.Spawn.RescuableUnits.Remove(gameObject);
            Destroy(gameObject);
        }
    }
    private void Move()
    {
        if (PlayerPrefs.GetInt("Level") <= 4 || Input.GetKey(KeyCode.Space))
            return;

        if (_moveTowardsRight)
            transform.position = Vector3.Lerp(transform.position, _rightPoint, 0.01f);
        else if (_moveTowardsLeft)
            transform.position = Vector3.Lerp(transform.position, _leftPoint, 0.01f);

        if (Vector3.Distance(transform.position, _rightPoint) < 0.1f) // If object reached the right point, start to move towards left point.
        {
            _moveTowardsLeft = true;
            _moveTowardsRight = false;
        }
        else if (Vector3.Distance(transform.position, _leftPoint) < 0.1f) // If object reached the left point, start to move towards right point.
        {
            _moveTowardsRight = true;
            _moveTowardsLeft = false;
        }
    }
}
