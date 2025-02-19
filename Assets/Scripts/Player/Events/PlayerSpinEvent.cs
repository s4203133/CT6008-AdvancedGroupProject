﻿using UnityEngine;
using LMO.Interfaces;
using LMO.Player;

namespace LMO.CustomEvents {

    public class PlayerSpinEvent : MonoBehaviour, ICustomEvent {
        [Header("SUBJECT")]
        [SerializeField] private PlayerSpinAttack playerSpin;

        // Observers
        private PlayerVFX playerVFX;
        private PlayerSquashAndStretch squishy;
        private Animator animator;

        public void Initialise(EventManager manager) {
            PlayerEventManager player = manager as PlayerEventManager;
            playerVFX = player.VFX;
            squishy = player.SqashAndStretch;
            animator = player.Anim;
        }

        // When the player dives, notify other systems so they can respond
        public void SubscribeEvents() {
            if (playerSpin == null) {
                return;
            }
            playerSpin.OnSpin += playerVFX.PlaySpinVFX;
            playerSpin.OnSpin += squishy.SpinAttack.Play;
            playerSpin.OnSpin += AnimateSpin;
        }

        public void UnsubscribeEvents() {
            if (playerSpin == null) {
                return;
            }
            playerSpin.OnSpin -= playerVFX.PlaySpinVFX;
            playerSpin.OnSpin -= squishy.SpinAttack.Play;
            playerSpin.OnSpin -= AnimateSpin;
        }

        private void AnimateSpin() {
            animator.SetTrigger("Spin");
        }
    }
}