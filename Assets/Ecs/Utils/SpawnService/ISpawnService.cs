namespace Ecs.Utils.SpawnService
{
    public interface ISpawnService<in TEntity, out TObject>
    {
        TObject Spawn(TEntity entity);
    }
}