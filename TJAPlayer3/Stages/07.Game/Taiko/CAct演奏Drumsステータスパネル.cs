using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;

using FDK;

namespace TJAPlayer3
{
	internal class CAct演奏Drumsステータスパネル : CAct演奏ステータスパネル共通
	{
		// コンストラクタ
        public override void On活性化()
        {

            base.On活性化();
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

            base.OnManagedDisposed();
		}
		public override int OnDraw()
		{
            

            return 0;
		}


		// その他

		#region [ private ]
		//-----------------

		//-----------------
		#endregion
	}
}
