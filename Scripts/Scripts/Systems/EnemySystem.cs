using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class EnemySystem : Singleton<EnemySystem>
{
    [SerializeField] private EnemyBoardView enemyBoardView;

    protected override void Awake()
    {
        base.Awake();

        // 중복 오브젝트 방지
        if (Instance != this) return;

        // Performer 등록
        ActionSystem.AttachPerformer<EnemyTurnGA>(EnemyTurnPerformer);
        ActionSystem.AttachPerformer<AttackHeroGA>(AttackHeroPerformer);
    }

     void OnDestroy()
    {
        // 안전하게 해제
        if (Instance != this) return;

        ActionSystem.DetachPerformer<EnemyTurnGA>();
        ActionSystem.DetachPerformer<AttackHeroGA>();
    }

    // 적 초기화
    public void Setup(List<EnemyData> enemyDatas)
    {
        foreach (var enemyData in enemyDatas)
        {
            enemyBoardView.AddEnemy(enemyData);
        }
    }

    // EnemyTurn 수행
    private IEnumerator EnemyTurnPerformer(EnemyTurnGA enemyTurnGA)
    {
        foreach (var enemy in enemyBoardView.EnemyViews)
        {
            AttackHeroGA attackHeroGA = new(enemy);
            ActionSystem.Instance.AddReaction(attackHeroGA);
        }

        yield return null;
    }

    // 공격 수행
    private IEnumerator AttackHeroPerformer(AttackHeroGA attackHeroGA)
    {
        EnemyView attacker = attackHeroGA.Attacker;

        // 이동 애니메이션
        Tween moveOut = attacker.transform.DOMoveX(attacker.transform.position.x - 1f, 0.15f);
        yield return moveOut.WaitForCompletion();

        Tween moveBack = attacker.transform.DOMoveX(attacker.transform.position.x + 1f, 0.25f);
        yield return moveBack.WaitForCompletion();

        // 데미지 적용
        DealDamageGA dealDamageGA = new(attacker.AttackPower, new() { HeroSystem.Instance.HeroView });
        ActionSystem.Instance.AddReaction(dealDamageGA);
    }
}
