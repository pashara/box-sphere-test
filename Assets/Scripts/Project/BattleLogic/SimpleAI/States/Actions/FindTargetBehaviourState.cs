using System.Collections.Generic;
using ECS.Systems;
using Entitas;
using ProjectShared.Battler;
using ThirdParty.ListExtensions;
using UniRx;

namespace Project.BattleLogic.SimpleAI.States.Actions
{
    public class FindTargetBehaviourState : IBehaviourState, IEnterState, IExecutableState
    {
        public class EnterTrigger : IBehaviourCheckTrigger
        {
            private CompositeDisposable _disposable = new();
            private readonly BattlerEntity _battlerEntity;


            public EnterTrigger(ICharacter character, IECSContext ecsContext)
            {
                _battlerEntity = ecsContext.Contexts.battler.GetEntityWithCharacterReference(character);
            }

            public bool ShouldProcessState()
            {
                if (!_battlerEntity.hasTeamComponent)
                    return false;

                return !_battlerEntity.hasAttackTarget;
            }

            public void Dispose()
            {
                _disposable.Dispose();
            }
        }

        private readonly BattlerEntity _battlerEntity;
        private readonly IGroup<BattlerEntity> _battlers;
        private List<BattlerEntity> handledBattlers = new List<BattlerEntity>();

        public FindTargetBehaviourState(ICharacter character, IECSContext ecsContext)
        {
            _battlerEntity = ecsContext.Contexts.battler.GetEntityWithCharacterReference(character);
            _battlers = ecsContext.Contexts.battler.GetGroup(BattlerMatcher.AllOf(BattlerMatcher.TeamComponent,
                BattlerMatcher.CharacterReference));
        }

        public void Enter()
        {
        }

        public void Exit()
        {
        }

        public bool Process(float deltaTime)
        {
            handledBattlers.Clear();
            foreach (var battler in _battlers)
            {
                if (battler.hasTeamComponent && battler.teamComponent.Value != _battlerEntity.teamComponent.Value)
                {
                    handledBattlers.Add(battler);
                }
            }

            var randomBattler = handledBattlers.Random();
            if (randomBattler == null)
            {
                return false;
            }

            _battlerEntity.AddAttackTarget(randomBattler.id.Value);

            return true;
        }
        
        public void Dispose()
        {
        }
    }
}