namespace CrystalProject.Dropper
{
    /// <summary>
    /// Contains data for dropping game units
    /// </summary>
    public interface IDropData
    {
        public bool CanBeDropped { get; }
        public int ScoreToDrop { get; }
    }
}