using GameClubClassLib.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClubClassLib.DataAccess
{
    public static class BoardGameData
    {
        public static DataTable BoardGameDataTable { get; set; }

        public static void InitialiseerBoardGameData(string bgCsvPath)
        {
            BoardGameDataTable = new DataTable();
            string[] header;

            using (StreamReader reader = new StreamReader(bgCsvPath))
            {
                header = reader.ReadLine().Split(";");
                for (int i = 0; i < header.Length; i++)
                {
                    BoardGameDataTable.Columns.Add(header[i]);
                }
                string[] fields;
                while (!reader.EndOfStream)
                {
                    fields = reader.ReadLine().Split(";");
                    BoardGameDataTable.Rows.Add(fields);
                    //DataRow dataRow = BoardGameDataTable.NewRow();
                    //dataRow[header[0]] = fields[0];
                    //dataRow[header[1]] = int.Parse(fields[1]);
                }
            }
        }

        public static List<BoardGame> GetBoardGameList()
        {
            //List<BoardGame> retval = new List<BoardGame>();
            //foreach (DataRow x in BoardGameDataTable.Rows)
            //{
            //    retval.Add(
            //        new BoardGame(
            //        Convert.ToInt32(x["BoardGameId"]),
            //        x["Title"].ToString(),
            //        Convert.ToInt32(x["Rank"]),
            //        x["ImageSource"].ToString(),
            //        Convert.ToInt32(x["ReleaseYear"]),
            //        Convert.ToDouble(x["GeekRating"]),
            //        Convert.ToDouble(x["AverageRating"]),
            //        Convert.ToInt32(x["NumberOfVotes"]),
            //        x["Description"].ToString(),
            //        Convert.ToDouble(x["DistributorPrice"]))
            //        );
            //}
            return BoardGameDataTable.AsEnumerable().Select(
                x => new BoardGame(
                    Convert.ToInt32(x["BoardGameId"]),
                    x["Title"].ToString(),
                    Convert.ToInt32(x["Rank"]),
                    x["ImageSource"].ToString(),
                    Convert.ToInt32(x["ReleaseYear"]),
                    Convert.ToDouble(x["GeekRating"]),
                    Convert.ToDouble(x["AverageRating"]),
                    Convert.ToInt32(x["NumberOfVotes"]),
                    x["Description"].ToString(),
                    Convert.ToDouble(x["DistributorPrice"]))
                ).OrderBy(x => x.Rank).ToList();
        }
    }
}
