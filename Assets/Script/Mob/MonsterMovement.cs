using UdonSharp;
using UnityEngine;
using UnityEngine.AI;
using VRC.SDKBase;
using VRC.Udon;

public class MonsterMovement : UdonSharpBehaviour
{
    private float mobSpeed = 1.0f; // 몬스터 이동 속도
    private Vector3 targetPosition; // 타겟 위치

    public int mobsLifeCount = 100; // 몬스터의 초기 생명 수

    void Start()
    {
        // 초기 타겟 포지션을 현재 위치로 설정
        targetPosition = transform.position;
    }

    private void FixedUpdate()
    {
        PlayerFind(); // 플레이어 찾기
        MoveTowardsPlayer(); // 플레이어를 향해 이동
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
        Vector3 direction = (targetPosition - transform.position).normalized; // 플레이어 방향 계산
        transform.position += direction * mobSpeed * Time.deltaTime; // 플레이어를 향해 이동

        // 몬스터의 회전도 플레이어를 향하도록 설정
        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }

    public void TakeDamage(int damage)
    {
        mobsLifeCount -= damage; // 데미지 만큼 생명 수 감소

        // 생명 수가 0 이하가 되면 몬스터를 제거
        if (mobsLifeCount <= 0)
        {
            Die();
        }
    }

    // 몬스터가 제거되는 메서드
    private void Die()
    {
        Debug.Log($"{gameObject.name}이(가) 사망했습니다.");
        Destroy(gameObject); // 몬스터 오브젝트 제거
    }
}
