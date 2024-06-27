
using UdonSharp;
using UnityEngine;
using UnityEngine.AI;
using VRC.SDKBase;
using VRC.Udon;

public class MonsterMovement : UdonSharpBehaviour
{
    public GameObject mobPrefab;
    private float mobSpeed = 1.0f;
    private Vector3 targetPosition;

    void Start()
    {
        // 초기 타겟 포지션을 현재 위치로 설정
        targetPosition = transform.position;
    }

    private void FixedUpdate()
    {
        PlayerFind();
        MoveTowardsPlayer();
    }

    private void PlayerFind()
    {
        int playerCount = VRCPlayerApi.GetPlayerCount(); // 현재 플레이어 수 가져오기
        VRCPlayerApi[] players = new VRCPlayerApi[playerCount]; // 플레이어 배열 생성
        VRCPlayerApi.GetPlayers(players);

        if (playerCount > 0)
        {
            // 첫 번째 플레이어의 위치를 타겟 포지션으로 설정
            targetPosition = players[0].GetPosition();
        }
    }

    private void MoveTowardsPlayer()
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        transform.position += direction * mobSpeed * Time.deltaTime;

        // 몬스터의 회전도 플레이어를 향하도록 설정
        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }

    //몬스터 Life 시스템
    //무기 시스템 먼저 ^^
}
