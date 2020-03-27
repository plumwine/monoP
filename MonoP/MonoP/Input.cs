using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoP
{
    /// <summary>
    /// キーボード入力、マウス入力の管理クラス
    /// </summary>
    static class Input
    {
        //移動量
        private static Vector2 velocity = Vector2.Zero;
        private static Vector2 padVelocity = Vector2.Zero;    //ゲームパッド用移動量
        //キーボード
        private static KeyboardState currentKey;//現在のキーの状態
        private static KeyboardState previousKey;//1フレーム前のキーの状態
        //ゲームパッド
        private static GamePadState currentGamePad;
        private static GamePadState previousGamePad;


        /// <summary>
        /// 更新
        /// </summary>
        public static void Update()
        {
            //キーボード
            previousKey = currentKey;
            currentKey = Keyboard.GetState();
            //ゲームパッド(１リプレイ用なので１つしか用意しない)
            previousGamePad = currentGamePad;
            currentGamePad = GamePad.GetState(PlayerIndex.One);

            //更新
            UpdateVelocity();
        }
        #region　キーボード関連

        //キーボード関連
        public static Vector2 Velocity()
        {
            return velocity;
        }

        /// <summary>
        /// キー入力による移動量の更新
        /// </summary>
        private static void UpdateVelocity()
        {
            //毎ループ初期化
            velocity = Vector2.Zero;

            //右
            if (currentKey.IsKeyDown(Keys.Right))
            {
                velocity.X += 1.0f;
            }
            //左
            if (currentKey.IsKeyDown(Keys.Left))
            {
                velocity.X -= 1.0f;
            }
            //上
            if (currentKey.IsKeyDown(Keys.Up))
            {
                velocity.Y -= 1.0f;
            }
            //下
            if (currentKey.IsKeyDown(Keys.Down))
            {
                velocity.Y += 1.0f;
            }

            //正規化
            if (velocity.Length() != 0)
            {
                velocity.Normalize();
            }
        }

        /// <summary>
        /// キーが押された瞬間か？
        /// </summary>
        /// <param name="key">チェックしたいキー</param>
        /// <returns>現在キーが押されていて、1フレーム前に押されていなければtrue</returns>
        public static bool IsKeyDown(Keys key)
        {
            return currentKey.IsKeyDown(key) && !previousKey.IsKeyDown(key);
        }

        /// <summary>
        /// キーが押された瞬間か？
        /// </summary>
        /// <param name="key">チェックしたいキー</param>
        /// <returns>押された瞬間ならtrue</returns>
        public static bool GetKeyTrigger(Keys key)
        {
            return IsKeyDown(key);
        }

        /// <summary>
        /// キーが押されているか？
        /// </summary>
        /// <param name="key">調べたいキー</param>
        /// <returns>キーが押されていたらtrue</returns>
        public static bool GetKeyState(Keys key)
        {
            return currentKey.IsKeyDown(key);
        }
        #endregion   キーボード関連

        #region ゲームパッド関連

        /// <summary>
        /// キーが押された瞬間か？
        /// </summary>
        /// <param name="index">チェックしたいキー</param>
        /// <param name="button"></param>
        /// <returns>現在のキーが押されていて、1フレーム前に押されていなければtrue</returns>
        public static bool IsKeyDown(Buttons button)
        {
            //つながっていればfalseえお返す
            if(currentGamePad.IsConnected == false)
            {
                return false;
            }
            return currentGamePad.IsButtonDown(button) &&
                   !previousGamePad.IsButtonDown(button);
        }
        /// <summary>
        /// キーが押された瞬間か？
        /// </summary>
        /// <param name="index">チェックしたいキー</param>
        /// <param name="button"></param>
        /// <returns押された瞬間なら></returns>
        public static bool GetKeyTrigger(Buttons button)
        {
            //つながってなければfalseを返す
            if (currentGamePad.IsConnected == false)
            {
                return false;
            }
            return IsKeyDown(button);
        }

        /// <summary>
        /// キーが押されているか？
        /// </summary>
        /// <param name="index">チェックしたいキー</param>
        /// <param name="button"></param>
        /// <returns>キーが押されていたらtrueを返す</returns>
        public static bool GetKeyState(Buttons button)
        {
            //つながってなければfalseを返す
            if (currentGamePad.IsConnected == false)
            {
                return false;
            }
            return currentGamePad.IsButtonDown(button);
        }
        

        //十字パッド******************

        /// <summary>
        /// ゲームパッド用移動量
        /// </summary>
        /// <returns></returns>
        public static Vector2 PadVelocity()
        {
            //つながっていなければ0を返す
            if(currentGamePad.IsConnected ==false)
            {
                return Vector2.Zero;
            }

            //毎ループ初期化
            padVelocity = Vector2.Zero;

            padVelocity.X = currentGamePad.ThumbSticks.Left.X;
            padVelocity.Y = -currentGamePad.ThumbSticks.Left.Y;
            //右
            if(currentGamePad.IsButtonDown(Buttons.DPadRight))
            {
                padVelocity.X += 1.0f;
            }
            //左
            if (currentGamePad.IsButtonDown(Buttons.DPadLeft))
            {
                padVelocity.X -= 1.0f;
            }
            //上
            if (currentGamePad.IsButtonDown(Buttons.DPadUp))
            {
                padVelocity.Y -= 1.0f;
            }
            //下
            if (currentGamePad.IsButtonDown(Buttons.DPadDown))
            {
                padVelocity.Y += 1.0f;
            }
            //正規化
            if(padVelocity.Length() != 0)
            {
                padVelocity.Normalize();
            }
            return padVelocity;
        }

        #region    左右スティック判定
        //左スティック*******************

        /// <summary>
        /// 左スティックが右に倒されているか
        /// </summary>
        /// <returns></returns>
        public static bool IsThumbSticks_Left_Right_Tilt_X()
        {
            return currentGamePad.ThumbSticks.Left.X > 0;
        }
        /// <summary>
        /// 左スティックが左に倒されているか
        /// </summary>
        /// <returns></returns>
        public static bool IsThumbSticks_Left_Left_Tilt_X()
        {
            return currentGamePad.ThumbSticks.Left.X < 0;
        }
        /// <summary>
        /// 左スティックが上に倒されているか
        /// </summary>
        /// <returns></returns>
        public static bool IsThumbSticks_Left_Up_Tilt_Y()
        {
            return currentGamePad.ThumbSticks.Left.Y > 0;
        }
        /// <summary>
        /// 左スティックが下に倒されているか
        /// </summary>
        /// <returns></returns>
        public static bool IsThumbSticks_Left_Down_Tilt_Y()
        {
            return currentGamePad.ThumbSticks.Left.Y < 0;
        }
        /// <summary>
        /// 左ステックが横にどれくらい倒されているかfloat
        /// </summary>
        /// <returns></returns>
        public static float GetThumbSticks_Left_Tilt_X()
        {
            return currentGamePad.ThumbSticks.Left.X;
        }

        /// <summary>
        /// 左ステックが横にどれくらい倒されているかfloat
        /// </summary>
        /// <returns></returns>
        public static float GetThumbSticks_Left_Tilt_Y()
        {
            return currentGamePad.ThumbSticks.Left.Y;
        }


        //右スティック********

        /// <summary>
        /// 右スティックが右に倒されているか
        /// </summary>
        /// <returns></returns>
        public static bool IsThumbSticks_Right_Right_Tilt_X()
        {
            return currentGamePad.ThumbSticks.Right.X > 0;
        }
        /// <summary>
        /// 右スティックが左に倒されているか
        /// </summary>
        /// <returns></returns>
        public static bool IsThumbSticks_Right_Left_Tilt_X()
        {
            return currentGamePad.ThumbSticks.Right.X < 0;
        }
        /// <summary>
        /// 右スティックが上に倒されているか
        /// </summary>
        /// <returns></returns>
        public static bool IsThumbSticks_Right_Up_Tilt_Y()
        {
            return currentGamePad.ThumbSticks.Right.Y > 0;
        }
        /// <summary>
        /// 右スティックが下に倒されているか
        /// </summary>
        /// <returns></returns>
        public static bool IsThumbSticks_Right_Down_Tilt_Y()
        {
            return currentGamePad.ThumbSticks.Right.Y < 0;
        }
        /// <summary>
        /// 右ステックが横にどれくらい倒されているかfloat
        /// </summary>
        /// <returns></returns>
        public static float GetThumbSticks_Right_Tilt_X()
        {
            return currentGamePad.ThumbSticks.Right.X;
        }

        /// <summary>
        /// 右ステックが横にどれくらい倒されているかfloat
        /// </summary>
        /// <returns></returns>
        public static float GetThumbSticks_Right_Tilt_Y()
        {
            return currentGamePad.ThumbSticks.Right.Y;
        }
        #endregion

        #endregion　ゲームパッド関連
    }

}
