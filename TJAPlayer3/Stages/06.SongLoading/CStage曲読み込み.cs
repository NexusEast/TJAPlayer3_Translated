﻿using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using SlimDX;
using System.Drawing.Text;
using FDK;
using TJAPlayer3.Common;

namespace TJAPlayer3
{
	internal class CStage曲読み込み : CStage
	{
		// コンストラクタ

		public CStage曲読み込み()
		{
			base.eステージID = CStage.Eステージ.曲読み込み;
			base.eフェーズID = CStage.Eフェーズ.共通_通常状態;
			base.bDeactivated = true;
			//base.list子Activities.Add( this.actFI = new CActFIFOBlack() );	// #27787 2012.3.10 yyagi 曲読み込み画面のフェードインの省略
			//base.list子Activities.Add( this.actFO = new CActFIFOBlack() );
		}


		// CStage 実装

		public override void On活性化()
		{
			Trace.TraceInformation( "曲読み込みステージを活性化します。" );
			Trace.Indent();
			try
			{
				this.str曲タイトル = "";
				this.strSTAGEFILE = "";

                var fontFamily = FontUtilities.GetFontFamilyOrFallback(TJAPlayer3.ConfigIni.FontName);
                this.pfTITLE = new CPrivateFastFont(fontFamily, TJAPlayer3.Skin.SongLoading_Title_FontSize);
                this.pfSUBTITLE = new CPrivateFastFont(fontFamily, TJAPlayer3.Skin.SongLoading_SubTitle_FontSize);

				this.nBGM再生開始時刻 = -1;
				this.nBGMの総再生時間ms = 0;
				if( this.sd読み込み音 != null )
				{
					TJAPlayer3.Sound管理.tサウンドを破棄する( this.sd読み込み音 );
					this.sd読み込み音 = null;
				}

		        string strDTXファイルパス = TJAPlayer3.stage選曲.r確定されたスコア.ファイル情報.ファイルの絶対パス;

		        var strフォルダ名 = Path.GetDirectoryName(strDTXファイルパス) + @"\";

		        if (File.Exists(strフォルダ名 + @"set.def"))
		        {
		            var cdtx = new CDTX(strDTXファイルパス, true, 1.0, 0, 1);

		            this.str曲タイトル = cdtx.TITLE;
		            this.strサブタイトル = cdtx.SUBTITLE;

		            cdtx.On非活性化();
		        }
		        else
		        {
		            var 譜面情報 = TJAPlayer3.stage選曲.r確定されたスコア.譜面情報;
		            this.str曲タイトル = 譜面情報.タイトル;
		            this.strサブタイトル = 譜面情報.strサブタイトル;
		        }

			    // For the moment, detect that we are performing
			    // calibration via there being an actual single
			    // player and the special song title and subtitle
			    // of the .tja used to perform input calibration
			    TJAPlayer3.IsPerformingCalibration =
			        !TJAPlayer3.ConfigIni.b太鼓パートAutoPlay &&
			        TJAPlayer3.ConfigIni.nPlayerCount == 1 &&
			        str曲タイトル == "Input Calibration" &&
			        strサブタイトル == "TJAPlayer3 Developers";

				this.strSTAGEFILE = CSkin.Path(@"Graphics\4_SongLoading\Background.png");

				base.On活性化();
			}
			finally
			{
				Trace.TraceInformation( "曲読み込みステージの活性化を完了しました。" );
				Trace.Unindent();
			}
		}
		public override void On非活性化()
		{
			Trace.TraceInformation( "曲読み込みステージを非活性化します。" );
			Trace.Indent();
			try
			{
                TJAPlayer3.t安全にDisposeする(ref this.pfTITLE);
                TJAPlayer3.t安全にDisposeする(ref this.pfSUBTITLE);
                base.On非活性化();
			}
			finally
			{
				Trace.TraceInformation( "曲読み込みステージの非活性化を完了しました。" );
				Trace.Unindent();
			}
		}
		public override void OnManagedResourceLoaded()
		{
			if( !base.bDeactivated )
			{
				this.tx背景 = TJAPlayer3.tテクスチャの生成( this.strSTAGEFILE, false );
                //this.txSongnamePlate = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\6_SongnamePlate.png" ) );
                this.ct待機 = new CCounter( 0, 600, 5, TJAPlayer3.Timer );
                this.ct曲名表示 = new CCounter( 1, 30, 30, TJAPlayer3.Timer );
				try
				{
				    // When performing calibration, inform the player that
				    // calibration is about to begin, rather than
				    // displaying the song title and subtitle as usual.

				    var タイトル = TJAPlayer3.IsPerformingCalibration
				        ? "Input calibration is about to begin."
				        : this.str曲タイトル;

				    var サブタイトル = TJAPlayer3.IsPerformingCalibration
				        ? "Please play as accurately as possible."
				        : this.strサブタイトル;

				    if( !string.IsNullOrEmpty(タイトル) )
					{
                        //this.txタイトル = new CTexture( CDTXMania.app.Device, image, CDTXMania.TextureFormat );
                        //this.txタイトル.vc拡大縮小倍率 = new Vector3( 0.5f, 0.5f, 1f );


					    using (var bmpSongTitle = this.pfTITLE.DrawPrivateFont( タイトル, TJAPlayer3.Skin.SongLoading_Title_ForeColor, TJAPlayer3.Skin.SongLoading_Title_BackColor ))

					    {
					        this.txタイトル = new CTexture( TJAPlayer3.app.Device, bmpSongTitle, TJAPlayer3.TextureFormat, false );
					        txタイトル.vc拡大縮小倍率.X = TJAPlayer3.GetSongNameXScaling(ref txタイトル, 710);
					    }

					    using (var bmpSongSubTitle = this.pfSUBTITLE.DrawPrivateFont( サブタイトル, TJAPlayer3.Skin.SongLoading_SubTitle_ForeColor, TJAPlayer3.Skin.SongLoading_SubTitle_BackColor ))


					    {
					        this.txサブタイトル = new CTexture( TJAPlayer3.app.Device, bmpSongSubTitle, TJAPlayer3.TextureFormat, false );
					    }
                    }
					else
					{
						this.txタイトル = null;
                        this.txサブタイトル = null;
                    }

                }
                catch ( CTextureCreateFailedException e )
				{
					Trace.TraceError( e.ToString() );
					Trace.TraceError( "テクスチャの生成に失敗しました。({0})", new object[] { this.strSTAGEFILE } );
					this.txタイトル = null;
                    this.txサブタイトル = null;
                    this.tx背景 = null;
				}
				base.OnManagedResourceLoaded();
			}
		}
		public override void OnManagedDisposed()
		{
			if( !base.bDeactivated )
			{
				TJAPlayer3.t安全にDisposeする(ref this.tx背景);
				TJAPlayer3.t安全にDisposeする(ref this.txタイトル);
				//CDTXMania.tテクスチャの解放( ref this.txSongnamePlate );
                TJAPlayer3.t安全にDisposeする(ref this.txサブタイトル);
				base.OnManagedDisposed();
			}
		}
		public override int OnDraw()
		{
			if( base.bDeactivated )
				return 0;

			#region [ 初めての進行描画 ]
			//-----------------------------
			if( base.b初めての進行描画 )
			{
				Cスコア cスコア1 = TJAPlayer3.stage選曲.r確定されたスコア;
				if( this.sd読み込み音 != null )
				{
					if( TJAPlayer3.Skin.sound曲読込開始音.b排他 && ( CSkin.Cシステムサウンド.r最後に再生した排他システムサウンド != null ) )
					{
						CSkin.Cシステムサウンド.r最後に再生した排他システムサウンド.t停止する();
					}
					this.sd読み込み音.t再生を開始する();
					this.nBGM再生開始時刻 = CSoundManager.rPlaybackTimer.n現在時刻;
					this.nBGMの総再生時間ms = this.sd読み込み音.nTotalPlayTimems;
				}
				else
				{
					TJAPlayer3.Skin.sound曲読込開始音.t再生する();
					this.nBGM再生開始時刻 = CSoundManager.rPlaybackTimer.n現在時刻;
					this.nBGMの総再生時間ms = TJAPlayer3.Skin.sound曲読込開始音.n長さ_現在のサウンド;
				}
				//this.actFI.tフェードイン開始();							// #27787 2012.3.10 yyagi 曲読み込み画面のフェードインの省略
				base.eフェーズID = CStage.Eフェーズ.共通_フェードイン;
				base.b初めての進行描画 = false;

				nWAVcount = 1;
				bitmapFilename = new Bitmap( 640, 24 );
				graphicsFilename = Graphics.FromImage( bitmapFilename );
				graphicsFilename.TextRenderingHint = TextRenderingHint.AntiAlias;
				ftFilename = new Font(FontUtilities.FallbackFontName, 24f, FontStyle.Bold, GraphicsUnit.Pixel );
			}
			//-----------------------------
			#endregion
            this.ct待機.tStart();



			#region [ ESC押下時は選曲画面に戻る ]
			if ( tキー入力() )
			{
				if ( this.sd読み込み音 != null )
				{
					this.sd読み込み音.tサウンドを停止する();
					this.sd読み込み音.t解放する();
				}
				return (int) E曲読込画面の戻り値.読込中止;
			}
			#endregion

			#region [ 背景、音符＋タイトル表示 ]
			//-----------------------------
            this.ct曲名表示.tStart();
			if( this.tx背景 != null )
				this.tx背景.t2D描画( TJAPlayer3.app.Device, 0, 0 );

            if (TJAPlayer3.Tx.SongLoading_Plate != null)
            {
                TJAPlayer3.Tx.SongLoading_Plate.bスクリーン合成 = TJAPlayer3.Skin.SongLoading_Plate_ScreenBlend; //あまりにも出番が無い
                TJAPlayer3.Tx.SongLoading_Plate.Opacity = C変換.nParsentTo255((this.ct曲名表示.nCurrentValue / 30.0));
                TJAPlayer3.Tx.SongLoading_Plate.t2D描画(TJAPlayer3.app.Device, TJAPlayer3.Skin.SongLoading_Plate_XY[0], TJAPlayer3.Skin.SongLoading_Plate_XY[1], TJAPlayer3.Skin.SongLoadingPlateHorizontalReferencePoint, VerticalReferencePoint.Center);
            }


			int y = 720 - 45;
			if( this.txタイトル != null )
			{
                int nサブタイトル補正 = string.IsNullOrEmpty(TJAPlayer3.stage選曲.r確定されたスコア.譜面情報.strサブタイトル) ? 15 : 0;

                this.txタイトル.Opacity = C変換.nParsentTo255( ( this.ct曲名表示.nCurrentValue / 30.0 ) );
                this.txタイトル.t2D描画(TJAPlayer3.app.Device, TJAPlayer3.Skin.SongLoading_Title_XY[0], TJAPlayer3.Skin.SongLoading_Title_XY[1] + nサブタイトル補正, TJAPlayer3.Skin.SongLoadingTitleHorizontalReferencePoint, VerticalReferencePoint.Center);
            }
			if( this.txサブタイトル != null )
			{
                this.txサブタイトル.Opacity = C変換.nParsentTo255( ( this.ct曲名表示.nCurrentValue / 30.0 ) );
                this.txサブタイトル.t2D描画(TJAPlayer3.app.Device, TJAPlayer3.Skin.SongLoading_SubTitle_XY[0], TJAPlayer3.Skin.SongLoading_SubTitle_XY[1], TJAPlayer3.Skin.SongLoadingSubTitleHorizontalReferencePoint, VerticalReferencePoint.Center);
            }
			//-----------------------------
			#endregion

			switch( base.eフェーズID )
			{
				case CStage.Eフェーズ.共通_フェードイン:
					//if( this.actFI.OnDraw() != 0 )			    // #27787 2012.3.10 yyagi 曲読み込み画面のフェードインの省略
																		// 必ず一度「CStaeg.Eフェーズ.共通_フェードイン」フェーズを経由させること。
																		// さもないと、曲読み込みが完了するまで、曲読み込み画面が描画されない。
						base.eフェーズID = CStage.Eフェーズ.NOWLOADING_DTXファイルを読み込む;
					return (int) E曲読込画面の戻り値.継続;

				case CStage.Eフェーズ.NOWLOADING_DTXファイルを読み込む:
					{
						timeBeginLoad = DateTime.Now;
						var str = TJAPlayer3.stage選曲.r確定されたスコア.ファイル情報.ファイルの絶対パス;

						CScoreIni ini = new CScoreIni( str + ".score.ini" );
						ini.t全演奏記録セクションの整合性をチェックし不整合があればリセットする();

						if( ( TJAPlayer3.DTX != null ) && TJAPlayer3.DTX.b活性化してる )
							TJAPlayer3.DTX.On非活性化();

                        //if( CDTXMania.DTX == null )
                        {
							TJAPlayer3.DTX = new CDTX(str, false, ((double)TJAPlayer3.ConfigIni.n演奏速度) / 20.0, ini.stファイル.BGMAdjust, 0, 0, true);

							CDTX qaa = new CDTX("E:\\TJAPlayer3_Translated\\Test\\Songs\\14,エンジェル ドリーム.tja", false, 1, 0, 0, 0, true);

                             
                            if( TJAPlayer3.ConfigIni.nPlayerCount == 2 )
						        TJAPlayer3.DTX_2P = new CDTX( str, false, ( (double) TJAPlayer3.ConfigIni.n演奏速度 ) / 20.0, ini.stファイル.BGMAdjust, 0, 1, true );
                            if( File.Exists( TJAPlayer3.DTX.strFolderName + @"\\set.def" ) )
                            {
						        TJAPlayer3.DTX = new CDTX( str, false, ( (double) TJAPlayer3.ConfigIni.n演奏速度 ) / 20.0, ini.stファイル.BGMAdjust, 0, 1, true );
                                if( TJAPlayer3.ConfigIni.nPlayerCount == 2 )
						            TJAPlayer3.DTX_2P = new CDTX( str, false, ( (double) TJAPlayer3.ConfigIni.n演奏速度 ) / 20.0, ini.stファイル.BGMAdjust, 0, 1, true );
                            }

					    	Trace.TraceInformation( "----曲情報-----------------" );
				    		Trace.TraceInformation( "TITLE: {0}", TJAPlayer3.DTX.TITLE );
			    			Trace.TraceInformation( "FILE: {0}",  TJAPlayer3.DTX.strtFileAbsolutePath );
		    				Trace.TraceInformation( "---------------------------" );

							var span = (TimeSpan) ( DateTime.Now - timeBeginLoad );
    						Trace.TraceInformation( "DTX読込所要時間:           {0}", span.ToString() );

                            // 段位認定モード用。
                            if (TJAPlayer3.stage選曲.n確定された曲の難易度 == (int)Difficulty.Dan && TJAPlayer3.DTX.List_DanSongs != null)
                            {
                                var fontFamily = FontUtilities.GetFontFamilyOrFallback(TJAPlayer3.ConfigIni.FontName);
                                using (var pfTitle = new CPrivateFont(fontFamily, 30))
                                using (var pfSubTitle = new CPrivateFont(fontFamily, 22))
                                {
                                    var titleForeColor = TJAPlayer3.Skin.Game_DanC_Title_ForeColor;
                                    var titleBackColor = TJAPlayer3.Skin.Game_DanC_Title_BackColor;
                                    var subtitleForeColor = TJAPlayer3.Skin.Game_DanC_SubTitle_ForeColor;
                                    var subtitleBackColor = TJAPlayer3.Skin.Game_DanC_SubTitle_BackColor;

                                    foreach (var danSong in TJAPlayer3.DTX.List_DanSongs)
                                    {
                                        if (!string.IsNullOrEmpty(danSong.Title))
                                        {
                                            using (var bmpSongTitle = pfTitle.DrawPrivateFont(danSong.Title, titleForeColor, titleBackColor))
                                            {
                                                danSong.TitleTex = TJAPlayer3.tテクスチャの生成(bmpSongTitle, false);
                                                danSong.TitleTex.vc拡大縮小倍率.X = TJAPlayer3.GetSongNameXScaling(ref danSong.TitleTex, 710);
                                            }
                                        }

                                        if (!string.IsNullOrEmpty(danSong.SubTitle))
                                        {
                                            using (var bmpSongSubTitle = pfSubTitle.DrawPrivateFont(danSong.SubTitle, subtitleForeColor, subtitleBackColor))
                                            {
                                                danSong.SubTitleTex = TJAPlayer3.tテクスチャの生成(bmpSongSubTitle, false);
                                                danSong.SubTitleTex.vc拡大縮小倍率.X = TJAPlayer3.GetSongNameXScaling(ref danSong.SubTitleTex, 710);
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        //2017.01.28 DD Config.iniに反映しないように変更
                        /*
                        switch( CDTXMania.DTX.nScoreModeTmp )
                        {
                            case 0:
                                CDTXMania.ConfigIni.nScoreMode = 0;
                                break;
                            case 1:
                                CDTXMania.ConfigIni.nScoreMode = 1;
                                break;
                            case 2:
                                CDTXMania.ConfigIni.nScoreMode = 2;
                                break;
                            case -1:
                                CDTXMania.ConfigIni.nScoreMode = 1;
                                break;
                        }
                        */

                        base.eフェーズID = CStage.Eフェーズ.NOWLOADING_WAV読み込み待機;
						timeBeginLoadWAV = DateTime.Now;
						return (int) E曲読込画面の戻り値.継続;
					}

                case CStage.Eフェーズ.NOWLOADING_WAV読み込み待機:
                    {
                        if( this.ct待機.nCurrentValue > 260 )
                        {
						    base.eフェーズID = CStage.Eフェーズ.NOWLOADING_WAVファイルを読み込む;
                        }
						return (int) E曲読込画面の戻り値.継続;
                    }

				case CStage.Eフェーズ.NOWLOADING_WAVファイルを読み込む:
					{
						if ( nWAVcount == 1 && TJAPlayer3.DTX.listWAV.Count > 0 )			// #28934 2012.7.7 yyagi (added checking Count)
						{
							ShowProgressByFilename( TJAPlayer3.DTX.listWAV[ nWAVcount ].strFileName );
						}
						int looptime = (TJAPlayer3.ConfigIni.b垂直帰線待ちを行う)? 3 : 1;	// VSyncWait=ON時は1frame(1/60s)あたり3つ読むようにする
						for ( int i = 0; i < looptime && nWAVcount <= TJAPlayer3.DTX.listWAV.Count; i++ )
						{
							if ( TJAPlayer3.DTX.listWAV[ nWAVcount ].listこのWAVを使用するチャンネル番号の集合.Count > 0 )	// #28674 2012.5.8 yyagi
							{
								TJAPlayer3.DTX.tWAVの読み込み( TJAPlayer3.DTX.listWAV[ nWAVcount ] );
							}
							nWAVcount++;
						}
						if ( nWAVcount <= TJAPlayer3.DTX.listWAV.Count )
						{
							ShowProgressByFilename( TJAPlayer3.DTX.listWAV[ nWAVcount ].strFileName );
						}
						if ( nWAVcount > TJAPlayer3.DTX.listWAV.Count )
						{
							TimeSpan span = ( TimeSpan ) ( DateTime.Now - timeBeginLoadWAV );
							Trace.TraceInformation( "WAV読込所要時間({0,4}):     {1}", TJAPlayer3.DTX.listWAV.Count, span.ToString() );
							timeBeginLoadWAV = DateTime.Now;

							if ( TJAPlayer3.ConfigIni.bDynamicBassMixerManagement )
							{
								TJAPlayer3.DTX.PlanToAddMixerChannel();
							}
                            TJAPlayer3.DTX.t太鼓チップのランダム化( TJAPlayer3.ConfigIni.eRandom.Taiko );

							TJAPlayer3.stage演奏ドラム画面.On活性化();

							span = (TimeSpan) ( DateTime.Now - timeBeginLoadWAV );

							base.eフェーズID = CStage.Eフェーズ.NOWLOADING_BMPファイルを読み込む;
						}
						return (int) E曲読込画面の戻り値.継続;
					}

				case CStage.Eフェーズ.NOWLOADING_BMPファイルを読み込む:
					{
						TimeSpan span;
						DateTime timeBeginLoadBMPAVI = DateTime.Now;

						if ( TJAPlayer3.ConfigIni.bAVIEnabled )
							TJAPlayer3.DTX.tAVIの読み込み();
						span = ( TimeSpan ) ( DateTime.Now - timeBeginLoadBMPAVI );

						span = ( TimeSpan ) ( DateTime.Now - timeBeginLoad );
						Trace.TraceInformation( "総読込時間:                {0}", span.ToString() );

                        if(TJAPlayer3.ConfigIni.FastRender)
                        {
                            FastRender.Render();
                        }


						if ( bitmapFilename != null )
						{
							bitmapFilename.Dispose();
							bitmapFilename = null;
						}
						if ( graphicsFilename != null )
						{
							graphicsFilename.Dispose();
							graphicsFilename = null;
						}
						if ( ftFilename != null )
						{
							ftFilename.Dispose();
							ftFilename = null;
						}
						TJAPlayer3.Timer.t更新();
                        //CSoundManager.rPlaybackTimer.t更新();
						base.eフェーズID = CStage.Eフェーズ.NOWLOADING_システムサウンドBGMの完了を待つ;
						return (int) E曲読込画面の戻り値.継続;
					}

				case CStage.Eフェーズ.NOWLOADING_システムサウンドBGMの完了を待つ:
					{
						long nCurrentTime = TJAPlayer3.Timer.n現在時刻;
						if( nCurrentTime < this.nBGM再生開始時刻 )
							this.nBGM再生開始時刻 = nCurrentTime;

//						if ( ( nCurrentTime - this.nBGM再生開始時刻 ) > ( this.nBGMの総再生時間ms - 1000 ) )
						if ( ( nCurrentTime - this.nBGM再生開始時刻 ) >= ( this.nBGMの総再生時間ms ) )	// #27787 2012.3.10 yyagi 1000ms == フェードイン分の時間
						{
							base.eフェーズID = CStage.Eフェーズ.共通_フェードアウト;
						}
						return (int) E曲読込画面の戻り値.継続;
					}

				case CStage.Eフェーズ.共通_フェードアウト:
					if ( this.ct待機.b終了値に達してない )
						return (int)E曲読込画面の戻り値.継続;

					if ( txFilename != null )
					{
						txFilename.Dispose();
					}
					if ( this.sd読み込み音 != null )
					{
						this.sd読み込み音.t解放する();
					}
					return (int) E曲読込画面の戻り値.読込完了;
			}
			return (int) E曲読込画面の戻り値.継続;
		}

		/// <summary>
		/// ESC押下時、trueを返す
		/// </summary>
		/// <returns></returns>
		protected bool tキー入力()
		{
			IInputDevice keyboard = TJAPlayer3.Input管理.Keyboard;
			if 	( keyboard.bキーが押された( (int) SlimDX.DirectInput.Key.Escape ) )		// escape (exit)
			{
				return true;
			}
			return false;
		}


		private void ShowProgressByFilename(string strFileName )
		{
			if ( graphicsFilename != null && ftFilename != null )
			{
				graphicsFilename.Clear( Color.Transparent );
				graphicsFilename.DrawString( strFileName, ftFilename, Brushes.White, new RectangleF( 0, 0, 640, 24 ) );
				if ( txFilename != null )
				{
					txFilename.Dispose();
				}
				txFilename = new CTexture( TJAPlayer3.app.Device, bitmapFilename, TJAPlayer3.TextureFormat );
				txFilename.vc拡大縮小倍率 = new Vector3( 0.5f, 0.5f, 1f );
				txFilename.t2D描画( TJAPlayer3.app.Device, 0, 720 - 16 );
			}
		}

		// その他

		#region [ private ]
		//-----------------
		//private CActFIFOBlack actFI;
		//private CActFIFOBlack actFO;
		private long nBGMの総再生時間ms;
		private long nBGM再生開始時刻;
		private CSound sd読み込み音;
		private string strSTAGEFILE;
		private string str曲タイトル;
        private string strサブタイトル;
		private CTexture txタイトル;
        private CTexture txサブタイトル;
		private CTexture tx背景;
        //private CTexture txSongnamePlate;
		private DateTime timeBeginLoad;
		private DateTime timeBeginLoadWAV;
		private int nWAVcount;
		private CTexture txFilename;
		private Bitmap bitmapFilename;
		private Graphics graphicsFilename;
		private Font ftFilename;
        private CCounter ct待機;
        private CCounter ct曲名表示;

        private CPrivateFastFont pfTITLE;
        private CPrivateFastFont pfSUBTITLE;
		//-----------------
		#endregion
	}
}
