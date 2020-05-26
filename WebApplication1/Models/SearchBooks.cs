﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
        public partial class SearchBooks
        {
            //[JsonProperty("kind")]
            //public string Kind { get; set; }

            //[JsonProperty("totalItems")]
            //public long TotalItems { get; set; }

            [JsonProperty("items")]
            public List<Item> Items { get; set; }
        }

        public partial class Item
        {
            //[JsonProperty("kind")]
            //public string Kind { get; set; }

            //[JsonProperty("id")]
            //public string Id { get; set; }

            //[JsonProperty("etag")]
            //public string Etag { get; set; }

            //[JsonProperty("selfLink")]
            //public Uri SelfLink { get; set; }

            [JsonProperty("volumeInfo")]
            public VolumeInfo VolumeInfo { get; set; }

            [JsonProperty("saleInfo")]
            public SaleInfo SaleInfo { get; set; }

            //[JsonProperty("accessInfo")]
            //public AccessInfo AccessInfo { get; set; }

            //[JsonProperty("searchInfo")]
            //public SearchInfo SearchInfo { get; set; }
        }

        //public partial class AccessInfo
        //{
        //    [JsonProperty("country")]
        //    public string Country { get; set; }

        //    [JsonProperty("viewability")]
        //    public string Viewability { get; set; }

        //    [JsonProperty("embeddable")]
        //    public bool Embeddable { get; set; }

        //    [JsonProperty("publicDomain")]
        //    public bool PublicDomain { get; set; }

        //    [JsonProperty("textToSpeechPermission")]
        //    public string TextToSpeechPermission { get; set; }

        //    [JsonProperty("epub")]
        //    public Epub Epub { get; set; }

        //    [JsonProperty("pdf")]
        //    public Epub Pdf { get; set; }

        //    [JsonProperty("webReaderLink")]
        //    public Uri WebReaderLink { get; set; }

        //    [JsonProperty("accessViewStatus")]
        //    public string AccessViewStatus { get; set; }

        //    [JsonProperty("quoteSharingAllowed")]
        //    public bool QuoteSharingAllowed { get; set; }
        //}

        //public partial class Epub
        //{
        //    [JsonProperty("isAvailable")]
        //    public bool IsAvailable { get; set; }

        //    [JsonProperty("acsTokenLink")]
        //    public Uri AcsTokenLink { get; set; }
        //}

        public partial class SaleInfo
        {
            //    [JsonProperty("country")]
            //    public string Country { get; set; }

            [JsonProperty("saleability")]
            public string Saleability { get; set; }

            //    [JsonProperty("isEbook")]
            //    public bool IsEbook { get; set; }

            //    [JsonProperty("listPrice")]
            //    public SaleInfoListPrice ListPrice { get; set; }

            //    [JsonProperty("retailPrice")]
            //    public SaleInfoListPrice RetailPrice { get; set; }

            //    [JsonProperty("buyLink")]
            //    public Uri BuyLink { get; set; }

            //    [JsonProperty("offers")]
            //    public List<Offer> Offers { get; set; }
        }

        //public partial class SaleInfoListPrice
        //{
        //    [JsonProperty("amount")]
        //    public double Amount { get; set; }

        //    [JsonProperty("currencyCode")]
        //    public string CurrencyCode { get; set; }
        //}

        //public partial class Offer
        //{
        //    [JsonProperty("finskyOfferType")]
        //    public long FinskyOfferType { get; set; }

        //    [JsonProperty("listPrice")]
        //    public OfferListPrice ListPrice { get; set; }

        //    [JsonProperty("retailPrice")]
        //    public OfferListPrice RetailPrice { get; set; }
        //}

        //public partial class OfferListPrice
        //{
        //    [JsonProperty("amountInMicros")]
        //    public long AmountInMicros { get; set; }

        //    [JsonProperty("currencyCode")]
        //    public string CurrencyCode { get; set; }
        //}

        //public partial class SearchInfo
        //{
        //    [JsonProperty("textSnippet")]
        //    public string TextSnippet { get; set; }
        //}

        public partial class VolumeInfo
        {
            [JsonProperty("title")]
            public string Title { get; set; }

            [JsonProperty("authors")]
            public List<string> Authors { get; set; }

            [JsonProperty("publisher")]
            public string Publisher { get; set; }

            //[JsonProperty("publishedDate")]
            //public DateTimeOffset PublishedDate { get; set; }

            [JsonProperty("description")]
            public string Description { get; set; }

            [JsonProperty("categories")]
            public List<string> Categories { get; set; }


            //[JsonProperty("industryIdentifiers")]
            //public List<IndustryIdentifier> IndustryIdentifiers { get; set; }

            //[JsonProperty("readingModes")]
            //public ReadingModes ReadingModes { get; set; }

            //[JsonProperty("pageCount")]
            //public long PageCount { get; set; }

            //[JsonProperty("printType")]
            //public string PrintType { get; set; }



            //[JsonProperty("averageRating")]
            //public long AverageRating { get; set; }

            //[JsonProperty("ratingsCount")]
            //public long RatingsCount { get; set; }

            //[JsonProperty("maturityRating")]
            //public string MaturityRating { get; set; }

            //[JsonProperty("allowAnonLogging")]
            //public bool AllowAnonLogging { get; set; }

            //[JsonProperty("contentVersion")]
            //public string ContentVersion { get; set; }

            //[JsonProperty("panelizationSummary")]
            //public PanelizationSummary PanelizationSummary { get; set; }

            //[JsonProperty("imageLinks")]
            //public ImageLinks ImageLinks { get; set; }

            [JsonProperty("language")]
            public string Language { get; set; }

            [JsonProperty("previewLink")]
            public Uri PreviewLink { get; set; }

            //[JsonProperty("infoLink")]
            //public Uri InfoLink { get; set; }

            //[JsonProperty("canonicalVolumeLink")]
            //public Uri CanonicalVolumeLink { get; set; }

        }
        //public partial class ImageLinks
        //{
        //    [JsonProperty("smallThumbnail")]
        //    public Uri SmallThumbnail { get; set; }

        //    [JsonProperty("thumbnail")]
        //    public Uri Thumbnail { get; set; }
        //}

        //public partial class IndustryIdentifier
        //{
        //    [JsonProperty("type")]
        //    public string Type { get; set; }

        //    [JsonProperty("identifier")]
        //    public string Identifier { get; set; }
        //}

        //public partial class PanelizationSummary
        //{
        //    [JsonProperty("containsEpubBubbles")]
        //    public bool ContainsEpubBubbles { get; set; }

        //    [JsonProperty("containsImageBubbles")]
        //    public bool ContainsImageBubbles { get; set; }
        //}

        //public partial class ReadingModes
        //{
        //    [JsonProperty("text")]
        //    public bool Text { get; set; }

        //    [JsonProperty("image")]
        //    public bool Image { get; set; }
        //}
}

