﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Runtime.InteropServices;
using FDK;

namespace TJAPlayer3
{
    internal class CAct演奏DrumsMob : CActivity
    {
        /// <summary>
        /// 踊り子
        /// </summary>
        public CAct演奏DrumsMob()
        {
            base.bDeactivated = true;
        }

        public override void On活性化()
        {
            ctMob = new CCounter();
            ctMobPtn = new CCounter();
            base.On活性化();
        }

        public override void On非活性化()
        {
            ctMob = null;
            ctMobPtn = null;
            base.On非活性化();
        }

        public override void OnManagedResourceLoaded()
        {
            base.OnManagedResourceLoaded();
        }

        public override void OnManagedDisposed()
        {
            base.OnManagedDisposed();
        }

        public override int OnDraw()
        {
            if(!TJAPlayer3.stage演奏ドラム画面.bDoublePlay)
            {
                if (ctMob != null || TJAPlayer3.Skin.Game_Mob_Ptn != 0) ctMob.tStartLoopDb();
                if (ctMobPtn != null || TJAPlayer3.Skin.Game_Mob_Ptn != 0) ctMobPtn.tStartLoopDb();

                //CDTXMania.act文字コンソール.tPrint(0, 0, C文字コンソール.Eフォント種別.白, ctMob.db現在の値.ToString());
                //CDTXMania.act文字コンソール.tPrint(0, 10, C文字コンソール.Eフォント種別.白, Math.Sin((float)this.ctMob.db現在の値 * (Math.PI / 180)).ToString());

                if(TJAPlayer3.Skin.Game_Mob_Ptn != 0 && TJAPlayer3.ConfigIni.eGaugeMode != EGaugeMode.Hard && TJAPlayer3.ConfigIni.eGaugeMode != EGaugeMode.ExHard)
                {
                    if (TJAPlayer3.stage演奏ドラム画面.actGauge.db現在のゲージ値[0] >= 100)
                    {
                        TJAPlayer3.Tx.Mob[(int)ctMobPtn.db現在の値]?.t2D描画(TJAPlayer3.app.Device, 0, (720 - (TJAPlayer3.Tx.Mob[0].szテクスチャサイズ.Height - 70)) + -((float)Math.Sin((float)this.ctMob.db現在の値 * (Math.PI / 180)) * 70));
                    }

                }
            }
            return base.OnDraw();
        }
        #region[ private ]
        //-----------------
        public CCounter ctMob;
        public CCounter ctMobPtn;
        //-----------------
        #endregion
    }
}
