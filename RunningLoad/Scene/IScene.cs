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
    /// シーンのインターフェイス
    /// 作成者:谷 永吾
    /// 作成日:2018/09/22
    /// </summary>
    interface IScene
    {
        void Initialize();//初期化
        void Update(GameTime gameTime);//更新処理
        void Draw(Renderer renderer);//描画処理
        bool IsEnd();//シーンの終了か？
        EScene Next();//次のシーンへ（シーン管理者への橋渡し）
        void Shutdown();//シーンの終了処理
    }
}
