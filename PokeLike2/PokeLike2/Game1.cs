// Copyright (c) 2016 Mischa Ahi
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PokeLike2
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public static UITexture DialogBox;
        public static UILabel Message;
        public static bool DebugMode = false;

        private Player player; 
        private Camera camera = new Camera();
        private UILabel healthBar;
        private Pokemon bisasam;
        private SpriteAnimation trainer;
        private Potion potion;
        private Potion potion2;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();

            //IsMouseVisible = true;
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            GameManager.Content = this.Content;

            Map map = new Map();
            map.LoadMapFromImage(Content.Load<Texture2D>("Map"));
            GameManager.AddGameObject(map);

            player = new Player(new Vector2(17, 12));
            GameManager.AddGameObject(player);

            trainer = new SpriteAnimation("player", Content.Load<Texture2D>("playerMovement"), Content.RootDirectory + "/playerMovement.xml");

            player.SpriteAnimation = trainer;
            player.SpriteAnimation.FrameDelay = 200;
            
            camera.SetTarget(player);

            // UI
            healthBar = new UILabel(Fonts.Arial, new Vector2(camera.X, camera.Y), ("HP: " + Player.Health.ToString() + ""), 0.5f, Color.Black);
            DialogBox = new UITexture(new Vector2(camera.X, camera.Y), Color.White, "800x120_gray");

            //Pokemons
            bisasam = new Pokemon(new Vector2( 3, 3 ), "Bisasam", "bisasam1", 120, 5, 10, 0, 2, "plant", true);
            bisasam = new Pokemon(new Vector2(31, 9 ), "Bisasam", "bisasam2", 20, 5, 5, 0, 2, "plant", false);
            bisasam = new Pokemon(new Vector2(32, 9 ), "Bisasam", "bisasam3", 20, 5, 5, 0, 2, "plant", false);
            bisasam = new Pokemon(new Vector2(32, 10 ), "Bisasam", "bisasam4", 20, 5, 5, 0, 2, "plant", false);
            bisasam = new Pokemon(new Vector2(31, 10), "Bisasam", "bisasam5", 20, 5, 5, 0, 2, "plant", false);
            bisasam = new Pokemon(new Vector2(30, 9 ), "Bisasam", "bisasam6", 20, 5, 5, 0, 2, "plant", false);
            bisasam = new Pokemon(new Vector2(30, 10), "Bisasam", "bisasam7", 20, 5, 5, 0, 2, "plant", false);
            bisasam = new Pokemon(new Vector2(14, 19), "Bisasam", "bisasam8", 20, 5, 5, 0, 2, "plant", false);

            //Items
            potion = new Potion(new Vector2(1, 1), Content.RootDirectory + "/items.xml", "PotionRed");
            potion2 = new Potion(new Vector2(30, 12), Content.RootDirectory + "/items.xml", "PotionRed");
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            InputManager.Update();
            GameManager.Update(gameTime);
            CollisionManager.Update();
            UIManager.Update(gameTime);

            //UI
            int windowWidth = 25 * Constant.TileSize;
            int windowHeight = 15 * Constant.TileSize;
            healthBar.Position = new Vector2(camera.X + windowWidth * 0.81f, camera.Y); // Updatet die Position der Healthbar in Abhängigkeit zur Kamera
            healthBar.Text = "HP: " + Player.Health.ToString() + "";
            DialogBox.Position = new Vector2(camera.X, camera.Y + windowHeight * 0.75f);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            camera.UpdatePosition(GraphicsDevice.Viewport);
            Matrix cameraTransform = Matrix.CreateTranslation(-camera.X, -camera.Y, 0);

            spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null, cameraTransform);

            GameManager.Draw(spriteBatch);
            UIManager.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        
    }
}
