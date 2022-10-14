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
    
    public enum RelativePosition
    {
        TopLeft,
        Top,
        TopRight,
        Left,
        Center,
        Right,
        BottomLeft,
        Bottom,
        BottomRight,
        FrontTopLeft,
        FrontTop,
        FrontTopRight,
        FrontLeft,
        Front,
        FrontRight,
        FrontBottomLeft,
        FrontBottom,
        FrontBottomRight,
        BackTopLeft,
        BackTop,
        BackTopRight,
        BackLeft,
        Back,
        BackRight,
        BackBottomLeft,
        BackBottom,
        BackBottomRight

        
    }
}