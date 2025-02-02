﻿using System.Drawing;
using FDK;

namespace TJAPlayer3
{
    internal class CAct演奏Drums演奏終了演出 : CActivity
    {
        /// <summary>
        /// 課題
        /// _クリア失敗 →素材不足(確保はできる。切り出しと加工をしてないだけ。)
        /// _
        /// </summary>
        public CAct演奏Drums演奏終了演出()
        {
            base.bDeactivated = true;
        }

        public void Start()
        {
            this.ct進行メイン = new CCounter(0, 300, 22, TJAPlayer3.Timer);
            // モードの決定。クリア失敗・フルコンボも事前に作っとく。
            if(TJAPlayer3.stage選曲.n確定された曲の難易度 == (int)Difficulty.Dan)
            {
                // 段位認定モード。
                if (!TJAPlayer3.stage演奏ドラム画面.actDan.GetFailedAllChallenges())
                {
                    // 段位認定モード、クリア成功
                    this.Mode[0] = EndMode.StageCleared;
                }
                else
                {
                    // 段位認定モード、クリア失敗
                    this.Mode[0] = EndMode.StageFailed;
                }
            }
            else
            {
                // 通常のモード。
                // ここでフルコンボフラグをチェックするが現時点ではない。
                // 今の段階では魂ゲージ80%以上でチェック。

                //ハードゲージの場合、完奏=クリアなので、
                //EndModeチェックのタイミングでは常にクリアになってもいい気がするけど、一応↓
                for (int i = 0; i < TJAPlayer3.ConfigIni.nPlayerCount; i++)
                {
                    if (TJAPlayer3.ConfigIni.eGaugeMode == EGaugeMode.Hard || TJAPlayer3.ConfigIni.eGaugeMode == EGaugeMode.ExHard)
                    {
                        if (TJAPlayer3.stage演奏ドラム画面.actGauge.db現在のゲージ値[i] > 0)
                            this.Mode[i] = EndMode.StageCleared;
                        else
                            this.Mode[i] = EndMode.StageFailed;
                    }
                    else
                    {
                        if (TJAPlayer3.stage演奏ドラム画面.actGauge.db現在のゲージ値[i] >= 80)
                        {
                            this.Mode[i] = EndMode.StageCleared;
                        }
                        else
                        {
                            this.Mode[i] = EndMode.StageFailed;
                        }
                    }
                }
            }
        }

        public override void On活性化()
        {
            this.bリザルトボイス再生済み = false;
            this.Mode = new EndMode[2];
            base.On活性化();
        }

        public override void On非活性化()
        {
            this.ct進行メイン = null;
            base.On非活性化();
        }

        public override void OnManagedResourceLoaded()
        {
            this.b再生済み = false;
            this.soundClear = TJAPlayer3.Sound管理.tサウンドを生成する(CSkin.Path(@"Sounds\Clear.ogg"), ESoundGroup.SoundEffect);
            base.OnManagedResourceLoaded();
        }

        public override void OnManagedDisposed()
        {
            if (this.soundClear != null)
                this.soundClear.t解放する();
            base.OnManagedDisposed();
        }

        public override int OnDraw()
        {
            if (base.b初めての進行描画)
            {
                base.b初めての進行描画 = false;
            }
            if (this.ct進行メイン != null && (TJAPlayer3.stage演奏ドラム画面.eフェーズID == CStage.Eフェーズ.演奏_演奏終了演出 || TJAPlayer3.stage演奏ドラム画面.eフェーズID == CStage.Eフェーズ.演奏_STAGE_CLEAR_フェードアウト || TJAPlayer3.stage演奏ドラム画面.eフェーズID == CStage.Eフェーズ.演奏_STAGE_FAILED_ハード))
            {
                this.ct進行メイン.tStart();

                //CDTXMania.act文字コンソール.tPrint( 0, 0, C文字コンソール.Eフォント種別.灰, this.ct進行メイン.nCurrentValue.ToString() );
                //仮置き
                for (int i = 0; i < TJAPlayer3.ConfigIni.nPlayerCount; i++)
                {
                    switch (this.Mode[i])
                    {
                        case EndMode.StageFailed:
                            break;
                        case EndMode.StageCleared:
                            int[] y = new int[] { 210, 386 };
                            for (int j = 0; j < TJAPlayer3.ConfigIni.nPlayerCount; j++)
                            {

                                //this.ct進行メイン.nCurrentValue = 18;
                                if (this.soundClear != null && !this.b再生済み)
                                {
                                    this.soundClear.t再生を開始する();
                                    this.b再生済み = true;
                                }
                            }
                            if (TJAPlayer3.Tx.End_Clear_Text != null)
                            {
                                //this.ct進行メイン.nCurrentValue = 18;
                                //if (this.soundClear != null && !this.b再生済み)
                                //{
                                //    this.soundClear.t再生を開始する();
                                //    this.b再生済み = true;
                                //}

                                #region[ 文字 ]
                                //登場アニメは20フレーム。うち最初の5フレームは半透過状態。
                                float[] f文字拡大率 = new float[] { 1.04f, 1.11f, 1.15f, 1.19f, 1.23f, 1.26f, 1.30f, 1.31f, 1.32f, 1.32f, 1.32f, 1.30f, 1.30f, 1.26f, 1.25f, 1.19f, 1.15f, 1.11f, 1.05f, 1.0f };
                                int[] n透明度 = new int[] { 43, 85, 128, 170, 213, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255 };
                                if (this.ct進行メイン.nCurrentValue >= 17)
                                {
                                    if (this.ct進行メイン.nCurrentValue <= 36)
                                    {
                                        TJAPlayer3.Tx.End_Clear_Text.vc拡大縮小倍率.Y = f文字拡大率[this.ct進行メイン.nCurrentValue - 17];
                                        TJAPlayer3.Tx.End_Clear_Text.Opacity = n透明度[this.ct進行メイン.nCurrentValue - 17];
                                        TJAPlayer3.Tx.End_Clear_Text.t2D描画(TJAPlayer3.app.Device, 634, (int)(y[i] - ((90 * f文字拡大率[this.ct進行メイン.nCurrentValue - 17]) - 90)), new Rectangle(0, 0, 90, 90));
                                    }
                                    else
                                    {
                                        TJAPlayer3.Tx.End_Clear_Text.vc拡大縮小倍率.Y = 1.0f;
                                        TJAPlayer3.Tx.End_Clear_Text.t2D描画(TJAPlayer3.app.Device, 634, y[i], new Rectangle(0, 0, 90, 90));
                                    }
                                }
                                if (this.ct進行メイン.nCurrentValue >= 19)
                                {
                                    if (this.ct進行メイン.nCurrentValue <= 38)
                                    {
                                        TJAPlayer3.Tx.End_Clear_Text.vc拡大縮小倍率.Y = f文字拡大率[this.ct進行メイン.nCurrentValue - 19];
                                        TJAPlayer3.Tx.End_Clear_Text.Opacity = n透明度[this.ct進行メイン.nCurrentValue - 19];
                                        TJAPlayer3.Tx.End_Clear_Text.t2D描画(TJAPlayer3.app.Device, 692, (int)(y[i] - ((90 * f文字拡大率[this.ct進行メイン.nCurrentValue - 19]) - 90)), new Rectangle(90, 0, 90, 90));
                                    }
                                    else
                                    {
                                        TJAPlayer3.Tx.End_Clear_Text.vc拡大縮小倍率.Y = 1.0f;
                                        TJAPlayer3.Tx.End_Clear_Text.t2D描画(TJAPlayer3.app.Device, 692, y[i], new Rectangle(90, 0, 90, 90));
                                    }
                                }
                                TJAPlayer3.Tx.End_Clear_Text.vc拡大縮小倍率.Y = 1.0f;
                                if (this.ct進行メイン.nCurrentValue >= 21)
                                {
                                    if (this.ct進行メイン.nCurrentValue <= 40)
                                    {
                                        TJAPlayer3.Tx.End_Clear_Text.vc拡大縮小倍率.Y = f文字拡大率[this.ct進行メイン.nCurrentValue - 21];
                                        TJAPlayer3.Tx.End_Clear_Text.Opacity = n透明度[this.ct進行メイン.nCurrentValue - 21];
                                        TJAPlayer3.Tx.End_Clear_Text.t2D描画(TJAPlayer3.app.Device, 750, y[i] - (int)((90 * f文字拡大率[this.ct進行メイン.nCurrentValue - 21]) - 90), new Rectangle(180, 0, 90, 90));
                                    }
                                    else
                                    {
                                        TJAPlayer3.Tx.End_Clear_Text.vc拡大縮小倍率.Y = 1.0f;
                                        TJAPlayer3.Tx.End_Clear_Text.t2D描画(TJAPlayer3.app.Device, 750, y[i], new Rectangle(180, 0, 90, 90));
                                    }
                                }
                                if (this.ct進行メイン.nCurrentValue >= 23)
                                {
                                    if (this.ct進行メイン.nCurrentValue <= 42)
                                    {
                                        TJAPlayer3.Tx.End_Clear_Text.vc拡大縮小倍率.Y = f文字拡大率[this.ct進行メイン.nCurrentValue - 23];
                                        TJAPlayer3.Tx.End_Clear_Text.Opacity = n透明度[this.ct進行メイン.nCurrentValue - 23];
                                        TJAPlayer3.Tx.End_Clear_Text.t2D描画(TJAPlayer3.app.Device, 819, y[i] - (int)((90 * f文字拡大率[this.ct進行メイン.nCurrentValue - 23]) - 90), new Rectangle(270, 0, 90, 90));
                                    }
                                    else
                                    {
                                        TJAPlayer3.Tx.End_Clear_Text.vc拡大縮小倍率.Y = 1.0f;
                                        TJAPlayer3.Tx.End_Clear_Text.t2D描画(TJAPlayer3.app.Device, 819, y[i], new Rectangle(270, 0, 90, 90));
                                    }
                                }
                                if (this.ct進行メイン.nCurrentValue >= 25)
                                {
                                    if (this.ct進行メイン.nCurrentValue <= 44)
                                    {
                                        TJAPlayer3.Tx.End_Clear_Text.vc拡大縮小倍率.Y = f文字拡大率[this.ct進行メイン.nCurrentValue - 25];
                                        TJAPlayer3.Tx.End_Clear_Text.Opacity = n透明度[this.ct進行メイン.nCurrentValue - 25];
                                        TJAPlayer3.Tx.End_Clear_Text.t2D描画(TJAPlayer3.app.Device, 890, (y[i] + 2) - (int)((90 * f文字拡大率[this.ct進行メイン.nCurrentValue - 25]) - 90), new Rectangle(360, 0, 90, 90));
                                    }
                                    else
                                    {
                                        TJAPlayer3.Tx.End_Clear_Text.vc拡大縮小倍率.Y = 1.0f;
                                        TJAPlayer3.Tx.End_Clear_Text.t2D描画(TJAPlayer3.app.Device, 890, y[i] + 2, new Rectangle(360, 0, 90, 90));
                                    }
                                }
                                if (this.ct進行メイン.nCurrentValue >= 50 && this.ct進行メイン.nCurrentValue < 90 && TJAPlayer3.Tx.End_Clear_Text_Effect != null)
                                {
                                    if (this.ct進行メイン.nCurrentValue < 70)
                                    {
                                        TJAPlayer3.Tx.End_Clear_Text_Effect.Opacity = (this.ct進行メイン.nCurrentValue - 50) * (255 / 20);
                                        TJAPlayer3.Tx.End_Clear_Text_Effect.t2D描画(TJAPlayer3.app.Device, 634, y[i] - 2);
                                    }
                                    else
                                    {
                                        TJAPlayer3.Tx.End_Clear_Text_Effect.Opacity = 255 - ((this.ct進行メイン.nCurrentValue - 70) * (255 / 20));
                                        TJAPlayer3.Tx.End_Clear_Text_Effect.t2D描画(TJAPlayer3.app.Device, 634, y[i] - 2);
                                    }
                                }
                                #endregion
                                #region[ バチお ]
                                if (this.ct進行メイン.nCurrentValue <= 11)
                                {
                                    if (TJAPlayer3.Tx.End_Clear_L[1] != null)
                                    {
                                        TJAPlayer3.Tx.End_Clear_L[1].t2D描画(TJAPlayer3.app.Device, 697, y[i] - 30);
                                        TJAPlayer3.Tx.End_Clear_L[1].Opacity = (int)(11.0 / this.ct進行メイン.nCurrentValue) * 255;
                                    }
                                    if (TJAPlayer3.Tx.End_Clear_R[1] != null)
                                    {
                                        TJAPlayer3.Tx.End_Clear_R[1].t2D描画(TJAPlayer3.app.Device, 738, y[i] - 30);
                                        TJAPlayer3.Tx.End_Clear_R[1].Opacity = (int)(11.0 / this.ct進行メイン.nCurrentValue) * 255;
                                    }
                                }
                                else if (this.ct進行メイン.nCurrentValue <= 35)
                                {
                                    TJAPlayer3.Tx.End_Clear_L[0]?.t2D描画(TJAPlayer3.app.Device, 697 - (int)((this.ct進行メイン.nCurrentValue - 12) * 10), y[i] - 30);
                                    TJAPlayer3.Tx.End_Clear_R[0]?.t2D描画(TJAPlayer3.app.Device, 738 + (int)((this.ct進行メイン.nCurrentValue - 12) * 10), y[i] - 30);
                                }
                                else if (this.ct進行メイン.nCurrentValue <= 46)
                                {
                                    //2016.07.16 kairera0467 またも原始的...
                                    float[] fRet = { 1.0f, 0.99f, 0.98f, 0.97f, 0.96f, 0.95f, 0.96f, 0.97f, 0.98f, 0.99f, 1.0f };

                                    if (TJAPlayer3.Tx.End_Clear_L[0] != null)
                                    {
                                        TJAPlayer3.Tx.End_Clear_L[0].t2D描画(TJAPlayer3.app.Device, 466, y[i] - 30);
                                        TJAPlayer3.Tx.End_Clear_L[0].vc拡大縮小倍率 = new SlimDX.Vector3(fRet[this.ct進行メイン.nCurrentValue - 36], 1.0f, 1.0f);
                                    }

                                    if (TJAPlayer3.Tx.End_Clear_R[0] != null)
                                    {
                                        TJAPlayer3.Tx.End_Clear_R[0].t2D描画(TJAPlayer3.app.Device, 1136 - 180 * fRet[this.ct進行メイン.nCurrentValue - 36], y[i] - 30);
                                        TJAPlayer3.Tx.End_Clear_R[0].vc拡大縮小倍率 = new SlimDX.Vector3(fRet[this.ct進行メイン.nCurrentValue - 36], 1.0f, 1.0f);
                                    }
                                }
                                else if (this.ct進行メイン.nCurrentValue <= 49)
                                {
                                    TJAPlayer3.Tx.End_Clear_L[1]?.t2D描画(TJAPlayer3.app.Device, 466, y[i] - 30);
                                    TJAPlayer3.Tx.End_Clear_R[1]?.t2D描画(TJAPlayer3.app.Device, 956, y[i] - 30);
                                }
                                else if (this.ct進行メイン.nCurrentValue <= 54)
                                {
                                    TJAPlayer3.Tx.End_Clear_L[2]?.t2D描画(TJAPlayer3.app.Device, 466, y[i] - 30);
                                    TJAPlayer3.Tx.End_Clear_R[2]?.t2D描画(TJAPlayer3.app.Device, 956, y[i] - 30);
                                }
                                else if (this.ct進行メイン.nCurrentValue <= 58)
                                {
                                    TJAPlayer3.Tx.End_Clear_L[3]?.t2D描画(TJAPlayer3.app.Device, 466, y[i] - 30);
                                    TJAPlayer3.Tx.End_Clear_R[3]?.t2D描画(TJAPlayer3.app.Device, 956, y[i] - 30);
                                }
                                else
                                {
                                    TJAPlayer3.Tx.End_Clear_L[4]?.t2D描画(TJAPlayer3.app.Device, 466, y[i] - 30);
                                    TJAPlayer3.Tx.End_Clear_R[4]?.t2D描画(TJAPlayer3.app.Device, 956, y[i] - 30);
                                }
                                #endregion
                            }
                            break;
                        case EndMode.StageFullCombo:
                            break;
                        default:
                            break;
                    }

                }



                if (this.ct進行メイン.bEnded)
                {
                    if (!this.bリザルトボイス再生済み)
                    {
                        TJAPlayer3.Skin.sound成績発表.t再生する();
                        this.bリザルトボイス再生済み = true;
                    }
                    return 1;
                }
            }

            return 0;
        }

        #region[ private ]
        //-----------------
        bool b再生済み;
        bool bリザルトボイス再生済み;
        CCounter ct進行メイン;
        //CTexture[] txバチお左_成功 = new CTexture[ 5 ];
        //CTexture[] txバチお右_成功 = new CTexture[ 5 ];
        //CTexture tx文字;
        //CTexture tx文字マスク;
        CSound soundClear;
        EndMode[] Mode;
        enum EndMode
        {
            StageFailed,
            StageCleared,
            StageFullCombo
        }
        //-----------------
        #endregion
    }
}
