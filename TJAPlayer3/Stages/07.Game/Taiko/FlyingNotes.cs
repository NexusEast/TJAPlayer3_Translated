using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Runtime.InteropServices;
using SlimDX;
using FDK;

namespace TJAPlayer3
{
	internal class FlyingNotes : CActivity
	{
		// コンストラクタ

		public FlyingNotes()
		{
            base.bDeactivated = true;
        }
		
		
		// メソッド
        public virtual void Start( int nLane, int nPlayer, bool isRoll = false )
		{
            if (TJAPlayer3.Tx.Notes != null)
            {
                for (int i = 0; i < 128; i++)
                {
                    if(!Flying[i].IsUsing)
                    {
                        // 初期化
                        Flying[i].IsUsing = true;
                        Flying[i].Lane = nLane;
                        Flying[i].Player = nPlayer;
                        Flying[i].X = StartPointX[nPlayer];
                        Flying[i].Y = StartPointY[nPlayer];
                        Flying[i].StartPointX = StartPointX[nPlayer];
                        Flying[i].StartPointY = StartPointY[nPlayer];
                        Flying[i].OldValue = 0;
                        Flying[i].IsRoll = isRoll;
                        // 角度の決定
                        Flying[i].Height = Math.Abs(TJAPlayer3.Skin.Game_Effect_FlyingNotes_EndPoint_Y[nPlayer] - StartPointY[nPlayer]);
                        Flying[i].Width = Math.Abs((TJAPlayer3.Skin.Game_Effect_FlyingNotes_EndPoint_X[nPlayer] - StartPointX[nPlayer])) / 2;
                        //Console.WriteLine("{0}, {1}", width2P, height2P);
                        Flying[i].Theta = ((Math.Atan2(Flying[i].Height, Flying[i].Width) * 180.0) / Math.PI);
                        Flying[i].Counter = new CCounter(0, (180), TJAPlayer3.Skin.Game_Effect_FlyingNotes_Timer, TJAPlayer3.Timer);
                        //Flying[i].Counter = new CCounter(0, 200000, CDTXMania.Skin.Game_Effect_FlyingNotes_Timer, CDTXMania.Timer);

                        Flying[i].IncreaseX = (1.00 * Math.Abs((TJAPlayer3.Skin.Game_Effect_FlyingNotes_EndPoint_X[nPlayer] - StartPointX[nPlayer]))) / (180);
                        Flying[i].IncreaseY = (1.00 * Math.Abs((TJAPlayer3.Skin.Game_Effect_FlyingNotes_EndPoint_Y[nPlayer] - StartPointY[nPlayer]))) / (180);
                        break;
                    }
                }
            }
        }

		// CActivity 実装

		public override void On活性化()
		{
            for (int i = 0; i < 128; i++)
            {
                Flying[i] = new Status();
                Flying[i].IsUsing = false;
                Flying[i].Counter = new CCounter();
            }
            for (int i = 0; i < 2; i++)
            {
                StartPointX[i] = TJAPlayer3.Skin.Game_Effect_FlyingNotes_StartPoint_X[i];
            }
            for (int i = 0; i < 2; i++)
            {
                StartPointY[i] = TJAPlayer3.Skin.Game_Effect_FlyingNotes_StartPoint_Y[i];
            }
            base.On活性化();
		}
		public override void On非活性化()
		{
            for (int i = 0; i < 128; i++)
            {
                Flying[i].Counter = null;
            }
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
                for (int i = 0; i < 128; i++)
                {
                    if (Flying[i].IsUsing)
                    {
                        Flying[i].OldValue = Flying[i].Counter.nCurrentValue;
                        Flying[i].Counter.tStart();
                        if (Flying[i].Counter.bEnded)
                        {
                            Flying[i].Counter.tStop();
                            Flying[i].IsUsing = false;
                            TJAPlayer3.stage演奏ドラム画面.actGauge.Start(Flying[i].Lane, E判定.Perfect, Flying[i].
                                Player);
                            TJAPlayer3.stage演奏ドラム画面.actChipEffects.Start(Flying[i].Player, Flying[i].Lane);
                        }
                        for (int n = Flying[i].OldValue; n < Flying[i].Counter.nCurrentValue; n++)
                        {
                            if(TJAPlayer3.Skin.Game_Effect_FlyingNotes_IsUsingEasing)
                            {
                                Flying[i].X = Flying[i].StartPointX + Flying[i].Width + ((-Math.Cos(Flying[i].Counter.nCurrentValue * (Math.PI / 180)) * Flying[i].Width));
                                //Flying[i].X += (Math.Cos(Flying[i].Counter.nCurrentValue * (Math.PI / 180))) * Flying[i].Increase;
                            }
                            else
                            {
                                Flying[i].X += Flying[i].IncreaseX;
                            }

                            if (n % TJAPlayer3.Skin.Game_Effect_FireWorks_Timing == 0 && !Flying[i].IsRoll && Flying[i].Counter.nCurrentValue > 18)
                            {
                                if (Flying[i].Lane == 3 || Flying[i].Lane == 4)
                                {
                                    TJAPlayer3.stage演奏ドラム画面.FireWorks.Start(Flying[i].Lane, Flying[i].Player, Flying[i].X, Flying[i].Y);
                                }
                            }


                            if (Flying[i].Player == 0)
                            {
                                Flying[i].Y = (TJAPlayer3.Skin.Game_Effect_FlyingNotes_StartPoint_Y[Flying[i].Player]) + -Math.Sin(Flying[i].Counter.nCurrentValue * (Math.PI / 180)) * TJAPlayer3.Skin.Game_Effect_FlyingNotes_Sine;
                                Flying[i].Y -= Flying[i].IncreaseY * Flying[i].Counter.nCurrentValue;
                            }
                            else
                            {
                                Flying[i].Y = (TJAPlayer3.Skin.Game_Effect_FlyingNotes_StartPoint_Y[Flying[i].Player]) + Math.Sin(Flying[i].Counter.nCurrentValue * (Math.PI / 180)) * TJAPlayer3.Skin.Game_Effect_FlyingNotes_Sine;
                                Flying[i].Y += Flying[i].IncreaseY * Flying[i].Counter.nCurrentValue;
                            }

                        }
                        //Flying[i].OldValue = Flying[i].Counter.nCurrentValue;

                        if (Flying[i].Player == 0)
                        {
                            TJAPlayer3.Tx.Notes?.t2D中心基準描画(TJAPlayer3.app.Device, (int)Flying[i].X, (int)Flying[i].Y, new Rectangle(Flying[i].Lane * 130, 0, 130, 130));
                        }
                        else if (Flying[i].Player == 1)
                        {
                            //
                            TJAPlayer3.Tx.Notes?.t2D中心基準描画(TJAPlayer3.app.Device, (int)Flying[i].X, (int)Flying[i].Y, new Rectangle(Flying[i].Lane * 130, 0, 130, 130));
                        }
                    }
                }
			}
            return base.OnDraw();
        }
		

		#region [ private ]
		//-----------------

        [StructLayout(LayoutKind.Sequential)]
        private struct Status
        {
            public int Lane;
            public int Player;
            public bool IsUsing;
            public CCounter Counter;
            public int OldValue;
            public double X;
            public double Y;
            public int Height;
            public int Width;
            public double IncreaseX;
            public double IncreaseY;
            public bool IsRoll;
            public int StartPointX;
            public int StartPointY;
            public double Theta;
        }

        private Status[] Flying = new Status[128];

        public readonly int[] StartPointX = new int[2];
        public readonly int[] StartPointY = new int[2];

        //-----------------
        #endregion
    }
}
