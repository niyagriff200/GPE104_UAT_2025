using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class DeathDestroy : Death
{  
    public override void Die()
    { 
        Destroy(gameObject);
    }
}
