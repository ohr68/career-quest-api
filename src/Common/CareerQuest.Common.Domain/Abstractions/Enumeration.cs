using System.Reflection;

namespace CareerQuest.Common.Domain.Abstractions;

#pragma warning disable S4035
public abstract class Enumeration(int id, string name) : IEquatable<Enumeration>
#pragma warning restore S4035
{
    public int Id { get; } = id;

    public string Name { get; } = name;

    public override string ToString()
    {
        return Name;
    }

    public static IEnumerable<T> GetAll<T>()
        where T : Enumeration
    {
        return typeof(T)
            .GetFields(
                BindingFlags.Public |
                BindingFlags.Static |
                BindingFlags.DeclaredOnly)
            .Select(x => x.GetValue(null))
            .Cast<T>();
    }
    
    public static T FromId<T>(int id)
        where T : Enumeration
    {
        T? enumeration = GetAll<T>()
            .FirstOrDefault(x => x.Id == id);

        if (enumeration is null)
        {
            throw new InvalidOperationException(
                $"Enumeration '{typeof(T).Name}' with id '{id}' was not found.");
        }

        return enumeration;
    }
    
    public bool Equals(Enumeration? other)
    {
        if (other is null)
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return GetType() == other.GetType() &&
               Id == other.Id;
    }

    public override bool Equals(object? obj)
    {
        return obj is Enumeration other &&
               Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(GetType(), Id);
    }

    public static bool operator ==(
        Enumeration? left,
        Enumeration? right) => Equals(left, right);

    public static bool operator !=(
        Enumeration? left,
        Enumeration? right) => !Equals(left, right);
}
