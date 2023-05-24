using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SlimDX;
using FDK;
using System.Drawing;

namespace TJAPlayer3
{
    class GoGoSplash : CActivity
    {
        public GoGoSplash()
        {
            this.bDeactivated = true;
        }

        public override void On活性化()
        {
            Splash = new CCounter();
            base.On活性化();
        }

        public override void On非活性化()
        {
            base.On非活性化();
        }

        /// <summary>
        /// ゴーゴースプラッシュの描画処理です。
        /// SkinCofigで本数を変更することができます。
        /// </summary>
        /// <returns></returns>
        public override int OnDraw()
        {
            if (Splash == null) return base.OnDraw();
            // for Debug
            // if (CDTXMania.Input管理.Keyboard.bキーが押された((int)SlimDX.DirectInput.Key.A)) StartSplash();
            Splash.tStart();
            if (Splash.bEnded)
            {
                Splash.nCurrentValue = 0;
                Splash.tStop();
            }
            if (Splash.b進行中)
            {
                for (int i = 0; i < TJAPlayer3.Skin.Game_Effect_GoGoSplash_X.Length; i++)
                {
                    if (i > TJAPlayer3.Skin.Game_Effect_GoGoSplash_Y.Length) break;
                    // Yの配列がiよりも小さかったらそこでキャンセルする。
                    if(TJAPlayer3.Skin.Game_Effect_GoGoSplash_Rotate && TJAPlayer3.Tx.Effects_GoGoSplash != null)
                    {
                        // Switch文を使いたかったが、定数じゃないから使えねぇ!!!!
                        if (i == 0)
                        {
                            TJAPlayer3.Tx.Effects_GoGoSplash.fZ軸中心回転 = -0.2792526803190927f;
                        }
                        else if (i == 1)
                        {
                            TJAPlayer3.Tx.Effects_GoGoSplash.fZ軸中心回転 = -0.13962634015954636f;
                        }
                        else if (i == TJAPlayer3.Skin.Game_Effect_GoGoSplash_X.Length - 2)
                        {
                            TJAPlayer3.Tx.Effects_GoGoSplash.fZ軸中心回転 = 0.13962634015954636f;
                        }
                        else if (i == TJAPlayer3.Skin.Game_Effect_GoGoSplash_X.Length - 1)
                        {
                            TJAPlayer3.Tx.Effects_GoGoSplash.fZ軸中心回転 = 0.2792526803190927f;
                        }
                        else
                        {
                            TJAPlayer3.Tx.Effects_GoGoSplash.fZ軸中心回転 = 0.0f;
                        }
                    }
                    TJAPlayer3.Tx.Effects_GoGoSplash?.t2D拡大率考慮下中心基準描画(TJAPlayer3.app.Device, TJAPlayer3.Skin.Game_Effect_GoGoSplash_X[i], TJAPlayer3.Skin.Game_Effect_GoGoSplash_Y[i], new Rectangle(TJAPlayer3.Skin.Game_Effect_GoGoSplash[0] * Splash.nCurrentValue, 0, TJAPlayer3.Skin.Game_Effect_GoGoSplash[0], TJAPlayer3.Skin.Game_Effect_GoGoSplash[1]));
                }
            }
            return base.OnDraw();
        }

        public void StartSplash()
        {
            Splash = new CCounter(0, TJAPlayer3.Skin.Game_Effect_GoGoSplash[2] - 1, TJAPlayer3.Skin.Game_Effect_GoGoSplash_Timer, TJAPlayer3.Timer);
        }

        private CCounter Splash;
    }
}
