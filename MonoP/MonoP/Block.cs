using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

namespace MonoP
{
    abstract class Block
    {
        protected Vector2 position;
        protected string name;
        protected bool isDeadFlag;

        protected enum State
        {
            Choose,
            Down,
            Under,
            Up,
        }

        public Block(string name)
        {
            this.name = name;

            position = Vector2.Zero;

            isDeadFlag = false;
        }

        public abstract void Initialize();

        public abstract void Update(GameTime gameTime);

        public abstract void Shutdown();

        public abstract void Hit(Block other);

        public bool IsDead()
        {
            return isDeadFlag;
        }

        public virtual void Draw(Renderer renderer)
        {
            renderer.DrawTexture(name, position);
        }

        public bool IsCollision(Block other)
        {
            //自分と相手の位置の長さと計算(2点間の距離)
            float length = (position - other.position).Length();
            //白玉画像のサイズは64なので、半径は32
            //自分半径と相手の半径の和
            float radiusSum = 32f + 32f;
            //半径の和と距離を比べて，等しいかまたは小さいか（以下か）
            if (length <= radiusSum)
            {
                return true;
            }
            return false;
        }

        public void SetPosition(ref Vector2 other)
        {
            other = position;
        }
    }
}
