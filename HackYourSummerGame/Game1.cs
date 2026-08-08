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
    public enum GameState
    {
        Menu,
        Battle,
        Upgrade,
        Defeat
    }

    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private List<Enemy> enemies;
        private List<Ally> allies;
        private Battlefield battlefield;
        private GameState gameState;

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
            gameState = GameState.Menu;

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


            enemies.Add(new Spider(100, 8, 100, new Vector2(600 + enemies.Count * 220, 50 + enemies.Count * 55), spider, healthBar, healthContainer));
            enemies.Add(new Spider(100, 8, 100, new Vector2(600 + enemies.Count * 220, 50 + enemies.Count * 55), spider, healthBar, healthContainer));
            enemies.Add(new Spider(100, 8, 86, new Vector2(600 + enemies.Count * 220, 50 + enemies.Count * 55), spider, healthBar, healthContainer));
            enemies.Add(new Spider(100, 8, 84, new Vector2(600 + enemies.Count * 220, 50 + enemies.Count * 55), spider, healthBar, healthContainer));
            battlefield = new Battlefield(allies, enemies);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            switch (gameState)
            {
                case GameState.Menu:


                    break;

                case GameState.Battle:

                    // if a battlefield exists, run it
                    if (battlefield != null)
                    {
                        battlefield.Update(gameTime);

                        if (battlefield.FightOver == 1)
                        {
                            battlefield = null;
                            gameState = GameState.Upgrade;
                        }
                        else if (battlefield.FightOver == -1)
                        {
                            battlefield = null;
                            gameState = GameState.Defeat;
                        }
                    }

                    break;

                case GameState.Upgrade:


                    break;

                case GameState.Defeat:


                    break;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.PaleVioletRed);

            _spriteBatch.Begin();

            switch (gameState)
            {
                case GameState.Menu:


                    break;

                case GameState.Battle:

                    // Draw fight
                    if (battlefield != null)
                    {
                        battlefield.Draw(_spriteBatch);
                    }

                    break;

                case GameState.Upgrade:


                    break;

                case GameState.Defeat:


                    break;
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
