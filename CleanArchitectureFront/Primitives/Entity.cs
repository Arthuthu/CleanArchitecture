﻿namespace CleanArchitectureFront.Primitives
{
    public abstract class Entity
    {
        public Guid Id { get; init; } = Guid.NewGuid();
    }
}
