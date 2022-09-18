using System.Collections.Generic;
using ECS.Systems;
using Entitas;
using ProjectShared.Battler;
using ProjectShared.Battler.Components;
using UniRx;

namespace Project.BattleLogic.SimpleAI.States.Movement
{
    public class FollowTargetState : IBehaviourState, IEnterState, IExecutableState
    {
        private readonly ICharacter _character;
        private readonly IECSContext _ecsContext;

        public class EnterTrigger : IBehaviourCheckTrigger
        {
            private CompositeDisposable _disposable = new();
            private readonly BattlerEntity _battlerEntity;
            private readonly IGroup<BattlerEntity> _battlers;


            public EnterTrigger(ICharacter character, IECSContext ecsContext)
            {
                _battlerEntity = ecsContext.Contexts.battler.GetEntityWithCharacterReference(character);
            }

            public bool ShouldProcessState()
            {
                return _battlerEntity.hasAttackTarget;
            }

            public void Dispose()
            {
                _disposable.Dispose();
            }
        }

        private readonly BattlerEntity _battlerEntity;
        private readonly IGroup<BattlerEntity> _battlers;
        private List<BattlerEntity> handledBattlers = new List<BattlerEntity>();
        private IAgentComponent _agentComponent;
        private IPositionHandler _targetPositionHandler;

        public FollowTargetState(ICharacter character, IECSContext ecsContext)
        {
            _character = character;
            _ecsContext = ecsContext;
            _battlerEntity = ecsContext.Contexts.battler.GetEntityWithCharacterReference(character);
        }

        public void Enter()
        {
            _character.CharacterComponentsContainer.TryGet(out _agentComponent);
        }

        public void Exit()
        {
        }

        public bool Process(float deltaTime)
        {
            var targetId = _battlerEntity.attackTarget.Value;
            var targetBattler = _ecsContext.Contexts.battler.GetEntityWithId(targetId);
            if (targetBattler == null)
            {
                _battlerEntity.RemoveAttackTarget();
                return false;
            }

            _targetPositionHandler = targetBattler.characterReference.Value.PositionHandler;
            
            
            _agentComponent.Value.SetDestination(_targetPositionHandler.Position);
            return true;
        }
        
        public void Dispose()
        {
        }
    }
}