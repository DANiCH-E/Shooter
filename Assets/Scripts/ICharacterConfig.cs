﻿
namespace Shooter
{
    public interface ICharacterConfig
    {
        float Health { get; }
        float Speed { get; }
        float MaxRadiansDelta { get; }
        float InitialSpeed { get; }
        float Boost { get; }
        float BoostForEscape { get; }
    }
}
