using System;
using System.Collections.Generic;
using System.Linq;
using JsonData;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Collections to work with
            List<Artist> Artists = JsonToFile<Artist>.ReadJson();
            List<Group> Groups = JsonToFile<Group>.ReadJson();

            //========================================================
            //Solve all of the prompts below using various LINQ queries
            //========================================================

            //There is only one artist in this collection from Mount Vernon, what is their name and age?
            var ArtistFromMountVernon = from artist in Artists where artist.Hometown == "Mount Vernon" select new {artist.RealName};
            foreach (var i in ArtistFromMountVernon)
            {
                Console.WriteLine(i);
            }
            //Who is the youngest artist in our collection of artists?
            var YoungestArtist = Artists.OrderBy(artist => artist.Age).First();
            Console.WriteLine(YoungestArtist.ArtistName + YoungestArtist.Age);

            var YoungestAge = Artists.Min(artist => artist.Age);
            var YoungestArtist2 = Artists.Where(artist => artist.Age == YoungestAge);
            foreach (var i in YoungestArtist2)
            {
                Console.WriteLine(i.ArtistName + i.Age);
            }

            //Display all artists with 'William' somewhere in their real name
            var ArtistNamedWilliam = Artists.Where(artist => artist.RealName.Contains("William"));
            foreach (var i in ArtistNamedWilliam)
            {
                 Console.WriteLine(i.ArtistName);
            }

            //Display the 3 oldest artist from Atlanta
            var ArtistFromAtlanta = Artists.Where(artist => artist.Hometown == "Atlanta");
            var OldestThree = ArtistFromAtlanta.OrderByDescending(artist => artist.Age).Take(3);
            foreach(var i in OldestThree)
            {
                Console.WriteLine(i.ArtistName);
            }

            //(Optional) Display the Group Name of all groups that have members that are not from New York City
            var ArtistWithGroup = Artists.Join(Groups, artist => artist.GroupId, group => group.Id, 
                                                (artist, group) => {
                                                    return new { Group = group.GroupName, Hometown = artist.Hometown};
                                                })
                                                .Where(artist => artist.Hometown != "New York City");
            Dictionary<string, string> ArtistNotFromNY = new Dictionary<string, string>();
            foreach (var i in ArtistWithGroup)
            {
                if( ArtistNotFromNY.ContainsKey(i.Group))
                {
                    ArtistNotFromNY.Add(i.Group, i.Group);
                    Console.WriteLine(i.Group);
                }
            }
            

            //(Optional) Display the artist names of all members of the group 'Wu-Tang Clan'
            var WuTangClan = Artists.Join(Groups, artist => artist.GroupId, group => group.Id,
                                            (artist, group) => {
                                                return new { Group = group.GroupName, Artist = artist.ArtistName};
                                            })
                                            .Where(artist => artist.Group == "Wu-Tang Clan");
                                        
            foreach(var i in WuTangClan)
            {
                Console.WriteLine(i.Artist);
            }
        }
    }
}
