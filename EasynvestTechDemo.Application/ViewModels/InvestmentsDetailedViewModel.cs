using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasynvestTechDemo.Application.ViewModels
{
    public class InvestmentsDetailedViewModel
    {
        [JsonProperty("valorTotal")]
        public double AmountTotal { get; set; }

        [JsonProperty("investimentos")]
        public List<InvestmentsDetailedViewModelItem> Investments { get; set; }


    }

    public class InvestmentsDetailedViewModelItem
    {
        [JsonProperty("nome")]
        public string Name { get; set; }

        [JsonProperty("valorInvestido")]
        public double InvestedAmount { get; set; }

        [JsonProperty("valorTotal")]
        public double Amount { get; set; }

        [JsonProperty("vencimento")]
        public DateTime ExpireDate { get; set; }

        [JsonProperty("Ir")]
        public double IncomeTax { get; set; }

        [JsonProperty("valorResgate")]
        public double DrawAmount { get; set; }

    }
}
