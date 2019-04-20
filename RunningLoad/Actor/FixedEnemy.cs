using Microsoft.Xna.Framework;
using RunningLoad.Def;
using RunningLoad.Device;
using RunningLoad.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunningLoad.Actor
{
    /// <summary>
    /// 移動しないエネミークラス
    /// </summary>
    class FixedEnemy : Character
    {
        //フィールド
        private Random rand = new Random();

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public FixedEnemy(IMediator mediator)
            : base("saboten", mediator)
        {
            Initialize();
        }

        /// <summary>
        /// 初期化
        /// </summary>
        public override void Initialize()
        {
            position = new Vector2(rand.Next(1300 + 64, 1300 + 64 + 300), 378);
            waitTime = 60 * rand.Next(1, 5);
        }

        /// <summary>
        /// 更新処理
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            position.X -= mediator.GetScroll() + 5f;//スクロールスピードより少し早めに

            if (position.X < -15)
            {
                waitTime -= 1;
                if (waitTime == 0)//待ち時間が０になったら
                {
                     Initialize();//初期化
                }
            }
            NewHitArea();//当たり判定生成しなおし
        }

        #region Characterクラスに委託
        /// <summary>
        /// 描画メソッド
        /// </summary>
        /// <param name="renderer"></param>
        //public void Draw(Renderer renderer)
        //{
        //    renderer.DrawTexture("black", position);
        //}
        #endregion

        /// <summary>
        /// 終了処理
        /// </summary>
        public override void Shutdown()
        { }

        public override void HitAction()
        {
            isDeadFlag = true;
        }

        /// <summary>
        /// 描画（0.15倍スケール）
        /// 横:96　縦:144
        /// </summary>
        /// <param name="renderer"></param>
        public override void Draw(Renderer renderer)
        {
            //当たり判定の着色
            //renderer.DrawTexture(
            //    "hitArea_Fix",
            //    new Vector2(hitArea.X, hitArea.Y),
            //    null,
            //    new Vector2(hitArea.Width, hitArea.Height),
            //    new Vector2(0, 0));
            if (!isDeadFlag)
            {
                renderer.DrawTexture(name, position);
            }
            else//死亡時は花を咲かす（笑）
            {
                renderer.DrawTexture("saboten_flower", position);
            }
        }

        public override void NewHitArea()
        {
            hitArea = new Rectangle(
                new Point((int)position.X + 10, (int)position.Y + 20),
                new Point(60, 100));
        }
    }
}
