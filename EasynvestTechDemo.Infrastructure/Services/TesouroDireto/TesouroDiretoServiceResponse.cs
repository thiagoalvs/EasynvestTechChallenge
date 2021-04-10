using EasynvestTechDemo.Domain.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasynvestTechDemo.Infrastructure.Services.TesouroDireto
{
    public class TesouroDiretoServiceResponse
    {
        [JsonProperty("tds")]
        public List<TesouroDiretoServiceResponseItem> Investments { get; set; }
    }

    public class TesouroDiretoServiceResponseItem
    {
        [JsonProperty("valorInvestido")]
        public double InvestedAmount { get; set; }

        [JsonProperty("valorTotal")]
        public double Amount { get; set; }

        [JsonProperty("vencimento")]
        public string ExpireDate { get; set; }

        [JsonProperty("dataDeCompra")]
        public string PurchaseDate { get; set; }

        [JsonProperty("iof")]
        public double Iof { get; set; }

        [JsonProperty("indice")]
        public string Index { get; set; }

        [JsonProperty("tipo")]
        public string Type { get; set; }

        [JsonProperty("nome")]
        public string Name { get; set; }

        [JsonIgnore]
        public EInvestmentType InvestmentType => EInvestmentType.TesouroDireto;
    }
}
