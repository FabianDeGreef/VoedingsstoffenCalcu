using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoedingsstoffenCalcu.DomainClasses
{
    public class Calculator
    {
        public static SavedProduct Calculate(Product product, decimal wight)
        {
            var productEdit = product;
            SavedProduct savedProduct = new SavedProduct()
            {
                OrginalProductId = product.ProductId,
                Naam =  product.Naam,
                Kilocalorieën = (productEdit.Kilocalorieën / 100) * wight,
                Kilojoules = (productEdit.Kilojoules / 100) * wight,
                Water = (productEdit.Water / 100) * wight,
                Eiwit = (productEdit.Eiwit / 100) * wight,
                Koolhydraten = (productEdit.Koolhydraten / 100) * wight,
                Suikers = (productEdit.Suikers / 100) * wight,
                Vet = (productEdit.Vet / 100) * wight,
                VerzadigdVet = (productEdit.VerzadigdVet / 100) * wight,
                EnkelvoudigOnverzadigdVet = (productEdit.EnkelvoudigOnverzadigdVet / 100) * wight,
                MeervoudigOnverzadigdeVetzuren = (productEdit.MeervoudigOnverzadigdeVetzuren / 100) * wight,
                Cholesterol = (productEdit.Cholesterol / 100) * wight,
                Vezels = (productEdit.Vezels / 100) * wight,
                Gewicht = wight
            };
            return savedProduct;
        }

        public static Result CalculateDayTotal(List<SavedProduct> products)
        {
            Result result = new Result();
            foreach (var product in products)
            {
                result.Kilocalorieën = result.Kilocalorieën + product.Kilocalorieën;
                result.Kilojoules = result.Kilojoules + product.Kilojoules;
                result.Water = result.Water + product.Water;
                result.Eiwit = result.Eiwit + product.Eiwit;
                result.Koolhydraten = result.Koolhydraten + product.Koolhydraten;
                result.Suikers = result.Suikers + product.Suikers;
                result.Vet = result.Vet + product.Vet;
                result.VerzadigdVet = result.VerzadigdVet + product.VerzadigdVet;
                result.EnkelvoudigOnverzadigdVet = result.EnkelvoudigOnverzadigdVet + product.EnkelvoudigOnverzadigdVet;
                result.MeervoudigOnverzadigdeVetzuren = result.MeervoudigOnverzadigdeVetzuren + product.MeervoudigOnverzadigdeVetzuren;
                result.Cholesterol = result.Cholesterol + product.Cholesterol;
                result.Vezels = result.Vezels + product.Vezels;
            }
            return result;
        }
    }
}
