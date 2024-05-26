using UnityEngine;

namespace CrystalProject.Units
{
    public interface ICombinable
    {
        /// <summary>
        /// Allow to combine with other objects
        /// </summary>
        /// <param name="tier">Tier of game unit.</param>
        /// <returns>Transform of combinable object</returns>
        public bool TryToCombine(int tier, Vector3 position);
    }
}