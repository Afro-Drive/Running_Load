using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using RunningLoad.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunningLoad.Scene
{
    /// <summary>
    /// ゲームシーン後の結果表示クラス
    /// 作成者:谷 永吾
    /// </summary>
    class Result :  IScene
    {
        private bool isEndFlag;//次のシーンへのトリガー
        private IScene backGroundScene;//ひとつ前のシーン（背景画としてのみ使用し、更新処理はしない）
        private SoundManager sound;//BGM，SEの取り扱い

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="previousScene">ひとつ前のシーン</param>
        public Result(IScene previousScene)
        {
            backGroundScene = previousScene;
            isEndFlag = false;
        }

        /// <summary>
        /// 描画
        /// </summary>
        /// <param name="renderer"></param>
        public void Draw(Renderer renderer)
        {
            backGroundScene.Draw(renderer);

            renderer.Begin();
            renderer.DrawTexture("ending", new Vector2(386, 232));
            renderer.End();
        }

        public void Initialize()
        {
            sound = DeviceManager.CreateInstance().GetSound();
            isEndFlag = false;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            sound.PlayBGM("endingBGM");
            if(Input.IskeyDown(Keys.Enter) || Input.IskeyDown(Keys.Space))
            {
                sound.PlaySE("select_menu");
                isEndFlag = true;
            }
        }

        /// <summary>
        /// シーン終了か？
        /// </summary>
        /// <returns>エンドフラグ</returns>
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
            return EScene.Title;
        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public void Shutdown()
        {   }
    }
}
