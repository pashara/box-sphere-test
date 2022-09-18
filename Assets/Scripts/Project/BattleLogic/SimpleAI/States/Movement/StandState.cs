namespace Project.BattleLogic.SimpleAI.States.Movement
{
    public class StandState : IBehaviourState, IEnterState
    {
        public class EnterTrigger : IBehaviourCheckTrigger
        {

            public EnterTrigger()
            {
            }

            public bool ShouldProcessState()
            {
                return true;
            }

            public void Dispose()
            {
            }
        }

        public StandState()
        {
        }

        public void Enter()
        {
        }

        public void Exit()
        {
        }
        
        public void Dispose()
        {
        }
    }
}