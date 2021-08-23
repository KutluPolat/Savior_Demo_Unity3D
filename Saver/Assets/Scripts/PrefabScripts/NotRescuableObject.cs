using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotRescuableObject : SaviorObject
{
    private GameObject _savior; //_savior = null means Savior object never collided with this NotRescuable object before.

    private void Update()
    {
        Debug.Log("Is hit to resc: " + GameManager.Savior.HitToRescuable);
        Debug.Log("Is hit to not resc: " + GameManager.Savior.HitToNotRescuable);
        RayCast();
    }

    private void RayCast()
    {
        if (Input.GetMouseButtonUp(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, RayLength, LayerMask))
            {
                if (gameObject.transform.position == hit.transform.position && _savior != null)
                {
                    Debug.Log("Start Coroutine");
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
}
