﻿using FDK;

namespace TJAPlayer3
{
	internal class CActFIFOStart : CActivity
	{
		// メソッド

		public void tフェードアウト開始()
		{
			this.mode = EFIFOモード.フェードアウト;

            this.counter = new CCounter( 0, 1500, 1, TJAPlayer3.Timer );
		}
		public void tフェードイン開始()
		{
			this.mode = EFIFOモード.フェードイン;
			this.counter = new CCounter( 0, 1500, 1, TJAPlayer3.Timer );
		}

		// CActivity 実装

		public override void On非活性化()
		{
			if( !base.bDeactivated )
			{
                //CDTXMania.tテクスチャの解放( ref this.tx幕 );
				base.On非活性化();
			}
		}
		public override void OnManagedResourceLoaded()
		{
			if( !base.bDeactivated )
			{
				//this.tx幕 = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\6_FO.png" ) );
 			//	this.tx幕2 = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\6_FI.png" ) );
				base.OnManagedResourceLoaded();
			}
		}
		public override int OnDraw()
		{
			if( base.bDeactivated || ( this.counter == null ) )
			{
				return 0;
			}
			this.counter.tStart();

			// Size clientSize = CDTXMania.app.Window.ClientSize;	// #23510 2010.10.31 yyagi: delete as of no one use this any longer.

            if( this.mode == EFIFOモード.フェードアウト )
            {
                if( TJAPlayer3.Tx.SongLoading_FadeOut != null )
			    {
                    int y = this.counter.nCurrentValue >= 840 ? 840 : this.counter.nCurrentValue;
                    TJAPlayer3.Tx.SongLoading_FadeOut.t2D描画( TJAPlayer3.app.Device, 0, 720 - y );
                }

			}
            else
            {
                if(TJAPlayer3.Tx.SongLoading_FadeIn != null )
                {
                    int y = this.counter.nCurrentValue >= 840 ? 840 : this.counter.nCurrentValue;
                    TJAPlayer3.Tx.SongLoading_FadeIn.t2D描画( TJAPlayer3.app.Device, 0, 0 - y );
                }
            }

            if( this.mode == EFIFOモード.フェードアウト )
            {
			    if( this.counter.nCurrentValue != 1500 )
			    {
				    return 0;
			    }
            }
            else if( this.mode == EFIFOモード.フェードイン )
            {
			    if( this.counter.nCurrentValue != 1500 )
			    {
				    return 0;
			    }
            }
			return 1;
		}


		// その他

		#region [ private ]
		//-----------------
		private CCounter counter;
        private CCounter ct待機;
		private EFIFOモード mode;
        //private CTexture tx幕;
        //private CTexture tx幕2;
		//-----------------
		#endregion
	}
}
