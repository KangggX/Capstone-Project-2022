using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locator : MonoBehaviour
{
    private static Transform _playerTransform;

    private void Start()
    {
        LocateSceneObjects();
    }

    private void LocateSceneObjects()
    {
        GameObject gameObject = GameObject.FindWithTag("Player");

        if (gameObject != null)
        {
            _playerTransform = gameObject.GetComponent<Transform>();
        }
    }

    public static Transform GetPlayerTransform()
    {
        return _playerTransform;
    }
}
