using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MonoP
{
    sealed class GameDevice
    {
        //唯一のインスタンス
        private static GameDevice instance;

        //デバイス関連フィールド
        private Renderer renderer;
       // private Sound sound;
        private static Random random;
        private ContentManager content;
        private GraphicsDevice graphics;
        private GameTime gameTime;

        /// <summary>
        /// コンストラクタ
        /// privarte宣言で外部からのnewでの実態生成はさせない
        /// </summary>
        /// <param name="content"></param>
        /// <param name="graphics"></param>
        private GameDevice(ContentManager content, GraphicsDevice graphics)
        {
            renderer = new Renderer(content, graphics);
            //sound = new Sound(content);
            random = new Random();
            this.content = content;
            this.graphics = graphics;
        }
        /// <summary>
        /// GameDeviceインスタンスの取得
        /// (Game1クラスで使う実態生成用)
        /// </summary>
        /// <param name="content">コンテンツ管理者</param>
        /// <param name="graphics">グラフィックス機器</param>
        /// <returns>インスタンス</returns>
        public static GameDevice Instance(ContentManager content,
            GraphicsDevice graphics)
        {
            //インスタンスがまだ生成されていなければ生成する
            if (instance == null)
            {
                instance = new GameDevice(content, graphics);
            }
            return instance;
        }
        /// <summary>
        /// インスタンスの取得
        /// </summary>
        /// <returns>GameDevuceインスタンス</returns>
        public static GameDevice Instance()
        {
            //まだインスタンスが生成されていなければエラー文を出す
            Debug.Assert(instance != null,
                "Game1クラスのInitializeメソッド内で引き数付きInstanceメソッドを読んでください");

            return instance;
        }
        /// <summary>
        /// 初期化
        /// </summary>
        public void Initialize()
        {

        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="gameTime">ゲーム時間</param>
        public void Update(GameTime gameTime)
        {
            //デバイスで絶対に1回の三井更新が必要なモノ
            //Input.Update();
            this.gameTime = gameTime;
        }
        /// <summary>
        /// レンダラーオブジェクトの取得
        /// </summary>
        /// <returns>描画オブジェクト</returns>
        public Renderer GetRenderer()
        {
            return renderer;
        }
        /// <summary>
        /// サウンドオブジェクトの取得
        /// </summary>
        /// <returns>サウンドオブジェクト</returns>
        //public Sound GetSound()
        //{
        //    return sound;
        //}
        /// <summary>
        /// 乱数オブジェクトの取得
        /// </summary>
        /// <returns>乱数オブジェクト</returns>
        public Random GetRandom()
        {
            return random;
        }
        /// <summary>
        /// コンテンツ管理者の取得
        /// </summary>
        /// <returns>コンテンツ管理者オジェクト</returns>
        public ContentManager GetContentManager()
        {
            return content;
        }
        /// <summary>
        /// グラフィックデバイスの取得
        /// </summary>
        /// <returns>グラフィックデバイスオブジェクト</returns>
        public GraphicsDevice GetGraphicsDevice()
        {
            return graphics;
        }

        public GameTime GetGameTime()
        {
            return gameTime;
        }
    }
}
