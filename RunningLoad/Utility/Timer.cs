using Microsoft.Xna.Framework;
using RunningLoad.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunningLoad.Utility
{
    /// <summary>
    /// タイマー抽象クラス
    /// 作成者:谷 永吾
    /// 作成開始日:2018/10/02
    /// </summary>
    abstract class Timer
    {
        protected float currentTime;//現在時間
        protected float limitTime;//制限時間
        protected bool isTimeFlag;//時間切れか？

        /// <summary>
        /// 引数なしのコンストラクタ
        /// (制限時間は自動で1秒に設定）
        /// </summary>
        public Timer()
        {
            limitTime = 1f * 60;
            isTimeFlag = false;
        }

        public Timer(float second)
            : this()
        {
            limitTime = second;
            isTimeFlag = false;
        }

        /// <summary>
        /// 現在時間の取得
        /// </summary>
        /// <returns></returns>
        public float CurrentTime()
        {
            return currentTime;
        }

        public bool IsTime()
        {
            return isTimeFlag;
        }
        //public void Draw(Renderer renderer)→現在はまだ使用は未定
        //{
        //    renderer.DrawTexture("")
        //}

        //抽象メソッド
        public abstract void Initialize();//タイマーの初期化

        public abstract bool TimeUP();//時間切れの通知

        public abstract void Update(GameTime gameTime);//時間経過

        
    }
}
