using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace HackYourSummerGame
{
    public enum PlayerScreen
    {
        CharacterSelect,
        MoveSelect
    }
    internal class Battlefield
    {
        // Teams
        private List<Ally> playerParty;
        private List<Enemy> enemyTeam;

        private Queue<Character> attackOrder;
        private PlayerScreen playerScreen;
        private TurnOrder currentTurn;

        private Random rng;
        private double timer;

        // Constructor
        public Battlefield(List<Ally> playerParty, List<Enemy> enemyTeam)
        {
            this.playerParty =new List<Ally>();
            this.enemyTeam = new List<Enemy>();

            for (int i = 0; i < playerParty.Count; i++)
            {
                this.playerParty = playerParty;
            }

            for (int i = 0; i < enemyTeam.Count; i++)
            {
                this.enemyTeam = enemyTeam;
            }
            
            currentTurn = TurnOrder.Ally;
            playerScreen = PlayerScreen.MoveSelect;
            attackOrder = new Queue<Character>();

            rng = new Random();
            timer = 0;
        }

        // Run battle instance
        public void Update(GameTime gameTime)
        {
            // Increase tm if needed
            if(attackOrder.Count == 0)
            {
                NextTurn();
            }

            if(attackOrder.Peek() is Enemy && timer > 0.5)
            {
                attackOrder.Dequeue().GenericAttack(playerParty[rng.Next(0, playerParty.Count)]);
                timer = 0;
            }
            else if(attackOrder.Peek() is Ally && timer > 0.5)
            {
                if ((attackOrder.Peek() as Ally).GetPlayerChoice(enemyTeam[rng.Next(0, enemyTeam.Count)]))
                {
                    attackOrder.Dequeue();
                    timer = 0;
                    
                }
            }

            if(timer < 100)
            {
                timer += gameTime.ElapsedGameTime.TotalSeconds;
            }

            // Run update commands
            for (int i = 0; i < playerParty.Count; i++)
            {
                playerParty[i].Update();
            }

            for (int i = 0; i < enemyTeam.Count; i++)
            {
                enemyTeam[i].Update();
            }
        }

        // Draw
        public void Draw(SpriteBatch sb)
        {
            for (int i = 0; i < playerParty.Count; i++)
            {
                playerParty[i].Draw(sb);
            }

            for (int i = 0; i < enemyTeam.Count; i++)
            {
                enemyTeam[i].Draw(sb);
            }
        }


        //
        private void NextTurn()
        {
            List<Character> aliveChar = new List<Character>();
            Double percentChange = int.MaxValue;
            aliveChar.AddRange(playerParty);
            aliveChar.AddRange(enemyTeam);

            // Determine change in tm
            for (int i = 0; i < aliveChar.Count; i++)
            {
                if ((100 - aliveChar[i].Turnmeter) / aliveChar[i].Speed < percentChange)
                {
                    percentChange = (100 - aliveChar[i].Turnmeter) / aliveChar[i].Speed;
                }
            }

            // Increase turn meter, then queue if 100% tm
            for (int i = 0; i < aliveChar.Count; i++)
            {
                aliveChar[i].Turnmeter += (aliveChar[i].Speed * percentChange);

                if(aliveChar[i].Turnmeter >= 100)
                {
                    aliveChar[i].Turnmeter -= 100;
                    attackOrder.Enqueue(aliveChar[i]);
                }
            }
        }
    }
}
