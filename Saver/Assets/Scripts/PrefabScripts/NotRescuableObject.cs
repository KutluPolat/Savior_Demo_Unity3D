using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotRescuableObject : SaviorObject
{
    private GameObject _savior; //_savior = null means Savior object never collided with this NotRescuable object before.
    private Vector3 _rightPoint, _leftPoint;
    private bool _moveTowardsRight, _moveTowardsLeft;

    private void Start()
    {
        LayerMask = LayerMask.GetMask("Units");

        var random = Random.Range(0f, 1f);

        if(random >= 0.5f)
            _moveTowardsRight = true;
        else
            _moveTowardsLeft = true;

        _rightPoint = new Vector3(transform.position.x + random, transform.position.y, transform.position.z);
        _leftPoint = new Vector3(transform.position.x - random, transform.position.y, transform.position.z);
    }

    private void Update()
    {
        RayCast();
        Move();
    }

    private void RayCast()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("Ray");
            Debug.Log("RayLength: " + RayLength);
            Debug.Log("LayerMask: " + LayerMask.value);
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, RayLength, LayerMask))
            {
                if (gameObject.transform.position == hit.transform.position && _savior != null)
                {
                    Debug.Log("1111");
                    StartCoroutine(InstantiateNewSaviorAndDestroyTheOldOne());
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        _savior = other.gameObject;
    }

    private IEnumerator InstantiateNewSaviorAndDestroyTheOldOne()
    {
        GameObject.Find("Savior(Clone)").AddComponent<Rigidbody>(); //Because we want savior to snap off from the unit.
        GameObject.Find("Savior(Clone)").GetComponent<SphereCollider>().isTrigger = false; //Because we want savior to snap off from the unit.
        gameObject.GetComponent<CapsuleCollider>().isTrigger = false;

        yield return new WaitForSeconds(1f);

        gameObject.GetComponent<CapsuleCollider>().isTrigger = true;
        GameManager.Spawn.SpawnSaviorObject();
        GameManager.Savior.IsCameraFollowing = false; //New Savior instantiated and camera have to go back to it's initial position.
        GameManager.Savior.HitToRescuable = false;
        GameManager.Savior.HitToNotRescuable = false;
        Destroy(GameObject.Find("Savior(Clone)"));
    }

    private void Move()
    {
        if(PlayerPrefs.GetInt("Level") <= 4)
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
