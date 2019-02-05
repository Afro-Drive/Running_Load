using Microsoft.Xna.Framework;
using RunningLoad.Device;
using RunningLoad.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunningLoad.Actor
{
    /// <summary>
    /// キャラクター管理・生成クラス
    /// 作成者:谷 永吾
    /// 作成日:2018/09/21
    /// </summary>
    class CharacterGenerateManager
    {
        //各種管理リスト
        private List<Runner> runners;//操作キャラ管理用リスト
        private List<FixedEnemy> fixEnems;//接地型エネミー管理用リスト
        private List<FlyingEnemy> flyings;//飛行型エネミー管理用リスト

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CharacterGenerateManager()
        {
            //各種リストの生成
            runners = new List<Runner>();
            fixEnems = new List<FixedEnemy>();
            flyings = new List<FlyingEnemy>();
        }

        /// <summary>
        /// キャラクターを型に応じて専用管理リストに追加
        /// </summary>
        /// <param name="addCharacter">追加するキャラクターオブジェクト</param>
        public void Add(Character addCharacter)
        {
            if (addCharacter is Runner) runners.Add((Runner)addCharacter);
            else if (addCharacter is FixedEnemy) fixEnems.Add((FixedEnemy)addCharacter);
            else if (addCharacter is FlyingEnemy) flyings.Add((FlyingEnemy)addCharacter);
        }

        /// <summary>
        /// キャラクターの生成
        /// </summary>
        /// <param name="name">Runner,Fixed,Flyingのどれか</param>
        public void Generate(string name, IMediator mediator)
        {
            Character generatedChara;//格納用のキャラクター変数を生成

            if (name == "Runner") generatedChara = new Runner(mediator);
            else if (name == "Fixed") generatedChara = new FixedEnemy(mediator);
            else if (name == "Flying") generatedChara = new FlyingEnemy(mediator);
            else //指定以外の文字列を入力したら
            {
#if DEBUG //デバッグモードの時のみ下記のエラー分をコンソールに表示
                Console.WriteLine("キャラクターに該当する名前がありませんでした。");
#endif
                return;
            }

            Add(generatedChara);
        }

        /// <summary>
        /// 管理キャラクタを総更新
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            foreach (var runner in runners) runner.Update(gameTime);
            foreach (var fixEnem in fixEnems) fixEnem.Update(gameTime);
            foreach (var flying in flyings) flying.Update(gameTime);

            JudgeCollision();//あたり判定を繰り返す
        }

        /// <summary>
        /// 管理キャラクターの総描画
        /// </summary>
        /// <param name="renderer"></param>
        public void Draw(Renderer renderer)
        {
            foreach (var runner in runners) runner.Draw(renderer);
            foreach (var fixedEnem in fixEnems) fixedEnem.Draw(renderer);
            foreach (var flying in flyings) flying.Draw(renderer);
        }

        /// <summary>
        /// 管理キャラの総初期化
        /// </summary>
        public void Initialize()
        {
            foreach (var runner in runners) runner.Initialize();
            foreach (var fixedEnem in fixEnems) fixedEnem.Initialize();
            foreach (var flying in flyings) flying.Initialize();
        }

        /// <summary>
        /// クリア処理（管理リストの要素をすべて破棄）
        /// </summary>
        public void Clear()
        {
            runners.Clear();
            fixEnems.Clear();
            flyings.Clear();
        }

        /// <summary>
        /// キャラ同士の一斉当たり判定
        /// </summary>
        public void JudgeCollision()
        {
            //プレイヤーと固定エネミー
            foreach(var runner in runners)
            {
                foreach(var fix in fixEnems)
                {
                    if (runner.IsHit(fix))
                    {
                        runner.HitAction();
                        fix.HitAction();
                    }
                }
            }
            //プレイヤーと飛行エネミー
            foreach (var runner in runners)
            {
                foreach(var fly in flyings)
                {
                    if (runner.IsHit(fly))
                    {
                        runner.HitAction();
                        fly.HitAction();
                    }
                }
            }
        }

        /// <summary>
        /// 運用終了か？
        /// </summary>
        /// <returns>プレイヤーが死んだかどうか</returns>
        public bool IsEnd()
        {
            foreach(var runner in runners)
            {
                //もしプレイヤーが死亡してたら真
                if (runner.IsDead()) return true;
            }
            return false;//普段は偽を返却
        }
    }
}
