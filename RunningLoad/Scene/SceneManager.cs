using Microsoft.Xna.Framework;
using RunningLoad.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunningLoad.Scene
{
    /// <summary>
    /// シーン管理者
    /// 作成者:谷 永吾
    /// 作成日:2018/09/22
    /// </summary>
    class SceneManager
    {
        //管理する現在シーンがからの場合も考えて処理を記述する
        //フィールド
        private Dictionary<EScene, IScene> scenePairs = new Dictionary<EScene, IScene>();//シーン管理用ディクショナリ（Key:EScene Value:IScene)
        private IScene currentScene;//現在の運用中のシーン

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SceneManager()
        {   }

        /// <summary>
        /// 現在シーンを描画
        /// </summary>
        /// <param name="renderer"></param>
        public void Draw(Renderer renderer)
        {
            //現在シーンが空っぽなら何もしない
            if (currentScene == null) return;
            currentScene.Draw(renderer);
        }

        /// <summary>
        /// 現在シーンを更新
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            //現在シーンがからっぽの場合は何もしない
            if (currentScene == null) return;

            currentScene.Update(gameTime);

            if (currentScene.IsEnd())//シーンが終了状態なら
            {
                //シーンを遷移
                SetScene(currentScene.Next());//返却値と引数の型の一致を利用
            }
        }

        #region　不要なメソッド
        ///// <summary>
        ///// 現在シーンの初期化（シーンオブジェクトを初期化するのみで自身は必要ない）
        ///// </summary>
        //public void Initialize()
        //{
        //    currentScene.Initialize();
        //}
        #endregion

        /// <summary>
        /// シーンの遷移（次のシーンの変更）
        /// </summary>
        /// <param name="nextScene">次のシーンのバリューに対応する列挙型（キー）</param>
        public void SetScene(EScene nextScene)
        {
            //現在シーンにすでに何かある場合は終了させる
            if (currentScene != null) currentScene.Shutdown();
            currentScene = scenePairs[nextScene];
            currentScene.Initialize();

            #region 処理の勘違い
            //シーンを遷移させるならむしろ現在シーンをカラにしなければならない
            //if (currentScene == null) return;
            //フィールドに現在シーンを所有しているから返却処理は必要ない
            //return currentScene = scenePairs[nextScene];
            #endregion
        }

        /// <summary>
        /// シーンの管理ディクショナリへの追加
        /// </summary>
        /// <param name="sceneKey">管理するシーンの列挙型名</param>
        /// <param name="sceneValue">シーンオブジェクト</param>
        public void AddScene(EScene sceneKey, IScene sceneValue)
        {
            //すでに追加済みの場合は何もしない
            if (scenePairs.ContainsKey(sceneKey)) return;
            scenePairs.Add(sceneKey, sceneValue);
        }
    }
}
