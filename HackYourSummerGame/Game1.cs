using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

namespace HackYourSummerGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private List<Enemy> enemies;
        private List<Ally> allies;
        private Battlefield battlefield;

        private Texture2D spider;
        private Texture2D cloakedStranger;
        private Texture2D attackButton;
        private Texture2D healthBar;
        private Texture2D healthContainer;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.PreferredBackBufferHeight = 1000;
            _graphics.PreferredBackBufferWidth = 1500;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            allies = new List<Ally>();
            enemies = new List<Enemy>();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Sprites
            cloakedStranger = Content.Load<Texture2D>("Small char 3 02c");
            spider = Content.Load<Texture2D>("Spider");
            attackButton = Content.Load<Texture2D>("AttackButton");
            healthBar = Content.Load<Texture2D>("Health Bar");
            healthContainer = Content.Load<Texture2D>("Health Container");

            // player / enemies
            allies.Add(new Ally(100, 9, 85, new Vector2(100, 850), cloakedStranger, attackButton, healthBar, healthContainer));


            enemies.Add(new Enemy(100, 8, 100, new Vector2(600 + enemies.Count * 220, 50 + enemies.Count * 55), spider, healthBar, healthContainer));
            enemies.Add(new Enemy(100, 8, 100, new Vector2(600 + enemies.Count * 220, 50 + enemies.Count * 55), spider, healthBar, healthContainer));
            enemies.Add(new Enemy(100, 8, 86, new Vector2(600 + enemies.Count * 220, 50 + enemies.Count * 55), spider, healthBar, healthContainer));
            enemies.Add(new Enemy(100, 8, 84, new Vector2(600 + enemies.Count * 220, 50 + enemies.Count * 55), spider, healthBar, healthContainer));
            battlefield = new Battlefield(allies, enemies);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            battlefield.Update(gameTime);


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.PaleVioletRed);

            _spriteBatch.Begin();

            battlefield.Draw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
