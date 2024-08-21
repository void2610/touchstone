namespace NBless
{
    using UnityEngine;
    using NManager;
    using NCharacter;
    public class Jump : BlessBase
    {
        public override bool OnPlayerCantJumped(Player p = null)
        {
            p.JumpByEnemy(1.5f);
            return true;
        }
    }
}
