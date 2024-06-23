using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Shooter
{
    [CreateAssetMenu(fileName = nameof(CharacterConfig), menuName = nameof(CharacterConfig))]
    public class CharacterConfig :  ScriptableObject, ICharacterConfig
    {
        [field: SerializeField]
        public float Health { get; private set; }

        [field: SerializeField]
        public float Speed { get; private set; }
        [field: SerializeField]
        [Tooltip("Rotation speed")]
        public float MaxRadiansDelta { get; private set; }
    }
}
