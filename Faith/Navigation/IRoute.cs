using Clio.Utilities;
using System.Collections.Generic;

namespace Faith.Navigation
{
    /// <summary>
    /// High-level route through an area that completes all objectives.  Not to be confused with actual pathfinding.
    /// </summary>
    public interface IRoute
    {
        /// <summary>
        /// Gets the remaining <see cref="Waypoint"/>s in a route, relative to the given starting position.
        /// </summary>
        /// <param name="startingPos">Position to resume the route from.</param>
        /// <returns>Remaining <see cref="Waypoint"/>s to complete the route.</returns>
        Queue<Waypoint> Calculate(Vector3 startingPos);
    }
}
