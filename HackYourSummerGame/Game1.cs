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

        private int level;
        private List<Enemy> enemies;
        private List<Ally> allies;
        private EnemyLoader enemyLoader;
        private Battlefield battlefield;
        private GameState gameState;
        private Queue<Ally> upgradeOrder;

        private Texture2D spider;
        private Texture2D cloakedStranger;
        private Texture2D attackButton;
        private Texture2D healthBar;
        private Texture2D healthContainer;
        private Texture2D start;
        private Texture2D menu;
        private Texture2D next;

        private Button startButton;
        private Button menuButton;
        private Button nextLevel;

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
            level = 1;
            gameState = GameState.Menu;
            upgradeOrder = new Queue<Ally>();

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
            start = Content.Load<Texture2D>("Start");
            menu = Content.Load<Texture2D>("Menu");
            next = Content.Load<Texture2D>("Next");

            // player / enemies
            allies.Add(new Ally(100, 9, 90, new Vector2(65, 650), cloakedStranger, healthBar, healthContainer, Content));
            //allies.Add(new Ally(100, 9, 100, new Vector2(125, 850), cloakedStranger, healthBar, healthContainer, Content));
            //allies.Add(new Ally(100, 9, 85, new Vector2(350, 650), cloakedStranger, healthBar, healthContainer, Content));
            //allies.Add(new Ally(100, 9, 65, new Vector2(420, 850), cloakedStranger, healthBar, healthContainer, Content));

            //enemies.Add(new Vyper(100, 0, 100, new Vector2(600 + enemies.Count * 220, 50 + enemies.Count * 55), spider, healthBar, healthContainer));
            //enemies.Add(new Vyper(100, 0, 100, new Vector2(600 + enemies.Count * 220, 50 + enemies.Count * 55), spider, healthBar, healthContainer));
            //enemies.Add(new Vyper(100, 0, 86, new Vector2(600 + enemies.Count * 220, 50 + enemies.Count * 55), spider, healthBar, healthContainer));
            //enemies.Add(new Vyper(100, 0, 84, new Vector2(600 + enemies.Count * 220, 50 + enemies.Count * 55), spider, healthBar, healthContainer));

            // Buttons
            startButton = new Button(start, new Rectangle(550, 400, 400, 200), new Rectangle(0, 0, 400, 200));
            menuButton = new Button(menu, new Rectangle(550, 400, 400, 200), new Rectangle(0, 0, 400, 200));
            nextLevel = new Button(next, new Rectangle(550, 400, 400, 200), new Rectangle(0, 0, 400, 200));

            enemyLoader = new EnemyLoader(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            switch (gameState)
            {
                case GameState.Menu:

                    // Start game
                    if (startButton.Clicked())
                    {
                        gameState = GameState.Battle;
                        
                        for(int i = 0; i < allies.Count; i++)
                        {
                            allies[i].Reset();
                        }

                        battlefield = new Battlefield(allies, enemyLoader.NextEnemySet(level));
                    }

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
                            for(int i = 0; i < allies.Count; i++)
                            {
                                upgradeOrder.Enqueue(allies[i]);
                            }
                            if (allies.Count < 3 && level % 5 == 0)
                            {
                                allies.Add(new Ally(100, 9, 90, new Vector2(65 + 100 * allies.Count, 650 + 100 * allies.Count),
                                    cloakedStranger, healthBar, healthContainer, Content));
                                for (int i = 0; i < level; i++)
                                {
                                    upgradeOrder.Enqueue(allies[allies.Count - 1]);
                                }
                            }
                        }
                        else if (battlefield.FightOver == -1)
                        {
                            battlefield = null;
                            gameState = GameState.Defeat;
                        }
                    }

                    break;

                case GameState.Upgrade:

                    if (upgradeOrder.Count > 0)
                    {
                        if (upgradeOrder.Peek().UpgradePlayer())
                        {
                            upgradeOrder.Dequeue();
                        }
                    }
                    else if(nextLevel.Clicked())
                    {
                        level++;
                        gameState = GameState.Battle;
                        for (int i = 0; i < allies.Count; i++)
                        {
                            allies[i].Reset();
                        }

                        battlefield = new Battlefield(allies, enemyLoader.NextEnemySet(level));
                    }

                    break;

                case GameState.Defeat:

                    // Take player to menu
                    if (menuButton.Clicked())
                    {
                        gameState = GameState.Menu;
                        level = 1;
                    }

                    break;
            }
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Wheat);

            _spriteBatch.Begin();

            switch (gameState)
            {
                case GameState.Menu:

                    startButton.Draw(_spriteBatch);

                    break;

                case GameState.Battle:

                    // Draw fight
                    if (battlefield != null)
                    {
                        battlefield.Draw(_spriteBatch);
                    }

                    break;

                case GameState.Upgrade:

                    if(upgradeOrder.Count > 0)
                    {
                        upgradeOrder.Peek().UpgradePlayerChoice(_spriteBatch);
                    }
                    else
                    {
                        nextLevel.Draw(_spriteBatch);
                    }

                    break;

                case GameState.Defeat:

                    menuButton.Draw(_spriteBatch);

                    break;
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
