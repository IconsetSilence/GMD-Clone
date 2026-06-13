using GMDClone.Gameplay;
using UnityEngine;

namespace GMDClone.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Character Speed", menuName = "GMD Clone/Character Speed")]
    public class CharacterSpeed : ScriptableObject
    {
        [SerializeField, Min(0)] private float _low, _normal, _medium, _high;

        public float GetSpeed(SpeedMode speedMode)
        {
            return speedMode switch
            {
                SpeedMode.Low => _low,
                SpeedMode.Normal => _normal,
                SpeedMode.Medium => _medium,
                SpeedMode.High => _high,
                _ => default
            };
        }
    }
}