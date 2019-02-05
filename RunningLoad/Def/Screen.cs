using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunningLoad.Def
{
    /// <summary>
    /// ゲーム画面の静的クラス
    /// staticにすることで一々インスタンスする必要がなくなる
    /// public readonlyで外部から参照は可能だが、外部からの編集は不可にする
    /// </summary>
    static class Screen
    {
        //幅
        public static readonly int Width = 1280;
        //縦
        public static readonly int Height = 720;
    }
}
