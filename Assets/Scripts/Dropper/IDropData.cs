namespace CrystalProject.Dropper
{
    public interface IDropData
    {
        public bool CanBeDropped { get; }
        public int ScoreToDrop { get; }
    }
}
