using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunningLoad.Device
{
    /// <summary>
    /// 画面のスクロール操作クラス
    /// 作成者:谷 永吾
    /// 開始日:2018/09/20
    /// </summary>
    class ScrollCamera
    {
        //フィールド
        //使用画像
        private readonly string LAND_asset = "stage";//地面
        private readonly string SKY_asset = "bluesky_cloudy";//空
        private Vector2 landPosition, skyPosition;
        private Vector2 nextLandPos, nextSkyPos;//描画位置の差分
        //private float scrollSpeed;//スクロールの速さ（読み取り専用）→自動アクセサにより削除

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ScrollCamera()
        {
            Scroll = 15f;//初期速度をセット

            landPosition = new Vector2(0, 500);
            skyPosition = new Vector2(0, 0);
            #region 不要な変数
            //position2 = new Vector2(700, 500);
            //position3 = new Vector2(1400, 500);
            //position4 = new Vector2(2100, 500);
            #endregion
            nextLandPos = new Vector2(800, 0);
            nextSkyPos = new Vector2(1480, 0);
        }

        public void Draw(Renderer renderer)
        {
            #region 画像を1枚を複数回描画する方法に変更
            //renderer.DrawTexture(name, position1);
            //renderer.DrawTexture(name, position2);
            //renderer.DrawTexture(name, position3);
            //renderer.DrawTexture(name, position4);
            #endregion
            for (int i = 0; i < 4; i++)
            {
                renderer.DrawTexture(SKY_asset, skyPosition + nextSkyPos * i);
            }
            for (int i = 0; i < 4; i++)
            {
                renderer.DrawTexture(LAND_asset, landPosition + nextLandPos * i);
            }
        }

        /// <summary>
        /// スクロール更新処理
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            //描画位置をスクロール
            landPosition.X -= Scroll;
            skyPosition.X -= Scroll - 10f;//地面より少し遅くスクロール
            #region 不要な処理
            //position2.X -= scrollSpeed;
            //position3.X -= scrollSpeed;
            //position4.X -= scrollSpeed;
            #endregion
            //フレームアウト後の処理
            if (landPosition.X < -1600) landPosition.X = 0;
            if (skyPosition.X < -1480) skyPosition.X = 0;
        }

        /// <summary>
        /// スクロール速度に関するアクセサ
        /// get→スクロール速度の取得
        /// set→スクロール速度の任意の上書き
        /// </summary>
        /// <returns></returns>
        public float Scroll { get; set; }
    }
}
