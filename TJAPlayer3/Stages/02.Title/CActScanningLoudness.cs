﻿using System;
using FDK;

namespace TJAPlayer3
{
	internal class CActScanningLoudness :  CActivity
	{
	    public bool bIsActivelyScanning;

		// CActivity 実装

		public override void On活性化()
		{
			if ( this.b活性化してる )
				return;
			base.On活性化();

			try
			{
				this.ctNowScanningLoudness = new CCounter();
				this.ctNowScanningLoudness.tStart( 0, 200, 29, TJAPlayer3.Timer );
			}
			finally
			{
			}
		}

		public override void On非活性化()
		{
			if ( this.bDeactivated )
				return;
			base.On非活性化();
			this.ctNowScanningLoudness = null;
		}

		public override int OnDraw()
		{
			if ( this.bDeactivated )
			{
				return 0;
			}
			this.ctNowScanningLoudness.tStartLoop();
			if ( bIsActivelyScanning && TJAPlayer3.Tx.Scanning_Loudness != null )
			{
                TJAPlayer3.Tx.Scanning_Loudness.Opacity = (int) ( 176.0 + 80.0 * Math.Sin( (double) (2 * Math.PI * this.ctNowScanningLoudness.nCurrentValue / 100.0 ) ) );
                TJAPlayer3.Tx.Scanning_Loudness.t2D描画( TJAPlayer3.app.Device, 18 + 89 + 18, 7 ); // 2018-09-03 twopointzero: display right of Enum_Song, using its width and margin
			}

			return 0;
		}

		private CCounter ctNowScanningLoudness;
	}
}
