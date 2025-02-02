﻿namespace FDK
{
	/// <summary>
	/// 一定間隔で単純増加する整数（カウント値）を扱う。
	/// </summary>
	/// <remarks>
	/// ○使い方
	/// 1.CCounterの変数をつくる。
	/// 2.CCounterを生成
	///   ctCounter = new CCounter( 0, 3, 10, CDTXMania.Timer );
	/// 3.進行メソッドを使用する。
	/// 4.ウマー。
	///
	/// double値を使う場合、tStartdb、t進行LoopDbを使うこと。
	/// また、double版では間隔の値はミリ秒単位ではなく、通常の秒単位になります。
	/// </remarks>
	public class CCounter
	{
		// 値プロパティ

		public int n開始値
		{
			get;
			private set;
		}
		public int n終了値
		{
			get;
			private set;
		}
		public int nCurrentValue
		{
			get;
			set;
		}
		public long n現在の経過時間ms
		{
			get;
			set;
		}

        public double db開始値
        {
            get;
            private set;
        }
        public double db終了値
        {
            get;
            private set;
        }
        public double db現在の値
        {
            get;
            set;
        }
        public double db現在の経過時間
        {
            get;
            set;
        }


		// 状態プロパティ

		public bool b進行中
		{
			get { return ( this.n現在の経過時間ms != -1 ); }
		}
		public bool b停止中
		{
			get { return !this.b進行中; }
		}
		public bool bEnded
		{
			get { return ( this.nCurrentValue >= this.n終了値 ); }
		}
		public bool b終了値に達してない
		{
			get { return !this.bEnded; }
		}

        /// <summary>通常のCCounterでは使用できません。</summary>
        public bool b進行中db
        {
            get { return ( this.db現在の経過時間 != -1 ); }
        }

        /// <summary>通常のCCounterでは使用できません。</summary>
        public bool b停止中db
        {
            get { return !this.b進行中db; }
        }

        /// <summary>通常のCCounterでは使用できません。</summary>
        public bool bEndeddb
        {
            get { return ( this.db現在の値 >= this.db終了値 ); }
        }

        /// <summary>通常のCCounterでは使用できません。</summary>
        public bool b終了値に達してないdb
        {
            get { return !this.bEndeddb; }
        }


		// コンストラクタ

		public CCounter()
		{
			this.timer = null;
			this.n開始値 = 0;
			this.n終了値 = 0;
			this.n間隔ms = 0;
			this.nCurrentValue = 0;
			this.n現在の経過時間ms = CTimer.n未使用;

            this.db開始値 = 0;
            this.db終了値 = 0;
            this.db間隔 = 0;
            this.db現在の値 = 0;
            this.db現在の経過時間 = CSoundTimer.n未使用;
		}

		/// <summary>生成と同時に開始する。</summary>
		public CCounter( int n開始値, int n終了値, int n間隔ms, CTimer timer )
			: this()
		{
			this.tStart( n開始値, n終了値, n間隔ms, timer );
		}

        /// <summary>生成と同時に開始する。(double版)</summary>
        public CCounter( double db開始値, double db終了値, double db間隔, CSoundTimer timer )
            : this()
        {
            this.tStart( db開始値, db終了値, db間隔 * 1000.0, timer );
        }


		// 状態操作メソッド

		/// <summary>
		/// カウントを開始する。
		/// </summary>
		/// <param name="n開始値">最初のカウント値。</param>
		/// <param name="n終了値">最後のカウント値。</param>
		/// <param name="n間隔ms">カウント値を１増加させるのにかける時間（ミリ秒単位）。</param>
		/// <param name="timer">カウントに使用するタイマ。</param>
		public void tStart( int n開始値, int n終了値, int n間隔ms, CTimer timer )
		{
			this.n開始値 = n開始値;
			this.n終了値 = n終了値;
			this.n間隔ms = n間隔ms;
			this.timer = timer;
			this.n現在の経過時間ms = this.timer.n現在時刻;
			this.nCurrentValue = n開始値;
            if (n間隔ms <= 0)
                n間隔ms = -n間隔ms;
		}

        /// <summary>
		/// カウントを開始する。(double版)
		/// </summary>
		/// <param name="db開始値">最初のカウント値。</param>
		/// <param name="db終了値">最後のカウント値。</param>
		/// <param name="db間隔">カウント値を１増加させるのにかける時間（秒単位）。</param>
		/// <param name="timer">カウントに使用するタイマ。</param>
		public void tStart( double db開始値, double db終了値, double db間隔, CSoundTimer timer )
		{
			this.db開始値 = db開始値;
			this.db終了値 = db終了値;
			this.db間隔 = db間隔;
			this.timerdb = timer;
			this.db現在の経過時間 = this.timerdb.dbシステム時刻;
			this.db現在の値 = db開始値;
            if (db間隔 <= 0)
                db間隔 = -db間隔;
		}

		/// <summary>
		/// 前回の tStart() の呼び出しからの経過時間をもとに、必要なだけカウント値を増加させる。
		/// カウント値が終了値に達している場合は、それ以上増加しない（終了値を維持する）。
		/// </summary>
		public void tStart()
		{
			if ( ( this.timer != null ) && ( this.n現在の経過時間ms != CTimer.n未使用 ) )
			{
				long num = this.timer.n現在時刻;
				if ( num < this.n現在の経過時間ms )
					this.n現在の経過時間ms = num;
                if (this.n間隔ms <= 0)
                    this.n間隔ms = -this.n間隔ms;

				while ( ( num - this.n現在の経過時間ms ) >= this.n間隔ms )
				{
					if ( ++this.nCurrentValue > this.n終了値 )
						this.nCurrentValue = this.n終了値;

					this.n現在の経過時間ms += this.n間隔ms;
				}
			}
		}

        /// <summary>
		/// 前回の tStart() の呼び出しからの経過時間をもとに、必要なだけカウント値を増加させる。
		/// カウント値が終了値に達している場合は、それ以上増加しない（終了値を維持する）。
		/// </summary>
		public void tStartdb()
		{
			if ( ( this.timerdb != null ) && ( this.db現在の経過時間 != CSoundTimer.n未使用 ) )
			{
				double num = this.timerdb.n現在時刻;
				if ( num < this.db現在の経過時間 )
					this.db現在の経過時間 = num;
                if (this.db間隔 <= 0)
                    this.db間隔 = -this.db間隔;

				while ( ( num - this.db現在の経過時間 ) >= this.db間隔 )
				{
					if ( ++this.db現在の値 > this.db終了値 )
						this.db現在の値 = this.db終了値;

					this.db現在の経過時間 += this.db間隔;
				}
			}
		}

		/// <summary>
		/// 前回の tStartLoop() の呼び出しからの経過時間をもとに、必要なだけカウント値を増加させる。
		/// カウント値が終了値に達している場合は、次の増加タイミングで開始値に戻る（値がループする）。
		/// </summary>
		public void tStartLoop()
		{
			if ( ( this.timer != null ) && ( this.n現在の経過時間ms != CTimer.n未使用 ) )
			{
				long num = this.timer.n現在時刻;
				if ( num < this.n現在の経過時間ms )
					this.n現在の経過時間ms = num;
                if (this.n間隔ms <= 0)
                    this.n間隔ms = -this.n間隔ms;

				while ( ( num - this.n現在の経過時間ms ) >= this.n間隔ms )
				{
					if ( ++this.nCurrentValue > this.n終了値 )
						this.nCurrentValue = this.n開始値;

					this.n現在の経過時間ms += this.n間隔ms;
				}
			}
		}

        /// <summary>
		/// 前回の tStartLoop() の呼び出しからの経過時間をもとに、必要なだけカウント値を増加させる。
		/// カウント値が終了値に達している場合は、次の増加タイミングで開始値に戻る（値がループする）。
		/// </summary>
		public void tStartLoopDb()
		{
			if ( ( this.timerdb != null ) && ( this.db現在の経過時間 != CSoundTimer.n未使用 ) )
			{
				double num = this.timerdb.n現在時刻;
				if ( num < this.n現在の経過時間ms )
					this.db現在の経過時間 = num;
                if (this.db間隔 <= 0)
                    this.db間隔 = -this.db間隔;

				while ( ( num - this.db現在の経過時間 ) >= this.db間隔 )
				{
					if ( ++this.db現在の値 > this.db終了値 )
						this.db現在の値 = this.db開始値;

					this.db現在の経過時間 += this.db間隔;
				}
			}
		}

		/// <summary>
		/// カウントを停止する。
		/// これ以降に tStart() や tStartLoop() を呼び出しても何も処理されない。
		/// </summary>
		public void tStop()
		{
			this.n現在の経過時間ms = CTimer.n未使用;
            this.db現在の経過時間 = CSoundTimer.n未使用;
		}


		// その他

		#region [ 応用：キーの反復入力をエミュレーションする ]
		//-----------------

		/// <summary>
		/// <para>「bキー押下」引数が true の間中、「tキー処理」デリゲート引数を呼び出す。</para>
		/// <para>ただし、2回目の呼び出しは1回目から 200ms の間を開けてから行い、3回目以降の呼び出しはそれぞれ 30ms の間隔で呼び出す。</para>
		/// <para>「bキー押下」が false の場合は何もせず、呼び出し回数を 0 にリセットする。</para>
		/// </summary>
		/// <param name="bキー押下">キーが押下されている場合は true。</param>
		/// <param name="tキー処理">キーが押下されている場合に実行する処理。</param>
		public void tキー反復( bool bキー押下, DGキー処理 tキー処理 )
		{
			const int n1回目 = 0;
			const int n2回目 = 1;
			const int n3回目以降 = 2;

			if ( bキー押下 )
			{
				switch ( this.nCurrentValue )
				{
					case n1回目:

						tキー処理();
						this.nCurrentValue = n2回目;
						this.n現在の経過時間ms = this.timer.n現在時刻;
						return;

					case n2回目:

						if ( ( this.timer.n現在時刻 - this.n現在の経過時間ms ) > 200 )
						{
							tキー処理();
							this.n現在の経過時間ms = this.timer.n現在時刻;
							this.nCurrentValue = n3回目以降;
						}
						return;

					case n3回目以降:

						if ( ( this.timer.n現在時刻 - this.n現在の経過時間ms ) > 30 )
						{
							tキー処理();
							this.n現在の経過時間ms = this.timer.n現在時刻;
						}
						return;
				}
			}
			else
			{
				this.nCurrentValue = n1回目;
			}
		}
		public delegate void DGキー処理();

		//-----------------
		#endregion

		#region [ private ]
		//-----------------
		private CTimer timer;
        private CSoundTimer timerdb;
		private int n間隔ms;
        private double db間隔;
		//-----------------
		#endregion
	}
}