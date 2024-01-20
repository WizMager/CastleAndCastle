namespace Db.Camera
{
    public interface ICameraBase
    {
        float MoveThreshold { get; }
        float MoveSensitive { get; }
    }
}