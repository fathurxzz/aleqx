namespace SpaceGame.DataAccess.Repositories
{
    public interface IResourceRepository
    {
        void UpdateMetalMine(int planetId, short roboticsLevel, short naniteLevel);
        void UpdateCrystalMine(int planetId, short roboticsLevel, short naniteLevel);
        void UpdateDeiteriumGenerator(int planetId, short roboticsLevel, short naniteLevel); 
    }
}