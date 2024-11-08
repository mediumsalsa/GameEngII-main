using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] CameraManager camMan;
    Camera playerCam;
    [SerializeField] LayerMask cubeFilter;
    [SerializeField] LayerMask ground;

    void Start()
    {
        playerCam = camMan.playerCamera;
    }

    void FixedUpdate()
    {
        float distance = 10;
        if (Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out RaycastHit hit, 10, ground.value))
        {
            distance = hit.distance;
        }
        Debug.Log($"Distance is {distance}");

        Debug.DrawLine(playerCam.transform.position, playerCam.transform.position + playerCam.transform.forward * 10);

        Renderer[] allRenderers = FindObjectsOfType<Renderer>();
        foreach (Renderer renderer in allRenderers)
        {
            if (renderer.gameObject.layer == Mathf.Log(cubeFilter.value, 2))
            {
                renderer.material.color = Color.blue;
            }
        }

        RaycastHit[] hits = Physics.RaycastAll(playerCam.transform.position, playerCam.transform.forward, distance, cubeFilter.value);
        foreach (RaycastHit hit2 in hits)
        {
            if (hit2.collider.TryGetComponent(out Renderer renderer))
            {
                renderer.material.color = Color.red;
            }
        }
        Debug.Log($"Hit {hits.Length} cubes");
    }
}
