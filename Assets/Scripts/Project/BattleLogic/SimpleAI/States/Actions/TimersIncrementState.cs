using System.Collections.Generic;

namespace Project.BattleLogic.SimpleAI.States.Actions
{
    public class TimersIncrementState : IBehaviourState, IParallelState
    {
        public class EnterTrigger : IBehaviourCheckTrigger
        {
            public bool ShouldProcessState() => true;
            public void Dispose() { }
        }

        private readonly List<FloatReference> _timers = new List<FloatReference>();

        public TimersIncrementState(List<FloatReference> timers)
        {
            _timers.AddRange(timers);
        }

        public TimersIncrementState(FloatReference timer)
        {
            _timers.Add(timer);
        }
        
        public void Dispose()
        {
        }

        public bool Process(float deltaTime)
        {
            foreach (var timer in _timers)
            {
                timer.Value += deltaTime;
            }

            return true;
        }
    }
}