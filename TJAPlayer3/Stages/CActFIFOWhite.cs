using FDK;

namespace TJAPlayer3
{
	internal class CActFIFOWhite : CActivity
	{
		// メソッド

		public void tフェードアウト開始()
		{
			this.mode = EFIFOモード.フェードアウト;
			this.counter = new CCounter( 0, 100, 5, TJAPlayer3.Timer );
		}
		public void tフェードイン開始()
		{
			this.mode = EFIFOモード.フェードイン;
			this.counter = new CCounter( 0, 100, 5, TJAPlayer3.Timer );
		}

		// CActivity 実装

		public override void On非活性化()
		{
			if( !base.bDeactivated )
			{
				//CDTXMania.tテクスチャの解放( ref this.tx白タイル64x64 );
				base.On非活性化();
			}
		}
		public override void OnManagedResourceLoaded()
		{
			if( !base.bDeactivated )
			{
				//this.tx白タイル64x64 = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\Tile white 64x64.png" ), false );
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
			if (TJAPlayer3.Tx.Tile_White != null)
			{
                TJAPlayer3.Tx.Tile_White.Opacity = ( this.mode == EFIFOモード.フェードイン ) ? ( ( ( 100 - this.counter.nCurrentValue ) * 0xff ) / 100 ) : ( ( this.counter.nCurrentValue * 0xff ) / 100 );
				for (int i = 0; i <= (SampleFramework.GameWindowSize.Width / 64); i++)		// #23510 2010.10.31 yyagi: change "clientSize.Width" to "640" to fix FIFO drawing size
				{
					for (int j = 0; j <= (SampleFramework.GameWindowSize.Height / 64); j++)	// #23510 2010.10.31 yyagi: change "clientSize.Height" to "480" to fix FIFO drawing size
					{
                        TJAPlayer3.Tx.Tile_White.t2D描画( TJAPlayer3.app.Device, i * 64, j * 64 );
					}
				}
			}
			if( this.counter.nCurrentValue != 100 )
			{
				return 0;
			}
			return 1;
		}


		// その他

		#region [ private ]
		//-----------------
		private CCounter counter;
		private EFIFOモード mode;
		//private CTexture tx白タイル64x64;
		//-----------------
		#endregion
	}
}
