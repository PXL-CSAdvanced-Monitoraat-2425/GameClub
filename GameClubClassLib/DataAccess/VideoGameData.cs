using GameClubClassLib.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClubClassLib.DataAccess
{
    public static class VideoGameData
    {
        public static DataTable VideoGameDataTable { get; set; }

        public static void InitialiseerVideoGameData(string videoGameCsvPath)
        {
            VideoGameDataTable = new DataTable();
            string[] header;

            using (StreamReader reader = new StreamReader(videoGameCsvPath))
            {
                header = reader.ReadLine().Split(";");
                for (int i = 0; i < header.Length; i++)
                {
                    VideoGameDataTable.Columns.Add(header[i]);
                }
                string[] fields;
                while (!reader.EndOfStream)
                {
                    fields = reader.ReadLine().Split(";");
                    VideoGameDataTable.Rows.Add(fields);
                }
            }
        }
        public static List<VideoGame> GetVideoGameList()
        {
            return VideoGameDataTable.AsEnumerable().Select(
                x =>
                new VideoGame(
                    Convert.ToInt32(x["VideoGameId"]),
                    x["Title"].ToString(),
                    Convert.ToInt32(x["Rank"]),
                    x["ImageSource"].ToString(),
                    Convert.ToInt32(x["ReleaseYear"]),
                    Convert.ToDouble(x["GeekRating"]),
                    Convert.ToDouble(x["AverageRating"]),
                    Convert.ToInt32(x["NumberOfVotes"]),
                    Convert.ToBoolean(x["IsSinglePlayerOnly"])
                )).OrderBy(x => x.Rank).ToList();
        }
    }
}
