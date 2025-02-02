﻿using System.Collections.Generic;
using TJAPlayer3.C曲リストノードComparers;

namespace TJAPlayer3
{
	internal class CActSortSongs : CActSelectPopupMenu
	{

		// コンストラクタ

		public CActSortSongs()
		{
			List<CItemBase> lci = new List<CItemBase>();
			lci.Add( new CItemList( "絶対パス",		CItemBase.Eパネル種別.通常, 0, "", "", new string[] { "Z,Y,X,...",		"A,B,C,..." } ) );
			lci.Add( new CItemList( "曲名",		CItemBase.Eパネル種別.通常, 0, "", "", new string[] { "Z,Y,X,...",		"A,B,C,..." } ) );
            //lci.Add( new CItemList( "Level",		CItemBase.Eパネル種別.通常, 0, "", "", new string[] { "99,98,97,...",	"1,2,3,..." } ) );
            //lci.Add( new CItemList( "Best Rank",	CItemBase.Eパネル種別.通常, 0, "", "", new string[] { "E,D,C,...",		"SS,S,A,..." } ) );
            //lci.Add( new CItemList( "PlayCount",	CItemBase.Eパネル種別.通常, 0, "", "", new string[] { "10,9,8,...",		"1,2,3,..." } ) );
            //lci.Add( new CItemList( "Author",		CItemBase.Eパネル種別.通常, 0, "", "", new string[] { "Z,Y,X,...",		"A,B,C,..." } ) );
            //lci.Add( new CItemList( "SkillPoint",	CItemBase.Eパネル種別.通常, 0, "", "", new string[] { "100,99,98,...",	"1,2,3,..." } ) );
#if TEST_SORTBGM
			lci.Add( new CItemList( "BPM",			CItemBase.Eパネル種別.通常, 0, "", "", new string[] { "300,200,...",	"70,80,90,..." } ) );
#endif
			lci.Add( new CItemList( "ジャンル",			CItemBase.Eパネル種別.通常, 0, "", "", new string[] { "AC15",	"AC8-14" } ) );

		    lci.Add( new CItemList( "Rating",			CItemBase.Eパネル種別.通常, 0, "", "", C曲リストノードComparerRating.GetItemListValues()) );

			lci.Add( new CItemList( "戻る",		CItemBase.Eパネル種別.通常, 0, "", "", new string[] { "", 				"" } ) );
			
			base.Initialize( lci, false, "SORT MENU" );
		}


		// メソッド
		public void tActivatePopupMenu( EInstrumentPart einst, ref CActSelect曲リスト ca )
		{
		    this.act曲リスト = ca;
			base.tActivatePopupMenu( einst );
		}
		//public void tDeativatePopupMenu()
		//{
		//	base.tDeativatePopupMenu();
		//}


		public override void tEnter押下Main( int itemIndex )
		{
		    int ItemIndexToSortOrder(int nSortOrder)
		    {
		        return nSortOrder * 2 - 1; // 0,1  => -1, 1
		    }

		    switch ( (EOrder)n現在の選択行 )
			{
				case EOrder.Path:
					this.act曲リスト.t曲リストのソート(
					    CSongs管理.t曲リストのソート1_絶対パス順, eInst, ItemIndexToSortOrder(itemIndex)
					);
					this.act曲リスト.t選択曲が変更された(true);
					break;
				case EOrder.Title:
					this.act曲リスト.t曲リストのソート(
					    CSongs管理.t曲リストのソート2_タイトル順, eInst, ItemIndexToSortOrder(itemIndex)
					);
					this.act曲リスト.t選択曲が変更された(true);
					break;
                //case (int) EOrder.Level:
                //    this.act曲リスト.t曲リストのソート(
                //        CSongs管理.t曲リストのソート4_LEVEL順, eInst, ItemIndexToSortOrder(itemIndex),
                //        this.act曲リスト.n現在のアンカ難易度レベル
                //    );
                //    this.act曲リスト.t選択曲が変更された( true );
                //    break;
                //case (int) EOrder.BestRank:
                //    this.act曲リスト.t曲リストのソート(
                //        CSongs管理.t曲リストのソート5_BestRank順, eInst, ItemIndexToSortOrder(itemIndex),
                //        this.act曲リスト.n現在のアンカ難易度レベル
                //    );
                //    break;
                //case (int) EOrder.PlayCount:
                //    // this.act曲リスト.t曲リストのソート3_演奏回数の多い順( eInst, ItemIndexToSortOrder(itemIndex) );
                //    this.act曲リスト.t曲リストのソート(
                //        CSongs管理.t曲リストのソート3_演奏回数の多い順, eInst, ItemIndexToSortOrder(itemIndex),
                //        this.act曲リスト.n現在のアンカ難易度レベル
                //    );
                //    this.act曲リスト.t選択曲が変更された( true );
                //    break;
                //case (int) EOrder.Author:
                //    this.act曲リスト.t曲リストのソート(
                //        CSongs管理.t曲リストのソート8_アーティスト名順, eInst, ItemIndexToSortOrder(itemIndex),
                //        this.act曲リスト.n現在のアンカ難易度レベル
                //    );
                //    this.act曲リスト.t選択曲が変更された( true );
                //    break;
                //case (int) EOrder.SkillPoint:
                //    this.act曲リスト.t曲リストのソート(
                //        CSongs管理.t曲リストのソート6_SkillPoint順, eInst, ItemIndexToSortOrder(itemIndex),
                //        this.act曲リスト.n現在のアンカ難易度レベル
                //    );
                //    this.act曲リスト.t選択曲が変更された( true );
                //    break;
#if TEST_SORTBGM
						case (int) ESortItem.BPM:
						this.act曲リスト.t曲リストのソート(
							CSongs管理.t曲リストのソート9_BPM順, eInst, ItemIndexToSortOrder(itemIndex),
							this.act曲リスト.n現在のアンカ難易度レベル
						);
					this.act曲リスト.t選択曲が変更された(true);
						break;
#endif
                //ジャンル順
				case EOrder.Genre:
					this.act曲リスト.t曲リストのソート(
                        //CDTXMania.Songs管理.t曲リストのソート7_更新日時順, eInst, ItemIndexToSortOrder(itemIndex),
                        //this.act曲リスト.n現在のアンカ難易度レベル
                        CSongs管理.t曲リストのソート9_ジャンル順, eInst, ItemIndexToSortOrder(itemIndex), 0
					);
					this.act曲リスト.t選択曲が変更された( true );
					break;
                case EOrder.Rating:
			        this.act曲リスト.t曲リストのソート(
			            CSongs管理.t曲リストのソート10_Rating順, eInst, itemIndex, 0
			        );
			        this.act曲リスト.t選択曲が変更された( true );
			        break;
				case EOrder.Return:
					this.tDeativatePopupMenu();
					break;
				default:
					break;
			}
		}

	    // CActivity 実装

		public override void On活性化()
		{
            //this.e現在のソート = EOrder.Title;
			base.On活性化();
		}
		public override void On非活性化()
		{
			if( !base.bDeactivated )
			{
				base.On非活性化();
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
			base.OnManagedDisposed();
		}

		#region [ private ]
		//-----------------

		private CActSelect曲リスト act曲リスト;

		private enum EOrder : int
		{
            Path = 0,
			Title = 1,
		    Genre = 2,
		    Rating = 3,
            Return = 4
		}

		//-----------------
		#endregion
	}


}
