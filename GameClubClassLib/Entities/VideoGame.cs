using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClubClassLib.Entities
{
    public class VideoGame : Game
    {
        public bool IsSinglePlayerOnly { get; set; }

        public VideoGame(int id, string title, int rank, string source, int releaseYear, double geekRating, double averageRating, int numberOfVoters, bool isSinglePlayerOnly) 
            : base(id, title, rank, source, releaseYear, geekRating, averageRating, numberOfVoters)
        {
            IsSinglePlayerOnly = isSinglePlayerOnly;
        }

        public override double GetAmazonPrice()
        {
            if (ReleaseYear < 1990)
            {
                return 9.99;
            }
            else if (ReleaseYear < 2010)
            {
                return 59.99;
            }
            else
            {
                return 69.99;
            }
        }

        public override double GetGeekGameShopPrice()
        {
            double price;
            if (ReleaseYear < 2000)
            {
                price = 29.99;
            }
            else
            {
                price = 49.99;
            }
            return price * (Rank <= 50 ? 1.5 : 1);
        }

        public override string ToString()
        {
            return $"{Rank} - {GeekRating} - {Title}";
        }
    }
}
