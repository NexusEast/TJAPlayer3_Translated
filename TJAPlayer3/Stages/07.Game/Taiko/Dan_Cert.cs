﻿using System;
using System.Drawing;
using System.Runtime.InteropServices;
using FDK;
using System.IO;

namespace TJAPlayer3
{
    internal class Dan_Cert : CActivity
    {
        /// <summary>
        /// 段位認定
        /// </summary>
        public Dan_Cert()
        {
            base.bDeactivated = true;
        }

        //
        Dan_C[] Challenge = new Dan_C[3];
        //

        public void Start(int number)
        {
            NowShowingNumber = number;
            Counter_In = new CCounter(0, 999, 1, TJAPlayer3.Timer);
            ScreenPoint = new double[] { TJAPlayer3.Skin.Game_Lane_Background_X[0] - (TJAPlayer3.Tx.DanC_Screen?.szテクスチャサイズ.Width ?? 1280) / 2, 1280 };
            TJAPlayer3.stage演奏ドラム画面.ReSetScore(TJAPlayer3.DTX.List_DanSongs[NowShowingNumber].ScoreInit, TJAPlayer3.DTX.List_DanSongs[NowShowingNumber].ScoreDiff);
            IsAnimating = true;
            TJAPlayer3.stage演奏ドラム画面.actPanel.SetPanelString(TJAPlayer3.DTX.List_DanSongs[NowShowingNumber].Title, TJAPlayer3.DTX.List_DanSongs[NowShowingNumber].Genre, 1 + NowShowingNumber + "曲目");
            Sound_Section?.tサウンドを先頭から再生する();
        }

        public override void On活性化()
        {
            for (int i = 0; i < 3; i++)
            {
                if(TJAPlayer3.DTX.Dan_C[i] != null) Challenge[i] = new Dan_C(TJAPlayer3.DTX.Dan_C[i]);
            }
            // 始点を決定する。
            ExamCount = 0;
            for (int i = 0; i < 3; i++)
            {
                if (Challenge[i] != null && Challenge[i].GetEnable() == true)
                    this.ExamCount++;
            }

            for (int i = 0; i < 3; i++)
            {
                Status[i] = new ChallengeStatus();
                Status[i].Timer_Amount = new CCounter();
                Status[i].Timer_Gauge = new CCounter();
                Status[i].Timer_Failed = new CCounter();
            }
            IsEnded = false;

            if (TJAPlayer3.stage選曲.n確定された曲の難易度 == (int)Difficulty.Dan) IsAnimating = true;
            base.On活性化();
        }

        public void Update()
        {
            for (int i = 0; i < 3; i++)
            {
                if (Challenge[i] == null || !Challenge[i].GetEnable()) return;
                var oldReached = Challenge[i].GetReached();
                var isChangedAmount = false;
                switch (Challenge[i].GetExamType())
                {
                    case Exam.Type.Gauge:
                        isChangedAmount = Challenge[i].Update((int)TJAPlayer3.stage演奏ドラム画面.actGauge.db現在のゲージ値[0]);
                        break;
                    case Exam.Type.JudgePerfect:
                        isChangedAmount = Challenge[i].Update((int)TJAPlayer3.stage演奏ドラム画面.nヒット数_Auto含む.Drums.Perfect + TJAPlayer3.stage演奏ドラム画面.nヒット数_Auto含まない.Drums.Perfect);
                        break;
                    case Exam.Type.JudgeGood:
                        isChangedAmount = Challenge[i].Update((int)TJAPlayer3.stage演奏ドラム画面.nヒット数_Auto含む.Drums.Great + TJAPlayer3.stage演奏ドラム画面.nヒット数_Auto含まない.Drums.Great);
                        break;
                    case Exam.Type.JudgeBad:
                        isChangedAmount = Challenge[i].Update((int)TJAPlayer3.stage演奏ドラム画面.nヒット数_Auto含まない.Drums.Miss);
                        break;
                    case Exam.Type.Score:
                        isChangedAmount = Challenge[i].Update((int)TJAPlayer3.stage演奏ドラム画面.actScore.GetScore(0));
                        break;
                    case Exam.Type.Roll:
                        isChangedAmount = Challenge[i].Update((int)(TJAPlayer3.stage演奏ドラム画面.GetRoll(0)));
                        break;
                    case Exam.Type.Hit:
                        isChangedAmount = Challenge[i].Update((int)(TJAPlayer3.stage演奏ドラム画面.nヒット数_Auto含む.Drums.Perfect + TJAPlayer3.stage演奏ドラム画面.nヒット数_Auto含まない.Drums.Perfect + TJAPlayer3.stage演奏ドラム画面.nヒット数_Auto含む.Drums.Great + TJAPlayer3.stage演奏ドラム画面.nヒット数_Auto含まない.Drums.Great + TJAPlayer3.stage演奏ドラム画面.GetRoll(0)));
                        break;
                    case Exam.Type.Combo:
                        isChangedAmount = Challenge[i].Update((int)TJAPlayer3.stage演奏ドラム画面.actCombo.n現在のコンボ数.P1最高値);
                        break;
                    default:
                        break;
                }

                // 値が変更されていたらアニメーションを行う。
                if (isChangedAmount)
                {
                    if(Status[i].Timer_Amount != null && Status[i].Timer_Amount.b終了値に達してない)
                    {
                        Status[i].Timer_Amount = new CCounter(0, 11, 12, TJAPlayer3.Timer);
                        Status[i].Timer_Amount.nCurrentValue = 1;
                    }
                    else
                    {
                        Status[i].Timer_Amount = new CCounter(0, 11, 12, TJAPlayer3.Timer);
                    }
                }

                // 条件の達成見込みがあるかどうか判断する。
                if (Challenge[i].GetExamRange() == Exam.Range.Less)
                {
                    Challenge[i].SetReached(!Challenge[i].IsCleared[0]);
                }
                else
                {
                    var notesRemain = TJAPlayer3.DTX.nNoteCount[3] - (TJAPlayer3.stage演奏ドラム画面.nヒット数_Auto含む.Drums.Perfect + TJAPlayer3.stage演奏ドラム画面.nヒット数_Auto含まない.Drums.Perfect) - (TJAPlayer3.stage演奏ドラム画面.nヒット数_Auto含む.Drums.Great + TJAPlayer3.stage演奏ドラム画面.nヒット数_Auto含まない.Drums.Great) - (TJAPlayer3.stage演奏ドラム画面.nヒット数_Auto含む.Drums.Miss + TJAPlayer3.stage演奏ドラム画面.nヒット数_Auto含まない.Drums.Miss);
                    // 残り音符数が0になったときに判断されるやつ
                    if (notesRemain <= 0)
                    {
                        // 残り音符数ゼロ
                        switch (Challenge[i].GetExamType())
                        {
                            case Exam.Type.Gauge:
                                if (Challenge[i].Amount < Challenge[i].Value[0]) Challenge[i].SetReached(true);
                                break;
                            case Exam.Type.Score:
                                if (Challenge[i].Amount < Challenge[i].Value[0]) Challenge[i].SetReached(true);
                                break;
                            default:
                                // 何もしない
                                break;
                        }
                    }
                    // 常に監視されるやつ。
                    switch (Challenge[i].GetExamType())
                    {
                        case Exam.Type.JudgePerfect:
                        case Exam.Type.JudgeGood:
                        case Exam.Type.JudgeBad:
                            if (notesRemain < (Challenge[i].Value[0] - Challenge[i].Amount)) Challenge[i].SetReached(true);
                            break;
                        case Exam.Type.Combo:
                            if (notesRemain + TJAPlayer3.stage演奏ドラム画面.actCombo.n現在のコンボ数.P1 < ((Challenge[i].Value[0])) && TJAPlayer3.stage演奏ドラム画面.actCombo.n現在のコンボ数.P1最高値 < (Challenge[i].Value[0])) Challenge[i].SetReached(true);
                            break;
                        default:
                            break;
                    }

                    // 音源が終了したやつの分岐。
                    // ( CDTXMania.DTX.listChip.Count > 0 ) ? CDTXMania.DTX.listChip[ CDTXMania.DTX.listChip.Count - 1 ].nNoiseTimems : 0;
                    if(!IsEnded)
                    {
                        if (TJAPlayer3.DTX.listChip.Count <= 0) continue;
                        if (TJAPlayer3.DTX.listChip[TJAPlayer3.DTX.listChip.Count - 1].nNoiseTimems < TJAPlayer3.Timer.n現在時刻)
                        {
                            switch (Challenge[i].GetExamType())
                            {
                                case Exam.Type.Score:
                                case Exam.Type.Roll:
                                case Exam.Type.Hit:
                                    if (Challenge[i].Amount < Challenge[i].Value[0]) Challenge[i].SetReached(true);
                                    break;
                                default:
                                    break;
                            }
                            IsEnded = true;
                        }
                    }
                }
                if(oldReached == false && Challenge[i].GetReached() == true)
                {
                    Sound_Failed?.tサウンドを先頭から再生する();
                }
            }
        }

        public override void On非活性化()
        {
            for (int i = 0; i < 3; i++)
            {
                Challenge[i] = null;
            }

            for (int i = 0; i < 3; i++)
            {
                Status[i].Timer_Amount = null;
                Status[i].Timer_Gauge = null;
                Status[i].Timer_Failed = null;
            }
            IsEnded = false;
            base.On非活性化();
        }

        public override void OnManagedResourceLoaded()
        {
            Dan_Plate = TJAPlayer3.tテクスチャの生成(Path.GetDirectoryName(TJAPlayer3.DTX.strtFileAbsolutePath) + @"\Dan_Plate.png");
            Sound_Section = TJAPlayer3.Sound管理.tサウンドを生成する(CSkin.Path(@"Sounds\Dan\Section.ogg"), ESoundGroup.SoundEffect);
            Sound_Failed = TJAPlayer3.Sound管理.tサウンドを生成する(CSkin.Path(@"Sounds\Dan\Failed.ogg"), ESoundGroup.SoundEffect);
            base.OnManagedResourceLoaded();
        }

        public override void OnManagedDisposed()
        {
            Dan_Plate?.Dispose();
            Sound_Section?.t解放する();
            Sound_Failed?.t解放する();
            base.OnManagedDisposed();
        }

        public override int OnDraw()
        {
            if (TJAPlayer3.stage選曲.n確定された曲の難易度 != (int)Difficulty.Dan) return base.OnDraw();
            Counter_In?.tStart();
            Counter_Wait?.tStart();
            Counter_Out?.tStart();
            Counter_Text?.tStart();

            if (Counter_Text != null)
            {
                if (Counter_Text.nCurrentValue >= 2000)
                {
                    for (int i = Counter_Text_Old; i < Counter_Text.nCurrentValue; i++)
                    {
                        if (i % 2 == 0)
                        {
                            if (TJAPlayer3.DTX.List_DanSongs[NowShowingNumber].TitleTex != null)
                            {
                                TJAPlayer3.DTX.List_DanSongs[NowShowingNumber].TitleTex.Opacity--;
                            }
                            if (TJAPlayer3.DTX.List_DanSongs[NowShowingNumber].SubTitleTex != null)
                            {
                                TJAPlayer3.DTX.List_DanSongs[NowShowingNumber].SubTitleTex.Opacity--;
                            }
                        }
                    }
                }
                else
                {
                    if (TJAPlayer3.DTX.List_DanSongs[NowShowingNumber].TitleTex != null)
                    {
                        TJAPlayer3.DTX.List_DanSongs[NowShowingNumber].TitleTex.Opacity = 255;
                    }
                    if (TJAPlayer3.DTX.List_DanSongs[NowShowingNumber].SubTitleTex != null)
                    {
                        TJAPlayer3.DTX.List_DanSongs[NowShowingNumber].SubTitleTex.Opacity = 255;
                    }
                }
                Counter_Text_Old = Counter_Text.nCurrentValue;
            }

            for (int i = 0; i < 3; i++)
            {
                Status[i].Timer_Amount?.tStart();
            }

            //for (int i = 0; i < 3; i++)
            //{
            //    if (Challenge[i] != null && Challenge[i].GetEnable())
            //        CDTXMania.act文字コンソール.tPrint(0, 20 * i, C文字コンソール.Eフォント種別.白, Challenge[i].ToString());
            //    else
            //        CDTXMania.act文字コンソール.tPrint(0, 20 * i, C文字コンソール.Eフォント種別.白, "None");
            //}
            //CDTXMania.act文字コンソール.tPrint(0, 80, C文字コンソール.Eフォント種別.白, String.Format("Notes Remain: {0}", CDTXMania.DTX.nNoteCount[3] - (CDTXMania.stage演奏ドラム画面.nヒット数_Auto含む.Drums.Perfect + CDTXMania.stage演奏ドラム画面.nヒット数_Auto含まない.Drums.Perfect) - (CDTXMania.stage演奏ドラム画面.nヒット数_Auto含む.Drums.Great + CDTXMania.stage演奏ドラム画面.nヒット数_Auto含まない.Drums.Great) - (CDTXMania.stage演奏ドラム画面.nヒット数_Auto含む.Drums.Miss + CDTXMania.stage演奏ドラム画面.nヒット数_Auto含まない.Drums.Miss)));

            // 背景を描画する。

            TJAPlayer3.Tx.DanC_Background?.t2D描画(TJAPlayer3.app.Device, 0, 0);


            // 残り音符数を描画する。
            var notesRemain = TJAPlayer3.DTX.nNoteCount[3] - (TJAPlayer3.stage演奏ドラム画面.nヒット数_Auto含む.Drums.Perfect + TJAPlayer3.stage演奏ドラム画面.nヒット数_Auto含まない.Drums.Perfect) - (TJAPlayer3.stage演奏ドラム画面.nヒット数_Auto含む.Drums.Great + TJAPlayer3.stage演奏ドラム画面.nヒット数_Auto含まない.Drums.Great) - (TJAPlayer3.stage演奏ドラム画面.nヒット数_Auto含む.Drums.Miss + TJAPlayer3.stage演奏ドラム画面.nヒット数_Auto含まない.Drums.Miss);

            DrawNumber(notesRemain, TJAPlayer3.Skin.Game_DanC_Number_XY[0], TJAPlayer3.Skin.Game_DanC_Number_XY[1], TJAPlayer3.Skin.Game_DanC_Number_Padding);

            // 段プレートを描画する。
            Dan_Plate?.t2D中心基準描画(TJAPlayer3.app.Device, TJAPlayer3.Skin.Game_DanC_Dan_Plate[0], TJAPlayer3.Skin.Game_DanC_Dan_Plate[1]);

            DrawExam(Challenge);

            // 幕のアニメーション
            if (Counter_In != null)
            {
                if (Counter_In.b終了値に達してない)
                {
                    for (int i = Counter_In_Old; i < Counter_In.nCurrentValue; i++)
                    {
                        ScreenPoint[0] += (TJAPlayer3.Skin.Game_Lane_Background_X[0] - ScreenPoint[0]) / 180.0;
                        ScreenPoint[1] += ((1280 / 2 + TJAPlayer3.Skin.Game_Lane_Background_X[0] / 2) - ScreenPoint[1]) / 180.0;
                    }
                    Counter_In_Old = Counter_In.nCurrentValue;
                    TJAPlayer3.Tx.DanC_Screen?.t2D描画(TJAPlayer3.app.Device, (int)ScreenPoint[0], TJAPlayer3.Skin.Game_Lane_Background_Y[0], new Rectangle(0, 0, TJAPlayer3.Tx.DanC_Screen.szテクスチャサイズ.Width / 2, TJAPlayer3.Tx.DanC_Screen.szテクスチャサイズ.Height));
                    TJAPlayer3.Tx.DanC_Screen?.t2D描画(TJAPlayer3.app.Device, (int)ScreenPoint[1], TJAPlayer3.Skin.Game_Lane_Background_Y[0], new Rectangle(TJAPlayer3.Tx.DanC_Screen.szテクスチャサイズ.Width / 2, 0, TJAPlayer3.Tx.DanC_Screen.szテクスチャサイズ.Width / 2, TJAPlayer3.Tx.DanC_Screen.szテクスチャサイズ.Height));
                    //CDTXMania.act文字コンソール.tPrint(0, 420, C文字コンソール.Eフォント種別.白, String.Format("{0} : {1}", ScreenPoint[0], ScreenPoint[1]));
                }
                if (Counter_In.bEnded)
                {
                    Counter_In = null;
                    Counter_Wait = new CCounter(0, 2299, 1, TJAPlayer3.Timer);
                }
            }
            if (Counter_Wait != null)
            {
                if (Counter_Wait.b終了値に達してない)
                {
                    TJAPlayer3.Tx.DanC_Screen?.t2D描画(TJAPlayer3.app.Device, TJAPlayer3.Skin.Game_Lane_Background_X[0], TJAPlayer3.Skin.Game_Lane_Background_Y[0]);
                }
                if (Counter_Wait.bEnded)
                {
                    Counter_Wait = null;
                    Counter_Out = new CCounter(0, 499, 1, TJAPlayer3.Timer);
                    Counter_Text = new CCounter(0, 2899, 1, TJAPlayer3.Timer);
                }
            }
            if (Counter_Text != null)
            {
                if (Counter_Text.b終了値に達してない)
                {
                    var title = TJAPlayer3.DTX.List_DanSongs[NowShowingNumber].TitleTex;
                    var subTitle = TJAPlayer3.DTX.List_DanSongs[NowShowingNumber].SubTitleTex;
                    if (subTitle == null)
                        title?.t2D拡大率考慮中央基準描画(TJAPlayer3.app.Device, 1280 / 2 + TJAPlayer3.Skin.Game_Lane_Background_X[0] / 2, TJAPlayer3.Skin.Game_Lane_Background_Y[0] + 65);
                    else
                    {
                        title?.t2D拡大率考慮中央基準描画(TJAPlayer3.app.Device, 1280 / 2 + TJAPlayer3.Skin.Game_Lane_Background_X[0] / 2, TJAPlayer3.Skin.Game_Lane_Background_Y[0] + 45);
                        subTitle?.t2D拡大率考慮中央基準描画(TJAPlayer3.app.Device, 1280 / 2 + TJAPlayer3.Skin.Game_Lane_Background_X[0] / 2, TJAPlayer3.Skin.Game_Lane_Background_Y[0] + 85);
                    }
                }
                if (Counter_Text.bEnded)
                {
                    Counter_Text = null;
                    IsAnimating = false;
                }
            }
            if (Counter_Out != null)
            {
                if (Counter_Out.b終了値に達してない)
                {
                    for (int i = Counter_Out_Old; i < Counter_Out.nCurrentValue; i++)
                    {
                        ScreenPoint[0] += -3;
                        ScreenPoint[1] += 3;
                    }
                    Counter_Out_Old = Counter_Out.nCurrentValue;
                    TJAPlayer3.Tx.DanC_Screen?.t2D描画(TJAPlayer3.app.Device, (int)ScreenPoint[0], TJAPlayer3.Skin.Game_Lane_Background_Y[0], new Rectangle(0, 0, TJAPlayer3.Tx.DanC_Screen.szテクスチャサイズ.Width / 2, TJAPlayer3.Tx.DanC_Screen.szテクスチャサイズ.Height));
                    TJAPlayer3.Tx.DanC_Screen?.t2D描画(TJAPlayer3.app.Device, (int)ScreenPoint[1], TJAPlayer3.Skin.Game_Lane_Background_Y[0], new Rectangle(TJAPlayer3.Tx.DanC_Screen.szテクスチャサイズ.Width / 2, 0, TJAPlayer3.Tx.DanC_Screen.szテクスチャサイズ.Width / 2, TJAPlayer3.Tx.DanC_Screen.szテクスチャサイズ.Height));
                    //CDTXMania.act文字コンソール.tPrint(0, 420, C文字コンソール.Eフォント種別.白, String.Format("{0} : {1}", ScreenPoint[0], ScreenPoint[1]));
                }
                if (Counter_Out.bEnded)
                {
                    Counter_Out = null;
                }
            }
            return base.OnDraw();
        }

        public void DrawExam(Dan_C[] dan_C)
        {
            var count = 0;
            for (int i = 0; i < 3; i++)
            {
                if (dan_C[i] != null && dan_C[i].GetEnable() == true)
                    count++;
            }
            for (int i = 0; i < count; i++)
            {
                #region ゲージの土台を描画する。
                TJAPlayer3.Tx.DanC_Base?.t2D描画(TJAPlayer3.app.Device, TJAPlayer3.Skin.Game_DanC_X[count - 1], TJAPlayer3.Skin.Game_DanC_Y[count - 1] + TJAPlayer3.Skin.Game_DanC_Size[1] * i + (i * TJAPlayer3.Skin.Game_DanC_Padding));
                #endregion


                #region ゲージを描画する。
                var drawGaugeType = 0;
                if (dan_C[i].GetExamRange() == Exam.Range.More)
                {
                    if (dan_C[i].GetAmountToPercent() >= 100)
                        drawGaugeType = 2;
                    else if (dan_C[i].GetAmountToPercent() >= 70)
                        drawGaugeType = 1;
                    else
                        drawGaugeType = 0;
                }
                else
                {
                    if (dan_C[i].GetAmountToPercent() >= 100)
                        drawGaugeType = 2;
                    else if (dan_C[i].GetAmountToPercent() > 70)
                        drawGaugeType = 1;
                    else
                        drawGaugeType = 0;
                }

                TJAPlayer3.Tx.DanC_Gauge[drawGaugeType]?.t2D拡大率考慮下基準描画(TJAPlayer3.app.Device,
                    TJAPlayer3.Skin.Game_DanC_X[count - 1] + TJAPlayer3.Skin.Game_DanC_Offset[0], TJAPlayer3.Skin.Game_DanC_Y[count - 1] + TJAPlayer3.Skin.Game_DanC_Size[1] * (i + 1) + ((i + 1) * TJAPlayer3.Skin.Game_DanC_Padding) - TJAPlayer3.Skin.Game_DanC_Offset[1], new Rectangle(0, 0, (int)(dan_C[i].GetAmountToPercent() * (TJAPlayer3.Tx.DanC_Gauge[drawGaugeType].szテクスチャサイズ.Width / 100.0)), TJAPlayer3.Tx.DanC_Gauge[drawGaugeType].szテクスチャサイズ.Height));
                #endregion

                #region 現在の値を描画する。
                var nowAmount = 0;
                if (dan_C[i].GetExamRange() == Exam.Range.Less)
                {
                    nowAmount = dan_C[i].Value[0] - dan_C[i].Amount;
                }
                else
                {
                    nowAmount = dan_C[i].Amount;
                }
                if (nowAmount < 0) nowAmount = 0;

                DrawNumber(nowAmount, TJAPlayer3.Skin.Game_DanC_X[count - 1] + TJAPlayer3.Skin.Game_DanC_Number_Small_Number_Offset[0], TJAPlayer3.Skin.Game_DanC_Y[count - 1] + TJAPlayer3.Skin.Game_DanC_Size[1] * (i + 1) + ((i + 1) * TJAPlayer3.Skin.Game_DanC_Padding) - TJAPlayer3.Skin.Game_DanC_Number_Small_Number_Offset[1], TJAPlayer3.Skin.Game_DanC_Number_Small_Padding, TJAPlayer3.Skin.Game_DanC_Number_Small_Scale, TJAPlayer3.Skin.Game_DanC_Number_Small_Scale, (Status[i].Timer_Amount != null ? ScoreScale[Status[i].Timer_Amount.nCurrentValue] : 0f));

                // 単位(あれば)
                switch (dan_C[i].GetExamType())
                {
                    case Exam.Type.Gauge:
                        // パーセント
                        TJAPlayer3.Tx.DanC_ExamUnit?.t2D拡大率考慮下基準描画(TJAPlayer3.app.Device, TJAPlayer3.Skin.Game_DanC_X[count - 1] + TJAPlayer3.Skin.Game_DanC_Number_Small_Number_Offset[0] + TJAPlayer3.Skin.Game_DanC_Number_Padding / 4 - TJAPlayer3.Skin.Game_DanC_Percent_Hit_Score_Padding[0], TJAPlayer3.Skin.Game_DanC_Y[count - 1] + TJAPlayer3.Skin.Game_DanC_Size[1] * (i + 1) + ((i + 1) * TJAPlayer3.Skin.Game_DanC_Padding) - TJAPlayer3.Skin.Game_DanC_Number_Small_Number_Offset[1], new Rectangle(0, TJAPlayer3.Skin.Game_DanC_ExamUnit_Size[1] * 0, TJAPlayer3.Skin.Game_DanC_ExamUnit_Size[0], TJAPlayer3.Skin.Game_DanC_ExamUnit_Size[1]));
                        break;
                    case Exam.Type.Score:
                        TJAPlayer3.Tx.DanC_ExamUnit?.t2D拡大率考慮下基準描画(TJAPlayer3.app.Device, TJAPlayer3.Skin.Game_DanC_X[count - 1] + TJAPlayer3.Skin.Game_DanC_Number_Small_Number_Offset[0] + TJAPlayer3.Skin.Game_DanC_Number_Padding / 4 - TJAPlayer3.Skin.Game_DanC_Percent_Hit_Score_Padding[2], TJAPlayer3.Skin.Game_DanC_Y[count - 1] + TJAPlayer3.Skin.Game_DanC_Size[1] * (i + 1) + ((i + 1) * TJAPlayer3.Skin.Game_DanC_Padding) - TJAPlayer3.Skin.Game_DanC_Number_Small_Number_Offset[1], new Rectangle(0, TJAPlayer3.Skin.Game_DanC_ExamUnit_Size[1] * 2, TJAPlayer3.Skin.Game_DanC_ExamUnit_Size[0], TJAPlayer3.Skin.Game_DanC_ExamUnit_Size[1]));

                        // 点
                        break;
                    case Exam.Type.Roll:
                    case Exam.Type.Hit:
                        TJAPlayer3.Tx.DanC_ExamUnit?.t2D拡大率考慮下基準描画(TJAPlayer3.app.Device, TJAPlayer3.Skin.Game_DanC_X[count - 1] + TJAPlayer3.Skin.Game_DanC_Number_Small_Number_Offset[0] + TJAPlayer3.Skin.Game_DanC_Number_Padding / 4 - TJAPlayer3.Skin.Game_DanC_Percent_Hit_Score_Padding[1], TJAPlayer3.Skin.Game_DanC_Y[count - 1] + TJAPlayer3.Skin.Game_DanC_Size[1] * (i + 1) + ((i + 1) * TJAPlayer3.Skin.Game_DanC_Padding) - TJAPlayer3.Skin.Game_DanC_Number_Small_Number_Offset[1], new Rectangle(0, TJAPlayer3.Skin.Game_DanC_ExamUnit_Size[1] * 1, TJAPlayer3.Skin.Game_DanC_ExamUnit_Size[0], TJAPlayer3.Skin.Game_DanC_ExamUnit_Size[1]));

                        // 打
                        break;
                    default:
                        // 何もしない
                        break;
                }

                #endregion


                #region 条件の文字を描画する。
                var offset = TJAPlayer3.Skin.Game_DanC_Exam_Offset[0];
                //offset -= CDTXMania.Skin.Game_DanC_ExamRange_Padding;
                // 条件の範囲
                TJAPlayer3.Tx.DanC_ExamRange?.t2D拡大率考慮下基準描画(TJAPlayer3.app.Device, TJAPlayer3.Skin.Game_DanC_X[count - 1] + offset - TJAPlayer3.Tx.DanC_ExamRange.szテクスチャサイズ.Width, TJAPlayer3.Skin.Game_DanC_Y[count - 1] + TJAPlayer3.Skin.Game_DanC_Size[1] * (i + 1) + ((i + 1) * TJAPlayer3.Skin.Game_DanC_Padding) - TJAPlayer3.Skin.Game_DanC_Exam_Offset[1], new Rectangle(0, TJAPlayer3.Skin.Game_DanC_ExamRange_Size[1] * (int)dan_C[i].GetExamRange(), TJAPlayer3.Skin.Game_DanC_ExamRange_Size[0], TJAPlayer3.Skin.Game_DanC_ExamRange_Size[1]));
                //offset -= CDTXMania.Skin.Game_DanC_ExamRange_Padding;
                offset -= TJAPlayer3.Skin.Game_DanC_ExamRange_Padding;

                // 単位(あれば)
                switch (dan_C[i].GetExamType())
                {
                    case Exam.Type.Gauge:
                        // パーセント
                        TJAPlayer3.Tx.DanC_ExamUnit?.t2D拡大率考慮下基準描画(TJAPlayer3.app.Device, TJAPlayer3.Skin.Game_DanC_X[count - 1] + offset - TJAPlayer3.Tx.DanC_ExamUnit.szテクスチャサイズ.Width, TJAPlayer3.Skin.Game_DanC_Y[count - 1] + TJAPlayer3.Skin.Game_DanC_Size[1] * (i + 1) + ((i + 1) * TJAPlayer3.Skin.Game_DanC_Padding) - TJAPlayer3.Skin.Game_DanC_Exam_Offset[1], new Rectangle(0, TJAPlayer3.Skin.Game_DanC_ExamUnit_Size[1] * 0, TJAPlayer3.Skin.Game_DanC_ExamUnit_Size[0], TJAPlayer3.Skin.Game_DanC_ExamUnit_Size[1]));
                        offset -= TJAPlayer3.Skin.Game_DanC_Percent_Hit_Score_Padding[0];
                        break;
                    case Exam.Type.Score:
                        TJAPlayer3.Tx.DanC_ExamUnit?.t2D拡大率考慮下基準描画(TJAPlayer3.app.Device, TJAPlayer3.Skin.Game_DanC_X[count - 1] + offset - TJAPlayer3.Tx.DanC_ExamUnit.szテクスチャサイズ.Width, TJAPlayer3.Skin.Game_DanC_Y[count - 1] + TJAPlayer3.Skin.Game_DanC_Size[1] * (i + 1) + ((i + 1) * TJAPlayer3.Skin.Game_DanC_Padding) - TJAPlayer3.Skin.Game_DanC_Exam_Offset[1], new Rectangle(0, TJAPlayer3.Skin.Game_DanC_ExamUnit_Size[1] * 2, TJAPlayer3.Skin.Game_DanC_ExamUnit_Size[0], TJAPlayer3.Skin.Game_DanC_ExamUnit_Size[1]));
                        offset -= TJAPlayer3.Skin.Game_DanC_Percent_Hit_Score_Padding[2];

                        // 点
                        break;
                    case Exam.Type.Roll:
                    case Exam.Type.Hit:
                        TJAPlayer3.Tx.DanC_ExamUnit?.t2D拡大率考慮下基準描画(TJAPlayer3.app.Device, TJAPlayer3.Skin.Game_DanC_X[count - 1] + offset - TJAPlayer3.Tx.DanC_ExamUnit.szテクスチャサイズ.Width, TJAPlayer3.Skin.Game_DanC_Y[count - 1] + TJAPlayer3.Skin.Game_DanC_Size[1] * (i + 1) + ((i + 1) * TJAPlayer3.Skin.Game_DanC_Padding) - TJAPlayer3.Skin.Game_DanC_Exam_Offset[1], new Rectangle(0, TJAPlayer3.Skin.Game_DanC_ExamUnit_Size[1] * 1, TJAPlayer3.Skin.Game_DanC_ExamUnit_Size[0], TJAPlayer3.Skin.Game_DanC_ExamUnit_Size[1]));
                        offset -= TJAPlayer3.Skin.Game_DanC_Percent_Hit_Score_Padding[1];

                        // 打
                        break;
                    default:
                        // 何もしない
                        break;
                }

                // 条件の数字
                DrawNumber(dan_C[i].Value[0], TJAPlayer3.Skin.Game_DanC_X[count - 1] + offset, TJAPlayer3.Skin.Game_DanC_Y[count - 1] + TJAPlayer3.Skin.Game_DanC_Size[1] * (i + 1) + ((i + 1) * TJAPlayer3.Skin.Game_DanC_Padding) - TJAPlayer3.Skin.Game_DanC_Exam_Offset[1], TJAPlayer3.Skin.Game_DanC_Number_Small_Padding, TJAPlayer3.Skin.Game_DanC_Number_Small_Scale, TJAPlayer3.Skin.Game_DanC_Number_Small_Scale);
                //offset -= CDTXMania.Skin.Game_DanC_Number_Small_Padding * (dan_C[i].Value[0].ToString().Length + 1);
                offset -= TJAPlayer3.Skin.Game_DanC_Number_Small_Padding * (dan_C[i].Value[0].ToString().Length);

                // 条件の種類
                TJAPlayer3.Tx.DanC_ExamType?.t2D拡大率考慮下基準描画(TJAPlayer3.app.Device, TJAPlayer3.Skin.Game_DanC_X[count - 1] + offset - TJAPlayer3.Tx.DanC_ExamType.szテクスチャサイズ.Width, TJAPlayer3.Skin.Game_DanC_Y[count - 1] + TJAPlayer3.Skin.Game_DanC_Size[1] * (i + 1) + ((i + 1) * TJAPlayer3.Skin.Game_DanC_Padding) - TJAPlayer3.Skin.Game_DanC_Exam_Offset[1], new Rectangle(0, TJAPlayer3.Skin.Game_DanC_ExamType_Size[1] * (int)dan_C[i].GetExamType(), TJAPlayer3.Skin.Game_DanC_ExamType_Size[0], TJAPlayer3.Skin.Game_DanC_ExamType_Size[1]));
                #endregion

                #region 条件達成失敗の画像を描画する。
                if (dan_C[i].GetReached())
                {
                    TJAPlayer3.Tx.DanC_Failed?.t2D描画(TJAPlayer3.app.Device, TJAPlayer3.Skin.Game_DanC_X[count - 1], TJAPlayer3.Skin.Game_DanC_Y[count - 1] + TJAPlayer3.Skin.Game_DanC_Size[1] * i + (i * TJAPlayer3.Skin.Game_DanC_Padding));
                }
                #endregion
            }
        }

        /// <summary>
        /// 段位チャレンジの数字フォントで数字を描画します。
        /// </summary>
        /// <param name="value">値。</param>
        /// <param name="x">一桁目のX座標。</param>
        /// <param name="y">一桁目のY座標</param>
        /// <param name="padding">桁数間の字間</param>
        /// <param name="scaleX">拡大率X</param>
        /// <param name="scaleY">拡大率Y</param>
        /// <param name="scaleJump">アニメーション用拡大率(Yに加算される)。</param>
        private static void DrawNumber(int value, int x, int y, int padding, float scaleX = 1.0f, float scaleY = 1.0f, float scaleJump = 0.0f)
        {
            var notesRemainDigit = 0;
            for (int i = value.ToString().Length; i > 0; i--)
            {
                if(TJAPlayer3.Tx.DanC_Number != null)
                {
                    TJAPlayer3.Tx.DanC_Number.vc拡大縮小倍率.X = scaleX;
                    TJAPlayer3.Tx.DanC_Number.vc拡大縮小倍率.Y = scaleY + scaleJump;

                    var number = Convert.ToInt32(value.ToString()[i - 1].ToString());
                    Rectangle rectangle = new Rectangle(TJAPlayer3.Skin.Game_DanC_Number_Size[0] * number - 1, 0, TJAPlayer3.Skin.Game_DanC_Number_Size[0], TJAPlayer3.Skin.Game_DanC_Number_Size[1]);
                    TJAPlayer3.Tx.DanC_Number.t2D拡大率考慮下中心基準描画(TJAPlayer3.app.Device, x - (notesRemainDigit * padding), y, rectangle);
                }
                notesRemainDigit++;
            }
        }

        /// <summary>
        /// n個の条件がひとつ以上達成失敗しているかどうかを返します。
        /// </summary>
        /// <returns>n個の条件がひとつ以上達成失敗しているか。</returns>
        public bool GetFailedAllChallenges()
        {
            var isFailed = false;
            for (int i = 0; i < this.ExamCount; i++)
            {
                if (Challenge[i].GetReached()) isFailed = true;
            }
            return isFailed;
        }

        /// <summary>
        /// n個の条件で段位認定モードのステータスを返します。
        /// </summary>
        /// <param name="dan_C">条件。</param>
        /// <returns>ExamStatus。</returns>
        public Exam.Status GetExamStatus(Dan_C[] dan_C)
        {
            var status = Exam.Status.Better_Success;
            var count = 0;
            for (int i = 0; i < 3; i++)
            {
                if (dan_C[i] != null && dan_C[i].GetEnable() == true)
                    count++;
            }
            for (int i = 0; i < count; i++)
            {
                if (!dan_C[i].GetCleared()[1]) status = Exam.Status.Success;
            }
            for (int i = 0; i < count; i++)
            {
                if (!dan_C[i].GetCleared()[0]) status = Exam.Status.Failure;
            }
            return status;
        }

        public Dan_C[] GetExam()
        {
            return Challenge;
        }


        private readonly float[] ScoreScale = new float[]
        {
            0.000f,
            0.111f, // リピート
            0.222f,
            0.185f,
            0.148f,
            0.129f,
            0.111f,
            0.074f,
            0.065f,
            0.033f,
            0.015f,
            0.000f
        };

        [StructLayout(LayoutKind.Sequential)]
        struct ChallengeStatus
        {
            public SlimDX.Color4 Color;
            public CCounter Timer_Gauge;
            public CCounter Timer_Amount;
            public CCounter Timer_Failed;
        }
        
        #region[ private ]
        //-----------------
        private int ExamCount;
        private ChallengeStatus[] Status = new ChallengeStatus[3];
        private CTexture Dan_Plate;
        private bool IsEnded;

        // アニメ関連
        private int NowShowingNumber;
        private CCounter Counter_In, Counter_Wait, Counter_Out, Counter_Text;
        private double[] ScreenPoint;
        private int Counter_In_Old, Counter_Out_Old, Counter_Text_Old;
        public bool IsAnimating;

        //音声関連
        private CSound Sound_Section;
        private CSound Sound_Failed;

        
        //-----------------
        #endregion
    }
}
