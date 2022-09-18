using ECS.Systems;
using Project.BattleLogic.SimpleAI.States.Movement;
using ProjectShared.Battler;
using UnityEngine;

namespace Project.BattleLogic.SimpleAI.Factory
{
    public interface IMovementBehaviourFactory
    {
        void Create(ICharacter character);
    }
    
    public class MovementBehaviourFactory : IMovementBehaviourFactory
    {
        private readonly IECSContext _ecsContext;

        public MovementBehaviourFactory(IECSContext ecsContext)
        {
            _ecsContext = ecsContext;
        }
        
        public void Create(ICharacter character)
        {
            var go = new GameObject("MovementBehaviourTree");
            go.transform.parent = character.AIRoot.transform;
            var stateMachine = go.AddComponent<BehaviourStateMachine>();
            
            stateMachine.PutAction(0, (new StandState.EnterTrigger(), new StandState()));
            
            stateMachine.PutAction(10, (
                new FollowTargetState.EnterTrigger(character, _ecsContext), 
                new FollowTargetState(character, _ecsContext)));

            // stateMachine.PutAction(50, (
            //     new FindTargetBehaviourState.EnterTrigger(character, _ecsContext),
            //     new FindTargetBehaviourState(character, _ecsContext)));
        }
    }
}