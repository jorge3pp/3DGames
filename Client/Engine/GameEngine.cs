using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Base;
using Engine.Managers;
using Microsoft.Xna.Framework;

namespace Engine
{
    public sealed class GameEngine : DrawableGameComponent
    {
        InputManager input;
        CameraManager camera;
        PhysicsManager physics;
        FrameRateCounter fpsCounter;
        DebugManager debug;

        private Scene activeScene;
        public Scene ActiveScene {get { return activeScene; } }

        public GameEngine(Game game) : base(game)
        {
            game.Components.Add(this);

            input = new InputManager(game);
            camera = new CameraManager(game);
            physics = new PhysicsManager(game);
            fpsCounter = new FrameRateCounter(game);
            debug = new DebugManager();
        }

        public void LoadScene(Scene newScene)
        {
            if (newScene != null)
            {
                if (activeScene != null)
                {
                    UnloadScene();
                }

                activeScene = newScene;
                activeScene.Initialize();
            }

        }

        public void UnloadScene()
        {
            activeScene = null;
            CameraManager.Clear();
            GameUtilities.SceneConetent.Unload();

        }

        public override void Update(GameTime gameTime)
        {
            GameUtilities.Time = gameTime;

            Game.Window.Title = "FPS: " + FrameRateCounter.FrameRate +
                ", DT: " + GameUtilities.DeltaTime;

            if (activeScene != null)
            {
                activeScene.Update();
            }

            base.Update(gameTime);
        }

        public override void Initialize()
        {
            debug.Initialize();

            base.Initialize();
        }

        public override void Draw(GameTime gameTime)
        {
            if (activeScene != null)
            {
                if (CameraManager.ActiveCamera != null)
                {
                    activeScene.Draw(CameraManager.ActiveCamera);
                    activeScene.DrawUI();
                    debug.Draw(CameraManager.ActiveCamera);
                }
            }

            GameUtilities.SetGraphicsDeviceFor3D();
            base.Draw(gameTime);
        }
    }
}
