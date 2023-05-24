using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Runtime.InteropServices;
using FDK;

namespace TJAPlayer3
{
	internal class CAct演奏Drumsスコア : CAct演奏スコア共通
	{
		// CActivity 実装（共通クラスからの差分のみ）

		public unsafe override int OnDraw()
        {
            if (!base.bDeactivated)
            {
                if (base.b初めての進行描画)
                {
                    base.b初めての進行描画 = false;
                }
                long num = FDK.CSoundManager.rPlaybackTimer.n現在時刻;
                //if (num < base.n進行用タイマ)
                //{
                //    base.n進行用タイマ = num;
                //}
                //while ((num - base.n進行用タイマ) >= 10)
                //{
                //    for (int j = 0; j < 4; j++)
                //    {
                //        this.n現在表示中のスコア[j] += this.nスコアの増分[j];

                //        if (this.n現在表示中のスコア[j] > (long) this.n現在の本当のスコア[j])
                //            this.n現在表示中のスコア[j] = (long) this.n現在の本当のスコア[j];
                //    }
                //    base.n進行用タイマ += 10;

                //}
                if( !this.ctTimer.b停止中 )
                {
                    this.ctTimer.tStart();
                    if( this.ctTimer.bEnded )
                    {
                        this.ctTimer.tStop();
                    }

                    //base.t小文字表示( 20, 150, string.Format( "{0,7:######0}", this.nスコアの増分.Guitar ) );
                }

                for (int i = 0; i < 4; i++)
                {
                    if (!this.ct点数アニメタイマ[i].b停止中)
                    {
                        this.ct点数アニメタイマ[i].tStart();
                        if (this.ct点数アニメタイマ[i].bEnded)
                        {
                            this.ct点数アニメタイマ[i].tStop();
                        }
                    }
                }

                for (int i = 0; i < 4; i++)
                {
                    if (!this.ctボーナス加算タイマ[i].b停止中)
                    {
                        this.ctボーナス加算タイマ[i].tStart();
                        if (this.ctボーナス加算タイマ[i].bEnded)
                        {
                            TJAPlayer3.stage演奏ドラム画面.actScore.BonusAdd(i);
                            this.ctボーナス加算タイマ[i].tStop();
                        }
                    }
                }

                //CDTXMania.act文字コンソール.tPrint(0, 0, C文字コンソール.Eフォント種別.白, this.ctボーナス加算タイマ[0].nCurrentValue.ToString());

                base.t小文字表示(TJAPlayer3.Skin.Game_Score_X[0], TJAPlayer3.Skin.Game_Score_Y[0], string.Format( "{0,7:######0}", this.n現在表示中のスコア[ 0 ].Taiko ), 0 , 256, 0);
                if( TJAPlayer3.stage演奏ドラム画面.bDoublePlay ) base.t小文字表示(TJAPlayer3.Skin.Game_Score_X[1], TJAPlayer3.Skin.Game_Score_Y[1], string.Format( "{0,7:######0}", this.n現在表示中のスコア[ 1 ].Taiko ), 0 , 256, 1);

                for( int i = 0; i < 256; i++ )
                {
                    if( this.stScore[ i ].b使用中 )
                    {
                        if( !this.stScore[ i ].ctTimer.b停止中 )
                        {
                            this.stScore[ i ].ctTimer.tStart();
                            if( this.stScore[ i ].ctTimer.bEnded )
                            {
                                this.n現在表示中のスコア[ this.stScore[ i ].nPlayer ].Taiko += (long)this.stScore[ i ].nAddScore;
                                if( this.stScore[ i ].b表示中 == true )
                                    this.n現在表示中のAddScore--;
                                this.stScore[ i ].ctTimer.tStop();
                                this.stScore[ i ].b使用中 = false;
                                if (ct点数アニメタイマ[stScore[i].nPlayer].b終了値に達してない)
                                {
                                    this.ct点数アニメタイマ[stScore[i].nPlayer] = new CCounter(0, 11, 12, TJAPlayer3.Timer);
                                    this.ct点数アニメタイマ[stScore[i].nPlayer].nCurrentValue = 1;
                                }
                                else
                                {
                                    this.ct点数アニメタイマ[stScore[i].nPlayer] = new CCounter(0, 11, 12, TJAPlayer3.Timer);
                                }
                                TJAPlayer3.stage演奏ドラム画面.actDan.Update();
                            }

                            int xAdd = 0;
                            int yAdd = 0;
                            int alpha = 0;

                            if ( this.stScore[i].ctTimer.nCurrentValue < 10)
                            {
                                xAdd = 25;
                                alpha = 150;
                            } else if (this.stScore[i].ctTimer.nCurrentValue < 20)
                            {
                                xAdd = 10;
                                alpha = 200;
                            } else if (this.stScore[i].ctTimer.nCurrentValue < 30)
                            {
                                xAdd = -5;
                                alpha = 250;
                            } else if (this.stScore[i].ctTimer.nCurrentValue < 40)
                            {
                                xAdd = -9;
                                alpha = 256;
                            } else if (this.stScore[i].ctTimer.nCurrentValue < 50)
                            {
                                xAdd = -10;
                                alpha = 256;
                            } else if (this.stScore[i].ctTimer.nCurrentValue < 60)
                            {
                                xAdd = -9;
                                alpha = 256;
                            } else if (this.stScore[i].ctTimer.nCurrentValue < 70)
                            {
                                xAdd = -5;
                                alpha = 256;
                            } else if (this.stScore[i].ctTimer.nCurrentValue < 80)
                            {
                                xAdd = -3;
                                alpha = 256;
                            } else
                            {
                                xAdd = 0;
                                alpha = 256;
                            }



                            if ( this.stScore[ i ].ctTimer.nCurrentValue > 300 )
                            {
                                yAdd = -1;
                            }
                            if (this.stScore[i].ctTimer.nCurrentValue > 310)
                            {
                                yAdd = -5;
                            }
                            if (this.stScore[i].ctTimer.nCurrentValue > 320)
                            {
                                yAdd = -7;
                            }
                            if (this.stScore[i].ctTimer.nCurrentValue > 330)
                            {
                                yAdd = -8;
                            }
                            if (this.stScore[i].ctTimer.nCurrentValue > 340)
                            {
                                yAdd = -8;
                                alpha = 256;
                            }
                            if (this.stScore[i].ctTimer.nCurrentValue > 350)
                            {
                                yAdd = -6;
                                alpha = 256;
                            }
                            if (this.stScore[i].ctTimer.nCurrentValue > 360)
                            {
                                yAdd = 0;
                                alpha = 256;
                            }
                            if (this.stScore[i].ctTimer.nCurrentValue > 370)
                            {
                                yAdd = 5;
                                alpha = 200;
                            }
                            if (this.stScore[i].ctTimer.nCurrentValue > 380)
                            {
                                yAdd = 12;
                                alpha = 150;
                            }
                            if (this.stScore[i].ctTimer.nCurrentValue > 390)
                            {
                                yAdd = 20;
                                alpha = 0;
                            }


                            if ( this.n現在表示中のAddScore < 10 && this.stScore[ i ].bBonusScore == false )
                                base.t小文字表示(TJAPlayer3.Skin.Game_Score_Add_X[this.stScore[i].nPlayer] + xAdd, this.stScore[ i ].nPlayer == 0 ? TJAPlayer3.Skin.Game_Score_Add_Y[ this.stScore[ i ].nPlayer ] + yAdd : TJAPlayer3.Skin.Game_Score_Add_Y[ this.stScore[ i ].nPlayer ] - yAdd, string.Format( "{0,7:######0}", this.stScore[ i ].nAddScore ), this.stScore[ i ].nPlayer + 1 , alpha, stScore[i].nPlayer);
                            if( this.n現在表示中のAddScore < 10 && this.stScore[ i ].bBonusScore == true )
                                base.t小文字表示(TJAPlayer3.Skin.Game_Score_AddBonus_X[this.stScore[i].nPlayer] + xAdd, TJAPlayer3.Skin.Game_Score_AddBonus_Y[ this.stScore[ i ].nPlayer ], string.Format( "{0,7:######0}", this.stScore[ i ].nAddScore ), this.stScore[ i ].nPlayer + 1 , alpha, stScore[i].nPlayer);
                            else
                            {
                                this.n現在表示中のAddScore--;
                                this.stScore[ i ].b表示中 = false;
                            }
                        }
                    }
                    //CDTXMania.act文字コンソール.tPrint(50, 0, C文字コンソール.Eフォント種別.白, this.ct点数アニメタイマ[0].nCurrentValue.ToString());
                    //CDTXMania.act文字コンソール.tPrint(50, 20, C文字コンソール.Eフォント種別.白, this.ct点数アニメタイマ[0].b進行中.ToString());
                }


                //this.n現在表示中のスコア.Taiko = (long)this.n現在の本当のスコア.Taiko;

                //string str = this.n現在表示中のスコア.Taiko.ToString( "0000000" );
                //for ( int i = 0; i < 7; i++ )
                //{
                //    Rectangle rectangle;
                //    char ch = str[i];
                //    if( ch.Equals(' ') )
                //    {
                //        rectangle = new Rectangle(0, 0, 24, 34);
                //    }
                //    else
                //    {
                //        int num4 = int.Parse(str.Substring(i, 1));
                //        rectangle = new Rectangle(num4 * 24, 0, 24, 34);
                //    }
                //    if( base.txScore != null )
                //    {
                //        base.txScore.t2D描画(CDTXMania.app.Device, 20 + (i * 20), 192, rectangle);
                //    }
                //}


                //CDTXMania.act文字コンソール.tPrint( 50, 200, C文字コンソール.Eフォント種別.白, str  );
            }
            return 0;
        }
	}
}
