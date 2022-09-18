using System;
using UnityEngine;

namespace Project.MonoTarget
{
    public static class MonoBehaviourTargetExtension
    {
        public static T GetTarget<T>(this IMonoBehaviourTarget target)
        {
            try
            {
                if (target.Target is T casted)
                {
                    return casted;
                }

                return default(T);
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
                return default(T);
            }
        }
    }
}