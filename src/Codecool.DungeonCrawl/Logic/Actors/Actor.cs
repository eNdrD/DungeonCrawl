using Codecool.DungeonCrawl.Logic.Map;
using Perlin.Display;
using Perlin.Geom;

namespace Codecool.DungeonCrawl.Logic.Actors
{
    /// <summary>
    ///     Actor is a base class for every entity in the dungeon.
    /// </summary>
    public abstract class Actor
    {
        // default ctor
        protected Actor(Cell cell, Rectangle tile)
        {
            Cell = cell;
            Cell.Actor = this;

            Sprite = new Sprite("tiles.png", false, tile);
            Sprite.ScaleX = Sprite.ScaleY = TileSet.Scale;

            Position = cell.Position;

            cell.Sprite.Parent.AddChild(Sprite);
        }

        /// <summary>
        ///     Invoked whenever another Actor attempts to walk onto this Actor's cell
        /// </summary>
        /// <param name="other"></param>
        /// <returns>Whether the other Actor can pass</returns>
        public virtual bool OnCollision(Actor other)
        {
            return true;
        }

        /// <summary>
        ///     Removes given actor from the game
        /// </summary>
        public void Destroy()
        {
            Sprite.Parent.RemoveChild(Sprite);
        }

        public Cell Cell { get; private set; }

        public (int x, int y) Position
        {
            get => _position;
            set
            {
                _position = value;
                Sprite.X = value.x * TileSet.Size * TileSet.Scale;
                Sprite.Y = value.y * TileSet.Size * TileSet.Scale;
            }
        }

        private (int x, int y) _position;

        public Sprite Sprite { get; set; }

        /// <summary>
        ///     Assign this Actor to given cell
        /// </summary>
        /// <param name="target"></param>
        public void AssignCell(Cell target)
        {
            Cell.Actor = null;
            Cell = target;
            target.Actor = this;

            Position = target.Position;
        }
    }
}