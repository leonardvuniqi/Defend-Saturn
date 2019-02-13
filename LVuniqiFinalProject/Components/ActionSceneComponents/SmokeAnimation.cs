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
using LVuniqiFinalProject.Scenes;

namespace LVuniqiFinalProject.Components.ActionSceneComponents
{
    public class SmokeAnimation : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Texture2D texture;
        private Vector2 position;
        private List<Rectangle> frames;
        private int frameIndex = 1;
        private Vector2 frameDimension;
        private float delayCounter;
        private float delay = 1f;

        public SmokeAnimation(Game game, SpriteBatch _spriteBatch, Vector2 _position) : base(game)
        {
            this.spriteBatch = _spriteBatch;
            this.texture = this.Game.Content.Load<Texture2D>("Images/SmokeAnimation");
            this.position = _position;
            frameDimension = new Vector2(128, 128);

            this.Enabled = false;
            this.Visible = false;

            CreateFrames();
        }

        private void CreateFrames()
        {
            frames = new List<Rectangle>();
            for (int i = 0; i < 7; i++)
            {
                int x = i * (int)frameDimension.X;
                int y = 0;
                Rectangle newRect = new Rectangle(x, y, (int)frameDimension.X, (int)frameDimension.Y);
                frames.Add(newRect);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(texture, position, frames[frameIndex], Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            delayCounter++;
            if (delayCounter > delay)
            {
                frameIndex++;
                if (frameIndex > 6)
                {
                    frameIndex = -1;
                    this.Enabled = false;
                    this.Visible = false;
                    //remove the explosion from the action scene components once its finished
                    var actionsceneCompononets = this.Game.Services.GetService<ActionScene>().Components;
                    actionsceneCompononets.Remove(this);
                }
                delayCounter = 0;
            }
            base.Update(gameTime);
        }
    }
}
