namespace CrystalProject.Units.Create
{
    public interface IUnitDispenser
    {
        /// <summary>
        /// Gets an unit object of a specific tier.
        /// </summary>
        /// <param name="unitTier">Tier of unit.</param>
        /// <returns>Unit object.</returns>
        public Unit GetUnit(int unitTier);
    }
}

