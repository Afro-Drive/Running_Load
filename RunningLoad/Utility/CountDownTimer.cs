using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace RunningLoad.Utility
{
    /// <summary>
    /// 現在時間が減少するタイマー継承クラス
    /// 制作者:谷 永吾
    /// 制作開始日:2018年10月2日
    /// </summary>
    class CountDownTimer : Timer
    {
        /// <summary>
        /// 親クラスを継承した引数なしコンストラクタ
        /// </summary>
        public CountDownTimer()
            :base()
        {
            currentTime = limitTime;
        }

        /// <summary>
        /// 親クラスを継承した引数ありのコンストラクタ
        /// </summary>
        /// <param name="second"></param>
        public CountDownTimer(float second)
            : base(second)
        {
            currentTime = second * 60;
        }

        /// <summary>
        /// タイマーの初期化（制限時間と現在時間を一致させる）
        /// </summary>
        public override void Initialize()
        {
            currentTime = limitTime;
            isTimeFlag = false;
        }

        /// <summary>
        /// 時間切れ
        /// </summary>
        /// <returns>現在時間が0秒以下になったか？</returns>
        public override bool TimeUP()
        {
            return currentTime <= 0.0f;
        }

        /// <summary>
        /// タイマーの起動
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            currentTime -= 1f;
            if (TimeUP())//時間切れになったら
            {
                isTimeFlag = true;//時間切れ通知
            }
        }
    }
}
