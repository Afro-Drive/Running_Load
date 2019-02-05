using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using RunningLoad.Def;
using RunningLoad.Device;
using RunningLoad.Utility;

namespace RunningLoad.Actor
{
    /// <summary>
    /// 飛行型のエネミークラス
    /// </summary>
    class FlyingEnemy : Character
    {
        //フィールド
        private static Random random = new Random();

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public FlyingEnemy(IMediator mediator)
            : base("puteranodon", mediator)
        {
            Initialize();
        }
        
        /// <summary>
        /// 衝突時の処理（現段階では何もしない）
        /// </summary>
        public override void HitAction()
        {
            isDeadFlag = true;
        }

        /// <summary>
        /// 中心位置に初期化
        /// </summary>
        public override void Initialize()
        {
            waitTime = random.Next(60, 120);

            int rnd = random.Next(4);
            switch (rnd)
            {
                case 0:
                    InitMiddle();
                    break;
                case 1:
                    InitMiddle2();
                    break;
                case 2:
                    InitBelow();
                    break;
                case 3:
                    InitAbove();
                    break;
            }
        }
        #region 初期化位置の一覧
        /// <summary>
        /// 真ん中の位置に初期化
        /// </summary>
        public void InitMiddle()
        {
            position = new Vector2(1300 + 64, 500 - 64 - 32);
        }

        /// <summary>
        /// しゃがみ状態でかわせる位置での初期化
        /// </summary>
        public void InitMiddle2()
        {
            position = new Vector2(1300 + 64, 500 - 64 * 2);
        }

        /// <summary>
        /// 下部の位置に初期化
        /// </summary>
        public void InitBelow()
        {
            position = new Vector2(1300 + 64, 500 - 64);
        }

        /// <summary>
        /// 上部の位置に初期化
        /// </summary>
        public void InitAbove()
        {
            position = new Vector2(1300 + 64, 500 - 64 * 3);
        }
        #endregion
        /// <summary>
        /// 終了処理
        /// </summary>
        public override void Shutdown()
        {  }

        /// <summary>
        /// 更新処理
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            //X座標の負の方向に15ずつ進む
            position.X -= mediator.GetScroll() + 5f;//スクロールスピードより少し早めに

            //画面外に出たら
            if (position.X < 0)
            {
                waitTime -= 1;
                if (waitTime <= 0)//待ち時間がなくなったら
                {
                    Initialize();//初期化（再び待ち時間、位置を設定する）
                }
            }
            //フレームごとに当たり判定エリアの生成
            NewHitArea();
        }

        /// <summary>
        /// 描画（0.2倍スケール）
        /// 横:158　縦:68
        /// </summary>
        /// <param name="renderer"></param>
        public override void Draw(Renderer renderer)
        {
            //当たり判定の着色
            //renderer.DrawTexture(
            //    "hitArea_Fly",
            //    new Vector2(hitArea.X, hitArea.Y),
            //    null,
            //    new Vector2(hitArea.Width, hitArea.Height),
            //    Vector2.Zero);
            renderer.DrawTexture(name, position, null, 0.2f, Vector2.Zero);
        }

        public override void NewHitArea()
        {
            hitArea = new Rectangle(
                new Point((int)position.X + 25, (int)position.Y),
                new Point(120, 60));
        }
    }
}
