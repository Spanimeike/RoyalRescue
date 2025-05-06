
using System.Collections.Generic;
using System.Drawing;

namespace PlatformerGame
{
    public class Bullet
    {
        public Point Position { get; set; }
        public Size Size { get; } = new Size(10, 5);
        public int Speed { get; }
        public bool IsPlayerBullet { get; }

        public Rectangle Bounds => new Rectangle(Position, Size);

        public Bullet(Point position, int speed, bool isPlayerBullet)
        {
            Position = position;
            Speed = speed;
            IsPlayerBullet = isPlayerBullet;
        }
    }

    public class Monster
    {
        public Point Position { get; set; }
        public Size Size { get; } = new Size(50, 50);
        public int ShootCooldown { get; set; } = 100;
        public const int ShootInterval = 100;

        public Rectangle Bounds => new Rectangle(Position, Size);
    }

    public class Player
    {
        public Point Position { get; set; }
        public Size Size { get; } = new Size(50, 80);
        public int VelocityX { get; set; }
        public int VelocityY { get; set; }
        public bool IsOnGround { get; set; }
        public bool IsDead { get; set; }
        public Rectangle Bounds => new Rectangle(Position, Size);
    }

    public class Princess
    {
        public Point Position { get; set; }
        public Size Size { get; } = new Size(50, 1000);
        public Rectangle Bounds => new Rectangle(Position, Size);
    }


    public class GameModel
    {
        public List<Princess> Princesses { get; } = new List<Princess>();
        public Player Player { get; } = new Player();
        public Size GameArea { get; set; }
        public int Gravity { get; } = 2;
        public int JumpStrength { get; } = -25;
        public int MoveSpeed { get; } = 10;
        public List<Rectangle> Platforms { get; } = new List<Rectangle>();
        public List<Rectangle> Spikes { get; } = new List<Rectangle>();
        public int CurrentLevel { get; private set; } = 1;
        public List<Rectangle> Walls { get; } = new List<Rectangle>();
        public List<Bullet> Bullets { get; } = new List<Bullet>();
        public List<Monster> Monsters { get; } = new List<Monster>();
        public const int PlayerBulletSpeed = 15;
        public const int MonsterBulletSpeed = 10;

        public GameModel(Size gameArea)
        {
            GameArea = gameArea;
            LoadLevel(1);
        }

        public void LoadLevel(int level)
        {
            CurrentLevel = level;
            Platforms.Clear();
            Spikes.Clear();

            Player.Position = new Point(100, GameArea.Height - 200);
            Player.VelocityX = 0;
            Player.VelocityY = 0;
            Player.IsDead = false;

            int platformHeight = 40;
            int spikeHeight = 20;

            if (level == 1)
            {
                Bullets.Clear();
                Platforms.Add(new Rectangle(1000, GameArea.Height - 100, GameArea.Width / 2, 100));


                Platforms.Add(new Rectangle(300, GameArea.Height - 250, 300, platformHeight));
                Platforms.Add(new Rectangle(700, GameArea.Height - 350, 300, platformHeight));
                Platforms.Add(new Rectangle(0, GameArea.Height - 200, 200, platformHeight));
                Platforms.Add(new Rectangle(2200, GameArea.Height - 200, 200, platformHeight));
                Platforms.Add(new Rectangle(1800, GameArea.Height - 170, 50, 70));


                Spikes.Add(new Rectangle(400, GameArea.Height - 270, 100, spikeHeight));
                Spikes.Add(new Rectangle(900, GameArea.Height - 370, 100, spikeHeight));
                Spikes.Add(new Rectangle(1500, GameArea.Height - 120, 100, spikeHeight));
                Spikes.Add(new Rectangle(1800, GameArea.Height - 190, 50, spikeHeight));
            }

            else if (level == 2)
            {
                Walls.Clear();
                Bullets.Clear();
                Platforms.Add(new Rectangle(GameArea.Width / 2, GameArea.Height - 100, GameArea.Width / 4, 100));
                Platforms.Add(new Rectangle(GameArea.Width / 2 + GameArea.Width / 3, GameArea.Height - 100, GameArea.Width / 3, 100));


                Platforms.Add(new Rectangle(400, GameArea.Height - 300, 200, platformHeight));
                Platforms.Add(new Rectangle(700, GameArea.Height - 450, 200, platformHeight));
                Platforms.Add(new Rectangle(400, GameArea.Height - 600, 200, platformHeight));
                Platforms.Add(new Rectangle(300, GameArea.Height - 750, 50, platformHeight));
                Platforms.Add(new Rectangle(500, GameArea.Height - 900, 50, platformHeight));
                Platforms.Add(new Rectangle(750, GameArea.Height - 870, 50, platformHeight));
                Platforms.Add(new Rectangle(1000, GameArea.Height - 500, 200, platformHeight));
                Platforms.Add(new Rectangle(1450, GameArea.Height - 270, 200, platformHeight));
                Platforms.Add(new Rectangle(1800, GameArea.Height - 250, 100, platformHeight));
                Platforms.Add(new Rectangle(2000, GameArea.Height - 300, 150, platformHeight));
                Platforms.Add(new Rectangle(0, GameArea.Height - 200, 200, platformHeight));

                Walls.Add(new Rectangle(900, GameArea.Height - 710, 20, 300));
                Walls.Add(new Rectangle(1570, GameArea.Height - 570, 20, 300));


                Spikes.Add(new Rectangle(450, GameArea.Height - 320, 100, spikeHeight));
                Spikes.Add(new Rectangle(750, GameArea.Height - 470, 100, spikeHeight));
                Spikes.Add(new Rectangle(450, GameArea.Height - 620, 100, spikeHeight));
                Spikes.Add(new Rectangle(1100, GameArea.Height - 520, 100, spikeHeight));
                Spikes.Add(new Rectangle(1200, GameArea.Height - 120, 150, spikeHeight));
                Spikes.Add(new Rectangle(1600, GameArea.Height - 290, 50, spikeHeight));
                Spikes.Add(new Rectangle(2000, GameArea.Height - 320, 150, spikeHeight));

                Spikes.Add(new Rectangle(1700, GameArea.Height - 120, 100, spikeHeight));
            }

            else if (level == 3)
            {
                Walls.Clear();
                Monsters.Clear();
                Bullets.Clear();
                Platforms.Add(new Rectangle(GameArea.Width / 2, GameArea.Height - 100, GameArea.Width / 2, 100));


                Platforms.Add(new Rectangle(300, GameArea.Height - 300, 180, platformHeight));
                Platforms.Add(new Rectangle(550, GameArea.Height - 450, 150, platformHeight));
                Platforms.Add(new Rectangle(750, GameArea.Height - 800, 50, platformHeight));
                Platforms.Add(new Rectangle(850, GameArea.Height - 600, 200, platformHeight));
                Platforms.Add(new Rectangle(1000, GameArea.Height - 900, 1000, platformHeight));
                Platforms.Add(new Rectangle(1600, GameArea.Height - 600, 1000, platformHeight));
                Platforms.Add(new Rectangle(2050, GameArea.Height - 300, 150, platformHeight));
                Platforms.Add(new Rectangle(1600, GameArea.Height - 300, 200, platformHeight));
                Platforms.Add(new Rectangle(1700, GameArea.Height - 1050, 150, platformHeight));
                Platforms.Add(new Rectangle(0, GameArea.Height - 200, 200, platformHeight));

                Walls.Add(new Rectangle(350, GameArea.Height - 420, 20, 150));
                Walls.Add(new Rectangle(1050, GameArea.Height - 860, 20, 300));
                Walls.Add(new Rectangle(1200, GameArea.Height - 1000, 20, 100));
                Walls.Add(new Rectangle(1830, GameArea.Height - 1010, 20, 110));
                Walls.Add(new Rectangle(2100, GameArea.Height - 1400, 20, 840));

                Monsters.Add(new Monster { Position = new Point(1600, GameArea.Height - 950) });

                Princesses.Add(new Princess { Position = new Point(2200, GameArea.Height - 400) });

                Spikes.Add(new Rectangle(430, GameArea.Height - 320, 50, spikeHeight));
                Spikes.Add(new Rectangle(1400, GameArea.Height - 920, 100, spikeHeight));
                Spikes.Add(new Rectangle(1750, GameArea.Height - 1070, 100, spikeHeight));
                Spikes.Add(new Rectangle(1350, GameArea.Height - 120, 150, spikeHeight));
                Spikes.Add(new Rectangle(1950, GameArea.Height - 120, 300, spikeHeight));
            }
        }

        public bool CheckPrincessRescue()
        {
            if (Princesses.Count == 0 || Player.IsDead) return false;

            foreach (var princess in Princesses)
            {
                if (Player.Bounds.IntersectsWith(princess.Bounds))
                {
                    return true;
                }
            }
            return false;
        }

        public void Update()
        {
            if (Player.IsDead)
            {
                return;
            }


            for (int i = Bullets.Count - 1; i >= 0; i--)
            {
                var bullet = Bullets[i];
                bullet.Position = new Point(bullet.Position.X + bullet.Speed, bullet.Position.Y);

        
                if (bullet.Position.X < 0 || bullet.Position.X > GameArea.Width)
                {
                    Bullets.RemoveAt(i);
                    continue;
                }

          
                if (bullet.IsPlayerBullet)
                {
                    foreach (var monster in Monsters.ToArray())
                    {
                        if (bullet.Bounds.IntersectsWith(monster.Bounds))
                        {
                            Monsters.Remove(monster);
                            Bullets.RemoveAt(i);
                            break;
                        }
                    }
                }
           
                else if (bullet.Bounds.IntersectsWith(Player.Bounds))
                {
                    Player.IsDead = true;
                    Bullets.RemoveAt(i);
                    return;
                }
            }

      
            foreach (var monster in Monsters)
            {
                if (monster.ShootCooldown <= 0)
                {
           
                    int direction = Player.Position.X > monster.Position.X ? 1 : -1;
                    Bullets.Add(new Bullet(
                        new Point(monster.Position.X + (direction == 1 ? monster.Size.Width : 0),
                                 monster.Position.Y + monster.Size.Height / 2),
                        direction * MonsterBulletSpeed,
                        false));

                    monster.ShootCooldown = Monster.ShootInterval;
                }
                else
                {
                    monster.ShootCooldown--;
                }
            }

    
            Player.VelocityY += Gravity;


            Player.Position = new Point(
                Player.Position.X + Player.VelocityX,
                Player.Position.Y + Player.VelocityY);

            Player.IsOnGround = false;


            foreach (var wall in Walls)
            {
                if (Player.Bounds.IntersectsWith(wall))
                {
                  
                    if (Player.VelocityX > 0 && Player.Position.X < wall.Left)
                    {
                        Player.Position = new Point(wall.Left - Player.Size.Width, Player.Position.Y);
                    }
                 
                    else if (Player.VelocityX < 0 && Player.Position.X > wall.Left)
                    {
                        Player.Position = new Point(wall.Right, Player.Position.Y);
                    }
                }
            }


            foreach (var platform in Platforms)
            {
                if (Player.Bounds.IntersectsWith(platform) && Player.VelocityY > 0)
                {
                    Player.Position = new Point(Player.Position.X, platform.Top - Player.Size.Height);
                    Player.VelocityY = 0;
                    Player.IsOnGround = true;
                }
            }


            foreach (var spike in Spikes)
            {
                if (Player.Bounds.IntersectsWith(spike))
                {
                    Player.IsDead = true;
                    return;
                }
            }


            if (Player.Position.X < 0)
                Player.Position = new Point(0, Player.Position.Y);

            if (Player.Position.Y > GameArea.Height)
                Player.IsDead = true;
        }
    }
}
