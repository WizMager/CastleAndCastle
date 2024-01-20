namespace Ecs.Utils.LinkedEntityRepository
{
    public interface ILinkedEntityRepository : IRepository<int, GameEntity>
    {
        bool TryGet(int id, out GameEntity entity);
    }
}