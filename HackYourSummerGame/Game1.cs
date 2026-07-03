using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

namespace HackYourSummerGame
{
    public enum TurnOrder
    {
        Ally,
        Enemy
    }

    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private TurnOrder currentTurn;
        private double timer;

        private Character enemy;
        private Ally player;


        private Texture2D spider;
        private Texture2D cloakedStranger;
        private Texture2D attackButton;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            currentTurn = TurnOrder.Ally;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Sprites
            cloakedStranger = Content.Load<Texture2D>("Small char 3 02c");
            spider = Content.Load<Texture2D>("Spider");
            attackButton = Content.Load<Texture2D>("AttackButton");

            // player / enemies
            player = new Ally(100, 9, new Vector2(100, 350), cloakedStranger, attackButton);
            enemy = new Character(100, 8, new Vector2(600, 50), spider);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();



            // Player attacks
            if (currentTurn == TurnOrder.Ally && timer > 1)
            {
                if (player.GetPlayerChoice(enemy))
                {
                    currentTurn = TurnOrder.Enemy;
                    timer = 0;
                }
            }
            else if (currentTurn == TurnOrder.Enemy && timer > 1)
            {
                enemy.GenericAttack(player);
                currentTurn = TurnOrder.Ally;
            }

            timer += gameTime.ElapsedGameTime.TotalSeconds;



            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            if(enemy.Health > 0)
            {
                enemy.Draw(_spriteBatch);
            }

            if (player.Health > 0)
            {
                player.Draw(_spriteBatch);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
