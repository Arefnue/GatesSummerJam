using System.Collections.Generic;
using Managers;
using UnityEngine;

namespace PlayerScripts
{
    public class WeaponController : MonoBehaviour
    {
        public GameObject rangedWeaponGO;
        public GameObject meleeGO;

        public List<AudioClip> swingClipList;

        public enum WeaponStateEnum{
            Ranged,
            Melee,
            MeleeFinish
        }
        private WeaponStateEnum weaponState = WeaponStateEnum.Ranged;

        private Animator _animator;


        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q) && weaponState == WeaponStateEnum.Ranged )
            {
                SwitchToMelee();
            }
        }

        public void ChangeWeaponState(WeaponStateEnum newState)
        {
            weaponState = newState;

            switch (weaponState)
            {
                case WeaponStateEnum.MeleeFinish:
                    _animator.SetTrigger("MeleeFinish");
                    SwitchToRanged();
                    break;
            }
        }

        public void PlayAttackAudio()
        {
            var randomIndex = Random.Range(0, swingClipList.Count);
            AudioManager.Manager.PlaySfx(swingClipList[randomIndex]);
        }

        private void SwitchToMelee()
        {
            rangedWeaponGO.SetActive(false);
            meleeGO.SetActive(true);
            _animator.SetTrigger("Melee");
        }

        private void SwitchToRanged()
        {
            rangedWeaponGO.SetActive(true);
            meleeGO.SetActive(false);
            weaponState = WeaponStateEnum.Ranged;
        }
    }
}
