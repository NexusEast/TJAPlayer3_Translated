﻿using FDK;

namespace TJAPlayer3
{
    internal class CAct演奏Drumsレーン : CActivity
    {
        public CAct演奏Drumsレーン()
        {
            base.bDeactivated = true;
        }

        public override void On非活性化()
        {
            ct分岐アニメ進行 = null;

            base.On非活性化();
        }

        public override void OnManagedResourceLoaded()
        {
            //this.tx普通譜面[ 0 ] = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\7_field_normal_base.png"));
            //this.tx玄人譜面[ 0 ] = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\7_field_expert_base.png"));
            //this.tx達人譜面[ 0 ] = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\7_field_master_base.png"));
            //this.tx普通譜面[ 1 ] = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\7_field_normal.png"));
            //this.tx玄人譜面[ 1 ] = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\7_field_expert.png"));
            //this.tx達人譜面[ 1 ] = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\7_field_master.png"));
            this.ct分岐アニメ進行 = new CCounter[ 4 ];
            this.nBefore = new int[ 4 ];
            this.nAfter = new int[ 4 ];
            for( int i = 0; i < TJAPlayer3.ConfigIni.nPlayerCount; i++ )
            {
                this.ct分岐アニメ進行[ i ] = new CCounter();
                this.nBefore[ i ] = 0;
                this.nAfter[ i ] = 0;
                this.bState[ i ] = false;
            }

            var laneBase0 = TJAPlayer3.Tx.Lane_Base[0];
            if (laneBase0 != null)
            {
                laneBase0.Opacity = 255;
            }

            base.OnManagedResourceLoaded();
        }

        public override int OnDraw()
        {
            for( int i = 0; i < TJAPlayer3.ConfigIni.nPlayerCount; i++ )
            {
                if( !this.ct分岐アニメ進行[ i ].b停止中 )
                {
                    this.ct分岐アニメ進行[ i ].tStart();
                    if( this.ct分岐アニメ進行[ i ].bEnded )
                    {
                        this.bState[ i ] = false;
                        this.ct分岐アニメ進行[ i ].tStop();
                    }
                }
            }


            //アニメーション中の分岐レイヤー(背景)の描画を行う。
            for( int i = 0; i < TJAPlayer3.ConfigIni.nPlayerCount; i++ )
            {
                if( TJAPlayer3.stage演奏ドラム画面.bUseBranch[ i ] == true )
                {
                    if( this.ct分岐アニメ進行[ i ].b進行中 )
                    {
                        #region[ 普通譜面_レベルアップ ]
                        //普通→玄人
                        if( nBefore[ i ] == 0 && nAfter[ i ] == 1 )
                        {
                            TJAPlayer3.Tx.Lane_Base[1].Opacity = this.ct分岐アニメ進行[ i ].nCurrentValue > 100 ? 255 : ( ( this.ct分岐アニメ進行[ i ].nCurrentValue * 0xff ) / 100 );
                            TJAPlayer3.Tx.Lane_Base[0].t2D描画( TJAPlayer3.app.Device, TJAPlayer3.Skin.Game_Lane_Background_X[ i ], TJAPlayer3.Skin.Game_Lane_Background_Y[ i ] );
                            TJAPlayer3.Tx.Lane_Base[1].t2D描画( TJAPlayer3.app.Device, TJAPlayer3.Skin.Game_Lane_Background_X[ i ], TJAPlayer3.Skin.Game_Lane_Background_Y[ i ] );
                        }
                        //普通→達人
                        if( nBefore[ i ] == 0 && nAfter[ i ] == 2)
                        {
                            TJAPlayer3.Tx.Lane_Base[0].t2D描画( TJAPlayer3.app.Device, TJAPlayer3.Skin.Game_Lane_Background_X[ i ], TJAPlayer3.Skin.Game_Lane_Background_Y[ i ] );
                            if( this.ct分岐アニメ進行[ i ].nCurrentValue < 100 )
                            {
                                TJAPlayer3.Tx.Lane_Base[1].Opacity = this.ct分岐アニメ進行[ i ].nCurrentValue > 100 ? 255 : ( ( this.ct分岐アニメ進行[ i ].nCurrentValue * 0xff ) / 100 );
                                TJAPlayer3.Tx.Lane_Base[1].t2D描画( TJAPlayer3.app.Device, TJAPlayer3.Skin.Game_Lane_Background_X[ i ], TJAPlayer3.Skin.Game_Lane_Background_Y[ i ] );
                            }
                            else if( this.ct分岐アニメ進行[ i ].nCurrentValue >= 100 && this.ct分岐アニメ進行[ i ].nCurrentValue < 150 )
                            {
                                TJAPlayer3.Tx.Lane_Base[1].Opacity = 255;
                                TJAPlayer3.Tx.Lane_Base[1].t2D描画( TJAPlayer3.app.Device, TJAPlayer3.Skin.Game_Lane_Background_X[ i ], TJAPlayer3.Skin.Game_Lane_Background_Y[ i ] );
                            }
                            else if( this.ct分岐アニメ進行[ i ].nCurrentValue >= 150 )
                            {
                                TJAPlayer3.Tx.Lane_Base[1].t2D描画( TJAPlayer3.app.Device, TJAPlayer3.Skin.Game_Lane_Background_X[ i ], TJAPlayer3.Skin.Game_Lane_Background_Y[ i ] );
                                TJAPlayer3.Tx.Lane_Base[2].Opacity = this.ct分岐アニメ進行[ i ].nCurrentValue > 250 ? 255 : ( ( (this.ct分岐アニメ進行[ i ].nCurrentValue - 150) * 0xff ) / 100 );
                                TJAPlayer3.Tx.Lane_Base[2].t2D描画( TJAPlayer3.app.Device, TJAPlayer3.Skin.Game_Lane_Background_X[ i ], TJAPlayer3.Skin.Game_Lane_Background_Y[ i ] );
                            }
                        }
                        #endregion
                        #region[ 玄人譜面_レベルアップ ]
                        if( nBefore[ i ] == 1 && nAfter[ i ] == 2 )
                        {
                            TJAPlayer3.Tx.Lane_Base[1].t2D描画( TJAPlayer3.app.Device, TJAPlayer3.Skin.Game_Lane_Background_X[ i ], TJAPlayer3.Skin.Game_Lane_Background_Y[ i ] );
                            TJAPlayer3.Tx.Lane_Base[2].Opacity = this.ct分岐アニメ進行[ i ].nCurrentValue > 100 ? 255 : ( ( this.ct分岐アニメ進行[ i ].nCurrentValue * 0xff ) / 100 );
                            TJAPlayer3.Tx.Lane_Base[2].t2D描画( TJAPlayer3.app.Device, TJAPlayer3.Skin.Game_Lane_Background_X[ i ], TJAPlayer3.Skin.Game_Lane_Background_Y[ i ] );
                        }
                        #endregion
                        #region[ 玄人譜面_レベルダウン ]
                        if( nBefore[ i ] == 1 && nAfter[ i ] == 0)
                        {
                            TJAPlayer3.Tx.Lane_Base[1].t2D描画( TJAPlayer3.app.Device, TJAPlayer3.Skin.Game_Lane_Background_X[ i ], TJAPlayer3.Skin.Game_Lane_Background_Y[ i ] );
                            TJAPlayer3.Tx.Lane_Base[0].Opacity = this.ct分岐アニメ進行[ i ].nCurrentValue > 100 ? 255 : ( ( this.ct分岐アニメ進行[ i ].nCurrentValue * 0xff ) / 100 );
                            TJAPlayer3.Tx.Lane_Base[0].t2D描画( TJAPlayer3.app.Device, TJAPlayer3.Skin.Game_Lane_Background_X[ i ], TJAPlayer3.Skin.Game_Lane_Background_Y[ i ] );
                        }
                        #endregion
                        #region[ 達人譜面_レベルダウン ]
                        if( nBefore[ i ] == 2 && nAfter[ i ] == 0)
                        {
                            TJAPlayer3.Tx.Lane_Base[2].t2D描画( TJAPlayer3.app.Device, TJAPlayer3.Skin.Game_Lane_Background_X[ i ], TJAPlayer3.Skin.Game_Lane_Background_Y[ i ] );
                            if( this.ct分岐アニメ進行[ i ].nCurrentValue < 100 )
                            {
                                TJAPlayer3.Tx.Lane_Base[1].Opacity = this.ct分岐アニメ進行[ i ].nCurrentValue > 100 ? 255 : ( ( this.ct分岐アニメ進行[ i ].nCurrentValue * 0xff ) / 100 );
                                TJAPlayer3.Tx.Lane_Base[1].t2D描画( TJAPlayer3.app.Device, TJAPlayer3.Skin.Game_Lane_Background_X[ i ], TJAPlayer3.Skin.Game_Lane_Background_Y[ i ] );
                            }
                            else if( this.ct分岐アニメ進行[ i ].nCurrentValue >= 100 && this.ct分岐アニメ進行[ i ].nCurrentValue < 150 )
                            {
                                TJAPlayer3.Tx.Lane_Base[1].Opacity = 255;
                                TJAPlayer3.Tx.Lane_Base[1].t2D描画( TJAPlayer3.app.Device, TJAPlayer3.Skin.Game_Lane_Background_X[ i ], TJAPlayer3.Skin.Game_Lane_Background_Y[ i ] );
                            }
                            else if( this.ct分岐アニメ進行[ i ].nCurrentValue >= 150 )
                            {
                                TJAPlayer3.Tx.Lane_Base[1].t2D描画( TJAPlayer3.app.Device, TJAPlayer3.Skin.Game_Lane_Background_X[ i ], TJAPlayer3.Skin.Game_Lane_Background_Y[ i ] );
                                TJAPlayer3.Tx.Lane_Base[0].Opacity = this.ct分岐アニメ進行[ i ].nCurrentValue > 250 ? 255 : ( ( ( this.ct分岐アニメ進行[ i ].nCurrentValue - 150 ) * 0xff ) / 100 );
                                TJAPlayer3.Tx.Lane_Base[0].t2D描画( TJAPlayer3.app.Device, TJAPlayer3.Skin.Game_Lane_Background_X[ i ], TJAPlayer3.Skin.Game_Lane_Background_Y[ i ] );
                            }
                        }
                        if( nBefore[ i ] == 2 && nAfter[ i ] == 1 )
                        {
                            TJAPlayer3.Tx.Lane_Base[2].t2D描画( TJAPlayer3.app.Device, TJAPlayer3.Skin.Game_Lane_Background_X[ i ], TJAPlayer3.Skin.Game_Lane_Background_Y[ i ] );
                            TJAPlayer3.Tx.Lane_Base[2].Opacity = this.ct分岐アニメ進行[ i ].nCurrentValue > 100 ? 255 : ( ( this.ct分岐アニメ進行[ i ].nCurrentValue * 0xff ) / 100 );
                            TJAPlayer3.Tx.Lane_Base[2].t2D描画( TJAPlayer3.app.Device, TJAPlayer3.Skin.Game_Lane_Background_X[ i ], TJAPlayer3.Skin.Game_Lane_Background_Y[ i ] );
                        }
                        #endregion
                    }
                }
            }
            return base.OnDraw();
        }

        public virtual void t分岐レイヤー_コース変化( int n現在, int n次回, int player )
        {
            if( n現在 == n次回 ) {
                return;
            }
            this.ct分岐アニメ進行[ player ] = new CCounter( 0, 300, 2, TJAPlayer3.Timer );
            this.bState[ player ] = true;

            this.nBefore[ player ] = n現在;
            this.nAfter[ player ] = n次回;

        }

        #region[ private ]
        //-----------------
        public bool[] bState = new bool[4];
        public CCounter[] ct分岐アニメ進行 = new CCounter[4];
        private int[] nBefore;
        private int[] nAfter;
        private int[] n透明度 = new int[4];
        //private CTexture[] tx普通譜面 = new CTexture[2];
        //private CTexture[] tx玄人譜面 = new CTexture[2];
        //private CTexture[] tx達人譜面 = new CTexture[2];
        //-----------------
        #endregion
    }
}
