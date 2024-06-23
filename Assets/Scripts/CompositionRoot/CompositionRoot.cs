using Shooter.Timer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Shooter.CompositionRoot
{
    public abstract class CompositionRoot<T> : MonoBehaviour where T : MonoBehaviour
    {
        public abstract T Compose(ITimer timer);
    }
}
