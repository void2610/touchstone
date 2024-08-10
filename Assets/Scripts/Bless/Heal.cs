namespace NBless
{
    using UnityEngine;
    using NManager;
    using NCharacter;
    public class Heal : BlessBase
    {
        public override bool OnActive(Player p = null)
        {
            if (p.hp < p.maxHp)
            {
                SoundManager.instance.PlaySe("heal");
                p.Heal(1);
                return true;
            }
            return false;
        }

        public override bool OnPlayerDamaged(Player p = null)
        {
            if (p.hp - 1 < p.maxHp)
            {
                SoundManager.instance.PlaySe("heal");
                p.Heal(1);
                return true;
            }
            return false;
        }
    }
}
