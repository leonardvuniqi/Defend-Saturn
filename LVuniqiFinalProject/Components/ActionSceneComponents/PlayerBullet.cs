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
    public class PlayerBullet : Bullet
    {

        public PlayerBullet(Game game, SpriteBatch spriteBatch, Vector2 position) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.texture = game.Content.Load<Texture2D>("Images/PlayerBullet");
            this.position = position;
            this.speed = new Vector2(0, 11);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            position -= speed;
            
            base.Update(gameTime);
        }
    }
}
