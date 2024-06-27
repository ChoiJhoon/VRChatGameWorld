
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class SwordAttack : UdonSharpBehaviour
{
    public float attackRange = 2.0f; // 공격 범위
    public int attackDamage = 10; // 공격 데미지
    public LayerMask targetLayer; // 공격 대상 레이어

    void Update()
    {
            PerformAttack();
    }

    void PerformAttack()
    {
        // 공격 범위 내의 모든 대상 감지
        Collider[] hitTargets = Physics.OverlapSphere(transform.position, attackRange, targetLayer);

        foreach (Collider target in hitTargets)
        {
            // 대상에게 데미지를 적용하는 로직
            Debug.Log($"{target.gameObject.name}이(가) 공격받았습니다.");
            // target.GetComponent<HealthSystem>()?.TakeDamage(attackDamage);
        }
    }

    void OnDrawGizmosSelected() // 공격 범위를 시각적으로 표시
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
