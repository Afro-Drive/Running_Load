using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using RunningLoad.Def;
using RunningLoad.Device;
using RunningLoad.Utility;

namespace RunningLoad.Actor
{
    /// <summary>
    /// 操作可能のプレイヤークラス
    /// </summary>
    class Runner : Character
    {
        //フィールド
        private float vx, vy;//ジャンプ中の移動量(x,y方向）
        private bool doSquatFlag;//しゃがみ状態か？
        private readonly float gravity = 0.2f;//重力
        private readonly float addGravity = 0.45f;//追加重力
        private readonly float jumpPower = 3.7f;//ジャンプ時の初速
        private SoundManager sound;//サウンド管理者

        private readonly float landPosY = 410; //着地点(Y座標)
        private readonly float squatLandPosY = 420; //しゃがみ時の着地点(Y座標)

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Runner(IMediator mediator)
            : base("dinasour_nikushoku", mediator)
        {

        }

        /// <summary>
        /// 初期化
        /// </summary>
        public override void Initialize()
        {
            sound = DeviceManager.CreateInstance().GetSound();
            //モーション管理ディクショナリを生成・登録
            motionDict = new Dictionary<string, Motion>()
            {
                { "run", new Motion(new Range(0, 5), new CountDownTimer(0.001f)) }, //走っている状態
                { "squat", new Motion(new Range(0, 4), new CountDownTimer(0.001f)) },　//しゃがんでいる状態
            };
            //モーションごとの切り取り範囲の登録
            for (int i = 0; i <= 5; i++)
            {
                motionDict["run"].Add(i,
                    new Rectangle(new Point(256 * i, 0), new Point(256, 128)));
            }
            for (int i = 0; i <= 4; i++)
            {
                motionDict["squat"].Add(i,
                    new Rectangle(new Point(256 * i, 0), new Point(256, 128)));
            }
            //最初は走っている状態でセット
            currentMotion = motionDict["run"];

            doSquatFlag = false;//しゃがみ状態でない
            position = new Vector2(100, 345);
            //加速度は0
            vx = 0;//今のところX方向は使用する予定はない
            vy = 0;
        }


        /// <summary>
        /// 更新処理
        /// </summary>
        /// <param name="gameTime">ゲーム時間</param>
        public override void Update(GameTime gameTime)
        {

            float speed = 5.0f; //移動速度

            #region 不要な操作処理
            //操作処理
            ////右
            //if (Keyboard.GetState().IsKeyDown(Keys.Right))
            //{
            //    movement.X += 1.0f;
            //}
            ////左
            //if (Keyboard.GetState().IsKeyDown(Keys.Left))
            //{
            //    movement.X -= 1.0f;
            //}
            #endregion

            #region ジャンプ処理
            //Y軸負の方向への力を生じる
            if ((Input.IskeyDown(Keys.Up) || Input.IskeyDown(Keys.Space)) && position.Y == landPosY)
            {
                sound.PlaySE("punyu");//ジャンプ音
                vy = -jumpPower; //初速度を生じる
            }
            //重力の適応
            if (Input.BeingKeyDown(Keys.Down))
            {
                vy += gravity + addGravity;//下キー押下状態ではさらに落下速度上昇
            }
            else if (Input.GetKeyState(Keys.Up) || Input.GetKeyState(Keys.Space))
            {
                vy += gravity - 0.09f;//上キーまたはスペースキー押下状態では滞空時間が上がる
            }
            else vy += gravity;//Y軸の正の方向（下向き）に座標を加算
            #endregion
            #region しゃがみ処理
            if (position.Y >= 345 && (Input.BeingKeyDown(Keys.Down) || Input.IskeyDown(Keys.Down)))
            {
                doSquatFlag = true;
            }
            if (doSquatFlag)
            {
                if (!Input.BeingKeyDown(Keys.Down) || Input.IskeyDown(Keys.Down))
                {
                    doSquatFlag = false;
                }
            }
            #endregion
            #region 誤った記述
            //下に落ちていく
            //if(movement.Y >= 200)//上記の処理でvyは負の数になるため不要
            //{
            //    vy = -vy;
            //}
            #endregion
            #region 重複した記述
            //if (position.Y >= 500 - 32)
            //{
            //    vy = 0;
            //}
            #endregion

            //位置座標に反映
            position = position + new Vector2(vx, vy) * speed;

            //着地時
            #region　必要のない処理より削除
            ////左
            //if(position.X < 0.0f)
            //{
            //    position.X = 0.0f;
            //}
            ////右
            //if(position.X > Screen.Width - 64)
            //{
            //    position.X = Screen.Width - 64;
            //}
            ////上
            //if (position.Y < 0.0f)
            //{
            //    position.Y = 0.0f;
            //}
            #endregion
            if (position.Y > landPosY && !doSquatFlag)
            {
                position.Y = landPosY;
                vy = 0;
            }
            else if (position.Y > squatLandPosY && doSquatFlag)
            {
                position.Y = squatLandPosY;
                vy = 0;
            }

            //使用するモーションの更新
            if (doSquatFlag)
                currentMotion = motionDict["squat"];
            else
                currentMotion = motionDict["run"];
            //モーションの更新
            currentMotion.Update(gameTime);

            //当たり判定エリアを生成し続ける
            NewHitArea();
        }

        #region Characterクラスに委託
        /// <summary>
        /// 描画メソッド
        /// </summary>
        /// <param name="renderer">描画オブジェクト</param>
        //public void Draw(Renderer renderer)
        //{
        //    renderer.DrawTexture("white", position);
        //}
        #endregion

        /// <summary>
        /// 終了処理
        /// </summary>
        public override void Shutdown()
        { }

        public override void HitAction()
        {
            sound.PlaySE("middle_punch");
            isDeadFlag = true;
        }

        /// <summary>
        /// あたり判定の新規生成(立位、臥位）
        /// </summary>
        public override void NewHitArea()
        {
            //しゃがみようのエリア生成
            if (doSquatFlag)
            {
                //従来の半分のエリアを生成
                hitArea = new Rectangle(
                    new Point((int)position.X + 60, (int)position.Y + 30),
                    new Point(170, 50));//Y方向の矩形の一辺が半分
            }
            //走っているときの当たり判定生成
            else
            {
                hitArea = new Rectangle(
                    new Point((int)position.X + 60, (int)position.Y + 15),
                    new Point(170, 50));
            }
        }
        /// <summary>
        /// 描画（0.2倍スケール）
        /// 横:296　縦:209
        /// </summary>
        /// <param name="renderer"></param>
        public override void Draw(Renderer renderer)
        {
            //ココのコメントを外すと当たり判定のエリアが出てくる
            //renderer.DrawTexture(
            //    "hitArea_R",
            //    new Vector2(hitArea.X, hitArea.Y),
            //    null,
            //    new Vector2(hitArea.Width, hitArea.Height),
            //    Vector2.Zero);

            if (IsDead())//死亡時の描画
            {
                renderer.DrawTexture("dinasour_cry", position);
            }
            else
            {
                if (doSquatFlag)//しゃがみ時の描画
                {
                    renderer.DrawTexture(
                        "dinasour_squat",
                        position,
                        currentMotion.DrawingRange());
                }
                else
                    renderer.DrawTexture(
                        "dinasour_running",
                        position,
                        currentMotion.DrawingRange());
            }
            //renderer.DrawTexture(name, position, null, 0.2f, Vector2.Zero);
        }
    }
}
