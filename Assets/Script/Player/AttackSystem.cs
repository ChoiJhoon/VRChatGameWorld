
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class AttackSystem : UdonSharpBehaviour
{
    public float attackRange = 2.0f; // 공격 범위
    public int AutoattackDamage = 1; // 공격 데미지
    public int attackDamage = 1;
    public float attackInterval = 1.0f; // 몇 초마다 공격을 실행할지
    public LayerMask targetLayer; // 공격 대상 레이어
    private float attackTimer; // 공격 타이머

    private void Start()
    {
        attackTimer = 0f;
    }

    void Update()
    {
        PerforAttackTime();
    }

    void PerformAttack()
    {
        // 공격 범위 내의 모든 대상 감지
        Collider[] hitTargets = Physics.OverlapSphere(transform.position, attackRange, targetLayer);
        foreach (Collider target in hitTargets)
        {
            // 대상에게 데미지를 적용하는 로직    
            Debug.Log($"{target.gameObject.name}이(가) 공격받았습니다.");
            // MonsterMovement 스크립트에서 TakeDamage 메서드를 호출
            MonsterMovement monster = target.GetComponent<MonsterMovement>();
            if (monster != null)
            {
                monster.TakeDamage(AutoattackDamage);
            }
        }
    }

    void PerforAttackTime()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer >= attackInterval)
        {
            PerformAttack();
            attackTimer = 0f; // 타이머 초기화
        }
    }

    void OnDrawGizmosSelected() // 공격 범위를 시각적으로 표시
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    void OnTriggerEnter(Collider other)
    {
        // 충돌한 객체가 몬스터인지 확인
        MonsterMovement monster = other.GetComponent<MonsterMovement>();
        if (monster != null)
        {
            Debug.Log($"{other.gameObject.name}이(가) 공격받았습니다.");
            monster.TakeDamage(attackDamage);
        }
    }
}
