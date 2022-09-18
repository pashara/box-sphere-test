using UnityEngine;

namespace ThirdParty.ComponentsBaker
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