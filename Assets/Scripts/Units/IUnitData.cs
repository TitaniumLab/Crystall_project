namespace CrystalProject.Units.Data
{
    public interface IUnitData 
    {
        public Unit Unit { get;  }
        public bool CanBeCombined { get; }
    }
}
