using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(DestroyObject());
        }
    }
    private IEnumerator DestroyObject()
    {
        PlayerPrefs.SetInt("Lives", PlayerPrefs.GetInt("Lives") - 1);
        GameObject.Find("Savior(Clone)").GetComponent<Animator>().SetTrigger("Fall");

        yield return new WaitForSeconds(1f);

        GameManager.Spawn.SpawnSaviorObject();
        Destroy(GameObject.Find("Savior(Clone)"));
    }
}
