namespace NBless
{
    using UnityEngine;
    using NManager;
    using NCharacter;
    public class Shield : BlessBase
    {

        public override bool OnPlayerDamaged(Player p = null)
        {
            SoundManager.instance.PlaySe("shield");
            return true;
        }
    }
}
