using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RescuableObject : SaviorObject
{
    private Vector3 _normalYPos, _underPlaneYPos, _initialYPos;

    private Vector2 _initialFingerPosition;

    private void Start()
    {
        _initialYPos = transform.position;
        _animator = gameObject.GetComponent<Animator>();
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
        CheckForDestroy();
    }
    private void FixedUpdate()
    {
        GetSwiping();
        Move();
    }

    private void GetSwiping()
    {
        _normalYPos = new Vector3(transform.position.x, _initialYPos.y, transform.position.z);
        _underPlaneYPos = new Vector3(_normalYPos.x, _initialYPos.y - 2f, _normalYPos.z);

#if UNITY_EDITOR
        if (Input.GetKey(KeyCode.Space) && GameManager.Savior.Rescuable != null && GameManager.Savior.Rescuable.transform.position == gameObject.transform.position)
        {
            _animator.SetTrigger("Rescuing");
            gameObject.transform.position = new Vector3(transform.position.x, transform.position.y - 0.05f, transform.position.z);
        }
        else GoBackToIdlePosition();
#endif

#if PLATFORM_ANDROID
        if (Input.touchCount > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began || Input.touches[0].phase == TouchPhase.Stationary)
            {
                // Basically, I'm saving the finger position when the player touches the screen the first time or holds still.
                // And I compare that positions with new ones after player moved his finger.
                // Rescuing action will start if the player moves his or her finger only in the y-axis. (Player has 50 pixels of the x-axis error margin.)
                _initialFingerPosition = Input.touches[0].position;
            }

            if (Input.touches[0].phase == TouchPhase.Moved)
            {
                if (Input.touches[0].position.x > _initialFingerPosition.x + 50 || Input.touches[0].position.x < _initialFingerPosition.x - 50) //If finger moves in x axis, return.
                {
                    GoBackToIdlePosition();
                    return;
                }
                if (Input.touches[0].position.y != _initialFingerPosition.y && GameManager.Savior.Rescuable != null && GameManager.Savior.Rescuable.transform.position == gameObject.transform.position)
                {
                    _animator.SetTrigger("Rescuing");
                    gameObject.transform.position = new Vector3(transform.position.x, transform.position.y - 0.05f, transform.position.z);
                }
            }
        }
        else GoBackToIdlePosition();
#endif
    }
    private void GoBackToIdlePosition()
    {
        if (Vector3.Distance(gameObject.transform.position, _normalYPos) > 0.05f)
        {
            _animator.SetTrigger("Idle");
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, _normalYPos, 0.025f);
        }
    }

    private void CheckForDestroy()
    {
        if (Vector3.Distance(transform.position, _underPlaneYPos) < 0.2f)
        {
            AudioManager.PlayMusicOrEffect("_saved");
            GameManager.Spawn.SpawnSaviorObject();
            GameManager.Spawn.RescuableUnits.Remove(gameObject);
            Destroy(gameObject);
        }
    }
    private void Move()
    {
        if (PlayerPrefs.GetInt("Level") <= 3 || Input.GetKey(KeyCode.Space))
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
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            _animator.SetTrigger("Sticked");
    }
}
