using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class MonsterGenSystem : UdonSharpBehaviour
{
    public GameObject MonsterPrefab;
    public float spawnTimeMin;
    public float spawnTimeMax;
    public float spawnRadius = 10f; // 원의 반지름

    private float spawnTime;
    private float timeAfterSpawn;

    void Start()
    {
        timeAfterSpawn = 0f;
        spawnTime = Random.Range(spawnTimeMin, spawnTimeMax);
        Debug.Log("MonsterGenSystem 시작");
    }

    void Update()
    {
        mobGen();
    }

    private void Spawn(Vector3 spawnPosition)
    {
        Instantiate(MonsterPrefab, spawnPosition, Quaternion.identity);
        Debug.Log($"몬스터가 생성되었습니다: 위치 {spawnPosition}");
    }

    private void mobGen() // 몬스터 생성
    {
        timeAfterSpawn += Time.deltaTime;

        if (timeAfterSpawn >= spawnTime)
        {
            Vector3[] spawnPoints = GenerateSpawnPoints();
            foreach (Vector3 spawnPoint in spawnPoints)
            {
                Spawn(spawnPoint);
            }
            timeAfterSpawn = 0f;
            spawnTime = Random.Range(spawnTimeMin, spawnTimeMax);
            Debug.Log("몹 생성");
        }
    }
    private Vector3[] GenerateSpawnPoints()
    {
        int numPoints = 10; // 생성할 스폰 포인트 수
        Vector3[] spawnPoints = new Vector3[numPoints];
        Vector3 playerPosition = Vector3.zero;

        int playerCount = VRCPlayerApi.GetPlayerCount();
        VRCPlayerApi[] players = new VRCPlayerApi[playerCount];
        VRCPlayerApi.GetPlayers(players);

        if (players.Length > 0)
        {
            playerPosition = players[0].GetPosition(); // 첫 번째 플레이어의 위치
        }

        float angleStep = 360f / numPoints;
        for (int i = 0; i < numPoints; i++)
        {
            float angle = i * angleStep;
            float radian = angle * Mathf.Deg2Rad;
            float x = playerPosition.x + spawnRadius * Mathf.Cos(radian);
            float z = playerPosition.z + spawnRadius * Mathf.Sin(radian);
            spawnPoints[i] = new Vector3(x, playerPosition.y, z);
        }

        return spawnPoints;
    }
}
