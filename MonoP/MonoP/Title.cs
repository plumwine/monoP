using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MonoP
{
    class Title : IScene
    {

        private bool isEndFlag;   //終了フラグ

        public void Draw(Renderer renderer)
        {
            renderer.Begin();

            renderer.DrawTexture("Z", Vector2.Zero);
            renderer.DrawTexture("box", new Vector2(300, 600));
            
            renderer.End();
        }

        public void Initialize()
        {
            isEndFlag = false;  //最初は終了しない
        }

        public bool IsEnd()
        {
            return isEndFlag;
        }

        public Scene Next()
        {
            return Scene.GamePlay;
        }

        public void Shutdown()
        {
        }

        public void Update(GameTime gameTime)
        {

        }
    }
}
