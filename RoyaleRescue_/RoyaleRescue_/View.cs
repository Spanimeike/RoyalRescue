// GameView.cs
using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using RoyaleRescue_.Properties;

namespace PlatformerGame
{
    public class GameView : Form
    {
        private Image _levelDecorationImage;
        private Image _princessImage;
        public Image PrincessImage => _princessImage;
        private GameController _controller;
        private Timer _gameTimer;
        private bool _isFullscreen;
        private Image _playerImage;
        private Image _playerImageLeft; // Новое изображение для ходьбы влево
        private Image _monsterImage;
        public Image PlayerImage => _playerImage;
        public Image PlayerImageLeft => _playerImageLeft; // Новое свойство
        public Image MonsterImage => _monsterImage;

        public GameView(GameController controller)
        {
            _controller = controller;
            DoubleBuffered = true;
            ClientSize = new Size(1200, 800);
            Text = "2D Platformer";
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;

            // Загрузка изображений
            _playerImage = LoadImageFromResources("player"); // Обычное изображение (вправо)
            _playerImageLeft = LoadImageFromResources("player1"); // Изображение для ходьбы влево
            _monsterImage = LoadImageFromResources("monster");
            _princessImage = LoadImageFromResources("princess");
            _levelDecorationImage = LoadImageFromResources("123");

            _gameTimer = new Timer { Interval = 16 };
            _gameTimer.Tick += (s, e) => _controller.Update();
            _gameTimer.Start();

            KeyDown += (s, e) => _controller.HandleKeyDown(e.KeyCode);
            KeyUp += (s, e) => _controller.HandleKeyUp(e.KeyCode);
        }


        public GameModel GetModel() => _controller.GetModel();

        private Image LoadImageFromResources(string fileName)
        {
            try
            {
                string path = System.IO.Path.Combine(Application.StartupPath, fileName + ".png");
                if (System.IO.File.Exists(path))
                {
                    return Image.FromFile(path);
                }
                MessageBox.Show($"File {fileName}.png not found in application directory!");
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading image {fileName}: {ex.Message}");
                return null;
            }
        }

        public void ToggleFullscreen()
        {
            if (_isFullscreen)
            {
                FormBorderStyle = FormBorderStyle.FixedSingle;
                WindowState = FormWindowState.Normal;
                ClientSize = new Size(1200, 800);
            }
            else
            {
                FormBorderStyle = FormBorderStyle.None;
                WindowState = FormWindowState.Maximized;
            }
            _isFullscreen = !_isFullscreen;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            _controller.Draw(e.Graphics);

            // Добавьте отрисовку изображения на 3 уровне
            if (_controller.GetModel().CurrentLevel == 3 && _levelDecorationImage != null)
            {
                e.Graphics.DrawImage(_levelDecorationImage, 2200, 900);
            }
        }
    }
}