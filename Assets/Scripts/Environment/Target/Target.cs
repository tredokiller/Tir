using Common;
using DG.Tweening;
using Interfaces;
using Managers;
using UnityEngine;
using Weapons;

namespace Environment.Target
{
    public class Target : MonoBehaviour , IDamageable
    {
        [SerializeField] private Animator animController;
        private static readonly int UpTrigger = Animator.StringToHash("Up");
        private static readonly int DownTrigger = Animator.StringToHash("Down");

        [SerializeField] private DamageZone[] damageZones;

        [Header("Particles")] 
        [SerializeField] private ParticleSystem metalParticle;
        
        private const float CoolDownToUp = 1.5f;

        private bool _isDown = false;

        private UIManager _uiManager;

        private void Start()
        {
            _uiManager = UIManager.instance;
        }

        public void TakeDamage(PointData pointData, DamageType damageType = DamageType.DefaultBullet)
        {
            if (_isDown == false)
            {
                PlayParticle(pointData.damagePosition);
                Down();
                CheckDamagedPart(pointData.damagePosition, pointData.uiPosition);
                DOVirtual.DelayedCall(CoolDownToUp, Up);
            }
        }

        private void Up()
        {
            if (_isDown)
            {
                DOVirtual.DelayedCall(0.25f, () => _isDown = false);
                animController.SetTrigger(UpTrigger);
            }
        }

        private void CheckDamagedPart(Vector3 damagePosition, Vector3 uiPosition)
        {
            foreach (var zone in damageZones)
            {
                if (zone.IsInDamageZone(damagePosition))
                {
                    _uiManager.CreateHitText(uiPosition, zone.Damage.ToString(), Damageable.IsCritical(zone.Damage));
                    return;
                }
            }
            
            _uiManager.CreateHitText(uiPosition, "35", false);
            
            
        }

        private void PlayParticle(Vector3 pos)
        {
            metalParticle.gameObject.transform.position = pos;
            metalParticle.gameObject.SetActive(true);
            metalParticle.Play();
        }

        private void Down()
        {
            if (_isDown == false)
            {
                _isDown = true;
                animController.SetTrigger(DownTrigger);
            }
        }
    }
}
