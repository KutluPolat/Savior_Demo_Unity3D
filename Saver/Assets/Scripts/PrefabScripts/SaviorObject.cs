using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaviorObject : MonoBehaviour
{
    private bool _isRayHitToUnits, _isSaviorHitToRescuable, _isSaviorHitToNotRescuable, _isCameraFollowing;
    private readonly float _force = 1000f;
    private int _lives = 3;

    private GameObject _rescuableUnitThatSaviorSticked;
    
    protected Animator _animator;
    protected readonly float RayLength = 500f;
    protected LayerMask LayerMask;

    protected Vector3 _rightPoint, _leftPoint; // These variables are used for making the units move slightly.
    protected bool _moveTowardsRight, _moveTowardsLeft; // These variables are used for making the units move slightly.

    private void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
        LayerMask = LayerMask.GetMask("Units");
    }

    void Update()
    {
        RaycastToUnits();
    }

    private void RaycastToUnits()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonUp(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, RayLength, LayerMask))
            {
                if (_isSaviorHitToNotRescuable || _isSaviorHitToRescuable) // To preventing spamming.
                    return;
                _isRayHitToUnits = true;
                _isCameraFollowing = true;
                StartCoroutine(MoveSaviorTowardsClickedUnit(hit.transform.gameObject));
            }
        }
#endif
#if PLATFORM_ANDROID
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);

            if (Physics.Raycast(ray, out hit, RayLength, LayerMask))
            {
                if (_isSaviorHitToNotRescuable || _isSaviorHitToRescuable)
                    return;
                _isRayHitToUnits = true;
                _isCameraFollowing = true;
                StartCoroutine(MoveSaviorTowardsClickedUnit(hit.transform.gameObject));
            }
        }
#endif
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.transform.tag)
        {
            case "Rescuable":

                AudioManager.PlayMusicOrEffect("_stickedToRescuable");
                _rescuableUnitThatSaviorSticked = other.gameObject;
                _isSaviorHitToRescuable = true;
                _isSaviorHitToNotRescuable = false; //This line of code is to improve robustness
                gameObject.transform.parent = other.transform;
                Destroy(gameObject.GetComponent<Rigidbody>()); //With the destroy of rigidbody, the savior will stand still and will look like stick to the unit.
                gameObject.GetComponent<SphereCollider>().isTrigger = false;
                _animator.SetTrigger("Save");

                break;

            case "NotRescuable":

                AudioManager.PlayMusicOrEffect("_stickedToNotRescuable");
                _isSaviorHitToNotRescuable = true;
                _isSaviorHitToRescuable = false; //Again, this line of code is to improve robustness
                gameObject.transform.parent = other.transform;
                Destroy(gameObject.GetComponent<Rigidbody>()); //With the destroy of rigidbody, the savior will stand still and will look like stick to the unit.
                PlayerPrefs.SetInt("Lives", PlayerPrefs.GetInt("Lives") - 1);
                gameObject.GetComponent<SphereCollider>().isTrigger = false;
                _animator.SetTrigger("FightBack");

                break;
        }
    }

    public IEnumerator MoveSaviorTowardsClickedUnit(GameObject clickedObject)
    {
        var rigidBody = gameObject.AddComponent<Rigidbody>();
        _animator.SetTrigger("Fall");

        yield return new WaitForSeconds(1f);

        if (rigidBody != null) //To improve robustness
        {
            _animator.SetTrigger("Fly");
            rigidBody.useGravity = false; //I'm disabling the gravity and resetting the velocity because we want gliding effect.
            rigidBody.velocity = Vector3.zero;
            var clickedObjectsHead = new Vector3(clickedObject.transform.position.x, clickedObject.transform.position.y + 0.6f, clickedObject.transform.position.z);
            var calculateForce = clickedObjectsHead - gameObject.transform.position;
            calculateForce = calculateForce.normalized;
            calculateForce = calculateForce * _force;
            rigidBody.AddForce(calculateForce);
        }
    }

    public bool GetIsRayHitToUnits()
    {
        return _isRayHitToUnits;
    }

    public bool GetIsSaviorHitToRescuable()
    {
        return _isSaviorHitToRescuable;
    }

    public bool GetIsSaviorHitTo_Not_Rescuable()
    {
        return _isSaviorHitToNotRescuable;
    }

    public bool GetIsCameraFollowing()
    {
        return _isCameraFollowing;
    }

    public Vector3 GetSaviorPosition()
    {
        return transform.position;
    }

    public int GetLives()
    {
        return _lives;
    }

    public GameObject GetRescuable()
    {
        return _rescuableUnitThatSaviorSticked;
    }

    public void SetHitToRescuable(bool value)
    {
        _isSaviorHitToRescuable = value;
    }

    public void SetHitToNotRescuable(bool value)
    {
        _isSaviorHitToNotRescuable = value;
    }
}
