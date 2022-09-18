using System.Collections.Generic;
using DesperateDevs.Utils;
using ECS.Components;
using ECS.Systems;
using Entitas;
using ProjectShared.Battler;
using ProjectShared.Battler.Components;
using UniRx;
using UnityEngine;

namespace Project.BattleLogic.SimpleAI.States.Actions
{
    public class AttackBehaviour : IBehaviourState, IEnterState, IExecutableState
    {
        public class EnterTrigger : IBehaviourCheckTrigger
        {
            private CompositeDisposable _disposable = new();
            private readonly BattlerEntity _battlerEntity;
            private readonly IGroup<BattlerEntity> _battlers;
            private IECSContext _ecsContext;

            private ICharacterDetectorComponent _characterDetector;
            private readonly BattlerEntity _currentBattler;

            public EnterTrigger(ICharacter character, IECSContext ecsContext)
            {
                _currentBattler = ecsContext.Contexts.battler.GetEntityWithCharacterReference(character);
                _ecsContext = ecsContext;
            }

            public bool ShouldProcessState()
            {
                return _currentBattler.hasAttackTarget &&
                       _ecsContext.Contexts.stats.GetEntityWithBattlerSourceId(_currentBattler.id.Value) != null;
            }

            public void Dispose()
            {
                _disposable.Dispose();
            }
        }

        private readonly BattlerEntity _battlerEntity;
        private readonly IGroup<BattlerEntity> _battlers;
        
        private IPositionHandler _targetPositionHandler;
        private ICharacterDetectorComponent _characterDetector;
        private readonly StatsEntity _statsEntity;
        private readonly FloatReference _attackTimer;
        private readonly IECSContext _ecsContext;

        public AttackBehaviour(ICharacter character, IECSContext ecsContext, FloatReference attackTimer)
        {
            _ecsContext = ecsContext;
            _attackTimer = attackTimer;
            _battlerEntity = ecsContext.Contexts.battler.GetEntityWithCharacterReference(character);
            character.CharacterComponentsContainer.TryGet(
                out _characterDetector);
            _statsEntity = _ecsContext.Contexts.stats.GetEntityWithBattlerSourceId(_battlerEntity.id.Value);
        }

        public void Enter()
        {
        }

        public void Exit()
        {
        }

        public bool Process(float deltaTime)
        {
            var attackTargetId = _battlerEntity.attackTarget.Value;
            var targetBattler = _ecsContext.Contexts.battler.GetEntityWithId(attackTargetId);
            var targetCharacter = targetBattler?.characterReference?.Value;
            var sourceStats = _ecsContext.Contexts.stats.GetEntityWithBattlerSourceId(_battlerEntity.id.Value);

            if (targetCharacter == null || sourceStats == null)
                return false;
            
            var targetInZone = (_characterDetector.HandledCharacters.Contains(targetCharacter));
            var attackSpeed = 0f;
            if (!_statsEntity.TryGetStat(StatType.AttackSpeed, ref attackSpeed))
            {
                return false;
            }

            var shouldShot = (_attackTimer.Value > (1f / attackSpeed));
            
            if (shouldShot && targetInZone)
            {
                Debug.LogError("Attack");
                _attackTimer.Value = 0f;
                var attackerEntity = _ecsContext.Contexts.attacker.CreateEntity();
                var statsCopy = _ecsContext.Contexts.stats.CreateEntity();
                sourceStats.Copy(statsCopy);
                attackerEntity.AddStatsSourceCopy(statsCopy);
                attackerEntity.AddDestinationAttack(attackTargetId);
                attackerEntity.AddSourceAttack(_battlerEntity.id.Value);
            }
            
            return true;
        }
        
        public void Dispose()
        {
        }
    }
}