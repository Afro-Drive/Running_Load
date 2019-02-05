using Microsoft.Xna.Framework;
using RunningLoad.Def;
using RunningLoad.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunningLoad.Utility
{
    /// <summary>
    /// ゲームプレイ中でのスコア管理クラス
    /// </summary>
    class Score
    {
        #region フィールド
        //使用画像
        private readonly string NUMBER = "number";
        private readonly string SCORE = "score";

        private Vector2 numberPos, scorePos;//描画位置
        private int realScore, visibleScore;//実スコア、描画用スコア
        private bool stopFlag;//仮想スコアの停止フラグ
        private int stopCnt;//一時的にスコア表記を点滅させるのに使用
        private SoundManager sound;//サウンド管理者
        #endregion

        #region 不要なフィールド
        //private Timer drawTimer;//表示時間
        //private readonly int Limit = 5;//点滅回数
        //private bool isVisible;//視認可能か？
        //private int displayCount = 0;//現在点滅回数
        #endregion
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Score()
        {
            realScore = 0;
            visibleScore = 0;
            stopFlag = false;
            scorePos = new Vector2(Screen.Width - 480, 16);
            numberPos = new Vector2(Screen.Width - 272, 16);

            sound = DeviceManager.CreateInstance().GetSound();
        }

        /// <summary>
        /// 描画処理
        /// </summary>
        /// <param name="renderer"></param>
        public void Draw(Renderer renderer)
        {
            //スコアの文字
            renderer.DrawTexture(SCORE, scorePos);
            //数字
            //点滅処理
            if (stopCnt % 6 < 3)
            {
                //描画処理
                renderer.DrawNumber(NUMBER, numberPos, visibleScore);
            }
            else//更新処理でフラグが反転してもここが実行されない→解決
            {   }
        }

        /// <summary>
        /// スコア更新処理（進むごとに増加）
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            realScore += 1;//実スコアは常に加算状態
            if (!stopFlag)//stopFlagがオフの時
            {
                visibleScore += 1;
            }

            // 100の倍数になった時
            if (realScore % 100 == 0)
            {
                sound.PlaySE("wadaiko1");
                if (!stopFlag)
                {
                    stopFlag = true;//stopFlagが偽なら真に
                }
            }

            // stopFlagがオンの時
            if (stopFlag)
            {
                // カウントする
                stopCnt++;
                // ある程度たったら
                if (stopCnt > 30)
                {
                    // 元に戻す
                    stopFlag = false;
                    stopCnt = 0;
                    visibleScore = realScore;
                }
            }
            #region タイマーを用いた点滅処理群（処理が反映されず削除）
            //スコアが100の倍数(ただし0を除く)かつ点滅回数が制限回数に達していない
            //while (visibleScore % 100 == 0 && visibleScore != 0 && displayCount < Limit)
            //{
            //    drawTimer.Update(gameTime);//点滅タイマーの起動
            //    if (drawTimer.TimeUP())
            //    {
            //        isVisible = !isVisible;//フラグの反転
            //        displayCount += 1;//点滅回数を加算
            //        drawTimer.Initialize();//タイマーを初期化
            //    }
            //}

            //if (displayCount == Limit)//点滅回数が制限に達している場合
            //{
            //    drawTimer.Initialize();
            //    isVisible = true;
            //    visibleScore = realScore;//実スコアに合わせる
            //    displayCount = 0;//点滅回数を初期化
            //    stopFlag = false;//virtualScoreの加算を開始
            //}
            #endregion
        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public void Shutdown()
        {
            //スコアの初期化
            realScore = 0;
            visibleScore = 0;

            stopFlag = false;
        }

        /// <summary>
        /// スコアの取得
        /// </summary>
        /// <returns>フィールドrealScore</returns>
        public int GetScore()
        {
            return realScore;
        }
    }
}
