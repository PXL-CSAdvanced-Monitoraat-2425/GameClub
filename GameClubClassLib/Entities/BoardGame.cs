using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClubClassLib.Entities
{
    public class BoardGame : Game
    {
        public string Description { get; set; }
        public double DistributorPrice { get; set; }


        public BoardGame(int id, string title, int rank, string source, int releaseYear, double geekRating, double averageRating, int numberOfVoters, string description, double distributorPrice)
            : base(id, title, rank, source, releaseYear, geekRating, averageRating, numberOfVoters)
        {
            Description = description;
            DistributorPrice = distributorPrice;
        }

        public override double GetAmazonPrice()
        {
            return DistributorPrice * 1.20;
        }

        public override double GetGeekGameShopPrice()
        {
            return DistributorPrice * (Rank > 50 ? 1.15 : 1.25);
        }
    }
}
