using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunningLoad.Device
{
    /// <summary>
    /// 2Dゲーム専用カメラクラス（SpriteBatch型のDrawメソッド内で使用）
    /// 作成者:谷 永吾
    /// 作成開始日:2018/10/01
    /// </summary>
    class Camera_2D
    {
        //フィールド
        private Vector2 cam_position = Vector2.Zero;//カメラの位置
        private Vector2 cam_zoom = Vector2.One;//ズーム値
        private Rectangle visibleArea;//視認領域
        private float cam_rotation = 0.0f;//回転量
        private Vector2 cam_centralPosition = Vector2.Zero;//画面の中心点

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="width">縦描画領域</param>
        /// <param name="height">横描画領域</param>
        public Camera_2D(int width, int height)
        {
            visibleArea = new Rectangle(0, 0, width, height);
            cam_position = new Vector2(width / 2, height / 2);
            cam_centralPosition = new Vector2(width / 2, height / 2);
            Position = cam_position;
        }

        /// <summary>
        /// カメラ位置のアクセサ
        /// （get→現在の位置を取得）
        /// （set→カメラの位置、視認範囲を設定）
        /// </summary>
        public Vector2 Position
        {
            get { return cam_position; }//取得
            set//引数に応じて設定
            {
                cam_position = value;
                visibleArea.X = (int)(cam_position.X - visibleArea.Width / 2);
                visibleArea.Y = (int)(cam_position.Y - visibleArea.Height / 2);
            }
        }

        /// <summary>
        /// 行列を用いた描画時のカメラのメンバの決定
        /// </summary>
        /// <returns></returns>
        public virtual Matrix GetMatrix()
        {
            //Vector3(Vector2 2D, float z)→Vector2型の変数を2次元平面とした奥行きzの3次元ベクトルの生成
            Vector3 pos3 = new Vector3(cam_position, 0);
            Vector3 screenPos3 = new Vector3(cam_centralPosition, 0.0f);

            //カメラのメンバに応じた描画状態を返却（行列を使用）
            return Matrix.CreateTranslation(-pos3) *//（おそらく）3次元の空間を生成しているようだ
                Matrix.CreateScale(cam_zoom.X, cam_zoom.Y, 1.0f) *//第一変数をX軸、第二変数をY軸、第三変数をZ軸とした3次元空間の生成
                Matrix.CreateRotationZ(cam_rotation) *//Z軸を回転軸とした指定引数分回転後の3次元空間の生成
                Matrix.CreateTranslation(screenPos3);
        }
    }
}
