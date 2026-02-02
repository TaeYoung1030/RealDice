
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarMovement : MonoBehaviour
{
    [Header("설정")]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] List<Transform> mapTiles;

    private int currentTileIndex = 0;

    private void Start()
    {
        if(mapTiles == null || mapTiles.Count ==0)
        {
            Debug.LogError("[AvataMovement] Map Tiles가 할당되지 않았습니다!");
        }
    }

    //주사위를 굴려서 나온 숫자만큼 아바타를 이동하는 함수
    public void MoveAvatar(int step)
    {
        StartCoroutine(MoveRoutine(step));
    }

    IEnumerator MoveRoutine(int step)
    {
        int targetIndex = currentTileIndex + step;

        for(int i=0; i<step; i++)
        {
            currentTileIndex++;

            if(currentTileIndex >= mapTiles.Count)
            {
                currentTileIndex = mapTiles.Count-1;
                break;
            }

            Transform nextTile = mapTiles[currentTileIndex];

            while(Vector3.Distance(transform.position, nextTile.position) > 0.05f)
            {
                transform.position = Vector3.MoveTowards(transform.position, nextTile.position, moveSpeed * Time.deltaTime);
                yield return null;
            }

            transform.position = nextTile.position;

            yield return new WaitForSeconds(0.2f);

        }
        Debug.Log("도착!");
        GameManager.instance.OnArriveTile();


    }
        



}
