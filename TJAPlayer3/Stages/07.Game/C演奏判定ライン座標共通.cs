﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace TJAPlayer3
{
	/// <summary>
	/// 判定ライン関係の座標処理をまとめたクラス
	/// </summary>
	internal class C演奏判定ライン座標共通 // TODO Effectively dead code?
	{
		/// <summary>
		/// 判定ラインのy座標
		/// </summary>
		private STDGBVALUE<int>[,,] n判定ラインY座標元データ = null;			// 補正無しの時の座標データ
		private STDGBVALUE<int>[,,] n演奏RGBボタンY座標元データ = null;

		/// <summary>
		/// 表示位置の補正データ
		/// 初期化は外部から行うこと。
		/// </summary>
		public STDGBVALUE<int> nJudgeLinePosY_delta;

		/// <summary>
		/// 判定ライン表示位置を、Vシリーズ互換にするかどうか。
		/// 設定は外部から行うこと。
		/// </summary>
		public STDGBVALUE<EJudgeLocation> n判定位置;

		/// <summary>
		/// コンストラクタ(座標値の初期化)
		/// </summary>
		public C演奏判定ライン座標共通()
		{
			n判定ラインY座標元データ = new STDGBVALUE<int>[ 2, 2, 2 ];
			#region [ 判定ライン座標の初期化]
			// Normal, Drums画面, 判定ライン
			n判定ラインY座標元データ[ 0, 0, 0 ].Drums  = 0;		//未使用
			n判定ラインY座標元データ[ 0, 0, 0 ].Guitar = 95;
			n判定ラインY座標元データ[ 0, 0, 0 ].Bass   = 95;
			// Reverse, Drums画面, 判定ライン
			n判定ラインY座標元データ[ 1, 0, 0 ].Drums  = 0;		//未使用
			n判定ラインY座標元データ[ 1, 0, 0 ].Guitar = 374;
			n判定ラインY座標元データ[ 1, 0, 0 ].Bass   = 374;
			// Normal, Drums画面, Wailing枠
			n判定ラインY座標元データ[ 0, 0, 1 ].Drums  = 0;		//未使用
			n判定ラインY座標元データ[ 0, 0, 1 ].Guitar = 69;
			n判定ラインY座標元データ[ 0, 0, 1 ].Bass   = 69;
			// Reverse, Drums画面, Wailing枠
			n判定ラインY座標元データ[ 1, 0, 1 ].Drums  = 0;		//未使用
			n判定ラインY座標元データ[ 1, 0, 1 ].Guitar = 350;
			n判定ラインY座標元データ[ 1, 0, 1 ].Bass   = 350;

			// Normal, GR画面, 判定ライン
			n判定ラインY座標元データ[ 0, 1, 0 ].Drums  = 0;		//未使用
			n判定ラインY座標元データ[ 0, 1, 0 ].Guitar = 40;
			n判定ラインY座標元データ[ 0, 1, 0 ].Bass   = 40;
			// Reverse, GR画面, 判定ライン
			n判定ラインY座標元データ[ 1, 1, 0 ].Drums  = 0;		//未使用
			n判定ラインY座標元データ[ 1, 1, 0 ].Guitar = 369;
			n判定ラインY座標元データ[ 1, 1, 0 ].Bass   = 369;
			// Normal, GR画面, Wailing枠
			n判定ラインY座標元データ[ 0, 1, 1 ].Drums  = 0;		//未使用
			n判定ラインY座標元データ[ 0, 1, 1 ].Guitar = 11;
			n判定ラインY座標元データ[ 0, 1, 1 ].Bass   = 11;
			// Reverse, GR画面, Wailing枠
			n判定ラインY座標元データ[ 1, 1, 1 ].Drums  = 0;		//未使用
			n判定ラインY座標元データ[ 1, 1, 1 ].Guitar = 340;
			n判定ラインY座標元データ[ 1, 1, 1 ].Bass   = 340;
			#endregion

			n演奏RGBボタンY座標元データ = new STDGBVALUE<int>[ 2, 2, 2 ];
			#region [ RGBボタン座標の初期化]
			// Normal, Drums画面, RGBボタン
			n演奏RGBボタンY座標元データ[ 0, 0, 0 ].Drums  = 0;		// 未使用
			n演奏RGBボタンY座標元データ[ 0, 0, 0 ].Guitar = 57;
			n演奏RGBボタンY座標元データ[ 0, 0, 0 ].Bass   = 57;
			// Reverse, Drums画面, RGBボタン
			n演奏RGBボタンY座標元データ[ 1, 0, 0 ].Drums  = 0;		// 未使用
			n演奏RGBボタンY座標元データ[ 1, 0, 0 ].Guitar = 57;
			n演奏RGBボタンY座標元データ[ 1, 0, 0 ].Bass   = 57;
			// Normal, Drums画面, RGBボタン(Vシリーズ)
			n演奏RGBボタンY座標元データ[ 0, 0, 1 ].Drums  = 0;		// 未使用
			n演奏RGBボタンY座標元データ[ 0, 0, 1 ].Guitar = 107;
			n演奏RGBボタンY座標元データ[ 0, 0, 1 ].Bass   = 107;
			// Reverse, Drums画面, RGBボタン(Vシリーズ)
			n演奏RGBボタンY座標元データ[ 1, 0, 1 ].Drums  = 0;		// 未使用
			n演奏RGBボタンY座標元データ[ 1, 0, 1 ].Guitar = 107;
			n演奏RGBボタンY座標元データ[ 1, 0, 1 ].Bass   = 107;

			// Normal, GR画面, RGBボタン
			n演奏RGBボタンY座標元データ[ 0, 1, 0 ].Drums  = 0;		// 未使用
			n演奏RGBボタンY座標元データ[ 0, 1, 0 ].Guitar = 3;
			n演奏RGBボタンY座標元データ[ 0, 1, 0 ].Bass   = 3;
			// Reverse, GR画面, RGBボタン
			n演奏RGBボタンY座標元データ[ 1, 1, 0 ].Drums  = 0;		// 未使用
			n演奏RGBボタンY座標元データ[ 1, 1, 0 ].Guitar = 3;
			n演奏RGBボタンY座標元データ[ 1, 1, 0 ].Bass   = 3;
			// Normal, GR画面, RGBボタン(Vシリーズ)
			n演奏RGBボタンY座標元データ[ 0, 1, 1 ].Drums  = 0;		// 未使用
			n演奏RGBボタンY座標元データ[ 0, 1, 1 ].Guitar = 44;
			n演奏RGBボタンY座標元データ[ 0, 1, 1 ].Bass   = 44;
			// Reverse, GR画面, RGBボタン(Vシリーズ)
			n演奏RGBボタンY座標元データ[ 1, 1, 1 ].Drums  = 0;		// 未使用
			n演奏RGBボタンY座標元データ[ 1, 1, 1 ].Guitar = 44;
			n演奏RGBボタンY座標元データ[ 1, 1, 1 ].Bass   = 44;
			#endregion

			n判定位置 = new STDGBVALUE<EJudgeLocation>();
			n判定位置.Drums  = EJudgeLocation.標準;
			n判定位置.Guitar = EJudgeLocation.標準;
			n判定位置.Bass   = EJudgeLocation.標準;

			// 補正値は、Normal/Reverse, Drums/GR画面共通
			nJudgeLinePosY_delta.Drums  = 0;
			nJudgeLinePosY_delta.Guitar = 0;
			nJudgeLinePosY_delta.Bass   = 0;
		}
    }
}
