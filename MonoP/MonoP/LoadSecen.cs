using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;


namespace MonoP
{
    class LoadScene : IScene
    {
        private Renderer renderer;//描画オブジェクト

        //読み込み用オブジェクト
        private TextureLoader textureLoader;

        private int totalResouceNum;//全リソース数
        private bool isEndFlag;//終了フラグ
       // private Timer timer;//演出用タイマー


        #region テクスチャ用
        /// <summary>
        /// テクスチャ読み込み用２次元配列の取得
        /// </summary>
        /// <returns></returns>
        private string[,] textureMatrix()
        {
            //テクスチャディレクトリのデフォルトパス
            string path = "./Texture/";

            //読み込み対象データ
            string[,] data = new string[,]
            {
                //{"bullet2"   , path },
                {"box" },
                {"J" },
                {"L" },
                {"S" },
                {"T" },
                {"Z" },
            };

            return data;
        }
        #endregion テクスチャ用

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LoadScene()
        {
            //描画オブジェクトを取得
            renderer = GameDevice.Instance().GetRenderer();

            //読み込む対象を取得し、実体生成
            textureLoader = new TextureLoader(textureMatrix());
          
        }

        /// <summary>
        /// 描画
        /// </summary>
        /// <param name="renderer"></param>
        public void Draw(Renderer renderer)
        {
            //描画開始
            renderer.Begin();

            //renderer.DrawTexture("load", new Vector2(20, 700));
            //現在読み込んでいる数を取得
            int currentCount =
                textureLoader.CurrentCount();

            //読み込むモノがあれば描画
            if (totalResouceNum != 0)
            {
                //読み込んだ割合
                float rate = (float)currentCount / totalResouceNum;
                //数字で描画
                //renderer.DrawNumber(
                //    "number",
                //    new Vector2(20, 700),
                //    (int)(rate * 100.0f));

                ////バーで描画
                //renderer.DrawTexture(
                //    "fade",
                //    new Vector2(0, 820),
                //    null,
                //    0.0f,
                //    Vector2.Zero,
                //    new Vector2(rate * Screen.Width, 20));
            }

            //終了
            //すべてのデータを読み込んだか？
            if (textureLoader.IsEnd())
            {
                isEndFlag = true;
            }

            //描画終了
            renderer.End();
        }

        /// <summary>
        /// 初期化
        /// </summary>
        public void Initialize()
        {
            //終了フラグを継続に設定
            isEndFlag = false;
            //テクスチャ読み込みオブジェクトを初期化
            textureLoader.Initialize();
            //全リソース数を計算
            totalResouceNum =
                textureLoader.RegistMAXNum();
        }

        /// <summary>
        /// シーン終了か？
        /// </summary>
        /// <returns></returns>
        public bool IsEnd()
        {
            return isEndFlag;
        }

        /// <summary>
        /// 次のシーン
        /// </summary>
        /// <returns></returns>
        public Scene Next()
        {
            return Scene.Title;
        }

        /// <summary>
        /// 終了
        /// </summary>
        public void Shutdown()
        {

        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            //演出確認用（大量のデータがあるときは設定時間を０に）
            //一定時間ごとに読み込み
            //timer.Update(gameTime);
            //if (timer.IsTime() == false)
            //{
            //    return;
            //}
            //timer.Initialize();

            //テクスチャから順々に読み込みを行う
            if (textureLoader.IsEnd() == false)
            {
                textureLoader.Update(gameTime);
            }
        }
    }
}
