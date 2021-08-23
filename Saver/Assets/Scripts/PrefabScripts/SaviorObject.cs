using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaviorObject : MonoBehaviour
{
    private bool _isRayHitToUnits, _isSaviorHitToRescuable, _isSaviorHitToNotRescuable, _isCameraFollowing;
    private readonly float _force = 1000f;
    private int _lives = 3;

    private GameObject _rescuableUnitThatSaviorSticked;

    protected readonly float RayLength = 100f;
    protected LayerMask LayerMask;
    
    private void Start()
    {
        LayerMask = LayerMask.GetMask("Units");
    }

    void Update()
    {
        RaycastToUnits();
    }

    private void RaycastToUnits()
    {
        if (Input.GetMouseButtonUp(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, RayLength, LayerMask))
            {
                if (_isSaviorHitToNotRescuable || _isSaviorHitToRescuable)
                    return;
                _isRayHitToUnits = true;
                _isCameraFollowing = true;
                StartCoroutine(MoveSaviorTowardsClickedUnit(hit.transform.gameObject));
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.transform.tag)
        {
            case "Rescuable":

                _rescuableUnitThatSaviorSticked = other.gameObject;
                _isSaviorHitToRescuable = true;
                _isSaviorHitToNotRescuable = false; //This line of code is to improve robustness
                gameObject.transform.parent = other.transform;
                Destroy(gameObject.GetComponent<Rigidbody>()); //With the destroy of rigidbody, the savior will stand still and will look like stick to the unit.

                break;

            case "NotRescuable":

                _isSaviorHitToNotRescuable = true;
                _isSaviorHitToRescuable = false; //Again, this line of code is to improve robustness
                Destroy(gameObject.GetComponent<Rigidbody>()); //With the destroy of rigidbody, the savior will stand still and will look like stick to the unit.
                PlayerPrefs.SetInt("Lives", PlayerPrefs.GetInt("Lives") - 1);

                break;
        }
    }

    public IEnumerator MoveSaviorTowardsClickedUnit(GameObject clickedObject)
    {
        var rigidBody = gameObject.AddComponent<Rigidbody>();

        yield return new WaitForSeconds(1f);

        if (rigidBody != null) //To improve robustness
        {
            rigidBody.useGravity = false; //I'm disabling the gravity and resetting the velocity because we want gliding effect.
            rigidBody.velocity = Vector3.zero;
            var calculateForce = clickedObject.transform.position - gameObject.transform.position;
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
