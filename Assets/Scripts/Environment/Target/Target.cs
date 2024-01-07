using System;
using Common;
using DG.Tweening;
using Interfaces;
using Managers;
using UnityEngine;
using Weapons;
using Random = UnityEngine.Random;

namespace Environment.Target
{
    public class Target : MonoBehaviour , IDamageable
    {
        public static event Action OnTargetDamaged;
        
        [SerializeField] private Animator animController;
        private static readonly int UpTrigger = Animator.StringToHash("Up");
        private static readonly int DownTrigger = Animator.StringToHash("Down");

        [SerializeField] private DamageZone[] damageZones;

        [Header("Particles")] 
        [SerializeField] private ParticleSystem metalParticle;
        
        private const float CoolDownToUp = 1.5f;

        private bool _isDown = false;

        [Header("Sounds")] 
        [SerializeField] private AudioClip[] metalSounds;
        private AudioSource _audioSource;
        
        private UIManager _uiManager;
        private LevelManager _levelManager;

        private Tween _toDownTweenTimer;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            _uiManager = UIManager.instance;
            _levelManager = LevelManager.instance;
        }

        public void TakeDamage(PointData pointData, DamageType damageType = DamageType.DefaultBullet)
        {
            if (_isDown == false)
            {
                OnTargetDamaged?.Invoke();
                
                PlayMetalSound();
                PlayParticle(pointData.damagePosition);
                Down();
                CheckDamagedPart(pointData.damagePosition, pointData.uiPosition);

                if (_levelManager.IsTraining)
                {
                    _levelManager.hits += 1;
                    return;
                }
                DOVirtual.DelayedCall(CoolDownToUp, Up);
            }
        }

        public void Up()
        {
            if (_isDown)
            {
                _isDown = false;
                animController.SetTrigger(UpTrigger);
            }
        }
        
        public void UpWithTimer(float timeToDown)
        {
            if (_isDown)
            {
                DOVirtual.DelayedCall(0.25f, () => _isDown = false);
                animController.SetTrigger(UpTrigger);

                _toDownTweenTimer = DOVirtual.DelayedCall(0.25f + timeToDown, Down);
            }
        }

        private void CheckDamagedPart(Vector3 damagePosition, Vector3 uiPosition)
        {
            int score = 35;
            foreach (var zone in damageZones)
            {
                if (zone.IsInDamageZone(damagePosition))
                {
                    score = (int) zone.Damage;
                    _levelManager.scores += score;
                    _uiManager.CreateHitText(uiPosition, zone.Damage.ToString(), Damageable.IsCritical(zone.Damage));
                    return;
                }
            }
            
            _levelManager.scores += score;
            _uiManager.CreateHitText(uiPosition, score.ToString(), false);
            
            
        }

        private void PlayParticle(Vector3 pos)
        {
            metalParticle.gameObject.transform.position = pos;
            metalParticle.gameObject.SetActive(true);
            metalParticle.Play();
        }

        public void Down()
        {
            if (_isDown == false)
            {
                _isDown = true;
                animController.SetTrigger(DownTrigger);
            }
        }

        public void PlayMetalSound()
        {
            var sound = metalSounds[Random.Range(0, metalSounds.Length)];

            _audioSource.clip = sound;
            _audioSource.Play();
        }

        public void Reset()
        {
            _toDownTweenTimer.Kill();
        }
    }
}
