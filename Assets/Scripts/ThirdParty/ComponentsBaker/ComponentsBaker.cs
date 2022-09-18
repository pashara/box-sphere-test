using System.Collections.Generic;
using UnityEngine;

namespace ProjectCore.ComponentsBaker
{
    public abstract class ComponentsBaker : MonoBehaviour
    {
        public void Find()
        {
            FindComponents();
        }

        protected abstract void FindComponents();
    }
}