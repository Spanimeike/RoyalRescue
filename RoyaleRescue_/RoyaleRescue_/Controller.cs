
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace PlatformerGame
{
    public class GameController
    {

        private GameModel _model;
        private GameView _view;

        private bool _princessRescued = false;

        public GameController()
        {
            _model = new GameModel(new Size(2400, 1600));
            _view = new GameView(this);
            _view.WindowState = FormWindowState.Maximized;
            Application.Run(_view);

        }

        public GameModel GetModel() => _model;

        public void Update()
        {

            _model.Update();

            if (!_princessRescued && _model.CheckPrincessRescue())
            {
                _princessRescued = true;
                ShowVictoryMessage();
                return;
            }


            if (_model.Player.Position.X > _model.GameArea.Width)
            {
                if (_model.CurrentLevel < 3)
                {
                    _model.LoadLevel(_model.CurrentLevel + 1);
                    _model.Player.Position = new Point(50, _model.Player.Position.Y);
                }
                else
                {

                    _model.LoadLevel(1);
                    _model.Player.Position = new Point(50, _model.Player.Position.Y);
                }
            }


            if (_model.Player.IsDead)
            {
                _model.LoadLevel(_model.CurrentLevel);
                _model.Player.IsDead = false;
            }

            _view.Invalidate();
        }

        private void ShowVictoryMessage()
        {
            _view.Invoke((MethodInvoker)delegate {
                var result = MessageBox.Show("Ты спас принцессу!", "Победа!",
                                   MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (result == DialogResult.OK)
                {
                    Application.Exit();
                }
            });
        }

        public void Draw(Graphics g)
        {
            g.Clear(Color.SkyBlue);

            int viewWidth = _view.ClientSize.Width;
            int viewHeight = _view.ClientSize.Height;
            int cameraX = Math.Max(0, Math.Min(_model.Player.Position.X - viewWidth / 2, _model.GameArea.Width - viewWidth));
            int cameraY = Math.Max(0, Math.Min(_model.Player.Position.Y - viewHeight / 2, _model.GameArea.Height - viewHeight));

            g.TranslateTransform(-cameraX, -cameraY);


            foreach (var bullet in _model.Bullets)
            {
                g.FillRectangle(Brushes.Black, bullet.Bounds);
            }


            foreach (var monster in _model.Monsters)
            {
                if (_view.MonsterImage != null)
                {
                    g.DrawImage(_view.MonsterImage, monster.Bounds);
                }
                else
                {
                    g.FillRectangle(Brushes.Purple, monster.Bounds);
                }
            }


            foreach (var wall in _model.Walls)
            {
                g.FillRectangle(Brushes.DarkGray, wall);
            }

            foreach (var platform in _model.Platforms)
            {
                g.FillRectangle(Brushes.Green, platform);
            }


            foreach (var spike in _model.Spikes)
            {
                Point[] points = {
            new Point(spike.Left + spike.Width / 2, spike.Top),
            new Point(spike.Right, spike.Bottom),
            new Point(spike.Left, spike.Bottom)
        };
                g.FillPolygon(Brushes.Red, points);
            }

            Image playerImageToDraw = _model.Player.VelocityX < 0 ? _view.PlayerImageLeft :
                        (_model.Player.VelocityX > 0 ? _view.PlayerImage : _view.PlayerImage);

            if (playerImageToDraw != null)
            {
                Rectangle bounds = _model.Player.Bounds;
                Rectangle drawRect = new Rectangle(
                    bounds.X,
                    bounds.Y,
                    bounds.Width,
                    bounds.Height);

                g.DrawImage(playerImageToDraw, drawRect);
            }
            else
            {
                Brush playerBrush = _model.Player.IsDead ? Brushes.Gray : Brushes.Blue;
                g.FillRectangle(playerBrush, _model.Player.Bounds);
            }

            var font = new Font("Arial", 24);
            g.ResetTransform();
            g.DrawString($"Level: {_model.CurrentLevel}", font, Brushes.Black, 20, 20);

            foreach (var princess in _model.Princesses)
            {
                if (_view.PrincessImage != null)
                {
                    g.DrawImage(_view.PrincessImage, princess.Bounds);
                }
                else
                {
                    g.FillRectangle(Brushes.Pink, princess.Bounds);

                    Point[] crownPoints = {
                new Point(princess.Position.X, princess.Position.Y - 10),
                new Point(princess.Position.X + 10, princess.Position.Y - 20),
                new Point(princess.Position.X + 20, princess.Position.Y - 10),
                new Point(princess.Position.X + 30, princess.Position.Y - 20),
                new Point(princess.Position.X + 40, princess.Position.Y - 10),
                new Point(princess.Position.X + 50, princess.Position.Y - 20),
                new Point(princess.Position.X + 50, princess.Position.Y - 10),
                new Point(princess.Position.X, princess.Position.Y - 10)
            };
                    g.FillPolygon(Brushes.Gold, crownPoints);
                }
            }
        }



        public void HandleKeyDown(Keys key)
        {
            if (_model.Player.IsDead) return;

            switch (key)
            {
                case Keys.Left:
                    _model.Player.VelocityX = -_model.MoveSpeed;
                    break;
                case Keys.Right:
                    _model.Player.VelocityX = _model.MoveSpeed;
                    break;
                case Keys.Space when _model.Player.IsOnGround:
                    _model.Player.VelocityY = _model.JumpStrength;
                    _model.Player.IsOnGround = false;
                    break;
                case Keys.F11:
                    _view.ToggleFullscreen();
                    break;
            }
        }

        public void HandleKeyUp(Keys key)
        {
            switch (key)
            {
                case Keys.Left when _model.Player.VelocityX < 0:
                case Keys.Right when _model.Player.VelocityX > 0:
                    _model.Player.VelocityX = 0;
                    break;
            }
        }
    }
}
