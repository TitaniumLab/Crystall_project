using UnityEngine;

namespace CrystalProject.Units
{
    public interface ICombinable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tier">Tier of game unit.</param>
        /// <returns></returns>
        public Transform TryToCombine(int tier);
    }
}