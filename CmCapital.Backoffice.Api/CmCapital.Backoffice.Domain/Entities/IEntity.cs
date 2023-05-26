﻿namespace CmCapital.Backoffice.Domain.Entities;

public interface IEntity<TKey> where TKey : IEquatable<TKey>
{
    TKey Id { get; set; }
}