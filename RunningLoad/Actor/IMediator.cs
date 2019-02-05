using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunningLoad.Actor
{
    /// <summary>
    /// ゲームの仲介者
    /// 制作者:谷 永吾
    /// 制作開始日:2018年10月2日
    /// </summary>
    interface IMediator
    {
        float GetScroll();

        void SetScroll(float speed);

        int GetScore();
    }
}
