using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrystalProject.Units.Data
{
    public interface IUnitData 
    {
        public Unit Unit { get;  }
        public bool CanBeCombined { get; }
    }
}
