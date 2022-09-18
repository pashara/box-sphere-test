using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Project.BattleLogic.SimpleAI
{
    public class BehaviourStateMachine : MonoBehaviour
    {
        private class BehaviourCasts
        {
            public int ExecutablePriority { get; }
            public IExecutableState ExecutableState { get; }
            public IEnterState EnterState { get; }
            public IGizmosableState GizmosableState { get; }
            public IParallelState ParallelState { get; }
            public bool TryNotInterrupt { get; }
            
            public BehaviourCasts(int executablePriority, IBehaviourState state)
            {
                ExecutablePriority = executablePriority;
                ExecutableState = state as IExecutableState;
                EnterState = state as IEnterState;
                GizmosableState = state as IGizmosableState;
                ParallelState = state as IParallelState;
                TryNotInterrupt = state is INotInterruptableBehaviour;
            }
        }

        private class ExecuteData
        {
            public IBehaviourCheckTrigger ExecuteTrigger { get; }
            public BehaviourCasts Handler { get; }

            public ExecuteData(IBehaviourCheckTrigger executeTrigger, BehaviourCasts handler)
            {
                ExecuteTrigger = executeTrigger;
                Handler = handler;
            }
        }
        
        private List<(int, ExecuteData)> _states = new();
        private List<ExecuteData> _parallelStates = new();
        private BehaviourCasts actualBehaviour = null;
        private CompositeDisposable _disposable = new();

        private void OnEnable()
        {
            Observable.EveryUpdate().Subscribe(x =>
            {
                Process(Time.deltaTime);
            }).AddTo(_disposable);
        }

        private void OnDisable()
        {
            _disposable.Clear();
        }
        
        public void PutAction(int priority, (IBehaviourCheckTrigger, IBehaviourState) data)
        {
            PutAction(priority, priority, data);
        }
        
        public void PutAction(int selectionPriority, int executablePriority, (IBehaviourCheckTrigger, IBehaviourState) data)
        {
            var actionInfo = new BehaviourCasts(executablePriority, data.Item2);
            var executeData = new ExecuteData(data.Item1, actionInfo);
            if (actionInfo.ParallelState != null)
            {
                _parallelStates.Add(executeData);
            }
            else
            {
                _states.Add((selectionPriority, executeData));
                _states.Sort((a, b) => b.Item1 - a.Item1);
            }
        }


        private void Process(float deltaTime)
        {
            foreach (var parallelState in _parallelStates)
            {
                if (parallelState.ExecuteTrigger.ShouldProcessState())
                {
                    parallelState.Handler.ParallelState.Process(deltaTime);
                }
            }

            var isStateRequireInterrupt = actualBehaviour is { TryNotInterrupt: true };
            
            var shouldCalculate = true;
            foreach (var state in _states)
            {
                if (isStateRequireInterrupt)
                {
                    shouldCalculate = (state.Item2.Handler.ExecutablePriority > actualBehaviour.ExecutablePriority);
                }
                
                if (shouldCalculate && state.Item2.ExecuteTrigger.ShouldProcessState())
                {
                    if (actualBehaviour != state.Item2.Handler)
                    {
                        actualBehaviour?.EnterState?.Exit();
                        actualBehaviour = state.Item2.Handler;
                        actualBehaviour.EnterState?.Enter();
                    }
                    break;
                }
            }

            if (actualBehaviour is { ExecutableState: { } })
            {
                if (!actualBehaviour.ExecutableState.Process(deltaTime))
                {
                    actualBehaviour?.EnterState?.Exit();
                    actualBehaviour = null;
                }
            }
        }

        private void OnDrawGizmos()
        {
            actualBehaviour?.GizmosableState?.OnDrawGizmos();
        }

        public void Clear()
        {
            actualBehaviour = null;
            _states.Clear();
            _parallelStates.Clear();
        }
    }
}