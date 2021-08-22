using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaviorObject : MonoBehaviour
{
    private bool _isRayHitToUnits, _isSaviorHitToRescuable, _isSaviorHitToNotRescuable;
    private LayerMask LayerMask;
    private readonly float RayLength = 100f, Force = 1000f;
    
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
                Debug.Log("You clicked the unit.");
                _isRayHitToUnits = true;
                StartCoroutine(MoveSaviorTowardsClickedUnit(hit.transform.gameObject));
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.transform.tag)
        {
            case "Rescuable":

                Debug.Log("Touched rescuable.");
                _isSaviorHitToRescuable = true;
                _isSaviorHitToNotRescuable = false; //This line of code is to improve robustness
                Destroy(gameObject.GetComponent<Rigidbody>());

                break;

            case "NotRescuable":

                Debug.Log("Touched not rescuable.");
                _isSaviorHitToNotRescuable = true;
                _isSaviorHitToRescuable = false; //Again, this line of code is to improve robustness
                Destroy(gameObject.GetComponent<Rigidbody>());
                StartCoroutine(DestroyGameObject());

                break;
        }
    }

    public IEnumerator MoveSaviorTowardsClickedUnit(GameObject clickedObject)
    {
        var rigidBody = gameObject.AddComponent<Rigidbody>();

        yield return new WaitForSeconds(1f);

        rigidBody.useGravity = false; //I'm disabling the gravity and resetting the velocity because we want gliding effect.
        rigidBody.velocity = Vector3.zero;
        var calculateForce = clickedObject.transform.position - gameObject.transform.position;
        calculateForce = calculateForce.normalized;
        calculateForce = calculateForce * Force;
        rigidBody.AddForce(calculateForce);
    }

    private IEnumerator DestroyGameObject()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
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
    public Vector3 GetSaviorPosition()
    {
        return transform.position;
    }
}
