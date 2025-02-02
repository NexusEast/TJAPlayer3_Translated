﻿using FDK;

namespace TJAPlayer3
{
    internal class CAct演奏DrumsDancer : CActivity
    {
        /// <summary>
        /// 踊り子
        /// </summary>
        public CAct演奏DrumsDancer()
        {
            base.bDeactivated = true;
        }

        public override void On活性化()
        {
            this.ct踊り子モーション = new CCounter();
            base.On活性化();
        }

        public override void On非活性化()
        {
            this.ct踊り子モーション = null;
            base.On非活性化();
        }

        public override void OnManagedResourceLoaded()
        {
            this.ar踊り子モーション番号 = C変換.ar配列形式のstringをint配列に変換して返す(TJAPlayer3.Skin.Game_Dancer_Motion);
            if(this.ar踊り子モーション番号 == null) ar踊り子モーション番号 = C変換.ar配列形式のstringをint配列に変換して返す("0,0");
            this.ct踊り子モーション = new CCounter(0, this.ar踊り子モーション番号.Length - 1, 0.01, CSoundManager.rPlaybackTimer);
            base.OnManagedResourceLoaded();
        }

        public override int OnDraw()
        {
            if( this.b初めての進行描画 )
            {
                this.b初めての進行描画 = false;
            }

            if (this.ct踊り子モーション != null || TJAPlayer3.Skin.Game_Dancer_Ptn != 0) this.ct踊り子モーション.tStartLoopDb();

            if (TJAPlayer3.ConfigIni.ShowDancer && this.ct踊り子モーション != null && TJAPlayer3.Skin.Game_Dancer_Ptn != 0)
            {
                var dancerTextures = TJAPlayer3.Tx.Dancer;
                if (dancerTextures != null)
                {
                    for (int i = 0; i < dancerTextures.Length; i++)
                    {
                        var dancerTexture = dancerTextures[i][this.ar踊り子モーション番号[(int)this.ct踊り子モーション.db現在の値]];
                        if (dancerTexture != null)
                        {
                            if ((int)TJAPlayer3.stage演奏ドラム画面.actGauge.db現在のゲージ値[0] >= TJAPlayer3.Skin.Game_Dancer_Gauge[i])
                            {
                                dancerTexture.t2D中心基準描画(TJAPlayer3.app.Device, TJAPlayer3.Skin.Game_Dancer_X[i], TJAPlayer3.Skin.Game_Dancer_Y[i]);
                            }
                        }
                    }
                }
            }
            return base.OnDraw();
        }

        #region[ private ]
        //-----------------
        public int[] ar踊り子モーション番号;
        public CCounter ct踊り子モーション;
        //-----------------
        #endregion
    }
}
