using System;
using ThirdParty.GizmosExtentsions;
using ThirdParty.StringHashCalculator;
using UnityEngine;

namespace ProjectCore.BattleLogic.Spawners
{
    public interface ISpawnerAnchor
    {
        int TeamId { get; }
        Vector3 Position { get; }
    }
    
    public class SpawnerAnchor : MonoBehaviour, ISpawnerAnchor
    {
        [SerializeField] private Color color;
        [SerializeField] private string teamIdentifier;

        public int TeamId => teamIdentifier.KeyHash();
        public Vector3 Position => transform.position;

        private void Awake()
        {
            gameObject.SetActive(false);
        }

        private void OnDrawGizmos()
        {
            using (new GizmosDisposable())
            {
                Gizmos.color = color;
                Gizmos.DrawWireSphere(transform.position, 3f);
                
                Gizmos.color = new Color(color.r, color.g, color.b, 0.3f);
                Gizmos.DrawSphere(transform.position, 3f);
            }
        }

    }
}