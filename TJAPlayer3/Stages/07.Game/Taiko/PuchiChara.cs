﻿using TJAPlayer3;
using FDK;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace TJAPlayer3
{
    class PuchiChara : CActivity
    {
        public PuchiChara()
        {
            base.bDeactivated = true;
        }

        public override void On活性化()
        {
            Counter = new CCounter(0, TJAPlayer3.Skin.Game_PuchiChara[2] - 1, TJAPlayer3.Skin.Game_PuchiChara_Timer, TJAPlayer3.Timer);
            SineCounter = new CCounter(0, 360, TJAPlayer3.Skin.Game_PuchiChara_SineTimer, CSoundManager.rPlaybackTimer);
            base.On活性化();
        }
        public override void On非活性化()
        {
            Counter = null;
            SineCounter = null;
            base.On非活性化();
        }
        
        public void ChangeBPM(double bpm)
        {
            Counter = new CCounter(0, TJAPlayer3.Skin.Game_PuchiChara[2] - 1, (int)(TJAPlayer3.Skin.Game_PuchiChara_Timer * bpm / TJAPlayer3.Skin.Game_PuchiChara[2]), TJAPlayer3.Timer);
            SineCounter = new CCounter(1, 360, TJAPlayer3.Skin.Game_PuchiChara_SineTimer * bpm / 180, CSoundManager.rPlaybackTimer);
        }

        /// <summary>
        /// ぷちキャラを描画する。(オーバーライドじゃないよ)
        /// </summary>
        /// <param name="x">X座標(中央)</param>
        /// <param name="y">Y座標(中央)</param>
        /// <param name="alpha">不透明度</param>
        /// <returns></returns>
        public int On進行描画(int x, int y, bool isGrowing, int alpha = 255, bool isBalloon = false)
        {
            if (!TJAPlayer3.ConfigIni.ShowPuchiChara) return base.OnDraw();
            if (Counter == null || SineCounter == null || TJAPlayer3.Tx.PuchiChara == null) return base.OnDraw();
            Counter.tStartLoop();
            SineCounter.tStartLoopDb();
            var sineY = Math.Sin(SineCounter.db現在の値 * (Math.PI / 180)) * (TJAPlayer3.Skin.Game_PuchiChara_Sine * (isBalloon ? TJAPlayer3.Skin.Game_PuchiChara_Scale[1] : TJAPlayer3.Skin.Game_PuchiChara_Scale[0]));
            TJAPlayer3.Tx.PuchiChara.vc拡大縮小倍率 = new SlimDX.Vector3((isBalloon ? TJAPlayer3.Skin.Game_PuchiChara_Scale[1] : TJAPlayer3.Skin.Game_PuchiChara_Scale[0]));
            TJAPlayer3.Tx.PuchiChara.Opacity = alpha;
            TJAPlayer3.Tx.PuchiChara.t2D中心基準描画(TJAPlayer3.app.Device, x, y + (int)sineY, new Rectangle(Counter.nCurrentValue * TJAPlayer3.Skin.Game_PuchiChara[0], (isGrowing ? TJAPlayer3.Skin.Game_PuchiChara[1] : 0), TJAPlayer3.Skin.Game_PuchiChara[0], TJAPlayer3.Skin.Game_PuchiChara[1]));
            return base.OnDraw();
        }

        private CCounter Counter;
        private CCounter SineCounter;
    }
}