using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace HackYourSummerGame
{
    internal class Battlefield
    {
        // Teams
        private List<Ally> playerParty;
        private List<Enemy> enemyTeam;

        private Queue<Character> attackOrder;
        private List<Button> enemySelect;

        private Random rng;
        private double timer;
        private int fightOver;
        private int enemyTarget;

        // Check if fight is over
        public int FightOver
        {
            get { return fightOver; }
        }

        // Constructor
        public Battlefield(List<Ally> playerParty, List<Enemy> enemyTeam)
        {
            this.playerParty =new List<Ally>();
            this.enemyTeam = new List<Enemy>();
            this.enemyTeam = enemyTeam;
            enemySelect = new List<Button>();

            for(int i = 0; i < playerParty.Count; i++)
            {
                this.playerParty.Add(playerParty[i]);
            }

            for (int i = 0; i < enemyTeam.Count; i++)
            {
                enemyTeam[i].TempSpeed = 0;
                enemySelect.Add(new Button(null, enemyTeam[i].Hitbox,
                    new Rectangle(0, 0, enemyTeam[i].Hitbox.Width, enemyTeam[i].Hitbox.Height)));
            }
            
            attackOrder = new Queue<Character>();

            rng = new Random();
            timer = 0;
            fightOver = 0;
        }

        // Run battle instance
        public void Update(GameTime gameTime)
        {
            // 
            for(int i = 0; i < enemySelect.Count; i++)
            {
                if (enemySelect[i].Clicked())
                {
                    enemyTarget = i;
                }
            }

            // Increase tm if needed
            if(attackOrder.Count == 0)
            {
                NextTurn();
            }

            if(attackOrder.Peek() is IEnemy && timer > 0.5)
            {
                (attackOrder.Dequeue() as IEnemy).GetNextMove(playerParty[rng.Next(0, playerParty.Count)]);
                timer = 0;
            }
            else if(attackOrder.Peek() is Ally && timer > 0.5)
            {
                if ((attackOrder.Peek() as Ally).GetPlayerChoice(enemyTeam[enemyTarget]))
                {
                    attackOrder.Dequeue();
                    timer = 0; 
                }
            }

            
            // Advance timer
            if(timer < 100)
            {
                timer += gameTime.ElapsedGameTime.TotalSeconds;
            }

            // Run update commands
            for (int i = 0; i < playerParty.Count; i++)
            {
                

                playerParty[i].Update();
                if (playerParty[i].Health <= 0)
                {
                    playerParty.RemoveAt(i);
                    i--;
                }
            }

            for (int i = 0; i < enemyTeam.Count; i++)
            {
                enemyTeam[i].Update();
                if (enemyTeam[i].Health <= 0)
                {
                    enemyTeam.RemoveAt(i);
                    enemySelect.RemoveAt(i);
                    i--;
                }
            }

            // Determine if fight is over and who won
            if(playerParty.Count == 0)
            {
                fightOver = 1;
            }
            else if(enemyTeam.Count == 0)
            {
                fightOver = -1;
            }
        }

        // Draw
        public void Draw(SpriteBatch sb)
        {
            for (int i = 0; i < playerParty.Count; i++)
            {
                playerParty[i].Draw(sb);
            }

            if(attackOrder.Count != 0 && attackOrder.Peek() is Ally)
            {
                ((Ally)attackOrder.Peek()).DrawButtons(sb);
            }

            for (int i = 0; i < enemyTeam.Count; i++)
            {
                enemyTeam[i].Draw(sb);
            }
        }


        // Advance turn meter and queue fighters
        private void NextTurn()
        {
            List<Character> aliveChar = new List<Character>();
            Double percentChange = int.MaxValue;
            aliveChar.AddRange(playerParty);
            aliveChar.AddRange(enemyTeam);

            // Determine change in tm
            for (int i = 0; i < aliveChar.Count; i++)
            {
                if ((100 - aliveChar[i].Turnmeter) / (aliveChar[i].Speed + aliveChar[i].TempSpeed) < percentChange)
                {
                    percentChange = (100 - (aliveChar[i].Turnmeter + aliveChar[i].TempSpeed)) / aliveChar[i].Speed;
                    if(percentChange <= 0)
                    {
                        percentChange = 0;
                        break;
                    }
                }
            }

            // Increase turn meter, then queue if 100% tm
            for (int i = 0; i < aliveChar.Count; i++)
            {
                aliveChar[i].Turnmeter += ((aliveChar[i].Speed + aliveChar[i].TempSpeed) * percentChange);

                if(aliveChar[i].Turnmeter >= 100)
                {
                    aliveChar[i].Turnmeter -= 100;
                    attackOrder.Enqueue(aliveChar[i]);
                }
            }
        }
    }
}
