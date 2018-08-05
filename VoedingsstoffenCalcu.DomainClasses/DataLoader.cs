using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoedingsstoffenCalcu.DomainClasses
{
    public class DataLoader
    {
        public static List<Product> Loader(string file)
        {
            using (var reader = new StreamReader(file))
            {
                List<Product> productList = new List<Product>();

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (line != null)
                    {
                        var values = line.Split(';');
                        var product = new Product()
                        {
                            Naam = Convert.ToString(values[0]),
                            Kilocalorieën = Convert.ToDecimal(values[1]),
                            Kilojoules = Convert.ToDecimal(values[2]),
                            Water = Convert.ToDecimal(values[3]),
                            Eiwit = Convert.ToDecimal(values[4]),
                            Koolhydraten = Convert.ToDecimal(values[5]),
                            Suikers = Convert.ToDecimal(values[6]),
                            Vet = Convert.ToDecimal(values[7]),
                            VerzadigdVet = Convert.ToDecimal(values[8]),
                            EnkelvoudigOnverzadigdVet = Convert.ToDecimal(values[9]),
                            MeervoudigOnverzadigdeVetzuren = Convert.ToDecimal(values[10]),
                            Cholesterol = Convert.ToDecimal(values[11]),
                            Vezels = Convert.ToDecimal(values[12]),
                        };
                        productList.Add(product);
                    }
                }
                return productList;
            }
        }
    }
}
