namespace NBless
{
    using UnityEngine;
    public class Heal : BlessBase
    {
        public override void OnActive()
        {
            Debug.Log("Heal OnActive");
        }

        public override void OnDeactive()
        {
            Debug.Log("Heal OnDeactive");
        }

        public override void OnPlayerDamaged()
        {
            Debug.Log("Heal OnPlayerDamaged");
        }

        public override void OnPlayerHealed()
        {
            Debug.Log("Heal OnPlayerHealed");
        }

        public override void OnPlayerDead()
        {
            Debug.Log("Heal OnPlayerDead");
        }

        public override void OnPlayerJumped()
        {
            Debug.Log("Heal OnPlayerJumped");
        }

        public override void OnPlayerLanded()
        {
            Debug.Log("Heal OnPlayerLanded");
        }

        public override void OnPlayerMoved()
        {
            Debug.Log("Heal OnPlayerMoved");
        }

        public override void OnPlayerAttacked()
        {
            Debug.Log("Heal OnPlayerAttacked");
        }

        public override void OnPlayerKilledEnemy()
        {
            Debug.Log("Heal OnPlayerKilledEnemy");
        }

        public override void OnStageCleared()
        {
            Debug.Log("Heal OnStageCleared");
        }
    }
}
