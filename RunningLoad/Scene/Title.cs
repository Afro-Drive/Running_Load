using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using RunningLoad.Device;

namespace RunningLoad.Scene
{
    /// <summary>
    /// タイトルシーン
    /// 作成者:谷 永吾
    /// 作成開始日:2018/09/24
    /// </summary>
    class Title : IScene
    {
        //フィールド
        private bool isEndFlag;//次のシーンへのトリガー
        private SoundManager sound;//BGM，SEを取り扱う

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Title()
        {
            isEndFlag = false;
        }

        /// <summary>
        /// 描画処理
        /// </summary>
        /// <param name="renderer"></param>
        public void Draw(Renderer renderer)
        {
            renderer.Begin();//シーンのクラスの描画時は終わりと始まりを記述するように！
            renderer.DrawTexture("title", Vector2.Zero);
            renderer.End();
        }

        /// <summary>
        /// 初期化
        /// </summary>
        public void Initialize()
        {
            isEndFlag = false;
            sound = DeviceManager.CreateInstance().GetSound();
        }

        /// <summary>
        /// 終了か？
        /// </summary>
        /// <returns>フィールドisEndFlag</returns>
        public bool IsEnd()
        {
            return isEndFlag;
        }

        /// <summary>
        /// 次のシーンへの移動指定
        /// </summary>
        /// <returns>遷移したいシーンに対応する列挙型</returns>
        public EScene Next()
        {
            return EScene.GameScene;
        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public void Shutdown()
        {   }

        /// <summary>
        /// 更新処理
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            sound.PlayBGM("titleBGM");
            //ボタン入力を確認
            if (Input.IskeyDown(Keys.Enter) || Input.IskeyDown(Keys.Space))
            {
                sound.PlaySE("select_menu");
                isEndFlag = true;
            }
        }
    }
}
