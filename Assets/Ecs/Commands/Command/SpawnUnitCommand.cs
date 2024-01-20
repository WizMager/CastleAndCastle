using Ecs.Commands.Generator;
using Game.Utils.Units;
using UnityEngine;

namespace Ecs.Commands.Command
{
    [Command]
    public struct SpawnUnitCommand
    {
        public Vector3 Position; 
        public Quaternion Rotation; 
        public EUnitType UnitType;
        public bool IsPlayerUnit;
    }
}