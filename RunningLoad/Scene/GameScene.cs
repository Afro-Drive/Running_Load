using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using RunningLoad.Actor;
using RunningLoad.Def;
using RunningLoad.Device;
using RunningLoad.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunningLoad.Scene
{
    /// <summary>
    /// 実際の操作を行うシーンクラス
    /// 作成者:谷 永吾
    /// 
    /// </summary>
    class GameScene : IScene, IMediator
    {
        //フィールド
        private ScrollCamera camera;//スクロール管理者
        private bool isEndFlag;//次のシーンへのトリガー

        private CharacterGenerateManager generalManager;//キャラクター生成・管理者
        private Score score;//スコア

        private SoundManager sound;//サウンド管理者
        #region キャラクター管理者に委託
        //キャラクター関連
        //private Runner runner;
        //private FixedEnemy fixEnem;
        //private FlyingEnemy flying;
        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public GameScene()
        {
            camera = new ScrollCamera();
            generalManager = new CharacterGenerateManager();
            score = new Score();
            #region キャラクター生成・管理者に委託
            //fixEnem = new FixedEnemy();
            //flying = new FlyingEnemy();
            //runner = new Runner();
            #endregion
            isEndFlag = false;
        }

        /// <summary>
        /// 描画メソッド
        /// </summary>
        /// <param name="renderer"></param>
        public void Draw(Renderer renderer)
        {
            renderer.Begin();

            camera.Draw(renderer);
            generalManager.Draw(renderer);
            score.Draw(renderer);
            #region キャラクター生成・管理者に委託
            //fixEnem.Draw(renderer);
            //flying.Draw(renderer);
            //runner.Draw(renderer);
            #endregion
            renderer.End();
        }

        /// <summary>
        /// 更新処理
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            sound.PlayBGM("playBGM");
            camera.Update(gameTime);
            generalManager.Update(gameTime);
            score.Update(gameTime);

            if (generalManager.IsEnd())//プレイヤーが死亡したら
            {
                isEndFlag = true;//シーンを終了させる
            }

            //1000点ごとにスピードアップ！
            if(GetScore() % 1000 == 0 && GetScore() > 0)
            {
                SetScroll(15f + 200f * GetScore() % 100);
            }
            #region キャラクター生成・管理者に委託
            //fixEnem.Update(gameTime);
            //flying.Update(gameTime);
            //runner.Update(gameTime);
            //if (runner.IsDead())
            // {
            //    //次のシーンへ
            //    isEndFlag = true;
            //}
            //JudgeCollision();//毎フレーム当たり判定処理
            #endregion
        }

        /// <summary>
        /// 初期化
        /// </summary>
        public void Initialize()
        {
            sound = DeviceManager.CreateInstance().GetSound();
            score.Shutdown();//シーン終了時でなく、シーン開始直前にスコアを0に戻す
            generalManager.Clear();

            //キャラクターの生成
            generalManager.Generate("Runner", this);
            generalManager.Generate("Flying", this);
            generalManager.Generate("Fixed", this);

            generalManager.Initialize();
            #region キャラクター生成・管理者に委託
            //runner.Initialize();
            //fixEnem.Initialize();
            //flying.Initialize();
            #endregion
            isEndFlag = false;
        }

        /// <summary>
        /// シーンの終了か？
        /// </summary>
        /// <returns></returns>
        public bool IsEnd()
        {
            return isEndFlag;
        }

        /// <summary>
        /// 次のシーンへ（シーン管理者への報告）
        /// </summary>
        /// <returns>遷移したいシーンオブジェクトに対応する列挙型</returns>
        public EScene Next()
        {
            return EScene.Result;
        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public void Shutdown()
        {    }

        /// <summary>
        /// （仲介者）スクロール速度の取得
        /// </summary>
        /// <returns>ScrollCameraクラスのscrollspeedフィールド</returns>
        public float GetScroll()
        {
            return camera.Scroll;
        }

        /// <summary>
        /// （仲介者）スクロール速度の変更
        /// </summary>
        /// <param name="speed">変更後のスクロール速度</param>
        public void SetScroll(float speed)
        {
            camera.Scroll = speed;
        }

        /// <summary>
        /// (仲介者)現在スコアの取得
        /// </summary>
        /// <returns></returns>
        public int GetScore()
        {
            return score.GetScore();
        }

        ///// <summary>
        ///// （仲介者）スクロール速度の取得、変更のアクセサ
        ///// </summary>
        //public float ScrollSpeed
        //{
        //    get { return camera.Scroll; }
        //    set { camera.Scroll = value; }
        //}
    }
}
