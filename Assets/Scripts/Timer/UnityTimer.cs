
using UnityEngine;

namespace Shooter.Timer
{
    public class UnityTimer : ITimer
    {
        public float DeltaTime => Time.deltaTime;
    }
}
