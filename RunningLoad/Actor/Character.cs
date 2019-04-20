using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
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
    /// キャラクター抽象クラス
    /// </summary>
    abstract class Character
    {
        //フィールド
        protected string name;//画像のアセット名
        protected Vector2 position;//位置
        protected Rectangle hitArea; //当たり判定エリア（矩形）
        protected bool isDeadFlag;//死亡フラグ
        protected int waitTime = 60;//次の出現までの時間(フレーム数60を基準とする）
        protected IMediator mediator;//ゲームの仲介者
        protected Dictionary<string, Motion> motionDict; //モーションを管理するディクショナリ
        protected Motion currentMotion; //現在動作中のモーション
        //protected SoundManager soundManager;//SE等を再生→ContentManagerが生成できないため引数に持ってこれず断念
        //protected ContentManager content;→インスタンスの引数が面倒なので削除

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="name">画像の名前</param>
        public Character(string name, IMediator mediator)
        {
            this.name = name;
            isDeadFlag = false;
            this.mediator = mediator;
            //実際の画像より1周りほど小さい判定エリアを生成
            NewHitArea();

            //空の状態で生成(各種派生クラスで別個に生成・追加する)
            motionDict = null;
            currentMotion = null;
        }

        //抽象メソッド
        public abstract void Initialize();
        public abstract void Update(GameTime gameTime);
        public abstract void Shutdown();
        public abstract void HitAction();
        public abstract void NewHitArea();

        /// <summary>
        /// 死んでいるか？
        /// </summary>
        /// <returns>死亡判定</returns>
        public bool IsDead()
        {
            return isDeadFlag;
        }
        #region あたり判定の新規生成→それぞれの使用画像に合わせるため抽象メソッド化
        //public void NewHitArea()
        //{
        //    hitArea = new Rectangle(
        //        new Point((int)position.X + 10, (int)position.Y + 3),
        //        new Point(64, 800));
        //}
        #endregion
        #region しゃがみ状態での当たり判定の生成→Runnerクラスに格納
        //public void NewHitArea_half()
        //{
        //    //従来の半分のエリアを生成
        //    hitArea = new Rectangle(
        //        new Point((int)position.X + 3, (int)position.Y + 3 + 32),
        //        new Point(64 - 2, (64 - 6) / 2));//Y方向の矩形の一辺が半分
        //}
        #endregion
        /// <summary>
        /// ヒットエリアの返却
        /// </summary>
        /// <returns></returns>
        public Rectangle HitArea()
        {
            return hitArea;
        }

        /// <summary>
        /// 当たったか？
        /// </summary>
        /// <param name="target">衝突対象</param>
        /// <returns>当たり判定エリアの衝突判定</returns>
        public bool IsHit(Character target)
        {
            return hitArea.Intersects(target.hitArea);
        }

        /// <summary>
        /// 描画メソッド
        /// </summary>
        /// <param name="renderer"></param>
        public virtual void Draw(Renderer renderer)
        {
            renderer.DrawTexture(name, position);
        }
    }
}
