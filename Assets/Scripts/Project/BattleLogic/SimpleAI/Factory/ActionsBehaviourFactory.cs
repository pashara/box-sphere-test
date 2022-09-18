using ECS.Systems;
using Project.BattleLogic.SimpleAI.States;
using Project.BattleLogic.SimpleAI.States.Actions;
using Project.BattleLogic.SimpleAI.States.Movement;
using ProjectShared.Battler;
using UnityEngine;

namespace Project.BattleLogic.SimpleAI.Factory
{
    public interface IActionsBehaviourFactory
    {
        void Create(ICharacter character);
    }

    public class ActionsBehaviourFactory : IActionsBehaviourFactory
    {
        private readonly IECSContext _ecsContext;

        public ActionsBehaviourFactory(IECSContext ecsContext)
        {
            _ecsContext = ecsContext;
        }
        
        public void Create(ICharacter character)
        {
            var go = new GameObject("ActionsBehaviourTree");
            go.transform.parent = character.AIRoot.transform;
            
            var lastAttackTimer = new FloatReference();
            
            var stateMachine = go.AddComponent<BehaviourStateMachine>();
            
            stateMachine.PutAction(0, (new StandState.EnterTrigger(), new StandState()));
            
            stateMachine.PutAction(10, (
                new FindTargetBehaviourState.EnterTrigger(character, _ecsContext),
                new FindTargetBehaviourState(character, _ecsContext)));
            
            stateMachine.PutAction(20, (
                new AttackBehaviour.EnterTrigger(character, _ecsContext), 
                new AttackBehaviour(character, _ecsContext, lastAttackTimer)));
            
            stateMachine.PutAction(0, (
                new TimersIncrementState.EnterTrigger(), 
                new TimersIncrementState(lastAttackTimer)));
        }
    }
}