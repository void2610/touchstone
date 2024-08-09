namespace NBless
{
    using UnityEngine;
    public class Shield : BlessBase
    {
        public override void OnActive()
        {
            Debug.Log("Shield OnActive");
        }

        public override void OnDeactive()
        {
            Debug.Log("Shield OnDeactive");
        }

        public override void OnPlayerDamaged()
        {
            Debug.Log("Shield OnPlayerDamaged");
        }

        public override void OnPlayerHealed()
        {
            Debug.Log("Shield OnPlayerHealed");
        }

        public override void OnPlayerDead()
        {
            Debug.Log("Shield OnPlayerDead");
        }

        public override void OnPlayerJumped()
        {
            Debug.Log("Shield OnPlayerJumped");
        }

        public override void OnPlayerLanded()
        {
            Debug.Log("Shield OnPlayerLanded");
        }

        public override void OnPlayerMoved()
        {
            Debug.Log("Shield OnPlayerMoved");
        }

        public override void OnPlayerAttacked()
        {
            Debug.Log("Shield OnPlayerAttacked");
        }

        public override void OnPlayerKilledEnemy()
        {
            Debug.Log("Shield OnPlayerKilledEnemy");
        }

        public override void OnStageCleared()
        {
            Debug.Log("Shield OnStaeCleared");
        }
    }
}
