using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class MissionItemSpawner : MonoBehaviour
{
    //재시작시 위치만 돌리는 방식이 아닌 새로 생성해야함 : 미션에 속한 아이템만 관리
    [SerializeField] GameObject cameraPrefab;
    [SerializeField] Transform cameraSpawnPoint;

    [SerializeField] GameObject filmPrefab;
    [SerializeField] Transform[] filmSpawnPoints;

    private readonly List<GameObject> spawnedItems = new();

    public void SpawnItems()
    {
        //카메라 1개와 필름 여러개 생성
        SpawnItem(cameraPrefab, cameraSpawnPoint);

        foreach(Transform point in filmSpawnPoints)
        {
            SpawnItem(filmPrefab, point);
        }
    }

    private void SpawnItem(GameObject prefab, Transform spawnPoint)
    {
        if(prefab == null || spawnPoint == null)
        {
            Debug.Log("연결되지 않았습니다");
            return;
        }

        GameObject item = Instantiate(
        prefab,
        spawnPoint.position,
        spawnPoint.rotation);

        spawnedItems.Add(item);
    }

    public void ResetItems()
    {
        //현재 남아있는 미션 아이템 제거
        //SpawnItems 호출
        ClearItems();
        SpawnItems();
    }

    public void ClearItems()
    {
        foreach (GameObject item in spawnedItems)
        {
            if (item != null)
                Destroy(item);
        }

        spawnedItems.Clear();
    }
}
