/*
 * Leonard Vuniqi
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LVuniqiFinalProject.Components.ActionSceneComponents
{
    /// <summary>
    /// Player class controls drawing and updating of the players position and their hitbox
    /// </summary>
    public class Player : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        public Texture2D texture;
        public Vector2 position;
        private Vector2 speed;
        private KeyboardState keyState;
        public Rectangle boundingBox;

        public int Lives { get; set; } = 3;
        public bool isAlive = false;

        /// <summary>
        /// The player for the game
        /// </summary>
        /// <param name="game">The game this belongs to</param>
        /// <param name="spriteBatch">The spritebatch to draw it to</param>
        /// <param name="texture">The texture of the player</param>
        /// <param name="position">The initial position of the player</param>
        /// <param name="speed">The intial speed of the player</param>
        public Player(Game game, SpriteBatch spriteBatch, Texture2D texture, Vector2 position, Vector2 speed) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.texture = texture;
            this.position = position;
            this.speed = speed;
            //When a player is created set them to be alive
            isAlive = true;
        }

        /// <summary>
        /// Draws the player according to the position
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(texture, position, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        /// <summary>
        /// Updates the players position and their boundingbox
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            //Updates the players position according the keyboard input
            keyState = Keyboard.GetState();
            if (keyState.IsKeyDown(Keys.W))
                position.Y = MathHelper.Clamp(position.Y - speed.Y, 0, Shared.stage.Y);

            if (keyState.IsKeyDown(Keys.S))
                position.Y = MathHelper.Clamp(position.Y + speed.Y, 0, Shared.stage.Y - texture.Height);

            if (keyState.IsKeyDown(Keys.A))
                position.X = MathHelper.Clamp(position.X - speed.X, 0, Shared.stage.X);

            if (keyState.IsKeyDown(Keys.D))
                position.X = MathHelper.Clamp(position.X + speed.X, 0, Shared.stage.X - texture.Width);

            //Updates the players hitbox to accomodate the position change
            boundingBox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);

            base.Update(gameTime);
        }
    }
}
