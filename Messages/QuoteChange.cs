#region S# License
/******************************************************************************************
NOTICE!!!  This program and source code is owned and licensed by
StockSharp, LLC, www.stocksharp.com
Viewing or use of this code requires your acceptance of the license
agreement found at https://github.com/StockSharp/StockSharp/blob/master/LICENSE
Removal of this comment is a violation of the license agreement.

Project: StockSharp.Messages.Messages
File: QuoteChange.cs
Created: 2015, 11, 11, 2:32 PM

Copyright 2010 by StockSharp, LLC
*******************************************************************************************/
#endregion S# License
namespace StockSharp.Messages
{
	using System;
	using System.Collections.Generic;
	using System.Runtime.Serialization;
	using System.Xml.Serialization;

	using Ecng.Common;
	using Ecng.Serialization;

	using StockSharp.Localization;

	/// <summary>
	/// Change actions.
	/// </summary>
	[System.Runtime.Serialization.DataContract]
	[Serializable]
	public enum QuoteChangeActions : byte
	{
		/// <summary>
		/// New quote for <see cref="QuoteChange.StartPosition"/>.
		/// </summary>
		[EnumMember]
		New,

		/// <summary>
		/// Update quote for <see cref="QuoteChange.StartPosition"/>.
		/// </summary>
		[EnumMember]
		Update,

		/// <summary>
		/// Delete quotes from <see cref="QuoteChange.StartPosition"/> till <see cref="QuoteChange.EndPosition"/>.
		/// </summary>
		[EnumMember]
		Delete,
	}

	/// <summary>
	/// Market depth quote representing bid or ask.
	/// </summary>
	[System.Runtime.Serialization.DataContract]
	[Serializable]
	[DisplayNameLoc(LocalizedStrings.Str273Key)]
	[DescriptionLoc(LocalizedStrings.Str274Key)]
	public class QuoteChange : Equatable<QuoteChange>, IExtendableEntity
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="QuoteChange"/>.
		/// </summary>
		public QuoteChange()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="QuoteChange"/>.
		/// </summary>
		/// <param name="side">Direction (buy or sell).</param>
		/// <param name="price">Quote price.</param>
		/// <param name="volume">Quote volume.</param>
		/// <param name="ordersCount">Orders count.</param>
		public QuoteChange(Sides side, decimal price, decimal volume, int? ordersCount = null)
		{
			Side = side;
			Price = price;
			Volume = volume;
			OrdersCount = ordersCount;
		}

		/// <summary>
		/// Quote price.
		/// </summary>
		[DataMember]
		[DisplayNameLoc(LocalizedStrings.PriceKey)]
		[DescriptionLoc(LocalizedStrings.Str275Key)]
		[MainCategory]
		public decimal Price { get; set; }

		/// <summary>
		/// Quote volume.
		/// </summary>
		[DataMember]
		[DisplayNameLoc(LocalizedStrings.VolumeKey)]
		[DescriptionLoc(LocalizedStrings.Str276Key)]
		[MainCategory]
		public decimal Volume { get; set; }

		/// <summary>
		/// Direction (buy or sell).
		/// </summary>
		[DataMember]
		[DisplayNameLoc(LocalizedStrings.Str128Key)]
		[DescriptionLoc(LocalizedStrings.Str277Key)]
		[MainCategory]
		public Sides Side { get; set; }

		/// <summary>
		/// Electronic board code.
		/// </summary>
		[DataMember]
		[DisplayNameLoc(LocalizedStrings.BoardKey)]
		[DescriptionLoc(LocalizedStrings.BoardCodeKey, true)]
		[MainCategory]
		public string BoardCode { get; set; }

		[field: NonSerialized]
		private IDictionary<string, object> _extensionInfo;

		/// <inheritdoc />
		[Ignore]
		[XmlIgnore]
		[DisplayNameLoc(LocalizedStrings.ExtendedInfoKey)]
		[DescriptionLoc(LocalizedStrings.Str427Key)]
		[MainCategory]
		public IDictionary<string, object> ExtensionInfo
		{
			get => _extensionInfo;
			set => _extensionInfo = value;
		}

		/// <summary>
		/// Orders count.
		/// </summary>
		[DataMember]
		[Nullable]
		public int? OrdersCount { get; set; }

		/// <summary>
		/// Start position, related for <see cref="Action"/>.
		/// </summary>
		[DataMember]
		[Nullable]
		public int? StartPosition { get; set; }

		/// <summary>
		/// End position, related for <see cref="Action"/>.
		/// </summary>
		[DataMember]
		[Nullable]
		public int? EndPosition { get; set; }

		/// <summary>
		/// Change action.
		/// </summary>
		[DataMember]
		[Nullable]
		public QuoteChangeActions? Action { get; set; }

		/// <summary>
		/// Create a copy of <see cref="QuoteChange"/>.
		/// </summary>
		/// <returns>Copy.</returns>
		public override QuoteChange Clone()
		{
			var clone = new QuoteChange(Side, Price, Volume, OrdersCount)
			{
				StartPosition = StartPosition,
				EndPosition = EndPosition,
				Action = Action,
			};
			this.CopyExtensionInfo(clone);
			return clone;
		}

		/// <summary>
		/// Compare <see cref="QuoteChange"/> on the equivalence.
		/// </summary>
		/// <param name="other">Another value with which to compare.</param>
		/// <returns><see langword="true" />, if the specified object is equal to the current object, otherwise, <see langword="false" />.</returns>
		protected override bool OnEquals(QuoteChange other)
		{
			return Price == other.Price && Side == other.Side;
		}

		/// <summary>
		/// Get the hash code of the object <see cref="QuoteChange"/>.
		/// </summary>
		/// <returns>A hash code.</returns>
		public override int GetHashCode()
		{
			return Price.GetHashCode() ^ Side.GetHashCode();
		}

		/// <inheritdoc />
		public override string ToString()
		{
			var side = Side == Sides.Buy ? LocalizedStrings.Bid : LocalizedStrings.Ask;
			return $"{side} {Price} {Volume}";
		}
	}
}