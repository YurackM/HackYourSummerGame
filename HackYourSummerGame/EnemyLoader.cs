using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HackYourSummerGame
{
    internal class EnemyLoader
    {
        private ContentManager content;
        private Random rng;

        private Texture2D spider;
        private Texture2D vyper;
        private Texture2D healthBar;
        private Texture2D healthContainer;

        public EnemyLoader(ContentManager content)
        {
            this.content = content;
            rng = new Random();
            LoadSprites();
        }

        //
        private void LoadSprites()
        {
            spider = content.Load<Texture2D>("Spider");
            vyper = content.Load<Texture2D>("Vyper");
            healthBar = content.Load<Texture2D>("Health Bar");
            healthContainer = content.Load<Texture2D>("Health Container");
        }

        // Choose a set of enemies for player to fight
        public List<Enemy> NextEnemySet(int level)
        {
            List<Enemy> enemySet = new List<Enemy>();
            double points = 25 * Math.Log(level+2);
            
            if(level == 1)
            {
                if(rng.Next(2) == 0)
                {
                    enemySet.Add(CreateSpider(level, enemySet.Count));
                }
                else
                {
                    enemySet.Add(CreateVyper(level, enemySet.Count));
                }
            }
            else if(level < 4)
            {
                    
            }

            return enemySet;
        }

        // Create a new Spider
        private Spider CreateSpider(int level, int enemyCount)
        {
            return new Spider(50 + (int)(level * 2), 20 + (int)(level), 40 + (int)(level * 0.5),
                new Vector2(600 + enemyCount * 220, 50 + enemyCount * 55), spider, healthBar, healthContainer); 
        }

        // Create a new Vyper
        private Vyper CreateVyper(int level, int enemyCount)
        {
            return new Vyper(45 + (int)(level * 1.5), 15 + (int)(level * 1.1), 45 + (int)(level * 0.65),
                new Vector2(600 + enemyCount * 220, 50 + enemyCount * 55), vyper, healthBar, healthContainer);
        }
    }
}
