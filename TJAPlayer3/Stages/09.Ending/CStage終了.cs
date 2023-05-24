using System.Diagnostics;
using FDK;

namespace TJAPlayer3
{
	internal class CStage終了 : CStage
	{
		// コンストラクタ

		public CStage終了()
		{
			base.eステージID = CStage.Eステージ.終了;
			base.eフェーズID = CStage.Eフェーズ.共通_通常状態;
			base.bDeactivated = true;
		}


		// CStage 実装

		public override void On活性化()
		{
			Trace.TraceInformation( "終了ステージを活性化します。" );
			Trace.Indent();
			try
			{
				this.ct時間稼ぎ = new CCounter();
				base.On活性化();
			}
			finally
			{
				Trace.TraceInformation( "終了ステージの活性化を完了しました。" );
				Trace.Unindent();
			}
		}
		public override void On非活性化()
		{
			Trace.TraceInformation( "終了ステージを非活性化します。" );
			Trace.Indent();
			try
			{
				base.On非活性化();
			}
			finally
			{
				Trace.TraceInformation( "終了ステージの非活性化を完了しました。" );
				Trace.Unindent();
			}
		}
		public override void OnManagedResourceLoaded()
		{
			if( !base.bDeactivated )
			{
				base.OnManagedResourceLoaded();
			}
		}
		public override void OnManagedDisposed()
		{
			if( !base.bDeactivated )
			{
				base.OnManagedDisposed();
			}
		}
		public override int OnDraw()
		{
            if( !TJAPlayer3.ConfigIni.bEndingAnime ) //2017.01.27 DD
            {
                return 1;
            }

			if( !base.bDeactivated )
			{
				if( base.b初めての進行描画 )
				{
					TJAPlayer3.Skin.soundゲーム終了音.t再生する();
					this.ct時間稼ぎ.tStart( 0, 3000, 1, TJAPlayer3.Timer );
                    base.b初めての進行描画 = false;
				}


				this.ct時間稼ぎ.tStart();

                TJAPlayer3.Tx.Exit_Background?.t2D描画(TJAPlayer3.app.Device, 0, 0);

                if( this.ct時間稼ぎ.bEnded && !TJAPlayer3.Skin.soundゲーム終了音.b再生中 )
				{
					return 1;
				}
			}
			return 0;
		}

		private CCounter ct時間稼ぎ;
	}
}
