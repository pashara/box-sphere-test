using System;
using UnityEngine;

namespace ThirdParty.GizmosExtentsions
{
    public class GizmosDisposable : IDisposable
    {
        private Color color;
        public GizmosDisposable()
        {
            color = Gizmos.color;
        }
        
        public void Dispose()
        {
            Gizmos.color = color;
        }
    }
    public class GizmosExtensions
    {
        
    }
}