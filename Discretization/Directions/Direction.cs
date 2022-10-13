namespace Discretization
{
    /// <summary>
    /// Directions of the simulation space.  Direction One can be x, ξ, r,
    ///  Direction Two can be y, η, θ, Direction Three can be z, ζ, φ and
    ///  Direction Time is the Time direction.
    /// </summary>
    public enum Direction
    {
        One,
        Two,
        Three,
        Time
    }
    public abstract class SpaceDirection
    {

    }
}