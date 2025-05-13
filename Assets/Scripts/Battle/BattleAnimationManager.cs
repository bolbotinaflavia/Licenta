using System;
using System.Collections;
using DefaultNamespace;
using Enemies;
using Sliders_scripts;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace Battle
{
    public class BattleAnimationManager:MonoBehaviour
    {
        [SerializeField] private PlayerUnit player;
        [SerializeField]
        private HpSlider hpBarPlayer;
        [SerializeField] private BattleUnit enemy;
        [SerializeField] private HpBar hpEnemy;
        [SerializeField]
        private HpBarAnimation hpEnemyA;

        [SerializeField] private HpBarAnimation hpPlayerA;
        [SerializeField]private Notification notification;

        public IEnumerator startAnimationsPlayerAttack()
        {
            
            StartCoroutine(playerAttack());
            yield return new WaitForSeconds(2f);
            StartCoroutine(enemy_attacked());
            yield return new WaitForSeconds(2f);
        }

        public IEnumerator startAnimationsEnemyAttack()
        {
            yield return new WaitForSeconds(2f);
            StartCoroutine(enemyAttack());
            yield return new WaitForSeconds(2f);
            StartCoroutine(player_attacked());
            yield return new WaitForSeconds(2f);
        }
        public IEnumerator playerAttack()
        {
            player.IsAttacking = true;
            yield return new WaitForSeconds(2f);
        }
        public IEnumerator enemy_attacked()
        {
            player.IsAttacking = false;
            enemy.IsAttacked = true;
            hpEnemyA.damaging_animation();
            yield return new WaitForSeconds(2f);
            enemy.IsAttacked = false;
            yield return new WaitForSeconds(1f);
            StartCoroutine(hp_enemy());
        }

        private IEnumerator hp_enemy()
        {
        
            hpEnemy.UpdateUI_Enemy();
            yield return new WaitForSeconds(1f);
        }
        
        private IEnumerator enemyAttack()
        {
            enemy.IsAttacking = true;
            yield return new WaitForSeconds(2f);
        
        }
        
        private IEnumerator player_attacked()
        {
            enemy.IsAttacking = false;
            player.IsAttacked = true;
            hpPlayerA.damaging_animation();
            yield return new WaitForSeconds(2f);
            player.IsAttacked = false;
            yield return new WaitForSeconds(1f);
            StartCoroutine(hp_player());
        }

        private IEnumerator hp_player()
        {
            hpBarPlayer.UpdateUI();
            yield return new WaitForSeconds(1f);
        }

    }
}