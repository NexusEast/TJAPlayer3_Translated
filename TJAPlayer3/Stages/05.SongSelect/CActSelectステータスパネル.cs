using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using FDK;

namespace TJAPlayer3
{
	internal class CActSelectステータスパネル : CActivity
	{
		// メソッド

		public CActSelectステータスパネル()
		{
			base.bDeactivated = true;
		}
		public void t選択曲が変更された()
		{

		}


		// CActivity 実装

		public override void On活性化()
		{

			base.On活性化();
		}
		public override void On非活性化()
		{

			base.On非活性化();
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
			if( !base.bDeactivated )
			{

			}
			return 0;
		}


		// その他

		#region [ private ]
		//-----------------
		//-----------------
		#endregion
	}
}
