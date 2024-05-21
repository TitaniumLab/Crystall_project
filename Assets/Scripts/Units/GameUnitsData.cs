using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrystalProject.Units
{
    public class GameUnitsData : MonoBehaviour
    {
        [SerializeField] private UnitBundleData _unitBundleData;
        private IGameUnit[] gameUnits;

        private void Awake()
        {
            
        }
    }
}