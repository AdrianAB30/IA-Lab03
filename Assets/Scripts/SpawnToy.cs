using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnToy : MonoBehaviour
{
    [Header("Object to Spawn")]
    public GameObject collectibleObject;

    [Header("Spawn Parameters")]
    public float initialHeight = 10f;
    public LayerMask shipLayer;
    public float spawnRadius = 5f;
    public float spawnInterval = 5f;

    [Header("Center Area Reference")]
    public Transform areaCenter;

    private float currentTime = 0f;

    void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime >= spawnInterval)
        {
            SpawnAboveShip();
            currentTime = 0f;
        }
    }

    void SpawnAboveShip()
    {
        if (areaCenter == null)
        {
            return;
        }

        Vector3 randomOffset = Random.insideUnitSphere * spawnRadius;
        randomOffset.y = 0;

        Vector3 startPoint = areaCenter.position + randomOffset + Vector3.up * initialHeight;

        if (Physics.Raycast(startPoint, Vector3.down, out RaycastHit hit, Mathf.Infinity, shipLayer))
        {
            Vector3 spawnPosition = hit.point + Vector3.up * 0.1f;

            Instantiate(collectibleObject, spawnPosition, Quaternion.identity);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (areaCenter != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(areaCenter.position, spawnRadius);
        }
    }
}