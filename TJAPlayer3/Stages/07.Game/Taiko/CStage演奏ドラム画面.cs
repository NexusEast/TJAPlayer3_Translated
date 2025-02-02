﻿using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Diagnostics;
using SlimDX;
using FDK;

namespace TJAPlayer3
{
	internal class CStage演奏ドラム画面 : CStage演奏画面共通
	{
		// コンストラクタ

		public CStage演奏ドラム画面()
		{
			base.eステージID = CStage.Eステージ.演奏;
			base.eフェーズID = CStage.Eフェーズ.共通_通常状態;
			base.bDeactivated = true;
			base.list子Activities.Add( this.actCombo = new CAct演奏Combo共通() );
			base.list子Activities.Add( this.actChipFireD = new CAct演奏DrumsチップファイアD() );
			base.list子Activities.Add( this.Rainbow = new Rainbow() );
            base.list子Activities.Add( this.actGauge = new CAct演奏Drumsゲージ() );
			base.list子Activities.Add( this.actJudgeString = new CAct演奏Drums判定文字列() );
			base.list子Activities.Add( this.actTaikoLaneFlash = new TaikoLaneFlash() );
			base.list子Activities.Add( this.actScore = new CAct演奏Drumsスコア() );
			base.list子Activities.Add( this.actStatusPanels = new CAct演奏Drumsステータスパネル() );
			base.list子Activities.Add( this.act譜面スクロール速度 = new CAct演奏スクロール速度() );
			base.list子Activities.Add( this.actAVI = new CAct演奏AVI() );
			base.list子Activities.Add( this.actPanel = new CAct演奏パネル文字列() );
			base.list子Activities.Add( this.actStageFailed = new CAct演奏ステージ失敗() );
			base.list子Activities.Add( this.actPlayInfo = new CAct演奏演奏情報() );
            base.list子Activities.Add( this.actFI = new CActFIFOStart() );
			base.list子Activities.Add( this.actFO = new CActFIFOBlack() );
			base.list子Activities.Add( this.actFOClear = new CActFIFOResult() );
            base.list子Activities.Add( this.actLane = new CAct演奏Drumsレーン() );
            base.list子Activities.Add( this.actEnd = new CAct演奏Drums演奏終了演出() );
            base.list子Activities.Add( this.actDancer = new CAct演奏DrumsDancer() );
            base.list子Activities.Add( this.actMtaiko = new CAct演奏DrumsMtaiko() );
            base.list子Activities.Add( this.actLaneTaiko = new CAct演奏Drumsレーン太鼓() );
            base.list子Activities.Add( this.actRoll = new CAct演奏Drums連打() );
            base.list子Activities.Add( this.actBalloon = new CAct演奏Drums風船() );
            base.list子Activities.Add( this.actChara = new CAct演奏Drumsキャラクター() );
            base.list子Activities.Add( this.actGame = new CAct演奏Drumsゲームモード() );
            base.list子Activities.Add( this.actBackground = new CAct演奏Drums背景() );
            base.list子Activities.Add( this.actRollChara = new CAct演奏Drums連打キャラ() );
            base.list子Activities.Add( this.actComboBalloon = new CAct演奏Drumsコンボ吹き出し() );
            base.list子Activities.Add( this.actComboVoice = new CAct演奏Combo音声() );
            base.list子Activities.Add( this.actPauseMenu = new CAct演奏PauseMenu() );
            base.list子Activities.Add(this.actChipEffects = new CAct演奏Drumsチップエフェクト());
            base.list子Activities.Add(this.actFooter = new CAct演奏DrumsFooter());
            base.list子Activities.Add(this.actRunner = new CAct演奏DrumsRunner());
            base.list子Activities.Add(this.actMob = new CAct演奏DrumsMob());
            base.list子Activities.Add(this.GoGoSplash = new GoGoSplash());
            base.list子Activities.Add(this.FlyingNotes = new FlyingNotes());
            base.list子Activities.Add(this.FireWorks = new FireWorks());
            base.list子Activities.Add(this.PuchiChara = new PuchiChara());

            base.list子Activities.Add(this.actDan = new Dan_Cert());
            #region[ 文字初期化 ]
			ST文字位置[] st文字位置Array = new ST文字位置[ 12 ];
			ST文字位置 st文字位置 = new ST文字位置();
			st文字位置.ch = '0';
			st文字位置.pt = new Point( 0, 0 );
			st文字位置Array[ 0 ] = st文字位置;
			ST文字位置 st文字位置2 = new ST文字位置();
			st文字位置2.ch = '1';
			st文字位置2.pt = new Point( 32, 0 );
			st文字位置Array[ 1 ] = st文字位置2;
			ST文字位置 st文字位置3 = new ST文字位置();
			st文字位置3.ch = '2';
			st文字位置3.pt = new Point( 64, 0 );
			st文字位置Array[ 2 ] = st文字位置3;
			ST文字位置 st文字位置4 = new ST文字位置();
			st文字位置4.ch = '3';
			st文字位置4.pt = new Point( 96, 0 );
			st文字位置Array[ 3 ] = st文字位置4;
			ST文字位置 st文字位置5 = new ST文字位置();
			st文字位置5.ch = '4';
			st文字位置5.pt = new Point( 128, 0 );
			st文字位置Array[ 4 ] = st文字位置5;
			ST文字位置 st文字位置6 = new ST文字位置();
			st文字位置6.ch = '5';
			st文字位置6.pt = new Point( 160, 0 );
			st文字位置Array[ 5 ] = st文字位置6;
			ST文字位置 st文字位置7 = new ST文字位置();
			st文字位置7.ch = '6';
			st文字位置7.pt = new Point( 192, 0 );
			st文字位置Array[ 6 ] = st文字位置7;
			ST文字位置 st文字位置8 = new ST文字位置();
			st文字位置8.ch = '7';
			st文字位置8.pt = new Point( 224, 0 );
			st文字位置Array[ 7 ] = st文字位置8;
			ST文字位置 st文字位置9 = new ST文字位置();
			st文字位置9.ch = '8';
			st文字位置9.pt = new Point( 256, 0 );
			st文字位置Array[ 8 ] = st文字位置9;
			ST文字位置 st文字位置10 = new ST文字位置();
			st文字位置10.ch = '9';
			st文字位置10.pt = new Point( 288, 0 );
			st文字位置Array[ 9 ] = st文字位置10;
			ST文字位置 st文字位置11 = new ST文字位置();
			st文字位置11.ch = '%';
			st文字位置11.pt = new Point( 320, 0 );
			st文字位置Array[ 10 ] = st文字位置11;
			ST文字位置 st文字位置12 = new ST文字位置();
			st文字位置12.ch = ' ';
			st文字位置12.pt = new Point( 0, 0 );
			st文字位置Array[ 11 ] = st文字位置12;
			this.st小文字位置 = st文字位置Array;

			st文字位置Array = new ST文字位置[ 12 ];
		    st文字位置 = new ST文字位置();
			st文字位置.ch = '0';
			st文字位置.pt = new Point( 0, 0 );
			st文字位置Array[ 0 ] = st文字位置;
			st文字位置2 = new ST文字位置();
			st文字位置2.ch = '1';
			st文字位置2.pt = new Point( 32, 0 );
			st文字位置Array[ 1 ] = st文字位置2;
			st文字位置3 = new ST文字位置();
			st文字位置3.ch = '2';
			st文字位置3.pt = new Point( 64, 0 );
			st文字位置Array[ 2 ] = st文字位置3;
			st文字位置4 = new ST文字位置();
			st文字位置4.ch = '3';
			st文字位置4.pt = new Point( 96, 0 );
			st文字位置Array[ 3 ] = st文字位置4;
			st文字位置5 = new ST文字位置();
			st文字位置5.ch = '4';
			st文字位置5.pt = new Point( 128, 0 );
			st文字位置Array[ 4 ] = st文字位置5;
			st文字位置6 = new ST文字位置();
			st文字位置6.ch = '5';
			st文字位置6.pt = new Point( 160, 0 );
			st文字位置Array[ 5 ] = st文字位置6;
			st文字位置7 = new ST文字位置();
			st文字位置7.ch = '6';
			st文字位置7.pt = new Point( 192, 0 );
			st文字位置Array[ 6 ] = st文字位置7;
			st文字位置8 = new ST文字位置();
			st文字位置8.ch = '7';
			st文字位置8.pt = new Point( 224, 0 );
			st文字位置Array[ 7 ] = st文字位置8;
			st文字位置9 = new ST文字位置();
			st文字位置9.ch = '8';
			st文字位置9.pt = new Point( 256, 0 );
			st文字位置Array[ 8 ] = st文字位置9;
			st文字位置10 = new ST文字位置();
			st文字位置10.ch = '9';
			st文字位置10.pt = new Point( 288, 0 );
			st文字位置Array[ 9 ] = st文字位置10;
			st文字位置11 = new ST文字位置();
			st文字位置11.ch = '%';
			st文字位置11.pt = new Point( 320, 0 );
			st文字位置Array[ 10 ] = st文字位置11;
			st文字位置12 = new ST文字位置();
			st文字位置12.ch = ' ';
			st文字位置12.pt = new Point( 0, 0 );
			st文字位置Array[ 11 ] = st文字位置12;
			this.st小文字位置 = st文字位置Array;
            #endregion
        }


		// メソッド

		public void t演奏結果を格納する( out CScoreIni.C演奏記録 Drums )
		{
			base.t演奏結果を格納する_ドラム( out Drums );
		}


		// CStage 実装

		public override void On活性化()
		{
            LoudnessMetadataScanner.StopBackgroundScanning(joinImmediately: false);

            this.actGame.t叩ききりまショー_初期化();
            base.ReSetScore(TJAPlayer3.DTX.nScoreInit[0, TJAPlayer3.stage選曲.n確定された曲の難易度], TJAPlayer3.DTX.nScoreDiff[TJAPlayer3.stage選曲.n確定された曲の難易度]);
			base.On活性化();

			dtLastQueueOperation = DateTime.MinValue;

            double dbPtn_Normal = (60.0 / TJAPlayer3.stage演奏ドラム画面.actPlayInfo.dbBPM) * TJAPlayer3.Skin.Game_Chara_Beat_Normal / this.actChara.arモーション番号.Length;
            double dbPtn_Clear = (60.0 / TJAPlayer3.stage演奏ドラム画面.actPlayInfo.dbBPM) * TJAPlayer3.Skin.Game_Chara_Beat_Clear / this.actChara.arクリアモーション番号.Length;
            double dbPtn_GoGo = (60.0 / TJAPlayer3.stage演奏ドラム画面.actPlayInfo.dbBPM) * TJAPlayer3.Skin.Game_Chara_Beat_GoGo / this.actChara.arゴーゴーモーション番号.Length;

            PuchiChara.ChangeBPM(60.0 / TJAPlayer3.stage演奏ドラム画面.actPlayInfo.dbBPM);

            if(TJAPlayer3.Skin.Game_Chara_Ptn_Normal != 0 )
            {
                this.actChara.ctChara_Normal = new CCounter( 0, this.actChara.arモーション番号.Length - 1, dbPtn_Normal, CSoundManager.rPlaybackTimer );
            } else
            {
                this.actChara.ctChara_Normal = new CCounter();
            }
            if (TJAPlayer3.Skin.Game_Chara_Ptn_Clear != 0 )
            {
                this.actChara.ctChara_Clear = new CCounter( 0, this.actChara.arクリアモーション番号.Length - 1, dbPtn_Clear, CSoundManager.rPlaybackTimer );
            } else
            {
                this.actChara.ctChara_Clear = new CCounter();
            }
            if( TJAPlayer3.Skin.Game_Chara_Ptn_GoGo != 0 )
            {
                this.actChara.ctChara_GoGo = new CCounter( 0, this.actChara.arゴーゴーモーション番号.Length - 1, dbPtn_GoGo, CSoundManager.rPlaybackTimer );
            } else
            {
                this.actChara.ctChara_GoGo = new CCounter();
            }

            if(this.actDancer.ct踊り子モーション != null)
            {
                double dbUnit_dancer = (((60 / (TJAPlayer3.stage演奏ドラム画面.actPlayInfo.dbBPM))) / this.actDancer.ar踊り子モーション番号.Length);
                this.actDancer.ct踊り子モーション = new CCounter(0, this.actDancer.ar踊り子モーション番号.Length - 1, dbUnit_dancer * TJAPlayer3.Skin.Game_Dancer_Beat, CSoundManager.rPlaybackTimer);
            }else
            {
                this.actDancer.ct踊り子モーション = new CCounter();
            }

            this.ct手つなぎ = new CCounter( 0, 60, 20, TJAPlayer3.Timer );

            // Discord Presence の更新
            var endTimeStamp = TJAPlayer3.DTX.listChip.Count == 0
                ? 0
                : Discord.GetUnixTime() + (long)TJAPlayer3.DTX.listChip[TJAPlayer3.DTX.listChip.Count - 1].nNoiseTimems / 1000;
            var difficultyName = TJAPlayer3.DifficultyNumberToEnum(TJAPlayer3.stage選曲.n確定された曲の難易度).ToString();
            Discord.UpdatePresence(TJAPlayer3.ConfigIni.SendDiscordPlayingInformation ? TJAPlayer3.DTX.TITLE + ".tja" : "",
                Properties.Discord.Stage_InGame + (TJAPlayer3.ConfigIni.b太鼓パートAutoPlay == true ? " (" + Properties.Discord.Info_IsAuto + ")" : ""),
                0,
                endTimeStamp,
                TJAPlayer3.ConfigIni.SendDiscordPlayingInformation ? difficultyName.ToLower() : "",
                TJAPlayer3.ConfigIni.SendDiscordPlayingInformation ? String.Format("COURSE:{0} ({1})", difficultyName, TJAPlayer3.stage選曲.n確定された曲の難易度) : "");
        }
		public override void On非活性化()
		{
            this.ct手つなぎ = null;
			base.On非活性化();

            LoudnessMetadataScanner.StartBackgroundScanning();
		}
		public override void OnManagedResourceLoaded()
		{
			if( !base.bDeactivated )
			{
			    // When performing calibration, reduce audio distraction from user input.
			    // For users who play primarily by listening to the music,
			    // you might think that we want them to hear drum sound effects during
			    // calibration, but we do not. Humans are remarkably good at adjusting
			    // the timing of their own physical movement, even without realizing it.
			    // We are calibrating their input timing for the purposes of judgment.
			    // We do not want them subconsciously playing early so as to line up
			    // their drum sound effects with the sounds of the input calibration file.
			    // Instead, we want them focused on the sounds of their keyboard, tatacon,
			    // other controller, etc. and the sounds of the input calibration audio file.
			    if (!TJAPlayer3.IsPerformingCalibration)
			    {
			        this.soundRed = TJAPlayer3.Sound管理.tサウンドを生成する( CSkin.Path( @"Sounds\Taiko\dong.ogg" ), ESoundGroup.SoundEffect );
			        this.soundBlue = TJAPlayer3.Sound管理.tサウンドを生成する( CSkin.Path( @"Sounds\Taiko\ka.ogg" ), ESoundGroup.SoundEffect );
			        this.soundAdlib = TJAPlayer3.Sound管理.tサウンドを生成する( CSkin.Path(@"Sounds\Taiko\Adlib.ogg"), ESoundGroup.SoundEffect );
			    }

			    base.OnManagedResourceLoaded();
			}
		}
		public override void OnManagedDisposed()
		{
			if( !base.bDeactivated )
			{
                if( this.soundRed != null )
                    this.soundRed.t解放する();
                if( this.soundBlue != null )
                    this.soundBlue.t解放する();
                if( this.soundAdlib != null )
                    this.soundAdlib.t解放する();
				base.OnManagedDisposed();
			}
		}
		public override int OnDraw()
		{
			base.sw.Start();
			if( !base.bDeactivated )
			{
				bool bIsFinishedPlaying = false;
                bool bIsFinishedEndAnime = false;
				bool bIsFinishedFadeout = false;
				#region [ 初めての進行描画 ]
				if ( base.b初めての進行描画 )
				{
                    CSoundManager.rPlaybackTimer.tリセット();
					TJAPlayer3.Timer.tリセット();
					this.ctチップ模様アニメ.Drums = new CCounter( 0, 1, 500, TJAPlayer3.Timer );
					this.ctチップ模様アニメ.Guitar = new CCounter( 0, 0x17, 20, TJAPlayer3.Timer );
					this.ctチップ模様アニメ.Bass = new CCounter( 0, 0x17, 20, TJAPlayer3.Timer );
					this.ctチップ模様アニメ.Taiko = new CCounter( 0, 1, 500, TJAPlayer3.Timer );

					base.eフェーズID = CStage.Eフェーズ.共通_フェードイン;
					this.actFI.tフェードイン開始();

					base.b初めての進行描画 = false;
				}
				#endregion
				if ( ( ( TJAPlayer3.ConfigIni.nRisky != 0 && this.actGauge.IsFailed( EInstrumentPart.TAIKO ) ) || this.actGame.st叩ききりまショー.ct残り時間.bEnded ) && ( base.eフェーズID == CStage.Eフェーズ.共通_通常状態 ) )
				{
					this.actStageFailed.Start();
					TJAPlayer3.DTX.t全チップの再生停止();
					base.eフェーズID = CStage.Eフェーズ.演奏_STAGE_FAILED;
				}

                //ハードゲージの閉店
                for (int i = 0; i < TJAPlayer3.ConfigIni.nPlayerCount; i++)
                {
                    if ((TJAPlayer3.ConfigIni.eGaugeMode == EGaugeMode.Hard || TJAPlayer3.ConfigIni.eGaugeMode == EGaugeMode.ExHard) && (base.eフェーズID == CStage.Eフェーズ.共通_通常状態))
                    {
                        if (this.actGauge.db現在のゲージ値[i] <= 0.0f)
                        {
                            TJAPlayer3.DTX.t全チップの再生停止();
                            this.eフェードアウト完了時の戻り値 = E演奏画面の戻り値.ステージ失敗_ハード;
                            base.eフェーズID = CStage.Eフェーズ.演奏_STAGE_FAILED_ハード;
                            this.actFOClear.tフェードアウト開始();
                        }
                    }
                }

                if ( !String.IsNullOrEmpty( TJAPlayer3.DTX.strBGIMAGE_PATH ) || ( TJAPlayer3.DTX.listAVI.Count == 0 ) ) //背景動画があったら背景画像を描画しない。
                {
				    this.t進行描画_背景();
                }

                if (TJAPlayer3.ConfigIni.bAVIEnabled && TJAPlayer3.DTX.listAVI.Count > 0)
                {
                    this.t進行描画_AVI();
                }
                else if (TJAPlayer3.ConfigIni.bBGAEnabled)
                {
                    actBackground.OnDraw();
                }

                if (!TJAPlayer3.ConfigIni.bAVIEnabled)
                {
                    actRollChara.OnDraw();
                }

                if (!bDoublePlay && TJAPlayer3.ConfigIni.ShowDancer)
                {
                    actDancer.OnDraw();
                }

                if(!bDoublePlay && TJAPlayer3.ConfigIni.ShowFooter)
                    this.actFooter.OnDraw();

                if( TJAPlayer3.ConfigIni.ShowChara )
                    this.actChara.OnDraw();

                if(TJAPlayer3.ConfigIni.ShowMob)
                    this.actMob.OnDraw();

                if ( TJAPlayer3.ConfigIni.eGameMode != EGame.OFF )
                    this.actGame.OnDraw();

				this.t進行描画_譜面スクロール速度();
				this.t進行描画_チップアニメ();

                if(TJAPlayer3.ConfigIni.ShowRunner)
                    this.actRunner.OnDraw();


                if (!TJAPlayer3.ConfigIni.bNoInfo)
                    this.t進行描画_パネル文字列();

                this.actLaneTaiko.OnDraw();

                if( ( TJAPlayer3.ConfigIni.eClipDispType == EClipDispType.ウィンドウのみ || TJAPlayer3.ConfigIni.eClipDispType == EClipDispType.両方 ) && TJAPlayer3.ConfigIni.nPlayerCount == 1 )
                    this.actAVI.t窓表示();

				if( !TJAPlayer3.ConfigIni.bNoInfo )
                    this.t進行描画_ゲージ();

                this.actLaneTaiko.ゴーゴー炎();


                for ( int i = 0; i < TJAPlayer3.ConfigIni.nPlayerCount; i++ )
                {
				    bIsFinishedPlaying = this.t進行描画_チップ( i );
                    this.t進行描画_チップ_連打( EInstrumentPart.DRUMS, i );
                }

                this.actDan.OnDraw();

                this.actMtaiko.OnDraw();

                this.GoGoSplash.OnDraw();
                this.t進行描画_リアルタイム判定数表示();

                if ( !TJAPlayer3.ConfigIni.bNoInfo )
			        this.t進行描画_コンボ();
                if( !TJAPlayer3.ConfigIni.bNoInfo )
				    this.t進行描画_スコア();


                this.Rainbow.OnDraw();
                this.FireWorks.OnDraw();
                this.actChipEffects.OnDraw();
                this.FlyingNotes.OnDraw();
                this.t進行描画_チップファイアD();

                this.actComboBalloon.OnDraw();

                for ( int i = 0; i < TJAPlayer3.ConfigIni.nPlayerCount; i++ )
                {
                    this.actRoll.On進行描画( this.n現在の連打数[ i ], i );
                }


                if( !TJAPlayer3.ConfigIni.bNoInfo )
                    this.t進行描画_判定文字列1_通常位置指定の場合();

                this.t進行描画_演奏情報();
                this.actPanel.t歌詞テクスチャを描画する();
                actChara.OnDraw_Balloon();
                this.t全体制御メソッド();

                
                this.actPauseMenu.t進行描画();
				this.t進行描画_STAGEFAILED();

               

                bIsFinishedEndAnime = this.actEnd.OnDraw() == 1 ? true : false;
				bIsFinishedFadeout = this.t進行描画_フェードイン_アウト();

                //演奏終了→演出表示→フェードアウト
                if( bIsFinishedPlaying && base.eフェーズID == CStage.Eフェーズ.共通_通常状態 )
                {
                    base.eフェーズID = CStage.Eフェーズ.演奏_演奏終了演出;
                    this.actEnd.Start();
                    if (TJAPlayer3.Skin.Game_Chara_Ptn_10combo_Max != 0)
                    {
                        if (TJAPlayer3.stage演奏ドラム画面.actGauge.db現在のゲージ値[0] >= 100)
                        {
                            double dbUnit = (((60.0 / (TJAPlayer3.stage演奏ドラム画面.actPlayInfo.dbBPM))));
                            this.actChara.アクションタイマーリセット();
                            this.actChara.ctキャラクターアクション_10コンボMAX = new CCounter(0, TJAPlayer3.Skin.Game_Chara_Ptn_10combo_Max - 1, (dbUnit / TJAPlayer3.Skin.Game_Chara_Ptn_10combo_Max) * 2, CSoundManager.rPlaybackTimer);
                            this.actChara.ctキャラクターアクション_10コンボMAX.tStartdb();
                            this.actChara.ctキャラクターアクション_10コンボMAX.db現在の値 = 0D;
                            this.actChara.bマイどんアクション中 = true;
                        }
                    }
                }
                else if( bIsFinishedEndAnime && base.eフェーズID == Eフェーズ.演奏_演奏終了演出 )
                {
                    this.eフェードアウト完了時の戻り値 = E演奏画面の戻り値.ステージクリア;
                    base.eフェーズID = CStage.Eフェーズ.演奏_STAGE_CLEAR_フェードアウト;
                    this.actFOClear.tフェードアウト開始();
                }

				if( bIsFinishedFadeout )
				{
					Debug.WriteLine( "Total OnDraw=" + sw.ElapsedMilliseconds + "ms" );
					return (int) this.eフェードアウト完了時の戻り値;
				}

				ManageMixerQueue();

				// キー入力

				this.tキー入力();
            }
            base.sw.Stop();
			return 0;
		}

		// その他

		#region [ private ]
		//-----------------
		[StructLayout( LayoutKind.Sequential )]
		private struct ST文字位置
		{
			public char ch;
			public Point pt;
		}
		public CAct演奏DrumsチップファイアD actChipFireD;

        public CAct演奏Drumsレーン actLane;
        public CAct演奏DrumsMtaiko actMtaiko;
        public CAct演奏Drumsレーン太鼓 actLaneTaiko;
        public CAct演奏Drums演奏終了演出 actEnd;
        private CAct演奏Drumsゲームモード actGame;
        public CAct演奏Drums背景 actBackground;
        public GoGoSplash GoGoSplash;
        public FlyingNotes FlyingNotes;
        public FireWorks FireWorks;
        public PuchiChara PuchiChara;
        private CCounter ct手つなぎ;

        public float nGauge = 0.0f;

        private readonly ST文字位置[] st小文字位置;
		//-----------------

		private void tドラムヒット処理( long nHitTime, Eパッド type, CDTX.CChip pChip, bool b両手入力, int nPlayer )
		{
            if( pChip == null )
            {
                return;
            }

            int nInput = 0;

            switch( type )
            {
                case Eパッド.LRed:
                case Eパッド.RRed:
                case Eパッド.LRed2P:
                case Eパッド.RRed2P:
                    nInput = 0;
                    if( b両手入力 )
                        nInput = 2;
                    break;
                case Eパッド.LBlue:
                case Eパッド.RBlue:
                case Eパッド.LBlue2P:
                case Eパッド.RBlue2P:
                    nInput = 1;
                    if( b両手入力 )
                        nInput = 3;
                    break;
            }

            int index = pChip.nChannelNumber;
			if ( ( index >= 0x11 ) && ( index <= 0x12 ) )
			{
			}
			else if ( ( index >= 0x13 ) && ( index <= 0x14 ) )
			{
			}
			else if ( ( index >= 0x1A ) && ( index <= 0x1B ) )
			{
			}
            else if( index == 0x1F )
            {
            }
            else if( index >= 0x15 && index <= 0x17 )
            {
			    this.tチップのヒット処理( nHitTime, pChip, EInstrumentPart.TAIKO, true, nInput, nPlayer );
                return;
            }
            else
            {
                return;
            }

			E判定 e判定 = this.e指定時刻からChipのJUDGEを返す( nHitTime, pChip );
            this.actGame.t叩ききりまショー_判定から各数値を増加させる( e判定, (int)( nHitTime - pChip.nNoiseTimems ) );
			if( e判定 == E判定.Miss )
			{
				return;
			}
			this.tチップのヒット処理( nHitTime, pChip, EInstrumentPart.TAIKO, true, nInput, nPlayer );
			if( ( e判定 != E判定.Poor ) && ( e判定 != E判定.Miss ) )
			{
                TJAPlayer3.stage演奏ドラム画面.actLaneTaiko.Start( pChip.nChannelNumber, e判定, b両手入力, nPlayer );

                int nFly;
                switch(pChip.nChannelNumber)
                {
                    case 0x11:
                        nFly = 1;
                        break;
                    case 0x12:
                        nFly = 2;
                        break;
                    case 0x13:
                    case 0x1A:
                        nFly = b両手入力 ? 3 : 1;
                        break;
                    case 0x14:
                    case 0x1B:
                        nFly = b両手入力 ? 4 : 2;
                        break;
                    case 0x1F:
                        nFly = nInput == 0 ? 1 : 2;
                        break;
                    default:
                        nFly = 1;
                        break;
                }

                this.actTaikoLaneFlash.PlayerLane[nPlayer].Start(PlayerLane.FlashType.Hit);
                this.FlyingNotes.Start(nFly, nPlayer);
			}
		}

		protected override void ドラムスクロール速度アップ()
		{
			TJAPlayer3.ConfigIni.n譜面スクロール速度.Drums = Math.Min( TJAPlayer3.ConfigIni.n譜面スクロール速度.Drums + 1, 1999 );
		}
		protected override void ドラムスクロール速度ダウン()
		{
			TJAPlayer3.ConfigIni.n譜面スクロール速度.Drums = Math.Max( TJAPlayer3.ConfigIni.n譜面スクロール速度.Drums - 1, 0 );
		}

	
		protected override void t進行描画_AVI()
		{
			base.t進行描画_AVI( 0, 0 );
		}

		private void t進行描画_チップファイアD()
		{
			this.actChipFireD.OnDraw();
		}

		protected override void t進行描画_演奏情報()
		{
			base.t進行描画_演奏情報( 1000, 257 );
		}

		protected override void t入力処理_ドラム()
		{
		    var nInputAdjustTimeMs = TJAPlayer3.ConfigIni.nInputAdjustTimeMs;

			for( int nPad = 0; nPad < (int) Eパッド.MAX; nPad++ )		// #27029 2012.1.4 from: <10 to <=10; Eパッドの要素が１つ（HP）増えたため。
																		//		  2012.1.5 yyagi: (int)Eパッド.MAX に変更。Eパッドの要素数への依存を無くすため。
			{
				List<STInputEvent> listInputEvent = TJAPlayer3.Pad.GetEvents( EInstrumentPart.DRUMS, (Eパッド) nPad );

				if( ( listInputEvent == null ) || ( listInputEvent.Count == 0 ) )
					continue;

				this.t入力メソッド記憶( EInstrumentPart.DRUMS );

				foreach( STInputEvent inputEvent in listInputEvent )
				{
					if( !inputEvent.b押された )
						continue;

					long nTime = inputEvent.nTimeStamp + nInputAdjustTimeMs - CSoundManager.rPlaybackTimer.n前回リセットした時のシステム時刻;

					bool bHitted = false;

                    int nLane = 0;
                    int nHand = 0;
                    int nChannel = 0;

                    //連打チップを検索してから通常音符検索
                    //連打チップの検索は、
                    //一番近くの連打音符を探す→時刻チェック
                    //発声 < 現在時刻 && 終わり > 現在時刻

                    //2015.03.19 kairera0467 Chipを1つにまとめて1つのレーン扱いにする。
                    int nUsePlayer = 0;
                    if( nPad >= 12 && nPad <= 15 ) {
                        nUsePlayer = 0;
                    } else if( nPad >= 16 && nPad <= 19 ) {
                        nUsePlayer = 1;
                        if( TJAPlayer3.ConfigIni.nPlayerCount < 2 ) //プレイ人数が2人以上でなければ入力をキャンセル
                            break;
                    }

                    var padTo = nUsePlayer == 0 ? nPad - 12 : nPad - 12 - 4;
                    var isDon = padTo < 2 ? true : false;

                    CDTX.CChip chipNoHit = chip現在処理中の連打チップ[nUsePlayer] == null ? GetChipOfNearest( nTime, nUsePlayer, isDon) : GetChipOfNearest(nTime, nUsePlayer);
                    E判定 e判定 = ( chipNoHit != null ) ? this.e指定時刻からChipのJUDGEを返す( nTime, chipNoHit ) : E判定.Miss;

                    bool b太鼓音再生フラグ = true;
                    if( chipNoHit != null )
                    {
                        if( chipNoHit.nChannelNumber == 0x1F && ( e判定 == E判定.Perfect || e判定 == E判定.Good ) )
                            b太鼓音再生フラグ = false;

                        if( chipNoHit.nChannelNumber == 0x1F && ( e判定 != E判定.Miss && e判定 != E判定.Poor ) )
                            this.soundAdlib?.t再生を開始する();
                    }

                    switch (nPad)
                    {
                        case 12:
                            nLane = 0;
                            nHand = 0;
                            nChannel = 0x11;
                            if( b太鼓音再生フラグ )
                            {
                                this.soundRed?.t再生を開始する();
                            }
                            break;
                        case 13:
                            nLane = 0;
                            nHand = 1;
                            nChannel = 0x11;
                            if( b太鼓音再生フラグ )
                            {
                                this.soundRed?.t再生を開始する();
                            }
                            break;
                        case 14:
                            nLane = 1;
                            nHand = 0;
                            nChannel = 0x12;
                            if( b太鼓音再生フラグ )
                                this.soundBlue?.t再生を開始する();
                            break;
                        case 15:
                            nLane = 1;
                            nHand = 1;
                            nChannel = 0x12;
                            if( b太鼓音再生フラグ )
                                this.soundBlue?.t再生を開始する();
                            break;
                        //以下2P
                        case 16:
                            nLane = 0;
                            nHand = 0;
                            nChannel = 0x11;
                            if( b太鼓音再生フラグ )
                            {
                                this.soundRed?.t再生を開始する();
                            }
                            break;
                        case 17:
                            nLane = 0;
                            nHand = 1;
                            nChannel = 0x11;
                            if( b太鼓音再生フラグ )
                            {
                                this.soundRed?.t再生を開始する();
                            }
                            break;
                        case 18:
                            nLane = 1;
                            nHand = 0;
                            nChannel = 0x12;
                            if( b太鼓音再生フラグ )
                                this.soundBlue?.t再生を開始する();
                            break;
                        case 19:
                            nLane = 1;
                            nHand = 1;
                            nChannel = 0x12;
                            if( b太鼓音再生フラグ )
                                this.soundBlue?.t再生を開始する();
                            break;
                    }

                    TJAPlayer3.stage演奏ドラム画面.actTaikoLaneFlash.PlayerLane[nUsePlayer].Start((PlayerLane.FlashType)nLane);
                    TJAPlayer3.stage演奏ドラム画面.actMtaiko.tMtaikoEvent(nChannel, nHand, nUsePlayer );

                    if( this.b連打中[ nUsePlayer ] )
                    {
                        chipNoHit = this.chip現在処理中の連打チップ[ nUsePlayer ];
                        e判定 = E判定.Perfect;
                    }



                    if( chipNoHit == null )
                    {
                        break;
                    }

					#region [ (A) ヒットしていればヒット処理して次の inputEvent へ ]
					//-----------------------------
					switch( ( (Eパッド) nPad ) )
					{
                        case Eパッド.LRed:
                        case Eパッド.LRed2P:
                            #region[ 面のヒット処理 ]
                            //-----------------------------
                            {
								if( e判定 != E判定.Miss && chipNoHit.nChannelNumber == 0x11 )
								{
									this.tドラムヒット処理( nTime, Eパッド.LRed, chipNoHit, false, nUsePlayer );
									bHitted = true;
								}
                                if( e判定 != E判定.Miss && ( chipNoHit.nChannelNumber == 0x13 || chipNoHit.nChannelNumber == 0x1A ) && !TJAPlayer3.ConfigIni.b大音符判定 )
								{
                                    this.tドラムヒット処理( nTime, Eパッド.LRed, chipNoHit, true, nUsePlayer );
                                    bHitted = true;
                                    this.nWaitButton = 0;
                                    break;
                                }
                                if( e判定 != E判定.Miss && ( chipNoHit.nChannelNumber == 0x13 || chipNoHit.nChannelNumber == 0x1A ) && TJAPlayer3.ConfigIni.b大音符判定 )
								{
                                    if( chipNoHit.eNoteState == ENoteState.none )
                                    {
                                        float time = chipNoHit.nNoiseTimems - CSoundManager.rPlaybackTimer.n現在時刻ms;
                                        if (time <= 110)
                                        {
                                            chipNoHit.nProcessTime = (int)CSoundManager.rPlaybackTimer.n現在時刻ms;
                                            chipNoHit.eNoteState = ENoteState.wait;
                                            this.nWaitButton = 2;
                                        }
                                    }
                                    else if (chipNoHit.eNoteState == ENoteState.wait)
                                    {
                                        float time = chipNoHit.nNoiseTimems - CSoundManager.rPlaybackTimer.n現在時刻ms;
                                        int nWaitTime = TJAPlayer3.ConfigIni.n両手判定の待ち時間;
                                        if( this.nWaitButton == 1 && time <= 110 && chipNoHit.nProcessTime + nWaitTime > (int)CSoundManager.rPlaybackTimer.n現在時刻ms )
                                        {
                                            this.tドラムヒット処理( nTime, Eパッド.LRed, chipNoHit, true, nUsePlayer );
                                            bHitted = true;
                                            this.nWaitButton = 0;
                                        }
                                        else if (this.nWaitButton == 2 && time <= 110 && chipNoHit.nProcessTime + nWaitTime < (int)CSoundManager.rPlaybackTimer.n現在時刻ms)
                                        {
                                            this.tドラムヒット処理(nTime, Eパッド.LRed, chipNoHit, false, nUsePlayer );
                                            bHitted = true;
                                            this.nWaitButton = 0;
                                        }
                                    }
								}
                                if( e判定 != E判定.Miss && ( chipNoHit.nChannelNumber == 0x15 || chipNoHit.nChannelNumber == 0x16 || chipNoHit.nChannelNumber == 0x17 ) )
                                {
						            this.tドラムヒット処理( nTime, Eパッド.LRed, chipNoHit, false, nUsePlayer );
                                }

                                if( !bHitted )
                                    break;
                                continue;
                            }
                            //-----------------------------
                            #endregion

                        case Eパッド.RRed:
                        case Eパッド.RRed2P:
                            #region[ 面のヒット処理 ]
                            //-----------------------------
                            {
                                if( e判定 != E判定.Miss && chipNoHit.nChannelNumber == 0x11 )
								{
									this.tドラムヒット処理( nTime, Eパッド.RRed, chipNoHit, false, nUsePlayer );
									bHitted = true;
								}
                                if( e判定 != E判定.Miss && ( chipNoHit.nChannelNumber == 0x13 || chipNoHit.nChannelNumber == 0x1A ) && !TJAPlayer3.ConfigIni.b大音符判定 )
								{
                                    this.tドラムヒット処理( nTime, Eパッド.RRed, chipNoHit, true, nUsePlayer );
                                    bHitted = true;
                                    this.nWaitButton = 0;
                                    break;
                                }
                                if( e判定 != E判定.Miss && ( chipNoHit.nChannelNumber == 0x13 || chipNoHit.nChannelNumber == 0x1A ) && TJAPlayer3.ConfigIni.b大音符判定 )
                                {
                                    if( chipNoHit.eNoteState == ENoteState.none )
                                    {
                                        float time = chipNoHit.nNoiseTimems - CSoundManager.rPlaybackTimer.n現在時刻ms;
                                        if( time <= 110 )
                                        {
                                            chipNoHit.nProcessTime = (int)CSoundManager.rPlaybackTimer.n現在時刻ms;
                                            chipNoHit.eNoteState = ENoteState.wait;
                                            this.nWaitButton = 1;
                                        }
                                    }
                                    else if( chipNoHit.eNoteState == ENoteState.wait )
                                    {
                                        float time = chipNoHit.nNoiseTimems - CSoundManager.rPlaybackTimer.n現在時刻ms;
                                        int nWaitTime = TJAPlayer3.ConfigIni.n両手判定の待ち時間;
                                        if( this.nWaitButton == 2 && time <= 110 && chipNoHit.nProcessTime + nWaitTime > (int)CSoundManager.rPlaybackTimer.n現在時刻ms )
                                        {
                                            this.tドラムヒット処理( nTime, Eパッド.RRed, chipNoHit, true, nUsePlayer );
                                            bHitted = true;
                                            this.nWaitButton = 0;
                                            break;
                                        }
                                        else if( this.nWaitButton == 2 && time <= 110 && chipNoHit.nProcessTime + nWaitTime < (int)CSoundManager.rPlaybackTimer.n現在時刻ms )
                                        {
                                            this.tドラムヒット処理( nTime, Eパッド.RRed, chipNoHit, false, nUsePlayer );
                                            bHitted = true;
                                            this.nWaitButton = 0;
                                        }
                                    }
								}
                                if( e判定 != E判定.Miss && ( chipNoHit.nChannelNumber == 0x15 || chipNoHit.nChannelNumber == 0x16 || chipNoHit.nChannelNumber == 0x17 ) )
                                {
						            this.tドラムヒット処理( nTime, Eパッド.RRed, chipNoHit, false, nUsePlayer );
                                }

                                if( !bHitted )
                                    break;

                                continue;
                            }
                            //-----------------------------
                            #endregion

                        case Eパッド.LBlue:
                        case Eパッド.LBlue2P:
                            #region[ ふちのヒット処理 ]
                            //-----------------------------
                            {
								if( e判定 != E判定.Miss && chipNoHit.nChannelNumber == 0x12 )
								{
									this.tドラムヒット処理( nTime, Eパッド.LBlue, chipNoHit, false, nUsePlayer );
									bHitted = true;
								}
                                if( e判定 != E判定.Miss && ( chipNoHit.nChannelNumber == 0x14 || chipNoHit.nChannelNumber == 0x1B ) && !TJAPlayer3.ConfigIni.b大音符判定 )
								{
                                    this.tドラムヒット処理(nTime, Eパッド.LBlue, chipNoHit, true, nUsePlayer );
                                    bHitted = true;
                                    this.nWaitButton = 0;
                                    break;
                                }
                                if( e判定 != E判定.Miss && ( chipNoHit.nChannelNumber == 0x14 || chipNoHit.nChannelNumber == 0x1B ) && TJAPlayer3.ConfigIni.b大音符判定 )
								{
                                    if( chipNoHit.eNoteState == ENoteState.none )
                                    {
                                        float time = chipNoHit.nNoiseTimems - CSoundManager.rPlaybackTimer.n現在時刻ms;
                                        if( time <= 110 )
                                        {
                                            chipNoHit.nProcessTime = (int)CSoundManager.rPlaybackTimer.n現在時刻ms;
                                            chipNoHit.eNoteState = ENoteState.wait;
                                            this.nWaitButton = 2;
                                        }
                                    }
                                    else if( chipNoHit.eNoteState == ENoteState.wait )
                                    {
                                        float time = chipNoHit.nNoiseTimems - CSoundManager.rPlaybackTimer.n現在時刻ms;
                                        int nWaitTime = TJAPlayer3.ConfigIni.n両手判定の待ち時間;
                                        if( this.nWaitButton == 1 && time <= 110 && chipNoHit.nProcessTime + nWaitTime > (int)CSoundManager.rPlaybackTimer.n現在時刻ms )
                                        {
                                            this.tドラムヒット処理( nTime, Eパッド.LBlue, chipNoHit, true, nUsePlayer );
                                            bHitted = true;
                                            this.nWaitButton = 0;
                                        }
                                        else if( this.nWaitButton == 2 && time <= 110 && chipNoHit.nProcessTime + nWaitTime < (int)CSoundManager.rPlaybackTimer.n現在時刻ms )
                                        {
                                            this.tドラムヒット処理( nTime, Eパッド.LBlue, chipNoHit, false, nUsePlayer );
                                            bHitted = true;
                                            this.nWaitButton = 0;
                                        }
                                    }
								}
                                if( e判定 != E判定.Miss && ( chipNoHit.nChannelNumber == 0x15 || chipNoHit.nChannelNumber == 0x16 ) )
                                {
						            this.tドラムヒット処理( nTime, Eパッド.LBlue, chipNoHit, false, nUsePlayer );
                                }

                                if( !bHitted )
                                    break;
                                continue;
                            }
                            //-----------------------------
                            #endregion

                        case Eパッド.RBlue:
                        case Eパッド.RBlue2P:
                            #region[ ふちのヒット処理 ]
                            //-----------------------------
                            {
								if( e判定 != E判定.Miss && chipNoHit.nChannelNumber == 0x12 )
								{
									this.tドラムヒット処理( nTime, Eパッド.RBlue, chipNoHit, false, nUsePlayer );
									bHitted = true;
								}
                                if( e判定 != E判定.Miss && ( chipNoHit.nChannelNumber == 0x14 || chipNoHit.nChannelNumber == 0x1B ) && !TJAPlayer3.ConfigIni.b大音符判定 )
								{
                                    this.tドラムヒット処理( nTime, Eパッド.RBlue, chipNoHit, true, nUsePlayer );
                                    bHitted = true;
                                    this.nWaitButton = 0;
                                    break;
                                }
                                if( e判定 != E判定.Miss && ( chipNoHit.nChannelNumber == 0x14 || chipNoHit.nChannelNumber == 0x1B ) && TJAPlayer3.ConfigIni.b大音符判定 )
								{
                                    if( chipNoHit.eNoteState == ENoteState.none )
                                    {
                                        float time = chipNoHit.nNoiseTimems - CSoundManager.rPlaybackTimer.n現在時刻ms;
                                        if( time <= 110 )
                                        {
                                            chipNoHit.nProcessTime = (int)CSoundManager.rPlaybackTimer.n現在時刻ms;
                                            chipNoHit.eNoteState = ENoteState.wait;
                                            this.nWaitButton = 1;
                                        }
                                    }
                                    else if (chipNoHit.eNoteState == ENoteState.wait)
                                    {
                                        float time = chipNoHit.nNoiseTimems - CSoundManager.rPlaybackTimer.n現在時刻ms;
                                        int nWaitTime = TJAPlayer3.ConfigIni.n両手判定の待ち時間;
                                        if( this.nWaitButton == 2 && time <= 110 && chipNoHit.nProcessTime + nWaitTime > (int)CSoundManager.rPlaybackTimer.n現在時刻ms )
                                        {
                                            this.tドラムヒット処理( nTime, Eパッド.RBlue, chipNoHit, true, nUsePlayer );
                                            bHitted = true;
                                            this.nWaitButton = 0;
                                            break;
                                        }
                                        else if( this.nWaitButton == 2 && time <= 110 && chipNoHit.nProcessTime + nWaitTime < (int)CSoundManager.rPlaybackTimer.n現在時刻ms )
                                        {
                                            this.tドラムヒット処理( nTime, Eパッド.RBlue, chipNoHit, false, nUsePlayer );
                                            bHitted = true;
                                            this.nWaitButton = 0;
                                        }
                                    }
                                }
                                if( e判定 != E判定.Miss && ( chipNoHit.nChannelNumber == 0x15 || chipNoHit.nChannelNumber == 0x16 ) )
                                {
						            this.tドラムヒット処理( nTime, Eパッド.RBlue, chipNoHit, false, nUsePlayer );
                                }

                                if( !bHitted )
                                    break;
                                continue;
                            }
                            //-----------------------------
                            #endregion
					}
                    //2016.07.14 kairera0467 Adlibの場合、一括して処理を行う。
					if( e判定 != E判定.Miss && chipNoHit.nChannelNumber == 0x1F )
					{
						this.tドラムヒット処理( nTime, (Eパッド)nPad, chipNoHit, false, nUsePlayer );
					    bHitted = true;
                    }

					//-----------------------------
					#endregion
					#region [ (B) ヒットしてなかった場合は、レーンフラッシュ、パッドアニメ、空打ち音再生を実行 ]
					//-----------------------------
					int pad = nPad;	// 以下、nPad の代わりに pad を用いる。（成りすまし用）
					// BAD or TIGHT 時の処理。
					if( TJAPlayer3.ConfigIni.bTight && !b連打中[nUsePlayer]) // 18/8/13 - 連打時にこれが発動すると困る!!! (AioiLight)
						this.tチップのヒット処理_BadならびにTight時のMiss( EInstrumentPart.DRUMS, 0, EInstrumentPart.TAIKO );
					//-----------------------------
					#endregion
				}
			}
		}

		protected override void t背景テクスチャの生成()
		{
			Rectangle bgrect = new Rectangle( 0, 0, 1280, 720 );
			string DefaultBgFilename = @"Graphics\5_Game\5_Background\0\Background.png";
			string BgFilename = "";
            if( !String.IsNullOrEmpty( TJAPlayer3.DTX.strBGIMAGE_PATH ) )
                BgFilename = TJAPlayer3.DTX.strBGIMAGE_PATH;
			base.t背景テクスチャの生成( DefaultBgFilename, bgrect, BgFilename );
		}
		protected override void t進行描画_チップ_Taiko( CConfigIni configIni, ref CDTX dTX, ref CDTX.CChip pChip, int nPlayer )
        {
            int nLane = 0;

            #region[ 作り直したもの ]
            if( pChip.bVisible )
            {
                if( !pChip.bHit || pChip.bShow )
				{
                    long nPlayTime = CSoundManager.rPlaybackTimer.n現在時刻ms;
                    if ((!pChip.bHit) && (pChip.nNoiseTimems <= nPlayTime))
                    {
                        bool bAutoPlay = false;
                        switch (nPlayer)
                        {
                            case 0:
                                bAutoPlay = TJAPlayer3.ConfigIni.b太鼓パートAutoPlay;
                                break;
                            case 1:
                                bAutoPlay = TJAPlayer3.ConfigIni.b太鼓パートAutoPlay2P;
                                break;
                            case 2:
                            case 3:
                                bAutoPlay = true;
                                break;
                        }

                        if (bAutoPlay)
                        {
                            pChip.bHit = true;
                            if (pChip.nChannelNumber != 0x1F)
                                this.FlyingNotes.Start(pChip.nChannelNumber < 0x1A ? (pChip.nChannelNumber - 0x10) : (pChip.nChannelNumber - 0x17), nPlayer);
                            if (pChip.nChannelNumber == 0x12 || pChip.nChannelNumber == 0x14 || pChip.nChannelNumber == 0x1B) nLane = 1;
                            TJAPlayer3.stage演奏ドラム画面.actTaikoLaneFlash.PlayerLane[nPlayer].Start((nLane == 0 ? PlayerLane.FlashType.Red : PlayerLane.FlashType.Blue));
                            TJAPlayer3.stage演奏ドラム画面.actTaikoLaneFlash.PlayerLane[nPlayer].Start(PlayerLane.FlashType.Hit);
                            this.actMtaiko.tMtaikoEvent(pChip.nChannelNumber, this.nHand[nPlayer], nPlayer);

                            int n大音符 = (pChip.nChannelNumber == 0x11 || pChip.nChannelNumber == 0x12 ? 2 : 0);

                            this.tチップのヒット処理(pChip.nNoiseTimems, pChip, EInstrumentPart.TAIKO, true, nLane + n大音符, nPlayer);
                            this.tサウンド再生(pChip, nPlayer);
                            return;
                        }
                    }


                    if ( pChip.nNodeAppearTimems != 0 && ( nPlayTime < pChip.nNoiseTimems - pChip.nNodeAppearTimems ) )
                        pChip.bShow = false;
                    else
                        pChip.bShow = true;


                    switch (nPlayer)
                    {
                        case 0:
                            break;
                        case 1:
                            break;
                    }
                    switch( pChip.nPlayerSide )
                    {
                        case 1:
                            break;
                    }

                    int x = 0;
                    int y = TJAPlayer3.Skin.Game_Lane_Field_Y[nPlayer];

                    if ( pChip.nMovementStandbyTimems != 0 && ( nPlayTime < pChip.nNoiseTimems - pChip.nMovementStandbyTimems ) )
                    {
                        x = (int)( ( ( ( pChip.nNoiseTimems ) - ( pChip.nNoiseTimems - pChip.nMovementStandbyTimems ) ) * pChip.dbBPM * pChip.dbSCROLL * ( this.act譜面スクロール速度.db現在の譜面スクロール速度.Drums + 1 ) ) / 502.8594 );
                    }
                    else
                    {
                        x = pChip.nBarDistancedot.Taiko;
                    }

                    int xTemp = 0;
                    int yTemp = 0;

                    #region[ ScrollDirection変更 ]
                    if( pChip.nScrollDirection != 0 )
                    {
                        xTemp = x;
                        yTemp = y;
                    }
                    switch ( pChip.nScrollDirection )
                    {
                        case 0:
                            x += ( TJAPlayer3.Skin.Game_Lane_Field_X[ nPlayer ] );
                            break;
                        case 1:
                            x = ( TJAPlayer3.Skin.Game_Lane_Field_X[ nPlayer ] );
                            y = TJAPlayer3.Skin.Game_Lane_Field_Y[ nPlayer ] - xTemp;
                            break;
                        case 2:
                            x = ( TJAPlayer3.Skin.Game_Lane_Field_X[ nPlayer ] + 3 );
                            y = TJAPlayer3.Skin.Game_Lane_Field_Y[ nPlayer ] + xTemp;
                            break;
                        case 3:
                            x += ( TJAPlayer3.Skin.Game_Lane_Field_X[ nPlayer ] );
                            y = TJAPlayer3.Skin.Game_Lane_Field_Y[ nPlayer ] - xTemp;
                            break;
                        case 4:
                            x += ( TJAPlayer3.Skin.Game_Lane_Field_X[ nPlayer ] );
                            y = TJAPlayer3.Skin.Game_Lane_Field_Y[ nPlayer ] + xTemp;
                            break;
                        case 5:
                            x = ( TJAPlayer3.Skin.Game_Lane_Field_X[ nPlayer ] + 10 ) - xTemp;
                            break;
                        case 6:
                            x = ( TJAPlayer3.Skin.Game_Lane_Field_X[ nPlayer ] ) - xTemp;
                            y = TJAPlayer3.Skin.Game_Lane_Field_Y[ nPlayer ] - xTemp;
                            break;
                        case 7:
                            x = ( TJAPlayer3.Skin.Game_Lane_Field_X[ nPlayer ] ) - xTemp;
                            y = TJAPlayer3.Skin.Game_Lane_Field_Y[ nPlayer ] + xTemp;
                            break;
                    }
                    #endregion

                    #region[ 両手待ち時 ]
                    if( pChip.eNoteState == ENoteState.wait )
                    {
                        x = ( TJAPlayer3.Skin.Game_Lane_Field_X[0] );
                    }
                    #endregion

                    #region[ HIDSUD & STEALTH ]
                    if( TJAPlayer3.ConfigIni.eSTEALTH == Eステルスモード.STEALTH )
                    {
                        pChip.bShow = false;
                    }
                    #endregion


                    if( pChip.dbSCROLL_Y != 0.0 )
                    {
                        y = TJAPlayer3.Skin.Game_Lane_Field_Y[ nPlayer ];
                        if( TJAPlayer3.ConfigIni.eScrollMode == EScrollMode.Normal )
                            y += (int) ( ( ( pChip.nNoiseTimems - CSoundManager.rPlaybackTimer.n現在時刻 ) * pChip.dbBPM * pChip.dbSCROLL_Y * ( this.act譜面スクロール速度.db現在の譜面スクロール速度.Drums + 1 ) ) / 502.8594 );
                        else if( TJAPlayer3.ConfigIni.eScrollMode == EScrollMode.BMSCROLL || TJAPlayer3.ConfigIni.eScrollMode == EScrollMode.HBSCROLL )
                            y += pChip.nBarDistancedot.Taiko;
                    }

                    if( pChip.nBarDistancedot.Drums < 0 )
                    {
                        this.actGame.st叩ききりまショー.b最初のチップが叩かれた = true;
                    }

                    if( ( 1400 > x ) && pChip.bShow )
                    {
                        if( TJAPlayer3.Tx.Notes != null )
                        {
                            int num9 = 0;
                            if (TJAPlayer3.Skin.Game_Notes_Anime)
                            {
                                if (this.actCombo.n現在のコンボ数[nPlayer] >= 300 && ctChipAnimeLag[nPlayer].bEnded)
                                {
                                    if ((int)ctChipAnime[nPlayer].db現在の値 == 1 || (int)ctChipAnime[nPlayer].db現在の値 == 3)
                                    {
                                        num9 = 260;
                                    }
                                    else
                                    {
                                        num9 = 0;
                                    }
                                }
                                else if (this.actCombo.n現在のコンボ数[nPlayer] >= 300 && !ctChipAnimeLag[nPlayer].bEnded)
                                {
                                    if ((int)ctChipAnime[nPlayer].db現在の値 == 1 || (int)ctChipAnime[nPlayer].db現在の値 == 3)
                                    {
                                        num9 = 130;
                                    }
                                    else
                                    {
                                        num9 = 0;
                                    }
                                }
                                else if (this.actCombo.n現在のコンボ数[nPlayer] >= 150)
                                {
                                    if ((int)ctChipAnime[nPlayer].db現在の値 == 1 || (int)ctChipAnime[nPlayer].db現在の値 == 3)
                                    {
                                        num9 = 130;
                                    }
                                    else
                                    {
                                        num9 = 0;
                                    }
                                }
                                else if (this.actCombo.n現在のコンボ数[nPlayer] >= 50 && ctChipAnimeLag[nPlayer].bEnded)
                                {
                                    if ((int)ctChipAnime[nPlayer].db現在の値 <= 1)
                                    {
                                        num9 = 130;
                                    }
                                    else
                                    {
                                        num9 = 0;
                                    }
                                }
                                else if (this.actCombo.n現在のコンボ数[nPlayer] >= 50 && !ctChipAnimeLag[nPlayer].bEnded)
                                {
                                    num9 = 0;
                                }
                                else
                                {
                                    num9 = 0;
                                }
                            }



                            int nSenotesY = TJAPlayer3.Skin.nSENotesY[nPlayer];
                            this.ct手つなぎ.tStartLoop();
                            int nHand = this.ct手つなぎ.nCurrentValue < 30 ? this.ct手つなぎ.nCurrentValue : 60 - this.ct手つなぎ.nCurrentValue;


                            x = ( x ) - ( ( int ) ( ( 130.0 * pChip.sbChipSizeScale ) / 2.0 ) );
                            TJAPlayer3.Tx.Notes.b加算合成 = false;

                            if (TJAPlayer3.Tx.SENotes != null)
                            {
                                TJAPlayer3.Tx.SENotes.b加算合成 = false;
                            }

                            var device = TJAPlayer3.app.Device;
                            switch ( pChip.nChannelNumber )
                            {
                                case 0x11:

                                    if( TJAPlayer3.ConfigIni.eSTEALTH != Eステルスモード.DORON )
                                    {
                                        TJAPlayer3.Tx.Notes.t2D描画( device, x, y, new Rectangle( 130, num9, 130, 130 ) );
                                    }

                                    TJAPlayer3.Tx.SENotes?.t2D描画( device, x - 2, y + nSenotesY, new Rectangle( 0, 30 * pChip.nSenote, 136, 30 ) );
                                    break;

                                case 0x12:
                                    if( TJAPlayer3.ConfigIni.eSTEALTH != Eステルスモード.DORON )
                                    {
                                        TJAPlayer3.Tx.Notes.t2D描画( device, x, y, new Rectangle( 260, num9, 130, 130) );
                                    }

                                    TJAPlayer3.Tx.SENotes?.t2D描画( device, x - 2, y + nSenotesY, new Rectangle( 0, 30 * pChip.nSenote, 136, 30 ) );
                                    break;
                                  
                                case 0x13:
                                    if( TJAPlayer3.ConfigIni.eSTEALTH != Eステルスモード.DORON )
                                    {
                                        TJAPlayer3.Tx.Notes.t2D描画( device, x, y, new Rectangle( 390, num9, 130, 130 ) );
                                    }

                                    TJAPlayer3.Tx.SENotes?.t2D描画( device, x - 2, y + nSenotesY, new Rectangle( 0, 30 * pChip.nSenote, 136, 30 ) );
                                    break;

                                case 0x14:
                                    if( TJAPlayer3.ConfigIni.eSTEALTH != Eステルスモード.DORON )
                                    {
                                        TJAPlayer3.Tx.Notes.t2D描画( device, x, y, new Rectangle( 520, num9, 130, 130 ) );
                                    }

                                    TJAPlayer3.Tx.SENotes?.t2D描画( device, x - 2, y + nSenotesY, new Rectangle( 0, 30 * pChip.nSenote, 136, 30 ) );
                                    break;

                                case 0x1A:
                                    if( TJAPlayer3.ConfigIni.eSTEALTH != Eステルスモード.DORON )
                                    {
                                        if( nPlayer == 0 )
                                        {
                                            TJAPlayer3.Tx.Notes_Arm?.t2D上下反転描画( device, x + 25, ( y + 74 ) + nHand );
                                            TJAPlayer3.Tx.Notes_Arm?.t2D上下反転描画( device, x + 60, ( y + 104 ) - nHand );
                                        }
                                        else if( nPlayer == 1 )
                                        {
                                            TJAPlayer3.Tx.Notes_Arm?.t2D描画( device, x + 25, ( y - 44 ) + nHand );
                                            TJAPlayer3.Tx.Notes_Arm?.t2D描画( device, x + 60, ( y - 14 ) - nHand );
                                        }

                                        TJAPlayer3.Tx.Notes.t2D描画( device, x, y, new Rectangle( 1690, num9, 130, 130 ) );
                                    }

                                    TJAPlayer3.Tx.SENotes?.t2D描画( device, x - 2, y + nSenotesY, new Rectangle( 0, 390, 136, 30 ) );
                                    break;

                                case 0x1B:
                                    if( TJAPlayer3.ConfigIni.eSTEALTH != Eステルスモード.DORON )
                                    {
                                        if( nPlayer == 0 )
                                        {
                                            TJAPlayer3.Tx.Notes_Arm?.t2D上下反転描画( device, x + 25, ( y + 74 ) + nHand );
                                            TJAPlayer3.Tx.Notes_Arm?.t2D上下反転描画( device, x + 60, ( y + 104 ) - nHand );
                                        }
                                        else if( nPlayer == 1 )
                                        {
                                            TJAPlayer3.Tx.Notes_Arm?.t2D描画( device, x + 25, ( y - 44 ) + nHand );
                                            TJAPlayer3.Tx.Notes_Arm?.t2D描画( device, x + 60, ( y - 14 ) - nHand );
                                        }

                                        TJAPlayer3.Tx.Notes.t2D描画( device, x, y, new Rectangle( 1820, num9, 130, 130 ) );
                                    }

                                    TJAPlayer3.Tx.SENotes?.t2D描画( device, x - 2, y + nSenotesY, new Rectangle( 0, 420, 136, 30 ) );
                                    break;

                                case 0x1F:
                                    break;
                            }
                        }
                    }
                }
            }

            #endregion
        }
		protected override void t進行描画_チップ_Taiko連打( CConfigIni configIni, ref CDTX dTX, ref CDTX.CChip pChip, int nPlayer )
        {
            int nSenotesY = TJAPlayer3.Skin.nSENotesY[ nPlayer ];
            int nノート座標 = 0;
            int nノート末端座標 = 0;
            int n先頭NoiseLocation = 0;

            // 2016.11.2 kairera0467
            // 黄連打音符を赤くするやつの実装方法メモ
            //前面を黄色、背面を変色後にしたものを重ねて、打数に応じて前面の透明度を操作すれば、色を操作できるはず。
            //ただしテクスチャのαチャンネル部分が太くなるなどのデメリットが出る。備えよう。

            #region[ 作り直したもの ]
            if( pChip.bVisible )
            {
                if( pChip.nChannelNumber >= 0x15 && pChip.nChannelNumber <= 0x18 )
                {
                    if( pChip.nNodeAppearTimems != 0 && ( CSoundManager.rPlaybackTimer.n現在時刻ms < pChip.nNoiseTimems - pChip.nNodeAppearTimems ) )
                        pChip.bShow = false;
                    else if( pChip.nNodeAppearTimems != 0 && pChip.nMovementStandbyTimems != 0 )
                        pChip.bShow = true;

                    if( pChip.nMovementStandbyTimems != 0 && ( CSoundManager.rPlaybackTimer.n現在時刻ms < pChip.nNoiseTimems - pChip.nMovementStandbyTimems ) )
                    {
                        nノート座標 = (int)( ( ( ( pChip.nNoiseTimems ) - ( pChip.nNoiseTimems - pChip.nMovementStandbyTimems ) ) * pChip.dbBPM * pChip.dbSCROLL * ( this.act譜面スクロール速度.db現在の譜面スクロール速度.Drums + 1 ) ) / 502.8594 );
                        nノート末端座標 = (int)( ( ( pChip.nNoteEndTimems - ( pChip.nNoiseTimems - pChip.nMovementStandbyTimems ) ) * pChip.dbBPM * pChip.dbSCROLL * ( this.act譜面スクロール速度.db現在の譜面スクロール速度.Drums + 1 ) ) / 502.8594 );
                    }
                    else
                    {
                        nノート座標 = 0;
                        nノート末端座標 = 0;
                    }
                }
                if( pChip.nChannelNumber == 0x18 )
                {
                    if( pChip.nNodeAppearTimems != 0 && ( CSoundManager.rPlaybackTimer.n現在時刻ms < n先頭NoiseLocation - pChip.nNodeAppearTimems ) )
                        pChip.bShow = false;
                    else
                        pChip.bShow = true;

                    CDTX.CChip cChip = null;
                    if( pChip.nMovementStandbyTimems != 0 ) // n先頭NoiseLocation value is only used when this condition is met
                    {
                        cChip = TJAPlayer3.stage演奏ドラム画面.r指定時刻に一番近い連打Chip_ヒット未済問わず不可視考慮( pChip.nNoiseTimems, 0x10 + pChip.nnRennDaNoteState, 0, nPlayer );
                        if( cChip != null )
                        {
                            n先頭NoiseLocation = cChip.nNoiseTimems;
                        }
                    }

                    //連打音符先頭の開始時刻を取得しなければならない。
                    //そうしなければ連打先頭と連打末端の移動開始時刻にズレが出てしまう。
                    if( pChip.nMovementStandbyTimems != 0 && ( CSoundManager.rPlaybackTimer.n現在時刻ms < n先頭NoiseLocation - pChip.nMovementStandbyTimems ) )
                    {
                        nノート座標 = (int)( ( ( pChip.nNoiseTimems - ( n先頭NoiseLocation - pChip.nMovementStandbyTimems ) ) * pChip.dbBPM * pChip.dbSCROLL * ( this.act譜面スクロール速度.db現在の譜面スクロール速度.Drums + 1 ) ) / 502.8594 );
                    }
                    else
                    {
                        nノート座標 = 0;
                    }
                }

                int x = TJAPlayer3.Skin.Game_Lane_Field_X[nPlayer] - (130 / 2) + pChip.nBarDistancedot.Taiko + 10;
                int x末端 = TJAPlayer3.Skin.Game_Lane_Field_X[nPlayer] - (130 / 2) + pChip.nBarNoteDistancedot.Taiko + 10;
                int y = TJAPlayer3.Skin.Game_Lane_Field_Y[ nPlayer ];

                if( pChip.nChannelNumber >= 0x15 && pChip.nChannelNumber <= 0x17 )
                {
                    if( pChip.nMovementStandbyTimems != 0 && ( CSoundManager.rPlaybackTimer.n現在時刻ms < pChip.nNoiseTimems - pChip.nMovementStandbyTimems ) )
                    {
                        x = TJAPlayer3.Skin.Game_Lane_Field_X[nPlayer] - (130 / 2) + nノート座標;
                        x末端 = TJAPlayer3.Skin.Game_Lane_Field_X[nPlayer] - (130 / 2) + nノート末端座標;
                    }
                    else
                    {
                        x = TJAPlayer3.Skin.Game_Lane_Field_X[nPlayer] - (130 / 2) + pChip.nBarDistancedot.Taiko + 10;
                        x末端 = TJAPlayer3.Skin.Game_Lane_Field_X[nPlayer] - (130 / 2) + pChip.nBarNoteDistancedot.Taiko + 10;
                    }
                }
                else if( pChip.nChannelNumber == 0x18 )
                {
                    if( pChip.nMovementStandbyTimems != 0 && ( CSoundManager.rPlaybackTimer.n現在時刻ms < n先頭NoiseLocation - pChip.nMovementStandbyTimems ) )
                    {
                        x = TJAPlayer3.Skin.Game_Lane_Field_X[nPlayer] - (130 / 2) + nノート座標;
                    }
                    else
                    {
                        x = TJAPlayer3.Skin.Game_Lane_Field_X[nPlayer] - (130 / 2) + pChip.nBarDistancedot.Taiko + 10;
                    }
                }


                if( pChip.nChannelNumber == 0x15 && pChip.nNoiseTimems < 5000 )
                {

                }
                if( pChip.nChannelNumber == 0x18 && pChip.nNoiseTimems < 5000 )
                {

                }

                #region[ HIDSUD & STEALTH ]
                if( TJAPlayer3.ConfigIni.eSTEALTH == Eステルスモード.STEALTH )
                {
                    pChip.bShow = false;
                }
                #endregion

                x -= 10;

                if( ( 1400 > x ) && pChip.bShow )
                {
                    if( TJAPlayer3.Tx.Notes != null )
                    {
                        int num9 = 0;
                        if (TJAPlayer3.Skin.Game_Notes_Anime)
                        {
                            if (this.actCombo.n現在のコンボ数[nPlayer] >= 300 && ctChipAnimeLag[nPlayer].bEnded)
                            {
                                if ((int)ctChipAnime[nPlayer].db現在の値 == 1 || (int)ctChipAnime[nPlayer].db現在の値 == 3)
                                {
                                    num9 = 260;
                                }
                                else
                                {
                                    num9 = 0;
                                }
                            }
                            else if (this.actCombo.n現在のコンボ数[nPlayer] >= 300 && !ctChipAnimeLag[nPlayer].bEnded)
                            {
                                if ((int)ctChipAnime[nPlayer].db現在の値 == 1 || (int)ctChipAnime[nPlayer].db現在の値 == 3)
                                {
                                    num9 = 130;
                                }
                                else
                                {
                                    num9 = 0;
                                }
                            }
                            else if (this.actCombo.n現在のコンボ数[nPlayer] >= 150)
                            {
                                if ((int)ctChipAnime[nPlayer].db現在の値 == 1 || (int)ctChipAnime[nPlayer].db現在の値 == 3)
                                {
                                    num9 = 130;
                                }
                                else
                                {
                                    num9 = 0;
                                }
                            }
                            else if (this.actCombo.n現在のコンボ数[nPlayer] >= 50 && ctChipAnimeLag[nPlayer].bEnded)
                            {
                                if ((int)ctChipAnime[nPlayer].db現在の値 <= 1)
                                {
                                    num9 = 130;
                                }
                                else
                                {
                                    num9 = 0;
                                }
                            }
                            else if (this.actCombo.n現在のコンボ数[nPlayer] >= 50 && !ctChipAnimeLag[nPlayer].bEnded)
                            {
                                num9 = 0;
                            }
                            else
                            {
                                num9 = 0;
                            }
                        }

                        //kairera0467氏 の TJAPlayer2forPC のコードを参考にし、打数に応じて色を変える(打数の変更以外はほとんどそのまんま) ろみゅ～？ 2018/8/20
                        pChip.RollInputTime?.tStart();
                        pChip.RollDelay?.tStart();

                        if (pChip.RollInputTime != null && pChip.RollInputTime.bEnded)
                        {
                            pChip.RollInputTime.tStop();
                            pChip.RollInputTime.nCurrentValue = 0;
                            pChip.RollDelay = new CCounter(0, 1, 1, TJAPlayer3.Timer);
                        }
                        
                        if (pChip.RollDelay != null && pChip.RollDelay.bEnded && pChip.RollEffectLevel > 0)
                        {
                            pChip.RollEffectLevel--;
                            pChip.RollDelay = new CCounter(0, 1, 1, TJAPlayer3.Timer);
                            pChip.RollDelay.nCurrentValue = 0;
                        }

                        float f減少するカラー = 1.0f - ((0.95f / 100) * pChip.RollEffectLevel);
                        var effectedColor = new Color4(1.0f, f減少するカラー, f減少するカラー);
                        var normalColor = new Color4(1.0f, 1.0f, 1.0f);
                        float f末端ノーツのテクスチャ位置調整 = 65f;

                        if ( pChip.nChannelNumber == 0x15 ) //連打(小)
                        {
                            int index = x末端 - x; //連打の距離
                            if ( TJAPlayer3.ConfigIni.eSTEALTH != Eステルスモード.DORON )
                            {
                                #region[末端をテクスチャ側でつなげる場合の方式]

                                if (TJAPlayer3.Skin.Game_RollColorMode != CSkin.RollColorMode.None)
                                    TJAPlayer3.Tx.Notes.color4 = effectedColor;
                                else
                                    TJAPlayer3.Tx.Notes.color4 = normalColor;
                                TJAPlayer3.Tx.Notes.vc拡大縮小倍率.X = (index - 65.0f + f末端ノーツのテクスチャ位置調整 + 1) / 128.0f;
                                TJAPlayer3.Tx.Notes.t2D描画(TJAPlayer3.app.Device, x + 64, y, new Rectangle(781, 0, 128, 130));
                                TJAPlayer3.Tx.Notes.vc拡大縮小倍率.X = 1.0f;
                                TJAPlayer3.Tx.Notes.t2D描画(TJAPlayer3.app.Device, x末端 + f末端ノーツのテクスチャ位置調整, y, 0, new Rectangle(910, num9, 130, 130));
                                if (TJAPlayer3.Skin.Game_RollColorMode == CSkin.RollColorMode.All)
                                    TJAPlayer3.Tx.Notes.color4 = effectedColor;
                                else
                                    TJAPlayer3.Tx.Notes.color4 = normalColor;
                                
                                TJAPlayer3.Tx.Notes.t2D描画(TJAPlayer3.app.Device, x, y, 0, new Rectangle(650, num9, 130, 130));
                                TJAPlayer3.Tx.Notes.color4 = normalColor;
                                #endregion
                            }

                            if (TJAPlayer3.Tx.SENotes != null)
                            {
                                TJAPlayer3.Tx.SENotes.vc拡大縮小倍率.X = index - 44;
                                TJAPlayer3.Tx.SENotes.t2D描画( TJAPlayer3.app.Device, x + 90, y + nSenotesY, new Rectangle( 60, 240, 1, 30 ) );
                                TJAPlayer3.Tx.SENotes.vc拡大縮小倍率.X = 1.0f;
                                TJAPlayer3.Tx.SENotes.t2D描画( TJAPlayer3.app.Device, x + 30, y + nSenotesY, new Rectangle(0, 240, 60, 30));
                                TJAPlayer3.Tx.SENotes.t2D描画(TJAPlayer3.app.Device, x, y + nSenotesY, new Rectangle(0, 30 * pChip.nSenote, 136, 30));
                            }
                        }
                        if( pChip.nChannelNumber == 0x16 )
                        {
                            int index = x末端 - x; //連打の距離

                            if( TJAPlayer3.ConfigIni.eSTEALTH != Eステルスモード.DORON )
                            {
                                #region[末端をテクスチャ側でつなげる場合の方式]

                                if (TJAPlayer3.Skin.Game_RollColorMode != CSkin.RollColorMode.None)
                                    TJAPlayer3.Tx.Notes.color4 = effectedColor;
                                else
                                    TJAPlayer3.Tx.Notes.color4 = normalColor;

                                TJAPlayer3.Tx.Notes.vc拡大縮小倍率.X = (index - 65 + f末端ノーツのテクスチャ位置調整 + 1) / 128f;
                                TJAPlayer3.Tx.Notes.t2D描画(TJAPlayer3.app.Device, x + 64, y, new Rectangle(1171, 0, 128, 130));

                                TJAPlayer3.Tx.Notes.vc拡大縮小倍率.X = 1.0f;
                                TJAPlayer3.Tx.Notes.t2D描画(TJAPlayer3.app.Device, x末端 + f末端ノーツのテクスチャ位置調整, y, 0, new Rectangle(1300, num9, 130, 130));
                                if (TJAPlayer3.Skin.Game_RollColorMode == CSkin.RollColorMode.All)
                                    TJAPlayer3.Tx.Notes.color4 = effectedColor;
                                else
                                    TJAPlayer3.Tx.Notes.color4 = normalColor;

                                TJAPlayer3.Tx.Notes.t2D描画(TJAPlayer3.app.Device, x, y, new Rectangle(1040, num9, 130, 130));
                                TJAPlayer3.Tx.Notes.color4 = normalColor;
                                #endregion
                            }

                            if (TJAPlayer3.Tx.SENotes != null)
                            {
                                TJAPlayer3.Tx.SENotes.vc拡大縮小倍率.X = index - 70;
                                TJAPlayer3.Tx.SENotes.t2D描画( TJAPlayer3.app.Device, x + 116, y + nSenotesY, new Rectangle( 60, 240, 1, 30 ) );
                                TJAPlayer3.Tx.SENotes.vc拡大縮小倍率.X = 1.0f;
                                TJAPlayer3.Tx.SENotes.t2D描画(TJAPlayer3.app.Device, x + 56, y + nSenotesY, new Rectangle(0, 240, 60, 30));
                                TJAPlayer3.Tx.SENotes.t2D描画(TJAPlayer3.app.Device, x - 2, y + nSenotesY, new Rectangle(0, 30 * pChip.nSenote, 136, 30));
                            }
                        }
                        if( pChip.nChannelNumber == 0x17 )
                        {
                            if( pChip.nNoiseTimems < CSoundManager.rPlaybackTimer.n現在時刻ms && pChip.nNoteEndTimems > CSoundManager.rPlaybackTimer.n現在時刻ms )
                                x = TJAPlayer3.Skin.Game_Lane_Field_X[nPlayer] - (130 / 2);
                            if( TJAPlayer3.ConfigIni.eSTEALTH != Eステルスモード.DORON )
                                TJAPlayer3.Tx.Notes.t2D描画( TJAPlayer3.app.Device, x, y, new Rectangle( 1430, num9, 260, 130 ) );

                            TJAPlayer3.Tx.SENotes?.t2D描画(TJAPlayer3.app.Device, x - 2, y + nSenotesY, new Rectangle(0, 30 * pChip.nSenote, 136, 30));
                        }
                        if( pChip.nChannelNumber == 0x18 )
                        {
                            //大きい連打か小さい連打かの区別方法を考えてなかったよちくしょう
                            TJAPlayer3.Tx.Notes.vc拡大縮小倍率.X = 1.0f;
                            int n = 0;
                            switch( pChip.nnRennDaNoteState )
                            {
                                case 5:
                                    n = 910;
                                    break;
                                case 6:
                                    n = 1300;
                                    break;
                                default:
                                    n = 910;
                                    break;
                            }
                            if( pChip.nnRennDaNoteState != 7 )
                            {
                                TJAPlayer3.Tx.SENotes?.t2D描画(TJAPlayer3.app.Device, x + 56, y + nSenotesY, new Rectangle( 58, 270, 78, 30 ) );
                            }
                        }
                    }
                }
            }
            if( pChip.nNoiseTimems < CSoundManager.rPlaybackTimer.n現在時刻ms && pChip.nNoteEndTimems > CSoundManager.rPlaybackTimer.n現在時刻ms )
            {
                //時間内でかつ0x9Aじゃないならならヒット処理
                if( pChip.nChannelNumber != 0x18 && ( nPlayer == 0 ? TJAPlayer3.ConfigIni.b太鼓パートAutoPlay : TJAPlayer3.ConfigIni.b太鼓パートAutoPlay2P ) )
                    this.tチップのヒット処理( pChip.nNoiseTimems, pChip, EInstrumentPart.TAIKO, false, 0, nPlayer );
            }
            #endregion
		}

		protected override void t進行描画_チップ_小節線( CConfigIni configIni, ref CDTX dTX, ref CDTX.CChip pChip, int nPlayer )
		{
            if( pChip.nCourse != this.n現在のコース[ nPlayer ] )
                return;

            int n小節番号plus1 = this.actPlayInfo.NowMeasure[nPlayer];
            int x = TJAPlayer3.Skin.Game_Lane_Field_X[ nPlayer ] + pChip.nBarDistancedot.Taiko;
            int y = TJAPlayer3.Skin.Game_Lane_Field_Y[ nPlayer ];

            if( pChip.dbSCROLL_Y != 0.0 )
            {
                y = TJAPlayer3.Skin.Game_Lane_Field_Y[ nPlayer ];
                y += (int) ( ( ( pChip.nNoiseTimems - CSoundManager.rPlaybackTimer.n現在時刻 ) * pChip.dbBPM * pChip.dbSCROLL_Y * ( this.act譜面スクロール速度.db現在の譜面スクロール速度.Drums + 1 )) / 502.8594);
            }

			if ( !pChip.bHit && pChip.nNoiseTimems > CSoundManager.rPlaybackTimer.n現在時刻 )
			{
				if ( configIni.bWavePlaybackAutoOffset && ( bIsDirectSound || bUseOSTimer ) )
				{
					dTX.tWave再生位置自動補正();
				}
			}
			if ( configIni.b演奏情報を表示する )
			{
                var nowMeasure = pChip.nIntNum_Internal;
                if (x >= TJAPlayer3.Skin.Game_Lane_Field_X[nPlayer] - 104)
                {
				    TJAPlayer3.act文字コンソール.tPrint(x + 8, y - 26, C文字コンソール.Eフォント種別.白, nowMeasure.ToString());
                }
			}
			if ( ( pChip.bVisible ) && (TJAPlayer3.Tx.Bar != null ) )
			{
                if( x >= 0 )
                {
                    Matrix mat = Matrix.Identity;
                    mat *= Matrix.RotationZ(C変換.DegreeToRadian(-(90.0f * (float)pChip.dbSCROLL_Y)));
                    mat *= Matrix.Translation((float)(x - 640.0f) - 1.5f, -(y - 360.0f + 65.0f), 0f);

                    if( pChip.bBranch )
                    {
                        TJAPlayer3.Tx.Bar_Branch?.t3D描画( TJAPlayer3.app.Device, mat, new Rectangle( 0, 0, TJAPlayer3.Skin.Game_Bar_Width, TJAPlayer3.Skin.Game_Bar_Height ) );
                    }
                    else
                    {
                        TJAPlayer3.Tx.Bar.t3D描画( TJAPlayer3.app.Device, mat, new Rectangle( 0, 0, TJAPlayer3.Skin.Game_Bar_Width, TJAPlayer3.Skin.Game_Bar_Height) );
                    }
                }
			}
		}

        /// <summary>
        /// 全体にわたる制御をする。
        /// </summary>
        public void t全体制御メソッド()
        {
            int t = (int)CSoundManager.rPlaybackTimer.n現在時刻ms;

            for( int i = 0; i < TJAPlayer3.ConfigIni.nPlayerCount; i++ )
            {
                if( this.chip現在処理中の連打チップ[ i ] != null )
                {
                    int n = this.chip現在処理中の連打チップ[ i ].nChannelNumber;


                    if( this.chip現在処理中の連打チップ[ i ].nChannelNumber == 0x17 && this.b連打中[ i ] == true )
                    {
                        if( this.chip現在処理中の連打チップ[ i ].nNoiseTimems <= (int)CSoundManager.rPlaybackTimer.n現在時刻ms && this.chip現在処理中の連打チップ[ i ].nNoteEndTimems + 500 >= (int)CSoundManager.rPlaybackTimer.n現在時刻ms)
                        {
                            this.chip現在処理中の連打チップ[ i ].bShow = false;
                            this.actBalloon.On進行描画( this.chip現在処理中の連打チップ[ i ].nBalloon, this.n風船残り[ i ], i );
                        }
                        else
                        {
                            this.n現在の連打数[i] = 0;


                        }
                       

                    }
                    else
                    {
                        if (actChara.CharaAction_Balloon_Breaking.b進行中 && chip現在処理中の連打チップ[i].nPlayerSide == 0)
                        {
                        }
                    }
                }
            }


            #region[ 譜面分岐ガイド建設予定地 ]
            //現在実験状態です。
            //画像などが完成したらメソッドorクラスとして分離します。

            float f現在の精度 = 0;
            int n種類 = 0;
            int n次回分岐までの小節数 = 0;
            string strNext = "BRANCH END";

            if( ( this.n分岐した回数[ 0 ] < TJAPlayer3.DTX.listBRANCH.Count ) && TJAPlayer3.ConfigIni.bBranchGuide && !TJAPlayer3.ConfigIni.b太鼓パートAutoPlay )
            {
                f現在の精度 = 0;
                n種類 = TJAPlayer3.DTX.listBRANCH[this.n分岐した回数[ 0 ]].nBranchType;
                strNext = "NORMAL";
                n次回分岐までの小節数 = (TJAPlayer3.DTX.listBRANCH[this.n分岐した回数[0]].nCurrentBar - 2) - TJAPlayer3.stage演奏ドラム画面.actPlayInfo.NowMeasure[0];

                if( TJAPlayer3.stage演奏ドラム画面.actPlayInfo.NowMeasure[0] < 0 )
                {
                    n次回分岐までの小節数 = TJAPlayer3.DTX.listBRANCH[this.n分岐した回数[ 0 ]].nCurrentBar - 2;
                }

                if( ( this.nBranch_Perfect[ 0 ] + this.nBranch_Good[ 0 ] + this.nBranch_Miss[ 0 ] ) != 0 )
                {
                    f現在の精度 = ( (float)this.nBranch_Perfect[ 0 ] / (float)( this.nBranch_Perfect[ 0 ] + this.nBranch_Good[ 0 ] + this.nBranch_Miss[ 0 ] ) ) * 100.0f;
                }

                if( n種類 == 0 )
                {
                    if( f現在の精度 < TJAPlayer3.DTX.listBRANCH[this.n分岐した回数[ 0 ]].nConditionValueA )
                    {
                        strNext = "NORMAL";
                    }
                    else if( f現在の精度 >= TJAPlayer3.DTX.listBRANCH[this.n分岐した回数[ 0 ]].nConditionValueA && f現在の精度 < TJAPlayer3.DTX.listBRANCH[this.n分岐した回数[ 0 ]].nConditionValueB )
                    {
                        strNext = "EXPERT";
                    }
                    else if( f現在の精度 >= TJAPlayer3.DTX.listBRANCH[this.n分岐した回数[ 0 ]].nConditionValueB )
                    {
                        strNext = "MASTER";
                    }

                    TJAPlayer3.act文字コンソール.tPrint( 0, 128, C文字コンソール.Eフォント種別.白, f現在の精度.ToString() );
                }
                if( n種類 == 1 )
                {
                    if( this.nBranch_roll[ 0 ] < TJAPlayer3.DTX.listBRANCH[this.n分岐した回数[ 0 ]].nConditionValueA )
                    {
                        strNext = "NORMAL";
                    }
                    else if( this.nBranch_roll[ 0 ] >= TJAPlayer3.DTX.listBRANCH[this.n分岐した回数[ 0 ]].nConditionValueA && this.nBranch_roll[ 0 ] < TJAPlayer3.DTX.listBRANCH[this.n分岐した回数[ 0 ]].nConditionValueB )
                    {
                        strNext = "EXPERT";
                    }
                    else if( this.nBranch_roll[ 0 ] >= TJAPlayer3.DTX.listBRANCH[this.n分岐した回数[ 0 ]].nConditionValueB )
                    {
                        strNext = "MASTER";
                    }

                    TJAPlayer3.act文字コンソール.tPrint( 0, 128, C文字コンソール.Eフォント種別.白, this.nBranch_roll.ToString() );
                }


                TJAPlayer3.act文字コンソール.tPrint( 0, 160, C文字コンソール.Eフォント種別.白, string.Format( "NEXT BRANCH:{0:##0}", n次回分岐までの小節数.ToString() ) );
                TJAPlayer3.act文字コンソール.tPrint( 0, 362, C文字コンソール.Eフォント種別.白, string.Format( "NEXT BRANCH INFO:{0:##0} , {1:##0}", TJAPlayer3.DTX.listBRANCH[ this.n分岐した回数[ 0 ] ].nConditionValueA.ToString(), TJAPlayer3.DTX.listBRANCH[ this.n分岐した回数[ 0 ] ].nConditionValueB.ToString() ) );
                TJAPlayer3.act文字コンソール.tPrint( 0, 144, C文字コンソール.Eフォント種別.白, strNext.ToString() );
            }
            else if( ( this.n分岐した回数[ 0 ] >= TJAPlayer3.DTX.listBRANCH.Count ) && TJAPlayer3.ConfigIni.bBranchGuide && !TJAPlayer3.ConfigIni.b太鼓パートAutoPlay  )
            {
                TJAPlayer3.act文字コンソール.tPrint( 0, 144, C文字コンソール.Eフォント種別.白, strNext.ToString() );
            }
            #endregion

            #region[ 片手判定をこっちに持ってきてみる。]
            for( int i = 0; i < TJAPlayer3.ConfigIni.nPlayerCount; i++ )
            {
                CDTX.CChip chipNoHit = GetChipOfNearest( CSoundManager.rPlaybackTimer.n現在時刻ms, i );

                if( chipNoHit != null && ( chipNoHit.nChannelNumber == 0x13 || chipNoHit.nChannelNumber == 0x14 || chipNoHit.nChannelNumber == 0x1A || chipNoHit.nChannelNumber == 0x1B ) )
                {
                    float timeC = chipNoHit.nNoiseTimems - CSoundManager.rPlaybackTimer.n現在時刻ms;
                    int nWaitTime = TJAPlayer3.ConfigIni.n両手判定の待ち時間;
                    if (chipNoHit.eNoteState == ENoteState.wait && timeC <= 110 && chipNoHit.nProcessTime + nWaitTime <= (int)CSoundManager.rPlaybackTimer.n現在時刻ms)
                    {
                        this.tドラムヒット処理(chipNoHit.nProcessTime, Eパッド.RRed, chipNoHit, false, i);
                        this.nWaitButton = 0;
                        chipNoHit.eNoteState = ENoteState.none;
                        chipNoHit.bHit = true;
                        chipNoHit.IsHitted = true;
                    }
                }
            }

            #endregion

            string strNull = "Found";
            if( TJAPlayer3.Input管理.Keyboard.bキーが押された( (int)SlimDX.DirectInput.Key.F1 ) )
            {
                if( !this.actPauseMenu.bIsActivePopupMenu )
                {
                    TJAPlayer3.Skin.sound変更音.t再生する();

                    CSoundManager.rPlaybackTimer.t一時停止();
	    			TJAPlayer3.Timer.t一時停止();
	                TJAPlayer3.DTX.t全チップの再生一時停止();
                    this.actAVI.tPauseControl();

                    this.bPAUSE = true;
                    this.actPauseMenu.tActivatePopupMenu( EInstrumentPart.DRUMS );
                }

            }
        }

        private void t進行描画_リアルタイム判定数表示()
        {
            if( TJAPlayer3.ConfigIni.nPlayerCount == 1 ? ( TJAPlayer3.ConfigIni.bJudgeCountDisplay && !TJAPlayer3.ConfigIni.b太鼓パートAutoPlay ) : false )
            {
                //ボードの横幅は333px
                //数字フォントの小さいほうはリザルトのものと同じ。
                TJAPlayer3.Tx.Judge_Meter?.t2D描画( TJAPlayer3.app.Device, 0, 360 );

                this.t小文字表示( 102, 494, string.Format( "{0,4:###0}", this.nヒット数_Auto含まない.Drums.Perfect.ToString() ), false );
                this.t小文字表示( 102, 532, string.Format( "{0,4:###0}", this.nヒット数_Auto含まない.Drums.Great.ToString() ), false );
                this.t小文字表示( 102, 570, string.Format( "{0,4:###0}", this.nヒット数_Auto含まない.Drums.Miss.ToString() ), false );

                int nNowTotal = this.nヒット数_Auto含まない.Drums.Perfect + this.nヒット数_Auto含まない.Drums.Great + this.nヒット数_Auto含まない.Drums.Miss;
                double dbたたけた率 = Math.Round((100.0 * ( TJAPlayer3.stage演奏ドラム画面.nヒット数_Auto含まない.Drums.Perfect + TJAPlayer3.stage演奏ドラム画面.nヒット数_Auto含まない.Drums.Great)) / (double)nNowTotal);
                double dbPERFECT率 = Math.Round((100.0 * TJAPlayer3.stage演奏ドラム画面.nヒット数_Auto含まない.Drums.Perfect) / (double)nNowTotal);
                double dbGREAT率 = Math.Round((100.0 * TJAPlayer3.stage演奏ドラム画面.nヒット数_Auto含まない.Drums.Great / (double)nNowTotal));
                double dbMISS率 = Math.Round((100.0 * TJAPlayer3.stage演奏ドラム画面.nヒット数_Auto含まない.Drums.Miss / (double)nNowTotal));

                if (double.IsNaN(dbたたけた率))
                    dbたたけた率 = 0;
                if (double.IsNaN(dbPERFECT率))
                    dbPERFECT率 = 0;
                if (double.IsNaN(dbGREAT率))
                    dbGREAT率 = 0;
                if (double.IsNaN(dbMISS率))
                    dbMISS率 = 0;

                this.t大文字表示( 202, 436, string.Format( "{0,3:##0}%", dbたたけた率 ) );
                this.t小文字表示( 206, 494, string.Format( "{0,3:##0}%", dbPERFECT率 ), false );
                this.t小文字表示( 206, 532, string.Format( "{0,3:##0}%", dbGREAT率 ), false );
                this.t小文字表示( 206, 570, string.Format( "{0,3:##0}%", dbMISS率 ), false );
            }
        }

        private void t小文字表示( int x, int y, string str, bool bOrange )
		{
			foreach( char ch in str )
			{
				for( int i = 0; i < this.st小文字位置.Length; i++ )
				{
                    if( ch == ' ' )
                    {
                        break;
                    }

					if( this.st小文字位置[ i ].ch == ch )
					{
                        var rectangle = new Rectangle( this.st小文字位置[ i ].pt.X, this.st小文字位置[ i ].pt.Y, 32, 38 );
                        TJAPlayer3.Tx.Result_Number?.t2D描画(TJAPlayer3.app.Device, x, y, rectangle);
                        break;
					}
				}
				x += 22;
			}
		}

        private void t大文字表示( int x, int y, string str )
		{
			foreach( char ch in str )
			{
				for( int i = 0; i < this.st小文字位置.Length; i++ )
				{
                    if( ch == ' ' )
                    {
                        break;
                    }

					if( this.st小文字位置[ i ].ch == ch )
					{
                        var rectangle = new Rectangle( this.st小文字位置[ i ].pt.X, 38, 32, 42 );
                        TJAPlayer3.Tx.Result_Number?.t2D描画(TJAPlayer3.app.Device, x, y, rectangle);
                        break;
					}
				}
				x += 28;
			}
		}
		#endregion
	}
}
