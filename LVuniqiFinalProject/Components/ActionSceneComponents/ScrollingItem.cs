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
    /// Scrolling item class to use for any items that would require scrolling across screen like the background
    /// </summary>
    public class ScrollingItem : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        public Texture2D texture;
        public Vector2 position1;
        public Vector2 position2;
        public Vector2 speed;

        /// <summary>
        /// Instantiates a new scrolling item
        /// </summary>
        /// <param name="game">The game</param>
        /// <param name="spriteBatch">The spritebatch to draw it to</param>
        /// <param name="tex">The texture for the scrolling item</param>
        /// <param name="pos1">The first position of the scrolling item</param>
        /// <param name="pos2">The second position of the scrolling item</param>
        /// <param name="speed">How fast the item will scroll</param>
        public ScrollingItem(Game game, SpriteBatch spriteBatch, Texture2D tex, Vector2 pos1, Vector2 pos2, Vector2 speed) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.texture = tex;
            this.position1 = pos1;
            this.position2 = pos2;
            this.speed = speed;
        }

        /// <summary>
        /// Draws the parallaxing item
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(texture, position1, Color.White);
            spriteBatch.Draw(texture, position2, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
        
        /// <summary>
        /// Updates the parallaxing item position
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            //This will create the parallaxing effect for any item needing parallax
            position1 += speed;
            position2 += speed;
            if(position1.Y >= Shared.stage.Y)
            {
                position1.Y = 0;
                position2.Y = -Shared.stage.Y;
            }

            base.Update(gameTime);
        }
    }
}
