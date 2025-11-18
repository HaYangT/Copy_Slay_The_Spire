using UnityEngine;
using System.Collections.Generic;

public class AIlEnemiesTM : TargetMode
{
    public override List<CombatantView> GetTargets()
    {
        return new(EnemySystem.Instance.Enemies);
    }
}
