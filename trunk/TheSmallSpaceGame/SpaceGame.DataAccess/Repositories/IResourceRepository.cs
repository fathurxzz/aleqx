namespace SpaceGame.DataAccess.Repositories
{
    public interface IResourceRepository
    {
        void UpdateMetalMine(int planetId);
        void UpdateCrystalMine(int planetId);
        void UpdateDeiteriumGenerator(int planetId); 
    }
}