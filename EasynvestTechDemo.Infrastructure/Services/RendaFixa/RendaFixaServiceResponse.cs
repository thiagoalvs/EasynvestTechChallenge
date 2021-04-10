using EasynvestTechDemo.Domain.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasynvestTechDemo.Infrastructure.Services.RendaFixa
{
    public class RendaFixaServiceResponse
    {
        [JsonProperty("lcis")]
        public List<RendaFixaServiceResponseItem> Investments { get; set; }
    }

    public class RendaFixaServiceResponseItem
    {
        [JsonProperty("capitalInvestido")]
        public double InvestedAmount { get; set; }

        [JsonProperty("capitalAtual")]
        public double Amount { get; set; }

        [JsonProperty("quantidade")]
        public double Qtd { get; set; }

        [JsonProperty("vencimento")]
        public string ExpireDate { get; set; }        

        [JsonProperty("iof")]
        public double Iof { get; set; }

        [JsonProperty("outrasTaxas")]
        public double OtherTaxes { get; set; }

        [JsonProperty("taxas")]
        public double Taxes { get; set; }

        [JsonProperty("indice")]
        public string Index { get; set; }

        [JsonProperty("tipo")]
        public string Type { get; set; }

        [JsonProperty("nome")]
        public string Name { get; set; }

        [JsonProperty("guarantidoFGC")]
        public bool FGCgranted { get; set; }

        [JsonProperty("dataOperacao")]
        public string PurchaseDate { get; set; }

        [JsonProperty("precoUnitario")]
        public double PriceUn { get; set; }

        [JsonProperty("primario")]
        public bool Primary { get; set; }

        [JsonIgnore]
        public EInvestmentType InvestmentType => EInvestmentType.RendaFixa;
    }
}
