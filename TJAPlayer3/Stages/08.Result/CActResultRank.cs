using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using SlimDX;
using FDK;

namespace TJAPlayer3
{
	internal class CActResultRank : CActivity
	{
		// コンストラクタ

		public CActResultRank()
		{
			base.bDeactivated = true;
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
			if( base.bDeactivated )
			{
				return 0;
			}
			if( base.b初めての進行描画 )
			{
				base.b初めての進行描画 = false;
			}

			return 1;
		}
		

		// その他

		#region [ private ]
		//-----------------
		//-----------------
		#endregion
	}
}
