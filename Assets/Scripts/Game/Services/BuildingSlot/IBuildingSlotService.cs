using UniRx;

namespace Game.Services.BuildingSlot
{
    public interface IBuildingSlotService
    {
        IReactiveCommand<Unit> SlotReleased { get; }
    }
}