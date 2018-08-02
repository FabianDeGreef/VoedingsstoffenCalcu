using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoedingsstoffenCalcu.DomainClasses
{
    public class Calculator
    {
        public static SavedProduct Calculate(Product product, decimal value)
        {
            var productEdit = product;
            SavedProduct savedProduct = new SavedProduct()
            {
                OrginalProductId = product.ProductId,
                Naam =  product.Naam,
                Kilocalorieën = (productEdit.Kilocalorieën / 100) * value,
                Kilojoules = (productEdit.Kilojoules / 100) * value,
                Water = (productEdit.Water / 100) * value,
                Eiwit = (productEdit.Eiwit / 100) * value,
                Koolhydraten = (productEdit.Koolhydraten / 100) * value,
                Suikers = (productEdit.Suikers / 100) * value,
                Vet = (productEdit.Vet / 100) * value,
                VerzadigdVet = (productEdit.VerzadigdVet / 100) * value,
                EnkelvoudigOnverzadigdVet = (productEdit.EnkelvoudigOnverzadigdVet / 100) * value,
                MeervoudigOnverzadigdeVetzuren = (productEdit.MeervoudigOnverzadigdeVetzuren / 100) * value,
                Cholesterol = (productEdit.Cholesterol / 100) * value,
                Vezels = (productEdit.Vezels / 100) * value
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
